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
        allDeath,//�S��(���m�S�����S)
        halfDeath,//����(���m�������S)
        oneDeath,//���m1�̎��S
        noDeathHp10Up,//1�̂����S�����Ȃ��i�S�Ă̕��m��HP��10�ȏ�j
        noDeathHp20Up//1�̂����S�����Ȃ��i�S�Ă̕��m��HP��20�ȏ�j
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
        // ���ݕ\������Ă��镺�m���폜
        //foreach (Transform child in field)
        //{
        //    Destroy(child.gameObject);
        //}
        HideSoliderList(soliderList, field);

        // �V�������m���X�g���쐬
        foreach (SoldierController solider in soliderList)
        {
            CreateAttackrSolider(solider, field, Attack);
        }
    }

    void HideSoliderList(List<SoldierController> soliderList, Transform field)
    {
        // ���ݕ\������Ă��镺�m���폜
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
            SoliderBattle(attackerCharacter, defenderCharacter);//�U���w������w���U��
            SoliderBattle(defenderCharacter, attackerCharacter);//����w���U���w���U��
            IsAliveCheckSolider(attackerCharacter, defenderCharacter);

            //�o�g��UI���X�V
            ShowSoliderList(attackerCharacter.soliderList, AttackerSoliderField, true);
            ShowSoliderList(defenderCharacter.soliderList, DefenderSoliderField, false);

            RetreatCheck(attackerCharacter, defenderCharacter);

            //�퓬�I���`�F�b�N
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
        // ���͂𖳌���
        inputEnabled = false;
        yield return new WaitForSeconds(2.0f);
        battleUI.HideBattleUI();
        // ���͂�L����
        inputEnabled = true;

        StartCoroutine(ShowEndBattle());
        yield return new WaitForSeconds(battleAfterWaitTime);

        CheckExtinct(defenderCharacter.influence);

        //�v���C���[���U�����̏ꍇ
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
        //�v���C���[��������̏ꍇ
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
        //HP��0�ɂȂ������m���擾
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
        //HP��0�ɂȂ������m�����X�g����폜����Destroy
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

        //�h�q���������Ă���ꍇ
        if (attackerSoliderHpSum > defenderSoliderHpSum)
        {
            if (defenceCharacter != GameMain.instance.playerCharacter)
            {
                switch (territoryManager.territory.defenceTerritoryType)
                {
                    //�����̏ꍇ�A�����ꂩ���m��HP��20�����ɂȂ�����ދp������
                    case Territory.DefenceTerritoryType.desert:
                        foreach (SoldierController solider in defenceCharacter.soliderList)
                        {
                            if (solider.hp < 20)
                            {
                                defenderRetreatFlag = true;
                                Debug.Log("�h�q���������܂���");
                            }
                        }
                        break;
                    //�r��̏ꍇ�A�����ꂩ���m��HP��10�����ɂȂ�����ދp������
                    case Territory.DefenceTerritoryType.wilderness:
                        foreach (SoldierController solider in defenceCharacter.soliderList)
                        {
                            if (solider.hp < 10)
                            {
                                defenderRetreatFlag = true;
                                Debug.Log("�h�q���������܂���");
                            }
                        }
                        break;
                    //�����̏ꍇ�A���m��1�̎��S������ދp������
                    case Territory.DefenceTerritoryType.plain:
                        if (defenceCharacter.soliderList.Count < 10)
                        {
                            defenderRetreatFlag = true;
                            Debug.Log("�h�q���������܂���");
                        }
                        break;
                    //�X�т̏ꍇ�A����(���m�������S)������ދp������
                    case Territory.DefenceTerritoryType.forest:
                        if (defenceCharacter.soliderList.Count < 5)
                        {
                            defenderRetreatFlag = true;
                            Debug.Log("�h�q���������܂���");
                        }
                        break;
                    //�Ԃ̏ꍇ�A�S��(���m�S�����S)������ދp������
                    case Territory.DefenceTerritoryType.fort:
                        if (defenceCharacter.soliderList.Count == 0)
                        {
                            defenderRetreatFlag = true;
                            Debug.Log("�h�q���������܂���");
                        }
                        break;
                }
            }
        }
        //�N�U���������Ă���ꍇ
        else if (attackerSoliderHpSum < defenderSoliderHpSum)
        {
            if (attackCharacter != GameMain.instance.playerCharacter)
            {
                switch (territoryManager.territory.attackTerritoryType)
                {
                    //�����̏ꍇ�A�����ꂩ���m��HP��20�����ɂȂ�����ދp������
                    case Territory.AttackTerritoryType.desert:
                        foreach (SoldierController solider in attackCharacter.soliderList)
                        {
                            if (solider.hp < 20)
                            {
                                attackerRetreatFlag = true;
                                Debug.Log("�N�U���������܂���");
                            }
                        }
                        break;
                    //�r��̏ꍇ�A�����ꂩ���m��HP��10�����ɂȂ�����ދp������
                    case Territory.AttackTerritoryType.wilderness:
                        foreach (SoldierController solider in attackCharacter.soliderList)
                        {
                            if (solider.hp < 10)
                            {
                                attackerRetreatFlag = true;
                                Debug.Log("�N�U���������܂���");
                            }
                        }
                        break;
                    //�����̏ꍇ�A���m��1�̎��S������ދp������
                    case Territory.AttackTerritoryType.plain:
                        if (attackCharacter.soliderList.Count < 10)
                        {
                            attackerRetreatFlag = true;
                            Debug.Log("�N�U���������܂���");
                        }
                        break;
                    //�X�т̏ꍇ�A����(���m�������S)������ދp������
                    case Territory.AttackTerritoryType.forest:
                        if (attackCharacter.soliderList.Count < 5)
                        {
                            attackerRetreatFlag = true;
                            Debug.Log("�N�U���������܂���");
                        }
                        break;
                    //�Ԃ̏ꍇ�A�S��(���m�S�����S)������ދp������
                    case Territory.AttackTerritoryType.fort:
                        if (attackCharacter.soliderList.Count == 0)
                        {
                            attackerRetreatFlag = true;
                            Debug.Log("�N�U���������܂���");
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
                Debug.Log("���������ł��i�N�U���̕����ł��j");
            }
        }
    }

    public void BattleEndCheck(CharacterController attackerCharacter, CharacterController defenderCharacter)
    {
        if (attackerRetreatFlag == true)
        {
            if (attackerCharacter == GameMain.instance.playerCharacter)
            {
                TitleFieldUI.instance.titleFieldText.text = "      �v���C���[�͑ދp���܂���";
            }
            else
            {
                TitleFieldUI.instance.titleFieldText.text = "      �G�R�͑ދp���܂���";
            }
            attackerCharacter.isAttackable = false;
            defenderCharacter.fame += 2;
            isBattleEnd = true;
        }
        else if (defenderRetreatFlag == true)
        {
            if (defenderCharacter == GameMain.instance.playerCharacter)
            {
                TitleFieldUI.instance.titleFieldText.text = "      �v���C���[�͑ދp���܂���";
            }
            else
            {
                TitleFieldUI.instance.titleFieldText.text = "      �G�R�͑ދp���܂���";
            }
            Debug.Log(attackerCharacter.name + "�̏����ł��B");
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
        TitleFieldUI.instance.titleFieldSubText.text = "�퓬�t�F�[�Y";
        mapField.SetActive(true);
        vsImageUI.gameObject.SetActive(false);
        cursor.gameObject.SetActive(true);

        // �J�[�\���̈ʒu��ݒ�
        RectTransform territoryRectTransform = territoryManager.territory.GetComponent<RectTransform>();
        cursor.SetPosition(territoryRectTransform);

        //cursor.transform.position = territoryManager.territory.position;
        battleDetailUI.ShowBattleDetailUI(attackerCharacter, defenderCharacter);
        if (attackerRetreatFlag == true)
        {
            TitleFieldUI.instance.titleFieldText.text = "      �h�q���̏����ł�";
        }
        else
        {
            TitleFieldUI.instance.titleFieldText.text = "      �N�U���̏����ł�";
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
            // �ꎞ�I�ȃ��X�g���쐬
            List<CharacterController> charactersToRemove = new List<CharacterController>();
            // foreach ���[�v�ňꎞ�I�ȃ��X�g�ɗv�f��ǉ�
            foreach (CharacterController chara in influence.characterList)
            {
                charactersToRemove.Add(chara);
            }

            // �ꎞ�I�ȃ��X�g�̗v�f�����̃��X�g����폜
            foreach (CharacterController chara in charactersToRemove)
            {
                // �L�����N�^�[�𐨗͂��珜�O����
                chara.influence.RemoveCharacter(chara);
                // �������ɏ���������
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
        //�U���\�ȃL���������擾
        if (GameMain.instance.battleTurnCharacter.influence != GameMain.instance.noneInfluence)
        {
            int attackableCharacterCount = GameMain.instance.battleTurnCharacter.influence.characterList.Count(c => c.isAttackable);
            //���͂ɏ�������L�������ɉ����ď����𕪂���
            switch (GameMain.instance.battleTurnCharacter.influence.characterList.Count)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    if (attackableCharacterCount > 2)
                    {
                        Debug.Log("�ēx" + GameMain.instance.battleTurnCharacter.name + "�̃^�[���ł�");
                        GameMain.instance.OtherBattlePhase(GameMain.instance.battleTurnCharacter);
                    }
                    else// if (defenderCharacter.influence == GameMain.instance.playerCharacter.influence)
                    {
                        Debug.Log("���̃L�����N�^�[�̃^�[���ł��B");
                        GameMain.instance.NextCharacterBattlePhase(GameMain.instance.battleTurnCharacter);
                    }
                    break;
                case 5:
                case 6:
                    if (attackableCharacterCount > 3)
                    {
                        Debug.Log("�ēx" + GameMain.instance.battleTurnCharacter.name + "�̃^�[���ł�");
                        GameMain.instance.OtherBattlePhase(GameMain.instance.battleTurnCharacter);
                    }
                    else// if (defenderCharacter.influence == GameMain.instance.playerCharacter.influence)
                    {
                        Debug.Log("���̃L�����N�^�[�̃^�[���ł��B");
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

        // ���X�g���烉���_���ɃC���f�b�N�X��I��
        int randomIndex = Random.Range(0, soliderList.Count);

        // �I�����ꂽ�����_���ȕ��m��Ԃ�
        return soliderList[randomIndex];
    }

    public void AttackBattle(CharacterController attackCharacter, CharacterController defenceCharacter)
    {
        StartCoroutine(WaitForAttackBattle(attackCharacter, defenceCharacter));
    }

    //�����̖͂������U�����鏈��
    public IEnumerator WaitForAttackBattle(CharacterController attackCharacter, CharacterController defenceCharacter)
    {
        attackerRetreatFlag = false;
        defenderRetreatFlag = false;

        attackerCharacter = attackCharacter;
        defenderCharacter = defenceCharacter;

        //�����퓬��ʕ\��
        TitleFieldUI.instance.titleFieldText.text = "      ���� VS �G�@�퓬�I";
        mapField.SetActive(true);
        cursor.gameObject.SetActive(true);

        // �J�[�\���̈ʒu��ݒ�
        RectTransform territoryRectTransform = territoryManager.territory.GetComponent<RectTransform>();
        cursor.SetPosition(territoryRectTransform);
        //cursor.transform.position = territoryManager.territory.position;

        vsImageUI.SetPosition(territoryRectTransform);
        //vsImageUI.transform.position = territoryManager.territory.position;

        battleDetailUI.ShowBattleDetailUI(attackerCharacter, defenderCharacter);
        StartCoroutine(GameMain.instance.BlinkCursor(1.0f));
        yield return new WaitForSeconds(1.0f);

        //�퓬���{
        SoundManager.instance.PlayBattleSE();
        while (attackerRetreatFlag == false && defenderRetreatFlag == false)
        {
            SoliderBattle(attackerCharacter, defenderCharacter);
            SoliderBattle(defenderCharacter, attackerCharacter);
            IsAliveCheckSolider(attackerCharacter, defenderCharacter);
            RetreatCheck(attackerCharacter, defenderCharacter);
            BattleEndCheck(attackerCharacter, defenderCharacter);
            battleDetailUI.ShowBattleDetailUI(attackerCharacter, defenderCharacter);
            vsImageUI.gameObject.SetActive(!vsImageUI.gameObject.activeSelf); // VS�C���[�W�̕\���E��\����؂�ւ���
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

    //�����̖͂������h�q���鏈��
    public IEnumerator WaitForDefenceBattle(CharacterController attackCharacter, CharacterController defenceCharacter)
    {
        attackerRetreatFlag = false;
        defenderRetreatFlag = false;

        attackerCharacter = attackCharacter;
        defenderCharacter = defenceCharacter;

        //�퓬�O�̉�ʕ\��
        TitleFieldUI.instance.titleFieldText.text = "      �G VS �����@�퓬�I";
        mapField.SetActive(true);
        cursor.gameObject.SetActive(true);

        // �J�[�\���̈ʒu��ݒ�
        RectTransform territoryRectTransform = territoryManager.territory.GetComponent<RectTransform>();
        cursor.SetPosition(territoryRectTransform);
        //cursor.transform.position = territoryManager.territory.position;

        vsImageUI.SetPosition(territoryRectTransform);
        //vsImageUI.transform.position = territoryManager.territory.position;

        battleDetailUI.ShowBattleDetailUI(attackCharacter, defenceCharacter);
        StartCoroutine(GameMain.instance.BlinkCursor(1.0f));
        yield return new WaitForSeconds(1.0f);

        //�퓬���{�@�퓬����ʕ\��
        SoundManager.instance.PlayBattleSE();
        while (attackerRetreatFlag == false && defenderRetreatFlag == false)
        {
            SoliderBattle(attackerCharacter, defenderCharacter);
            SoliderBattle(defenderCharacter, attackerCharacter);
            IsAliveCheckSolider(attackerCharacter, defenderCharacter);
            RetreatCheck(attackerCharacter, defenderCharacter);
            BattleEndCheck(attackerCharacter, defenderCharacter);
            battleDetailUI.ShowBattleDetailUI(attackerCharacter, defenderCharacter);
            vsImageUI.gameObject.SetActive(!vsImageUI.gameObject.activeSelf); // VS�C���[�W�̕\���E��\����؂�ւ���
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

        TitleFieldUI.instance.titleFieldText.text = "�퓬��������܂���";
        StartCoroutine(GameMain.instance.BlinkTerritory(0.5f, attackerCharacter, GameMain.instance.playerCharacter, territoryManager.territory));
        yield return new WaitForSeconds(battleAfterWaitTime);

        attackerCharacter.isAttackable = false;
        attackerCharacter.isBattle = true;
        territoryUIOnMouse.ChangeTerritoryByBattle(attackerCharacter.influence);
        isBattleEnd = true;

        CheckExtinct(GameMain.instance.playerCharacter.influence);

        mapField.SetActive(false);

        //���̏����ֈڍs
        CheckAttackableCharacterInInfluence();
    }
}
