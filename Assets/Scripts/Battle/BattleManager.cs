using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static GameMain;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Cursor cursor;
    [SerializeField] VSImageUI vsImageUI;
    //[SerializeField] InflueneceManager influeneceManager;
    [SerializeField] TerritoryManager territoryManager;
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;
    [SerializeField] Transform AttackerSoliderField, DefenderSoliderField;
    //[SerializeField] SoliderController attackSoliderPrefab;
    //[SerializeField] SoliderController defenceSoliderPrefab;
    [SerializeField] GameObject attackSoliderPrefab;
    [SerializeField] GameObject defenceSoliderPrefab;
    [SerializeField] BattleDetailUI battleDetailUI;
    [SerializeField] BattleUI battleUI;
    [SerializeField] GameObject mapField;

    public CharacterController attackerCharacter;
    public CharacterController defenderCharacter;

    public Influence influence;

    public bool isBattleEnd;

    public bool attackerRetreatFlag = false;
    public bool defenderRetreatFlag = false;

    private bool inputEnabled = true;

    public float battleBeforeWaitTime = 1.5f;
    public float battleAfterWaitTime = 1.5f;

    public enum RetreatFlag
    {
        allDeath,//‘S–Å(•ºm‘Sˆõ€–S)
        halfDeath,//”¼‰ó(•ºm”¼•ª€–S)
        oneDeath,//•ºm1‘Ì€–S
        noDeathHp10Up,//1‘Ì‚à€–S‚³‚¹‚È‚¢i‘S‚Ä‚Ì•ºm‚ÌHP‚ª10ˆÈãj
        noDeathHp20Up//1‘Ì‚à€–S‚³‚¹‚È‚¢i‘S‚Ä‚Ì•ºm‚ÌHP‚ª20ˆÈãj
    }
    public RetreatFlag retreatFlag;

    private void Start()
    {
        //StartBattle();
    }

    public void StartBattle(CharacterController attackerCharacter, CharacterController defenderCharacter)
    {
        attackerRetreatFlag = false;
        defenderRetreatFlag = false;
        this.attackerCharacter = attackerCharacter;
        this.defenderCharacter = defenderCharacter;
        CreateSoliderList(attackerCharacter.soliderList, AttackerSoliderField, true);
        CreateSoliderList(defenderCharacter.soliderList, DefenderSoliderField, false);
        ShowSoliderList(attackerCharacter.soliderList, AttackerSoliderField, true);
        ShowSoliderList(defenderCharacter.soliderList, DefenderSoliderField, false);
    }

    void CreateSoliderList(List<SoldierController> soliderList, Transform field, bool Attack)
    {
        foreach (SoldierController solider in soliderList)
        {
            CreateAttackrSolider(solider, field, Attack);
        }
    }

    void CreateAttackrSolider(SoldierController solider, Transform field, bool Attack)
    {
        if (Attack)
        {
            var soldierObject = Instantiate(attackSoliderPrefab, field);
            soldierObject.GetComponent<SoldierImageView>().
                ShowBattleSoldier(
                solider.icon,
                solider.hp,
                solider.maxHP);
            //SoliderController battleSolider = Instantiate(attackSoliderPrefab, field, false);
            //battleSolider.ShowBattleSoliderUI(solider, Attack);
        }
        else
        {
            var soldierObject = Instantiate(defenceSoliderPrefab, field);
            soldierObject.GetComponent<SoldierImageView>().
                ShowBattleSoldier(
                solider.icon,
                solider.hp,
                solider.maxHP);
            //SoliderController battleSolider = Instantiate(defenceSoliderPrefab, field, false);
            //battleSolider.ShowBattleSoliderUI(solider, Attack);
        }
    }

    void ShowSoliderList(List<SoldierController> soliderList, Transform field, bool Attack)
    {
        // Œ»İ•\¦‚³‚ê‚Ä‚¢‚é•ºm‚ğíœ
        //foreach (Transform child in field)
        //{
        //    Destroy(child.gameObject);
        //}
        HideSoliderList(soliderList, field);

        // V‚µ‚¢•ºmƒŠƒXƒg‚ğì¬
        foreach (SoldierController solider in soliderList)
        {
            CreateAttackrSolider(solider, field, Attack);
        }
    }

    void HideSoliderList(List<SoldierController> soliderList, Transform field)
    {
        // Œ»İ•\¦‚³‚ê‚Ä‚¢‚é•ºm‚ğíœ
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }
    }

    public void BattleButton()
    {
        if (inputEnabled)
        {
            SoundManager.instance.PlayTrainingSE();
            SoliderBattle(attackerCharacter, defenderCharacter);//UŒ‚w‚ªç”õw‚ğUŒ‚
            SoliderBattle(defenderCharacter, attackerCharacter);//ç”õw‚ªUŒ‚w‚ğUŒ‚
            IsAliveCheckSolider(attackerCharacter, defenderCharacter);

            //ƒoƒgƒ‹UI‚ğXV
            ShowSoliderList(attackerCharacter.soliderList, AttackerSoliderField, true);
            ShowSoliderList(defenderCharacter.soliderList, DefenderSoliderField, false);

            RetreatCheck(attackerCharacter, defenderCharacter);

            //í“¬I—¹ƒ`ƒFƒbƒN
            if (attackerRetreatFlag == true || defenderRetreatFlag == true)
            {
                if (attackerRetreatFlag == true)
                {
                    HideSoliderList(attackerCharacter.soliderList, AttackerSoliderField);
                }
                else if (defenderRetreatFlag == true)
                {
                    HideSoliderList(defenderCharacter.soliderList, DefenderSoliderField);
                }
                BattleEndCheck(attackerCharacter, defenderCharacter);
                StartCoroutine(PlayerBattleEnd());
            }
        }
    }

    public void RetreatButton()
    {
        if (inputEnabled)
        {
            if (attackerCharacter == GameMain.instance.playerCharacter)
            {
                HideSoliderList(attackerCharacter.soliderList, AttackerSoliderField);
                attackerRetreatFlag = true;
            }
            else if (defenderCharacter == GameMain.instance.playerCharacter)
            {
                HideSoliderList(defenderCharacter.soliderList, DefenderSoliderField);
                defenderRetreatFlag = true;
            }
            BattleEndCheck(attackerCharacter, defenderCharacter);
            StartCoroutine(PlayerBattleEnd());
        }
    }

    IEnumerator PlayerBattleEnd()
    {
        // “ü—Í‚ğ–³Œø‰»
        inputEnabled = false;
        yield return new WaitForSeconds(2.0f);
        battleUI.HideBattleUI();
        // “ü—Í‚ğ—LŒø‰»
        inputEnabled = true;

        StartCoroutine(ShowEndBattle());
        yield return new WaitForSeconds(battleAfterWaitTime);

        CheckExtinct(defenderCharacter.influence);

        //ƒvƒŒƒCƒ„[‚ªUŒ‚‘¤‚Ìê‡
        if (attackerCharacter == GameMain.instance.playerCharacter)
        {
            if (GameMain.instance.battleTurnCharacter == GameMain.instance.playerCharacter)
            {
                GameMain.instance.PlayerBattlePhase();
            }
            else
            {
                CheckAttackableCharacterInInfluence();
            }
        }
        //ƒvƒŒƒCƒ„[‚ªç”õ‘¤‚Ìê‡
        else
        {
            CheckAttackableCharacterInInfluence();
        }
    }

    public void SoliderBattle(CharacterController attackChara, CharacterController defenceChara)
    {
        if (attackChara.soliderList.Count != 0 && defenceChara.soliderList.Count != 0)
        {
            foreach (SoldierController attackerSolider in attackChara.soliderList)
            {
                SoldierController defenderSolider = GetRandomSolider(defenceChara.soliderList);
                attackerSolider.Attack(attackChara, defenceChara, defenderSolider, territoryManager.territory);
                //defenderSolider.CounterAttack(attackChara, defenceChara, attackerSolider, influeneceManager.territory);
            }
        }
        else
        {
            return;
        }
    }

    public void IsAliveCheckSolider(CharacterController attackChara, CharacterController defenceChara)
    {
        //HP‚ª0‚É‚È‚Á‚½•ºm‚ğæ“¾
        List<SoldierController> deadAttackers = new List<SoldierController>();
        foreach (SoldierController solider in attackChara.soliderList)
        {
            if (solider.isAlive == false)
            {
                deadAttackers.Add(solider);
            }
        }
        List<SoldierController> deadDefenders = new List<SoldierController>();
        foreach (SoldierController solider in defenceChara.soliderList)
        {
            if (solider.isAlive == false)
            {
                deadDefenders.Add(solider);
            }
        }
        //HP‚ª0‚É‚È‚Á‚½•ºm‚ğƒŠƒXƒg‚©‚çíœ‚©‚ÂDestroy
        foreach (SoldierController solider in deadAttackers)
        {
            attackChara.soliderList.Remove(solider);
            Destroy(solider);
        }
        foreach (SoldierController solider in deadDefenders)
        {
            defenceChara.soliderList.Remove(solider);
            Destroy(solider);
        }
    }

    public void RetreatCheck(CharacterController attackCharacter, CharacterController defenceCharacter)
    {
        if (attackCharacter == GameMain.instance.playerCharacter)
        {
            if (attackCharacter.soliderList.Count == 0)
            {
                attackerRetreatFlag = true;
            }
        }

        if (defenceCharacter == GameMain.instance.playerCharacter)
        {
            if (defenceCharacter.soliderList.Count == 0)
            {
                defenderRetreatFlag = true;
            }
        }

        int attackerSoliderHpSum = 0;
        foreach (SoldierController solider in attackCharacter.soliderList)
        {
            attackerSoliderHpSum += solider.hp;
        }

        int defenderSoliderHpSum = 0;
        foreach (SoldierController solider in defenceCharacter.soliderList)
        {
            defenderSoliderHpSum += solider.hp;
        }

        //–h‰q‘¤‚ª•‰‚¯‚Ä‚¢‚éê‡
        if (attackerSoliderHpSum > defenderSoliderHpSum)
        {
            if (defenceCharacter != GameMain.instance.playerCharacter)
            {
                switch (territoryManager.territory.defenceTerritoryType)
                {
                    //»”™‚Ìê‡A‚¢‚¸‚ê‚©•ºm‚ÌHP‚ª20–¢–‚É‚È‚Á‚½‚ç‘Ş‹p‚³‚¹‚é
                    case Territory.DefenceTerritoryType.desert:
                        foreach (SoldierController solider in defenceCharacter.soliderList)
                        {
                            if (solider.hp < 20)
                            {
                                defenderRetreatFlag = true;
                                Debug.Log("–h‰q‘¤‚ª•‰‚¯‚Ü‚µ‚½");
                            }
                        }
                        break;
                    //r–ì‚Ìê‡A‚¢‚¸‚ê‚©•ºm‚ÌHP‚ª10–¢–‚É‚È‚Á‚½‚ç‘Ş‹p‚³‚¹‚é
                    case Territory.DefenceTerritoryType.wilderness:
                        foreach (SoldierController solider in defenceCharacter.soliderList)
                        {
                            if (solider.hp < 10)
                            {
                                defenderRetreatFlag = true;
                                Debug.Log("–h‰q‘¤‚ª•‰‚¯‚Ü‚µ‚½");
                            }
                        }
                        break;
                    //•½Œ´‚Ìê‡A•ºm‚ª1‘Ì€–S‚µ‚½‚ç‘Ş‹p‚³‚¹‚é
                    case Territory.DefenceTerritoryType.plain:
                        if (defenceCharacter.soliderList.Count < 10)
                        {
                            defenderRetreatFlag = true;
                            Debug.Log("–h‰q‘¤‚ª•‰‚¯‚Ü‚µ‚½");
                        }
                        break;
                    //X—Ñ‚Ìê‡A”¼‰ó(•ºm”¼•ª€–S)‚µ‚½‚ç‘Ş‹p‚³‚¹‚é
                    case Territory.DefenceTerritoryType.forest:
                        if (defenceCharacter.soliderList.Count < 5)
                        {
                            defenderRetreatFlag = true;
                            Debug.Log("–h‰q‘¤‚ª•‰‚¯‚Ü‚µ‚½");
                        }
                        break;
                    //Ô‚Ìê‡A‘S–Å(•ºm‘Sˆõ€–S)‚µ‚½‚ç‘Ş‹p‚³‚¹‚é
                    case Territory.DefenceTerritoryType.fort:
                        if (defenceCharacter.soliderList.Count == 0)
                        {
                            defenderRetreatFlag = true;
                            Debug.Log("–h‰q‘¤‚ª•‰‚¯‚Ü‚µ‚½");
                        }
                        break;
                }
            }
        }
        //NU‘¤‚ª•‰‚¯‚Ä‚¢‚éê‡
        else if (attackerSoliderHpSum < defenderSoliderHpSum)
        {
            if (attackCharacter != GameMain.instance.playerCharacter)
            {
                switch (territoryManager.territory.attackTerritoryType)
                {
                    //»”™‚Ìê‡A‚¢‚¸‚ê‚©•ºm‚ÌHP‚ª20–¢–‚É‚È‚Á‚½‚ç‘Ş‹p‚³‚¹‚é
                    case Territory.AttackTerritoryType.desert:
                        foreach (SoldierController solider in attackCharacter.soliderList)
                        {
                            if (solider.hp < 20)
                            {
                                attackerRetreatFlag = true;
                                Debug.Log("NU‘¤‚ª•‰‚¯‚Ü‚µ‚½");
                            }
                        }
                        break;
                    //r–ì‚Ìê‡A‚¢‚¸‚ê‚©•ºm‚ÌHP‚ª10–¢–‚É‚È‚Á‚½‚ç‘Ş‹p‚³‚¹‚é
                    case Territory.AttackTerritoryType.wilderness:
                        foreach (SoldierController solider in attackCharacter.soliderList)
                        {
                            if (solider.hp < 10)
                            {
                                attackerRetreatFlag = true;
                                Debug.Log("NU‘¤‚ª•‰‚¯‚Ü‚µ‚½");
                            }
                        }
                        break;
                    //•½Œ´‚Ìê‡A•ºm‚ª1‘Ì€–S‚µ‚½‚ç‘Ş‹p‚³‚¹‚é
                    case Territory.AttackTerritoryType.plain:
                        if (attackCharacter.soliderList.Count < 10)
                        {
                            attackerRetreatFlag = true;
                            Debug.Log("NU‘¤‚ª•‰‚¯‚Ü‚µ‚½");
                        }
                        break;
                    //X—Ñ‚Ìê‡A”¼‰ó(•ºm”¼•ª€–S)‚µ‚½‚ç‘Ş‹p‚³‚¹‚é
                    case Territory.AttackTerritoryType.forest:
                        if (attackCharacter.soliderList.Count < 5)
                        {
                            attackerRetreatFlag = true;
                            Debug.Log("NU‘¤‚ª•‰‚¯‚Ü‚µ‚½");
                        }
                        break;
                    //Ô‚Ìê‡A‘S–Å(•ºm‘Sˆõ€–S)‚µ‚½‚ç‘Ş‹p‚³‚¹‚é
                    case Territory.AttackTerritoryType.fort:
                        if (attackCharacter.soliderList.Count == 0)
                        {
                            attackerRetreatFlag = true;
                            Debug.Log("NU‘¤‚ª•‰‚¯‚Ü‚µ‚½");
                        }
                        break;
                }
            }
        }
        else
        {
            if (attackerSoliderHpSum == 0)
            {
                attackerRetreatFlag = true;
                Debug.Log("ˆø‚«•ª‚¯‚Å‚·iNU‘¤‚Ì•‰‚¯‚Å‚·j");
            }
        }
    }

    public void BattleEndCheck(CharacterController attackerCharacter, CharacterController defenderCharacter)
    {
        if (attackerRetreatFlag == true)
        {
            if (attackerCharacter == GameMain.instance.playerCharacter)
            {
                TitleFieldUI.instance.titleFieldText.text = "      ƒvƒŒƒCƒ„[‚Í‘Ş‹p‚µ‚Ü‚µ‚½";
            }
            else
            {
                TitleFieldUI.instance.titleFieldText.text = "      “GŒR‚Í‘Ş‹p‚µ‚Ü‚µ‚½";
            }
            attackerCharacter.isAttackable = false;
            defenderCharacter.fame += 2;
            isBattleEnd = true;
        }
        else if (defenderRetreatFlag == true)
        {
            if (defenderCharacter == GameMain.instance.playerCharacter)
            {
                TitleFieldUI.instance.titleFieldText.text = "      ƒvƒŒƒCƒ„[‚Í‘Ş‹p‚µ‚Ü‚µ‚½";
            }
            else
            {
                TitleFieldUI.instance.titleFieldText.text = "      “GŒR‚Í‘Ş‹p‚µ‚Ü‚µ‚½";
            }
            Debug.Log(attackerCharacter.name + "‚ÌŸ—˜‚Å‚·B");
            attackerCharacter.isAttackable = false;
            attackerCharacter.fame += 2;
            territoryUIOnMouse.ChangeTerritoryByBattle(attackerCharacter.influence);
            isBattleEnd = true;
        }

        attackerCharacter.isBattle = true;
        defenderCharacter.isBattle = true;
    }

    public IEnumerator ShowEndBattle()
    {
        TitleFieldUI.instance.titleFieldSubText.text = "í“¬ƒtƒF[ƒY";
        mapField.SetActive(true);
        vsImageUI.gameObject.SetActive(false);
        cursor.gameObject.SetActive(true);

        // ƒJ[ƒ\ƒ‹‚ÌˆÊ’u‚ğİ’è
        RectTransform territoryRectTransform = territoryManager.territory.GetComponent<RectTransform>();
        cursor.SetPosition(territoryRectTransform);

        //cursor.transform.position = territoryManager.territory.position;
        battleDetailUI.ShowBattleDetailUI(attackerCharacter, defenderCharacter);
        if (attackerRetreatFlag == true)
        {
            TitleFieldUI.instance.titleFieldText.text = "      –h‰q‘¤‚ÌŸ—˜‚Å‚·";
        }
        else
        {
            TitleFieldUI.instance.titleFieldText.text = "      NU‘¤‚ÌŸ—˜‚Å‚·";
            StartCoroutine(GameMain.instance.BlinkTerritory(0.5f, attackerCharacter, defenderCharacter, territoryManager.territory));
        }
        yield return new WaitForSeconds(battleAfterWaitTime);

        battleDetailUI.HideBattleDetailUI();
        mapField.SetActive(false);
        cursor.gameObject.SetActive(false);

        GameMain.instance.step = Step.Attack;
    }

    public void CheckExtinct(Influence influence)
    {
        if (influence.territoryList.Count == 0)
        {
            // ˆê“I‚ÈƒŠƒXƒg‚ğì¬
            List<CharacterController> charactersToRemove = new List<CharacterController>();
            // foreach ƒ‹[ƒv‚Åˆê“I‚ÈƒŠƒXƒg‚É—v‘f‚ğ’Ç‰Á
            foreach (CharacterController chara in influence.characterList)
            {
                charactersToRemove.Add(chara);
            }

            // ˆê“I‚ÈƒŠƒXƒg‚Ì—v‘f‚ğŒ³‚ÌƒŠƒXƒg‚©‚çíœ
            foreach (CharacterController chara in charactersToRemove)
            {
                // ƒLƒƒƒ‰ƒNƒ^[‚ğ¨—Í‚©‚çœŠO‚·‚é
                chara.influence.RemoveCharacter(chara);
                // –³Š‘®‚ÉŠ‘®‚³‚¹‚é
                GameMain.instance.noneInfluence.AddCharacter(chara);
                if (chara.isLord == true)
                {
                    chara.isLord = false;
                }
            }
        }
    }

    public void CheckAttackableCharacterInInfluence()
    {
        Debug.Log("CheckAttackableCharacterInInfluence");
        //UŒ‚‰Â”\‚ÈƒLƒƒƒ‰”‚ğæ“¾
        if (GameMain.instance.battleTurnCharacter.influence != GameMain.instance.noneInfluence)
        {
            int attackableCharacterCount = GameMain.instance.battleTurnCharacter.influence.characterList.Count(c => c.isAttackable);
            //¨—Í‚ÉŠ‘®‚·‚éƒLƒƒƒ‰”‚É‰‚¶‚Äˆ—‚ğ•ª‚¯‚é
            switch (GameMain.instance.battleTurnCharacter.influence.characterList.Count)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    if (attackableCharacterCount > 2)
                    {
                        Debug.Log("Ä“x" + GameMain.instance.battleTurnCharacter.name + "‚Ìƒ^[ƒ“‚Å‚·");
                        GameMain.instance.OtherBattlePhase(GameMain.instance.battleTurnCharacter);
                    }
                    else// if (defenderCharacter.influence == GameMain.instance.playerCharacter.influence)
                    {
                        Debug.Log("Ÿ‚ÌƒLƒƒƒ‰ƒNƒ^[‚Ìƒ^[ƒ“‚Å‚·B");
                        GameMain.instance.NextCharacterBattlePhase(GameMain.instance.battleTurnCharacter);
                    }
                    break;
                case 5:
                case 6:
                    if (attackableCharacterCount > 3)
                    {
                        Debug.Log("Ä“x" + GameMain.instance.battleTurnCharacter.name + "‚Ìƒ^[ƒ“‚Å‚·");
                        GameMain.instance.OtherBattlePhase(GameMain.instance.battleTurnCharacter);
                    }
                    else// if (defenderCharacter.influence == GameMain.instance.playerCharacter.influence)
                    {
                        Debug.Log("Ÿ‚ÌƒLƒƒƒ‰ƒNƒ^[‚Ìƒ^[ƒ“‚Å‚·B");
                        GameMain.instance.NextCharacterBattlePhase(GameMain.instance.battleTurnCharacter);
                    }
                    break;
            }
        }
    }

    private SoldierController GetRandomSolider(List<SoldierController> soliderList)
    {
        if (soliderList.Count == 0)
        {
            return null;
        }

        // ƒŠƒXƒg‚©‚çƒ‰ƒ“ƒ_ƒ€‚ÉƒCƒ“ƒfƒbƒNƒX‚ğ‘I‘ğ
        int randomIndex = Random.Range(0, soliderList.Count);

        // ‘I‘ğ‚³‚ê‚½ƒ‰ƒ“ƒ_ƒ€‚È•ºm‚ğ•Ô‚·
        return soliderList[randomIndex];
    }

    public void AttackBattle(CharacterController attackCharacter, CharacterController defenceCharacter)
    {
        StartCoroutine(WaitForAttackBattle(attackCharacter, defenceCharacter));
    }

    //©¨—Í‚Ì–¡•û‚ªUŒ‚‚·‚éˆ—
    public IEnumerator WaitForAttackBattle(CharacterController attackCharacter, CharacterController defenceCharacter)
    {
        attackerRetreatFlag = false;
        defenderRetreatFlag = false;

        attackerCharacter = attackCharacter;
        defenderCharacter = defenceCharacter;

        //‰Šúí“¬‰æ–Ê•\¦
        TitleFieldUI.instance.titleFieldText.text = "      –¡•û VS “G@í“¬I";
        mapField.SetActive(true);
        cursor.gameObject.SetActive(true);

        // ƒJ[ƒ\ƒ‹‚ÌˆÊ’u‚ğİ’è
        RectTransform territoryRectTransform = territoryManager.territory.GetComponent<RectTransform>();
        cursor.SetPosition(territoryRectTransform);
        //cursor.transform.position = territoryManager.territory.position;

        vsImageUI.SetPosition(territoryRectTransform);
        //vsImageUI.transform.position = territoryManager.territory.position;

        battleDetailUI.ShowBattleDetailUI(attackerCharacter, defenderCharacter);
        StartCoroutine(GameMain.instance.BlinkCursor(1.0f));
        yield return new WaitForSeconds(1.0f);

        //í“¬À{
        SoundManager.instance.PlayBattleSE();
        while (attackerRetreatFlag == false && defenderRetreatFlag == false)
        {
            SoliderBattle(attackerCharacter, defenderCharacter);
            SoliderBattle(defenderCharacter, attackerCharacter);
            IsAliveCheckSolider(attackerCharacter, defenderCharacter);
            RetreatCheck(attackerCharacter, defenderCharacter);
            BattleEndCheck(attackerCharacter, defenderCharacter);
            battleDetailUI.ShowBattleDetailUI(attackerCharacter, defenderCharacter);
            vsImageUI.gameObject.SetActive(!vsImageUI.gameObject.activeSelf); // VSƒCƒ[ƒW‚Ì•\¦E”ñ•\¦‚ğØ‚è‘Ö‚¦‚é
            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine(ShowEndBattle()); 
        yield return new WaitForSeconds(battleAfterWaitTime);

        CheckExtinct(defenderCharacter.influence);

        GameMain.instance.PlayerBattlePhase();
    }

    public void DefenceBattle(CharacterController attackCharacter, CharacterController defenceCharacter)
    {
        StartCoroutine(WaitForDefenceBattle(attackCharacter, defenceCharacter));
    }

    //©¨—Í‚Ì–¡•û‚ª–h‰q‚·‚éˆ—
    public IEnumerator WaitForDefenceBattle(CharacterController attackCharacter, CharacterController defenceCharacter)
    {
        attackerRetreatFlag = false;
        defenderRetreatFlag = false;

        attackerCharacter = attackCharacter;
        defenderCharacter = defenceCharacter;

        //í“¬‘O‚Ì‰æ–Ê•\¦
        TitleFieldUI.instance.titleFieldText.text = "      “G VS –¡•û@í“¬I";
        mapField.SetActive(true);
        cursor.gameObject.SetActive(true);

        // ƒJ[ƒ\ƒ‹‚ÌˆÊ’u‚ğİ’è
        RectTransform territoryRectTransform = territoryManager.territory.GetComponent<RectTransform>();
        cursor.SetPosition(territoryRectTransform);
        //cursor.transform.position = territoryManager.territory.position;

        vsImageUI.SetPosition(territoryRectTransform);
        //vsImageUI.transform.position = territoryManager.territory.position;

        battleDetailUI.ShowBattleDetailUI(attackCharacter, defenceCharacter);
        StartCoroutine(GameMain.instance.BlinkCursor(1.0f));
        yield return new WaitForSeconds(1.0f);

        //í“¬À{@í“¬’†‰æ–Ê•\¦
        SoundManager.instance.PlayBattleSE();
        while (attackerRetreatFlag == false && defenderRetreatFlag == false)
        {
            SoliderBattle(attackerCharacter, defenderCharacter);
            SoliderBattle(defenderCharacter, attackerCharacter);
            IsAliveCheckSolider(attackerCharacter, defenderCharacter);
            RetreatCheck(attackerCharacter, defenderCharacter);
            BattleEndCheck(attackerCharacter, defenderCharacter);
            battleDetailUI.ShowBattleDetailUI(attackerCharacter, defenderCharacter);
            vsImageUI.gameObject.SetActive(!vsImageUI.gameObject.activeSelf); // VSƒCƒ[ƒW‚Ì•\¦E”ñ•\¦‚ğØ‚è‘Ö‚¦‚é
            yield return new WaitForSeconds(0.05f);
        }

        StartCoroutine(ShowEndBattle());
        yield return new WaitForSeconds(battleAfterWaitTime);

        CheckExtinct(defenderCharacter.influence);

        CheckAttackableCharacterInInfluence();
    }

    public void AbandonBattle()
    {
        StartCoroutine(WaitForAbandonBattle());
    }

    public IEnumerator WaitForAbandonBattle()
    {
        mapField.SetActive(true);

        TitleFieldUI.instance.titleFieldText.text = "í“¬‚ğ•úŠü‚µ‚Ü‚µ‚½";
        StartCoroutine(GameMain.instance.BlinkTerritory(0.5f, attackerCharacter, GameMain.instance.playerCharacter, territoryManager.territory));
        yield return new WaitForSeconds(battleAfterWaitTime);

        attackerCharacter.isAttackable = false;
        attackerCharacter.isBattle = true;
        territoryUIOnMouse.ChangeTerritoryByBattle(attackerCharacter.influence);
        isBattleEnd = true;

        CheckExtinct(GameMain.instance.playerCharacter.influence);

        mapField.SetActive(false);

        //Ÿ‚Ìˆ—‚ÖˆÚs
        CheckAttackableCharacterInInfluence();
    }
}
