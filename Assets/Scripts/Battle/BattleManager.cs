using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static GameMain;
using Cysharp.Threading.Tasks;
using System;

public class BattleManager : MonoBehaviour
{
    [SerializeField] Cursor cursor;
    [SerializeField] private UtilityParamObject varParam;
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;
    [SerializeField] Transform AttackerSoliderField, DefenderSoliderField;
    [SerializeField] GameObject attackSoliderPrefab;
    [SerializeField] GameObject defenceSoliderPrefab;
    [SerializeField] BattleDetailUI battleDetailUI;
    [SerializeField] BattleUI battleUI;
    [SerializeField] GameObject mapField;

    public CharacterController attackerCharacter;
    public CharacterController defenderCharacter;

    public Influence influence;

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
        }
        else
        {
            var soldierObject = Instantiate(defenceSoliderPrefab, field);
            soldierObject.GetComponent<SoldierImageView>().
                ShowBattleSoldier(
                solider.icon,
                solider.hp,
                solider.maxHP);
        }
    }

    void ShowSoliderList(List<SoldierController> soliderList, Transform field, bool Attack)
    {
        // ���ݕ\������Ă��镺�m���폜
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
        _ = HandleButtonClickAsync();
    }

    private async UniTask HandleButtonClickAsync()
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
                await PlayerBattleEnd();
            }
        }
    }

    public async void RetreatButton()
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
            await PlayerBattleEnd();
        }
    }

    async UniTask PlayerBattleEnd()
    {
        // ���͂𖳌���
        inputEnabled = false;
        await UniTask.Delay(TimeSpan.FromSeconds(2.0f));
        battleUI.HideBattleUI();
        // ���͂�L����
        inputEnabled = true;

        await ShowEndBattle();

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
                attackerSolider.Attack(attackChara, defenceChara, defenderSolider, varParam.Territory);
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

    /// <summary>
    /// �ދp���邩�`�F�b�N����
    /// </summary>
    /// <param name="attackCharacter"></param>
    /// <param name="defenceCharacter"></param>
    public void RetreatCheck(CharacterController attackCharacter, CharacterController defenceCharacter)
    {
        //�v���C���[���퓬����ꍇ
        if (attackCharacter == GameMain.instance.playerCharacter || defenceCharacter == GameMain.instance.playerCharacter)
        {
            if (attackCharacter.soliderList.Count == 0)
            {
                attackerRetreatFlag = true;
            }

            if (defenceCharacter.soliderList.Count == 0)
            {
                defenderRetreatFlag = true;
            }
        }
        else
        {
            int attackerSoliderHpSum = attackCharacter.CalcSoldierHPSum();
            int defenderSoliderHpSum = defenceCharacter.CalcSoldierHPSum();

            //�h�q���������Ă���ꍇ
            if (attackerSoliderHpSum > defenderSoliderHpSum)
            {
                RetreatFlagCheck(defenceCharacter, ref defenderRetreatFlag);
            }
            //�N�U���������Ă���ꍇ
            else if (attackerSoliderHpSum < defenderSoliderHpSum)
            {
                RetreatFlagCheck(attackCharacter, ref attackerRetreatFlag);
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
    }

    /// <summary>
    /// �ދp�t���O�̃`�F�b�N
    /// </summary>
    /// <param name="character"></param>
    /// <param name="retreatFlag"></param>
    private void RetreatFlagCheck(CharacterController character, ref bool retreatFlag)
    {
        // �e�����Ɋ�Â��đދp�t���O��ݒ肷��
        bool ShouldRetreat(int threshold)
        {
            return character.soliderList.Any(soldier => soldier.hp < threshold);
        }

        int soldierCount = character.soliderList.Count;

        switch (varParam.Territory.defenceTerritoryType)
        {
            case Territory.DefenceTerritoryType.desert:

                retreatFlag = ShouldRetreat(20);
                break;

            case Territory.DefenceTerritoryType.wilderness:
                retreatFlag = ShouldRetreat(10);
                break;

            case Territory.DefenceTerritoryType.plain:
                retreatFlag = soldierCount < 10;
                break;

            case Territory.DefenceTerritoryType.forest:
                retreatFlag = soldierCount < 5;
                break;

            case Territory.DefenceTerritoryType.fort:
                retreatFlag = soldierCount == 0;
                break;
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
        }

        attackerCharacter.isBattle = true;
        defenderCharacter.isBattle = true;
    }

    /// <summary>
    /// �퓬��̏���
    /// </summary>
    /// <returns></returns>
    public async UniTask ShowEndBattle()
    {
        TitleFieldUI.instance.titleFieldSubText.text = "�퓬�t�F�[�Y";
        mapField.SetActive(true);
        GameMain.instance.VSImageUI.gameObject.SetActive(false);
        cursor.gameObject.SetActive(true);

        // �J�[�\���̈ʒu��ݒ�
        RectTransform territoryRectTransform = varParam.Territory.GetComponent<RectTransform>();
        cursor.SetPosition(territoryRectTransform);

        battleDetailUI.ShowBattleDetailUI(attackerCharacter, defenderCharacter);
        if (attackerRetreatFlag == true)
        {
            TitleFieldUI.instance.titleFieldText.text = "      �h�q���̏����ł�";
        }
        else
        {
            TitleFieldUI.instance.titleFieldText.text = "      �N�U���̏����ł�";
            StartCoroutine(GameMain.instance.BlinkTerritory(0.5f, attackerCharacter, defenderCharacter, varParam.Territory));
        }
        await UniTask.Delay(TimeSpan.FromSeconds(battleAfterWaitTime));

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
        int randomIndex = UnityEngine.Random.Range(0, soliderList.Count);

        // �I�����ꂽ�����_���ȕ��m��Ԃ�
        return soliderList[randomIndex];
    }

    /// <summary>
    /// ���R�̔z�����퓬���鏈��
    /// </summary>
    /// <param name="attackCharacter"></param>
    /// <param name="defenceCharacter"></param>
    /// <returns></returns>
    public async UniTask MySubordinateBattle(CharacterController attackCharacter, CharacterController defenceCharacter)
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
        RectTransform territoryRectTransform = varParam.Territory.GetComponent<RectTransform>();
        cursor.SetPosition(territoryRectTransform);

        GameMain.instance.VSImageUI.SetPosition(territoryRectTransform);

        battleDetailUI.ShowBattleDetailUI(attackCharacter, defenceCharacter);
        await GameMain.instance.BlinkCursor(1.0f);

        //�퓬���{
        await AIBattle(attackCharacter, defenceCharacter);

        await ShowEndBattle();

        CheckExtinct(defenceCharacter.influence);

        //�������h�q�̏ꍇ
        if (GameMain.instance.defenceFlag)
        {
            //���������N�U���͂̃t�F�[�Y����
            CheckAttackableCharacterInInfluence();
        }
        else
        {
            //�v���C���[�o�g���t�F�[�Y�ֈڍs
            GameMain.instance.PlayerBattlePhase();
        }
    }

    /// <summary>
    /// �퓬���������
    /// </summary>
    /// <returns></returns>
    public async UniTask AbandonBattle()
    {
        mapField.SetActive(true);

        TitleFieldUI.instance.titleFieldText.text = "�퓬��������܂���";
        await GameMain.instance.BlinkTerritory(0.5f, attackerCharacter, GameMain.instance.playerCharacter, varParam.Territory);

        attackerCharacter.isAttackable = false;
        attackerCharacter.isBattle = true;
        territoryUIOnMouse.ChangeTerritoryByBattle(attackerCharacter.influence);

        CheckExtinct(GameMain.instance.playerCharacter.influence);

        mapField.SetActive(false);

        //���̏����ֈڍs
        CheckAttackableCharacterInInfluence();
    }

    /// <summary>
    /// �v���C���[�ȊO���퓬���鏈��
    /// </summary>
    /// <param name="attackChara"></param>
    /// <param name="defenceChara"></param>
    /// <returns></returns>
    public async UniTask AIBattle(CharacterController attackChara, CharacterController defenceChara)
    {
        attackerCharacter = attackChara;
        defenderCharacter = defenceChara;

        SoundManager.instance.PlayBattleSE();
        while (attackerRetreatFlag == false && defenderRetreatFlag == false)
        {
            SoliderBattle(attackChara, defenceChara);
            SoliderBattle(defenceChara, attackChara);
            IsAliveCheckSolider(attackChara, defenceChara);
            RetreatCheck(attackChara, defenceChara);
            BattleEndCheck(attackChara, defenceChara);
            battleDetailUI.ShowBattleDetailUI(attackChara, defenceChara);
            GameMain.instance.VSImageUI.gameObject.SetActive(!GameMain.instance.VSImageUI.gameObject.activeSelf); // VS�C���[�W�̕\���E��\����؂�ւ���
            await UniTask.Delay(TimeSpan.FromSeconds(0.05f));
        }
    }
}
