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
        allDeath,//全滅(兵士全員死亡)
        halfDeath,//半壊(兵士半分死亡)
        oneDeath,//兵士1体死亡
        noDeathHp10Up,//1体も死亡させない（全ての兵士のHPが10以上）
        noDeathHp20Up//1体も死亡させない（全ての兵士のHPが20以上）
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

    void CreateSoliderList(List<SoliderController> soliderList, Transform field, bool Attack)
    {
        foreach (SoliderController solider in soliderList)
        {
            CreateAttackrSolider(solider, field, Attack);
        }
    }

    void CreateAttackrSolider(SoliderController solider, Transform field, bool Attack)
    {
        if (Attack)
        {
            var soldierObject = Instantiate(attackSoliderPrefab, field);
            soldierObject.GetComponent<SoldierImageView>().
                ShowBattleSoldier(
                solider.soliderModel.icon,
                solider.soliderModel.hp,
                solider.soliderModel.maxHP);
            //SoliderController battleSolider = Instantiate(attackSoliderPrefab, field, false);
            //battleSolider.ShowBattleSoliderUI(solider, Attack);
        }
        else
        {
            var soldierObject = Instantiate(defenceSoliderPrefab, field);
            soldierObject.GetComponent<SoldierImageView>().
                ShowBattleSoldier(
                solider.soliderModel.icon,
                solider.soliderModel.hp,
                solider.soliderModel.maxHP);
            //SoliderController battleSolider = Instantiate(defenceSoliderPrefab, field, false);
            //battleSolider.ShowBattleSoliderUI(solider, Attack);
        }
    }

    void ShowSoliderList(List<SoliderController> soliderList, Transform field, bool Attack)
    {
        // 現在表示されている兵士を削除
        //foreach (Transform child in field)
        //{
        //    Destroy(child.gameObject);
        //}
        HideSoliderList(soliderList, field);

        // 新しい兵士リストを作成
        foreach (SoliderController solider in soliderList)
        {
            CreateAttackrSolider(solider, field, Attack);
        }
    }

    void HideSoliderList(List<SoliderController> soliderList, Transform field)
    {
        // 現在表示されている兵士を削除
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
            SoliderBattle(attackerCharacter, defenderCharacter);//攻撃陣が守備陣を攻撃
            SoliderBattle(defenderCharacter, attackerCharacter);//守備陣が攻撃陣を攻撃
            IsAliveCheckSolider(attackerCharacter, defenderCharacter);

            //バトルUIを更新
            ShowSoliderList(attackerCharacter.soliderList, AttackerSoliderField, true);
            ShowSoliderList(defenderCharacter.soliderList, DefenderSoliderField, false);

            RetreatCheck(attackerCharacter, defenderCharacter);

            //戦闘終了チェック
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
        // 入力を無効化
        inputEnabled = false;
        yield return new WaitForSeconds(2.0f);
        battleUI.HideBattleUI();
        // 入力を有効化
        inputEnabled = true;

        StartCoroutine(ShowEndBattle());
        yield return new WaitForSeconds(battleAfterWaitTime);

        CheckExtinct(defenderCharacter.influence);

        //プレイヤーが攻撃側の場合
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
        //プレイヤーが守備側の場合
        else
        {
            CheckAttackableCharacterInInfluence();
        }
    }

    public void SoliderBattle(CharacterController attackChara, CharacterController defenceChara)
    {
        if (attackChara.soliderList.Count != 0 && defenceChara.soliderList.Count != 0)
        {
            foreach (SoliderController attackerSolider in attackChara.soliderList)
            {
                SoliderController defenderSolider = GetRandomSolider(defenceChara.soliderList);
                attackerSolider.soliderModel.Attack(attackChara, defenceChara, defenderSolider, territoryManager.territory);
                //defenderSolider.soliderModel.CounterAttack(attackChara, defenceChara, attackerSolider, influeneceManager.territory);
            }
        }
        else
        {
            return;
        }
    }

    public void IsAliveCheckSolider(CharacterController attackChara, CharacterController defenceChara)
    {
        //HPが0になった兵士を取得
        List<SoliderController> deadAttackers = new List<SoliderController>();
        foreach (SoliderController solider in attackChara.soliderList)
        {
            if (solider.soliderModel.isAlive == false)
            {
                deadAttackers.Add(solider);
            }
        }
        List<SoliderController> deadDefenders = new List<SoliderController>();
        foreach (SoliderController solider in defenceChara.soliderList)
        {
            if (solider.soliderModel.isAlive == false)
            {
                deadDefenders.Add(solider);
            }
        }
        //HPが0になった兵士をリストから削除かつDestroy
        foreach (SoliderController solider in deadAttackers)
        {
            attackChara.soliderList.Remove(solider);
            Destroy(solider);
        }
        foreach (SoliderController solider in deadDefenders)
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
        foreach (SoliderController solider in attackCharacter.soliderList)
        {
            attackerSoliderHpSum += solider.soliderModel.hp;
        }

        int defenderSoliderHpSum = 0;
        foreach (SoliderController solider in defenceCharacter.soliderList)
        {
            defenderSoliderHpSum += solider.soliderModel.hp;
        }

        //防衛側が負けている場合
        if (attackerSoliderHpSum > defenderSoliderHpSum)
        {
            if (defenceCharacter != GameMain.instance.playerCharacter)
            {
                switch (territoryManager.territory.defenceTerritoryType)
                {
                    //砂漠の場合、いずれか兵士のHPが20未満になったら退却させる
                    case Territory.DefenceTerritoryType.desert:
                        foreach (SoliderController solider in defenceCharacter.soliderList)
                        {
                            if (solider.soliderModel.hp < 20)
                            {
                                defenderRetreatFlag = true;
                                Debug.Log("防衛側が負けました");
                            }
                        }
                        break;
                    //荒野の場合、いずれか兵士のHPが10未満になったら退却させる
                    case Territory.DefenceTerritoryType.wilderness:
                        foreach (SoliderController solider in defenceCharacter.soliderList)
                        {
                            if (solider.soliderModel.hp < 10)
                            {
                                defenderRetreatFlag = true;
                                Debug.Log("防衛側が負けました");
                            }
                        }
                        break;
                    //平原の場合、兵士が1体死亡したら退却させる
                    case Territory.DefenceTerritoryType.plain:
                        if (defenceCharacter.soliderList.Count < 10)
                        {
                            defenderRetreatFlag = true;
                            Debug.Log("防衛側が負けました");
                        }
                        break;
                    //森林の場合、半壊(兵士半分死亡)したら退却させる
                    case Territory.DefenceTerritoryType.forest:
                        if (defenceCharacter.soliderList.Count < 5)
                        {
                            defenderRetreatFlag = true;
                            Debug.Log("防衛側が負けました");
                        }
                        break;
                    //砦の場合、全滅(兵士全員死亡)したら退却させる
                    case Territory.DefenceTerritoryType.fort:
                        if (defenceCharacter.soliderList.Count == 0)
                        {
                            defenderRetreatFlag = true;
                            Debug.Log("防衛側が負けました");
                        }
                        break;
                }
            }
        }
        //侵攻側が負けている場合
        else if (attackerSoliderHpSum < defenderSoliderHpSum)
        {
            if (attackCharacter != GameMain.instance.playerCharacter)
            {
                switch (territoryManager.territory.attackTerritoryType)
                {
                    //砂漠の場合、いずれか兵士のHPが20未満になったら退却させる
                    case Territory.AttackTerritoryType.desert:
                        foreach (SoliderController solider in attackCharacter.soliderList)
                        {
                            if (solider.soliderModel.hp < 20)
                            {
                                attackerRetreatFlag = true;
                                Debug.Log("侵攻側が負けました");
                            }
                        }
                        break;
                    //荒野の場合、いずれか兵士のHPが10未満になったら退却させる
                    case Territory.AttackTerritoryType.wilderness:
                        foreach (SoliderController solider in attackCharacter.soliderList)
                        {
                            if (solider.soliderModel.hp < 10)
                            {
                                attackerRetreatFlag = true;
                                Debug.Log("侵攻側が負けました");
                            }
                        }
                        break;
                    //平原の場合、兵士が1体死亡したら退却させる
                    case Territory.AttackTerritoryType.plain:
                        if (attackCharacter.soliderList.Count < 10)
                        {
                            attackerRetreatFlag = true;
                            Debug.Log("侵攻側が負けました");
                        }
                        break;
                    //森林の場合、半壊(兵士半分死亡)したら退却させる
                    case Territory.AttackTerritoryType.forest:
                        if (attackCharacter.soliderList.Count < 5)
                        {
                            attackerRetreatFlag = true;
                            Debug.Log("侵攻側が負けました");
                        }
                        break;
                    //砦の場合、全滅(兵士全員死亡)したら退却させる
                    case Territory.AttackTerritoryType.fort:
                        if (attackCharacter.soliderList.Count == 0)
                        {
                            attackerRetreatFlag = true;
                            Debug.Log("侵攻側が負けました");
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
                Debug.Log("引き分けです（侵攻側の負けです）");
            }
        }
    }

    public void BattleEndCheck(CharacterController attackerCharacter, CharacterController defenderCharacter)
    {
        if (attackerRetreatFlag == true)
        {
            if (attackerCharacter == GameMain.instance.playerCharacter)
            {
                TitleFieldUI.instance.titleFieldText.text = "      プレイヤーは退却しました";
            }
            else
            {
                TitleFieldUI.instance.titleFieldText.text = "      敵軍は退却しました";
            }
            attackerCharacter.isAttackable = false;
            defenderCharacter.fame += 2;
            isBattleEnd = true;
        }
        else if (defenderRetreatFlag == true)
        {
            if (defenderCharacter == GameMain.instance.playerCharacter)
            {
                TitleFieldUI.instance.titleFieldText.text = "      プレイヤーは退却しました";
            }
            else
            {
                TitleFieldUI.instance.titleFieldText.text = "      敵軍は退却しました";
            }
            Debug.Log(attackerCharacter.name + "の勝利です。");
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
        TitleFieldUI.instance.titleFieldSubText.text = "戦闘フェーズ";
        mapField.SetActive(true);
        vsImageUI.gameObject.SetActive(false);
        cursor.gameObject.SetActive(true);

        // カーソルの位置を設定
        RectTransform territoryRectTransform = territoryManager.territory.GetComponent<RectTransform>();
        cursor.SetPosition(territoryRectTransform);

        //cursor.transform.position = territoryManager.territory.position;
        battleDetailUI.ShowBattleDetailUI(attackerCharacter, defenderCharacter);
        if (attackerRetreatFlag == true)
        {
            TitleFieldUI.instance.titleFieldText.text = "      防衛側の勝利です";
        }
        else
        {
            TitleFieldUI.instance.titleFieldText.text = "      侵攻側の勝利です";
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
            // 一時的なリストを作成
            List<CharacterController> charactersToRemove = new List<CharacterController>();
            // foreach ループで一時的なリストに要素を追加
            foreach (CharacterController chara in influence.characterList)
            {
                charactersToRemove.Add(chara);
            }

            // 一時的なリストの要素を元のリストから削除
            foreach (CharacterController chara in charactersToRemove)
            {
                // キャラクターを勢力から除外する
                chara.influence.RemoveCharacter(chara);
                // 無所属に所属させる
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
        //攻撃可能なキャラ数を取得
        if (GameMain.instance.battleTurnCharacter.influence != GameMain.instance.noneInfluence)
        {
            int attackableCharacterCount = GameMain.instance.battleTurnCharacter.influence.characterList.Count(c => c.isAttackable);
            //勢力に所属するキャラ数に応じて処理を分ける
            switch (GameMain.instance.battleTurnCharacter.influence.characterList.Count)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    if (attackableCharacterCount > 2)
                    {
                        Debug.Log("再度" + GameMain.instance.battleTurnCharacter.name + "のターンです");
                        GameMain.instance.OtherBattlePhase(GameMain.instance.battleTurnCharacter);
                    }
                    else// if (defenderCharacter.influence == GameMain.instance.playerCharacter.influence)
                    {
                        Debug.Log("次のキャラクターのターンです。");
                        GameMain.instance.NextCharacterBattlePhase(GameMain.instance.battleTurnCharacter);
                    }
                    break;
                case 5:
                case 6:
                    if (attackableCharacterCount > 3)
                    {
                        Debug.Log("再度" + GameMain.instance.battleTurnCharacter.name + "のターンです");
                        GameMain.instance.OtherBattlePhase(GameMain.instance.battleTurnCharacter);
                    }
                    else// if (defenderCharacter.influence == GameMain.instance.playerCharacter.influence)
                    {
                        Debug.Log("次のキャラクターのターンです。");
                        GameMain.instance.NextCharacterBattlePhase(GameMain.instance.battleTurnCharacter);
                    }
                    break;
            }
        }
    }

    private SoliderController GetRandomSolider(List<SoliderController> soliderList)
    {
        if (soliderList.Count == 0)
        {
            return null;
        }

        // リストからランダムにインデックスを選択
        int randomIndex = Random.Range(0, soliderList.Count);

        // 選択されたランダムな兵士を返す
        return soliderList[randomIndex];
    }

    public void AttackBattle(CharacterController attackCharacter, CharacterController defenceCharacter)
    {
        StartCoroutine(WaitForAttackBattle(attackCharacter, defenceCharacter));
    }

    //自勢力の味方が攻撃する処理
    public IEnumerator WaitForAttackBattle(CharacterController attackCharacter, CharacterController defenceCharacter)
    {
        attackerRetreatFlag = false;
        defenderRetreatFlag = false;

        attackerCharacter = attackCharacter;
        defenderCharacter = defenceCharacter;

        //初期戦闘画面表示
        TitleFieldUI.instance.titleFieldText.text = "      味方 VS 敵　戦闘！";
        mapField.SetActive(true);
        cursor.gameObject.SetActive(true);

        // カーソルの位置を設定
        RectTransform territoryRectTransform = territoryManager.territory.GetComponent<RectTransform>();
        cursor.SetPosition(territoryRectTransform);
        //cursor.transform.position = territoryManager.territory.position;

        vsImageUI.SetPosition(territoryRectTransform);
        //vsImageUI.transform.position = territoryManager.territory.position;

        battleDetailUI.ShowBattleDetailUI(attackerCharacter, defenderCharacter);
        StartCoroutine(GameMain.instance.BlinkCursor(1.0f));
        yield return new WaitForSeconds(1.0f);

        //戦闘実施
        SoundManager.instance.PlayBattleSE();
        while (attackerRetreatFlag == false && defenderRetreatFlag == false)
        {
            SoliderBattle(attackerCharacter, defenderCharacter);
            SoliderBattle(defenderCharacter, attackerCharacter);
            IsAliveCheckSolider(attackerCharacter, defenderCharacter);
            RetreatCheck(attackerCharacter, defenderCharacter);
            BattleEndCheck(attackerCharacter, defenderCharacter);
            battleDetailUI.ShowBattleDetailUI(attackerCharacter, defenderCharacter);
            vsImageUI.gameObject.SetActive(!vsImageUI.gameObject.activeSelf); // VSイメージの表示・非表示を切り替える
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

    //自勢力の味方が防衛する処理
    public IEnumerator WaitForDefenceBattle(CharacterController attackCharacter, CharacterController defenceCharacter)
    {
        attackerRetreatFlag = false;
        defenderRetreatFlag = false;

        attackerCharacter = attackCharacter;
        defenderCharacter = defenceCharacter;

        //戦闘前の画面表示
        TitleFieldUI.instance.titleFieldText.text = "      敵 VS 味方　戦闘！";
        mapField.SetActive(true);
        cursor.gameObject.SetActive(true);

        // カーソルの位置を設定
        RectTransform territoryRectTransform = territoryManager.territory.GetComponent<RectTransform>();
        cursor.SetPosition(territoryRectTransform);
        //cursor.transform.position = territoryManager.territory.position;

        vsImageUI.SetPosition(territoryRectTransform);
        //vsImageUI.transform.position = territoryManager.territory.position;

        battleDetailUI.ShowBattleDetailUI(attackCharacter, defenceCharacter);
        StartCoroutine(GameMain.instance.BlinkCursor(1.0f));
        yield return new WaitForSeconds(1.0f);

        //戦闘実施　戦闘中画面表示
        SoundManager.instance.PlayBattleSE();
        while (attackerRetreatFlag == false && defenderRetreatFlag == false)
        {
            SoliderBattle(attackerCharacter, defenderCharacter);
            SoliderBattle(defenderCharacter, attackerCharacter);
            IsAliveCheckSolider(attackerCharacter, defenderCharacter);
            RetreatCheck(attackerCharacter, defenderCharacter);
            BattleEndCheck(attackerCharacter, defenderCharacter);
            battleDetailUI.ShowBattleDetailUI(attackerCharacter, defenderCharacter);
            vsImageUI.gameObject.SetActive(!vsImageUI.gameObject.activeSelf); // VSイメージの表示・非表示を切り替える
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

        TitleFieldUI.instance.titleFieldText.text = "戦闘を放棄しました";
        StartCoroutine(GameMain.instance.BlinkTerritory(0.5f, attackerCharacter, GameMain.instance.playerCharacter, territoryManager.territory));
        yield return new WaitForSeconds(battleAfterWaitTime);

        attackerCharacter.isAttackable = false;
        attackerCharacter.isBattle = true;
        territoryUIOnMouse.ChangeTerritoryByBattle(attackerCharacter.influence);
        isBattleEnd = true;

        CheckExtinct(GameMain.instance.playerCharacter.influence);

        mapField.SetActive(false);

        //次の処理へ移行
        CheckAttackableCharacterInInfluence();
    }
}
