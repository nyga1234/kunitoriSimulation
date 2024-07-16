using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;
using static GameManager;
using UnityEngine.TextCore.Text;
using System.Linq;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Cursor cursor;
    [SerializeField] VSImageUI vsImageUI;
    //[SerializeField] InflueneceManager influeneceManager;
    [SerializeField] TerritoryManager territoryManager;
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;
    [SerializeField] Transform AttackerSoliderField, DefenderSoliderField;
    [SerializeField] SoliderController attackSoliderPrefab;
    [SerializeField] SoliderController defenceSoliderPrefab;
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
            SoliderController battleSolider = Instantiate(attackSoliderPrefab, field, false);
            battleSolider.ShowBattleSoliderUI(solider, Attack);
        }
        else
        {
            SoliderController battleSolider = Instantiate(defenceSoliderPrefab, field, false);
            battleSolider.ShowBattleSoliderUI(solider, Attack);
        }
    }

    void ShowSoliderList(List<SoliderController> soliderList, Transform field, bool Attack)
    {
        // ���ݕ\������Ă��镺�m���폜
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }

        // �V�������m���X�g���쐬
        foreach (SoliderController solider in soliderList)
        {
            CreateAttackrSolider(solider, field, Attack);
        }
    }

    void HideSoliderList(List<SoliderController> soliderList, Transform field)
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
            if (attackerCharacter == GameManager.instance.playerCharacter)
            {
                HideSoliderList(attackerCharacter.soliderList, AttackerSoliderField);
                attackerRetreatFlag = true;
            }
            else if (defenderCharacter == GameManager.instance.playerCharacter)
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
        if (attackerCharacter == GameManager.instance.playerCharacter)
        {
            if (GameManager.instance.battleTurnCharacter == GameManager.instance.playerCharacter)
            {
                GameManager.instance.PlayerBattlePhase();
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
        //HP��0�ɂȂ������m���擾
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
        //HP��0�ɂȂ������m�����X�g����폜
        foreach (SoliderController solider in deadAttackers)
        {
            attackChara.soliderList.Remove(solider);
        }
        foreach (SoliderController solider in deadDefenders)
        {
            defenceChara.soliderList.Remove(solider);
        }
    }

    public void RetreatCheck(CharacterController attackCharacter, CharacterController defenceCharacter)
    {
        if (attackCharacter == GameManager.instance.playerCharacter)
        {
            if (attackCharacter.soliderList.Count == 0)
            {
                attackerRetreatFlag = true;
            }
        }

        if (defenceCharacter == GameManager.instance.playerCharacter)
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

        //�h�q���������Ă���ꍇ
        if (attackerSoliderHpSum > defenderSoliderHpSum)
        {
            if (defenceCharacter != GameManager.instance.playerCharacter)
            {
                switch (territoryManager.territory.defenceTerritoryType)
                {
                    //�����̏ꍇ�A�����ꂩ���m��HP��20�����ɂȂ�����ދp������
                    case Territory.DefenceTerritoryType.desert:
                        foreach (SoliderController solider in defenceCharacter.soliderList)
                        {
                            if (solider.soliderModel.hp < 20)
                            {
                                defenderRetreatFlag = true;
                                Debug.Log("�h�q���������܂���");
                            }
                        }
                        break;
                    //�r��̏ꍇ�A�����ꂩ���m��HP��10�����ɂȂ�����ދp������
                    case Territory.DefenceTerritoryType.wilderness:
                        foreach (SoliderController solider in defenceCharacter.soliderList)
                        {
                            if (solider.soliderModel.hp < 10)
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
            if (attackCharacter != GameManager.instance.playerCharacter)
            {
                switch (territoryManager.territory.attackTerritoryType)
                {
                    //�����̏ꍇ�A�����ꂩ���m��HP��20�����ɂȂ�����ދp������
                    case Territory.AttackTerritoryType.desert:
                        foreach (SoliderController solider in attackCharacter.soliderList)
                        {
                            if (solider.soliderModel.hp < 20)
                            {
                                attackerRetreatFlag = true;
                                Debug.Log("�N�U���������܂���");
                            }
                        }
                        break;
                    //�r��̏ꍇ�A�����ꂩ���m��HP��10�����ɂȂ�����ދp������
                    case Territory.AttackTerritoryType.wilderness:
                        foreach (SoliderController solider in attackCharacter.soliderList)
                        {
                            if (solider.soliderModel.hp < 10)
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
            if (attackerCharacter == GameManager.instance.playerCharacter)
            {
                TitleFieldUI.instance.titleFieldText.text = "      �v���C���[�͑ދp���܂���";
            }
            else
            {
                TitleFieldUI.instance.titleFieldText.text = "      �G�R�͑ދp���܂���";
            }
            attackerCharacter.characterModel.isAttackable = false;
            defenderCharacter.characterModel.fame += 2;
            isBattleEnd = true;
        }
        else if (defenderRetreatFlag == true)
        {
            if (defenderCharacter == GameManager.instance.playerCharacter)
            {
                TitleFieldUI.instance.titleFieldText.text = "      �v���C���[�͑ދp���܂���";
            }
            else
            {
                TitleFieldUI.instance.titleFieldText.text = "      �G�R�͑ދp���܂���";
            }
            Debug.Log(attackerCharacter.characterModel.name + "�̏����ł��B");
            attackerCharacter.characterModel.isAttackable = false;
            attackerCharacter.characterModel.fame += 2;
            territoryUIOnMouse.ChangeTerritoryByBattle(attackerCharacter.influence);
            isBattleEnd = true;
        }

        attackerCharacter.characterModel.isBattle = true;
        defenderCharacter.characterModel.isBattle = true;
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
            StartCoroutine(GameManager.instance.BlinkTerritory(0.5f, attackerCharacter, defenderCharacter, territoryManager.territory));
        }
        yield return new WaitForSeconds(battleAfterWaitTime);

        battleDetailUI.HideBattleDetailUI();
        mapField.SetActive(false);
        cursor.gameObject.SetActive(false);

        GameManager.instance.step = Step.Attack;
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
                GameManager.instance.noneInfluence.AddCharacter(chara);
                if (chara.characterModel.isLord == true)
                {
                    chara.characterModel.isLord = false;
                }
            }
        }
    }

    public void CheckAttackableCharacterInInfluence()
    {
        Debug.Log("CheckAttackableCharacterInInfluence");
        //�U���\�ȃL���������擾
        if (GameManager.instance.battleTurnCharacter.influence != GameManager.instance.noneInfluence)
        {
            int attackableCharacterCount = GameManager.instance.battleTurnCharacter.influence.characterList.Count(c => c.characterModel.isAttackable);
            //���͂ɏ�������L�������ɉ����ď����𕪂���
            switch (GameManager.instance.battleTurnCharacter.influence.characterList.Count)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                    if (attackableCharacterCount > 2)
                    {
                        Debug.Log("�ēx" + GameManager.instance.battleTurnCharacter.characterModel.name + "�̃^�[���ł�");
                        GameManager.instance.OtherBattlePhase(GameManager.instance.battleTurnCharacter);
                    }
                    else// if (defenderCharacter.influence == GameManager.instance.playerCharacter.influence)
                    {
                        Debug.Log("���̃L�����N�^�[�̃^�[���ł��B");
                        GameManager.instance.NextCharacterBattlePhase(GameManager.instance.battleTurnCharacter);
                    }
                    break;
                case 5:
                case 6:
                    if (attackableCharacterCount > 3)
                    {
                        Debug.Log("�ēx" + GameManager.instance.battleTurnCharacter.characterModel.name + "�̃^�[���ł�");
                        GameManager.instance.OtherBattlePhase(GameManager.instance.battleTurnCharacter);
                    }
                    else// if (defenderCharacter.influence == GameManager.instance.playerCharacter.influence)
                    {
                        Debug.Log("���̃L�����N�^�[�̃^�[���ł��B");
                        GameManager.instance.NextCharacterBattlePhase(GameManager.instance.battleTurnCharacter);
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
        StartCoroutine(GameManager.instance.BlinkCursor(1.0f));
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

        GameManager.instance.PlayerBattlePhase();
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
        StartCoroutine(GameManager.instance.BlinkCursor(1.0f));
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
        StartCoroutine(GameManager.instance.BlinkTerritory(0.5f, attackerCharacter, GameManager.instance.playerCharacter, territoryManager.territory));
        yield return new WaitForSeconds(battleAfterWaitTime);

        attackerCharacter.characterModel.isAttackable = false;
        attackerCharacter.characterModel.isBattle = true;
        territoryUIOnMouse.ChangeTerritoryByBattle(attackerCharacter.influence);
        isBattleEnd = true;

        CheckExtinct(GameManager.instance.playerCharacter.influence);

        mapField.SetActive(false);

        //���̏����ֈڍs
        CheckAttackableCharacterInInfluence();
    }
}
