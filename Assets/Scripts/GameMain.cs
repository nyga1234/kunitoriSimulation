using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class GameMain : SingletonMonoBehaviour<GameMain>
{
    [SerializeField] UtilityParamObject constParam;
    public List<CharacterController> characterList;
    public List<Influence> influenceList;

    [SerializeField] Cursor cursor;
    [SerializeField] VSImageUI vsImageUI;
    [SerializeField] SoldierController soliderPrefab;
    [SerializeField] TitleFieldUI titleFieldUI;
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] YesNoUI yesNoUI;
    [SerializeField] BattleManager battleManager;
    [SerializeField] TerritoryManager territoryManager;
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;
    [SerializeField] CharacterTurnUI characterTurnUI;
    [SerializeField] CharacterMenuUI characterMenuUI;
    [SerializeField] PersonalMenuUI personalMenuUI;
    [SerializeField] BattleMenuUI battleMenuUI;
    [SerializeField] CharacterIndexUI characterIndexUI;
    [SerializeField] AbandonUI abandonUI;
    [SerializeField] AttackedCharacterUI attackedCharacterUI;
    [SerializeField] LandformInformationUI landformInformationUI;
    //[SerializeField] CharacterSearchUI characterSearchUI;
    [SerializeField] DetailUI detailsUI;
    [SerializeField] CharacterDetailUI characterDetailUI;
    [SerializeField] InfluenceUI influenceUI;
    [SerializeField] InfluenceOnMapUI influenceOnMapUI;
    [SerializeField] BattleUI battleUI;
    [SerializeField] BattleDetailUI battleDetailUI;
    [SerializeField] GameObject mapField;
    [SerializeField] GameObject characterIndexMenu;
    [SerializeField] GameObject characterSearchMenu;
    public TerritoryGenerator territoryGenerator;
    public CharacterController playerCharacter;
    
    public List<Territory> allTerritoryList;
    public Influence noneInfluence;

    public CharacterController battleTurnCharacter;

    public bool defenceFlag;
    public int turnCount;
    public int territoryCouont;
    public Influence uniteInfluence; //���ꂵ������
    public bool uniteCountryFlag = false;

    [SerializeField] Territory plainTerritorPrefab;
    [SerializeField] Transform parent;
    public List<Territory> initializeTerritoryList;

    //�t�F�[�Y�̊Ǘ�
    public enum Phase
    {
        CharacterChoicePhase,//�L�����I���t�F�[�Y
        SetupPhase,//�Z�b�g�A�b�v�t�F�[�Y
        PlayerLordPhase,//�v���C���[�̎�t�F�[�Y(�v���C���[���̎�̏ꍇ�̂݁j
        OtherLordPhase,//���̎�t�F�[�Y
        PlayerPersonalPhase,//�v���C���[�l�t�F�[�Y
        OtherPersonalPhase,//���l�t�F�[�Y
        BattlePhase,//�o�g���t�F�[�Y
        PlayerBattlePhase,//�v���C���[�o�g���t�F�[�Y
        OtherBattlePhase,//���o�g���t�F�[�Y
    }
    public Phase phase;

    public enum Step
    {
        Choice,
        Enter,
        Information,
        Appointment,
        Search,
        Banishment,
        Attack,
        Battle,
        End,
    }
    public Step step;

    //���������͖�
    private string noneInfluenceName = "NoneInfluence";

    private void Start()
    {
        Initialize();

        if (SaveLoadManager.SelectSlot < 0)
        {
            StartGame();
        }
        else
        {
            GameManager.instance.LoadGame(SaveLoadManager.SelectSlot);
        }

        SceneController.instance.Stack.Add("GameMain");
    }

    private void OnDestroy()
    {
        SceneController.instance.Stack.Remove("GameMain");
    }

    private void Update()
    {
        if (!yesNoUI.gameObject.activeSelf && !dialogueUI.gameObject.activeSelf)
        {
            MouseDown1ToBack();
        }
    }

    private void Initialize()
    {
        Debug.Log("Initialize");
        this.influenceList.Clear();
        this.characterList.Clear();

        foreach (Influence influence in constParam.influenceList)
        {
            influenceList.Add(Instantiate(influence));
        }
        noneInfluence = influenceList.Find(c => c.influenceName == "NoneInfluence");
        foreach (CharacterController character in constParam.characterList)
        {
            characterList.Add(Instantiate(character));
        }
        
        initializeTerritoryList = territoryGenerator.InitializeTerritory();
    }

    public void StartGame()
    {
        Debug.Log("StartGame");
        
        //�̎�
        CharacterController serugius = characterList.Find(c => c.characterId == 1);
        CharacterController victor = characterList.Find(c => c.characterId == 2);
        CharacterController arisia = characterList.Find(c => c.characterId == 3);
        CharacterController rourenthius = characterList.Find(c => c.characterId == 4);
        CharacterController feodoora = characterList.Find(c => c.characterId == 25);
        //�̎�z��
        CharacterController eresuthia = characterList.Find(c => c.characterId == 5);
        CharacterController renius = characterList.Find(c => c.characterId == 6);
        CharacterController peruseus = characterList.Find(c => c.characterId == 7);
        CharacterController amerieru = characterList.Find(c => c.characterId == 8);
        CharacterController karisutaana = characterList.Find(c => c.characterId == 9);
        CharacterController mariseruda = characterList.Find(c => c.characterId == 10);
        CharacterController venethia = characterList.Find(c => c.characterId == 11);
        CharacterController siguma = characterList.Find(c => c.characterId == 12);
        CharacterController jovannni = characterList.Find(c => c.characterId == 26);
        CharacterController simon = characterList.Find(c => c.characterId == 27);
        //������
        CharacterController sofuronia = characterList.Find(c => c.characterId == 13);
        CharacterController ferisithi = characterList.Find(c => c.characterId == 14);
        CharacterController rainnharuto = characterList.Find(c => c.characterId == 15);
        CharacterController ferics = characterList.Find(c => c.characterId == 16);
        CharacterController veronica = characterList.Find(c => c.characterId == 17);
        CharacterController reo = characterList.Find(c => c.characterId == 18);
        CharacterController marukus = characterList.Find(c => c.characterId == 19);
        CharacterController reira = characterList.Find(c => c.characterId == 20);
        CharacterController marissa = characterList.Find(c => c.characterId == 21);
        CharacterController nataasya = characterList.Find(c => c.characterId == 22);
        CharacterController iruma = characterList.Find(c => c.characterId == 23);
        CharacterController riisya = characterList.Find(c => c.characterId == 24);
        CharacterController oriannna = characterList.Find(c => c.characterId == 29);
        CharacterController garahaddo = characterList.Find(c => c.characterId == 30);
        CharacterController aressandoro = characterList.Find(c => c.characterId == 31);
        CharacterController akuserion = characterList.Find(c => c.characterId == 28);
        CharacterController dhionyusios = characterList.Find(c => c.characterId == 32);
        CharacterController tadeus = characterList.Find(c => c.characterId == 33);
        CharacterController siruvietto = characterList.Find(c => c.characterId == 34);
        CharacterController ruben = characterList.Find(c => c.characterId == 35);

        //�L�����N�^�[�𐨗͂֏���������
        foreach (Influence influence in influenceList)
        {
            if (influence.influenceName == "�Z���M�E�X")
            {
                Debug.Log("�L�����𐨗͂֏���");
                influence.AddCharacter(serugius);
                influence.AddCharacter(eresuthia);
                influence.AddCharacter(renius);
            }
            else if (influence.influenceName == "���B�N�^�[")
            {
                influence.AddCharacter(victor);
                influence.AddCharacter(peruseus);
                influence.AddCharacter(amerieru);

            }
            else if (influence.influenceName == "�A���V�A")
            {
                influence.AddCharacter(arisia);
                influence.AddCharacter(karisutaana);
                influence.AddCharacter(mariseruda);
            }
            else if (influence.influenceName == "���[�����e�B�E�X")
            {
                influence.AddCharacter(rourenthius);
                influence.AddCharacter(venethia);
                influence.AddCharacter(siguma);
            }
            else if (influence.influenceName == "�t�F�I�h�[��")
            {
                influence.AddCharacter(feodoora);
                influence.AddCharacter(jovannni);
                influence.AddCharacter(simon);
            }
            else if (influence.influenceName == noneInfluenceName)
            {
                influence.AddCharacter(sofuronia);
                influence.AddCharacter(ferisithi);
                influence.AddCharacter(rainnharuto);
                influence.AddCharacter(ferics);
                influence.AddCharacter(veronica);
                influence.AddCharacter(reo);
                influence.AddCharacter(marukus);
                influence.AddCharacter(reira);
                influence.AddCharacter(marissa);
                influence.AddCharacter(nataasya);
                influence.AddCharacter(iruma);
                influence.AddCharacter(riisya);
                influence.AddCharacter(akuserion);
                influence.AddCharacter(oriannna);
                influence.AddCharacter(garahaddo);
                influence.AddCharacter(aressandoro);
                influence.AddCharacter(dhionyusios);
                influence.AddCharacter(tadeus);
                influence.AddCharacter(siruvietto);
                influence.AddCharacter(ruben);
            }
        }

        //�L�����N�^�[�֕��m�����蓖��
        foreach (CharacterController character in characterList)
        {
            if (character == serugius || character == victor || character == arisia || character == rourenthius || character == feodoora)
            {
                foreach (SoldierController soldier in constParam.soldierList1)
                {
                    character.soliderList.Add(Instantiate(soldier));
                }
            }
            else if (character == eresuthia || character == peruseus || character == karisutaana || character == venethia || character == jovannni)
            {
                foreach (SoldierController soldier in constParam.soldierList2)
                {
                    character.soliderList.Add(Instantiate(soldier));
                }
            }
            else if (character == renius || character == amerieru || character == mariseruda || character == siguma || character == simon)
            {
                foreach (SoldierController soldier in constParam.soldierList3)
                {
                    character.soliderList.Add(Instantiate(soldier));
                }
            }
            else
            {
                foreach (SoldierController soldier in constParam.soldierList4)
                {
                    character.soliderList.Add(Instantiate(soldier));
                }
            }
        }

        allTerritoryList = territoryGenerator.GenerateTerritory(initializeTerritoryList, influenceList);

        phase = Phase.CharacterChoicePhase;
        PhaseCalc();
    }

    public void PhaseCalc()
    {
        switch (phase)
        {
            case Phase.CharacterChoicePhase:
                CharacterChoicePhase();
                break;
            case Phase.SetupPhase:
                SetupPhase();
                break;

            case Phase.PlayerLordPhase:
                PlayerLordPhase();
                break;

            case Phase.OtherLordPhase:
                StartCoroutine(OtherLordPhase());
                break;

            case Phase.PlayerPersonalPhase:
                PlayerPersonalPhase();
                break;

            case Phase.OtherPersonalPhase:
                StartCoroutine(OtherPersonalPhase());
                break;

            case Phase.BattlePhase:
                BattlePhase();
                break;

            case Phase.PlayerBattlePhase:
                PlayerBattlePhase();
                break;

                //case Phase.OtherBattlePhase:
                //    OtherBattlePhase();
                //    break;
        }
    }

    void CharacterChoicePhase()
    {
        titleFieldUI.ShowCharacterChoiceText();
        mapField.SetActive(true);

        step = Step.Choice;
    }

    void SetupPhase()
    {
        turnCount++;

        //�S�ẴL�����N�^�[���U���\�ɐݒ�
        foreach (CharacterController character in characterList)
        {
            character.isAttackable = true;
        }

        //�S�ẴL�����N�^�[�̃o�g���t���O��false�ɐݒ�
        foreach (CharacterController character in characterList)
        {
            character.isBattle = false;
        }

        //�S�Ă̕��m��HP����
        foreach (CharacterController character in characterList)
        {
            foreach (SoldierController solider in character.soliderList)
            {
                solider.hp = solider.maxHP;
            }
        }

        //�����ɉ������g���̕ύX

        //�̓y���ɉ������������L�����N�^�[�ɕ��z
        foreach (Influence influence in influenceList)
        {
            if (influence != noneInfluence)
            {
                int territoryIncome = 15;
                int influenceIncome = 0;//�l��������
                influenceIncome = influence.territoryList.Count * territoryIncome;
                foreach (CharacterController character in influence.characterList)
                {
                    character.gold += Mathf.RoundToInt(influenceIncome * character.salary / 100.0f);
                }
            }
            else
            {
                foreach (CharacterController character in influence.characterList)
                {
                    character.gold += Mathf.RoundToInt(character.tact / 100.0f * 10.0f);
                }
            }
        }

        phase = Phase.PlayerLordPhase;
        PhaseCalc();
    }

    void PlayerLordPhase()
    {
        if (playerCharacter.isLord == true)
        {
            Debug.Log("�v���C���[�̎�t�F�[�Y�ł��B");

            titleFieldUI.ShowChangeLordTurnText();
            ShowLordUI(playerCharacter);
        }
        else
        {
            step = Step.End;

            phase = Phase.OtherLordPhase;
            PhaseCalc();
        }
    }

    IEnumerator OtherLordPhase()
    {
        Debug.Log("���L�����N�^�[�̗̎�t�F�[�Y�ł��B");

        titleFieldUI.ShowChangeLordTurnText();

        mapField.SetActive(true);
        //���L�����N�^�[�̗̎僊�X�g�̎擾
        List<CharacterController> lordCharacterList = characterList.FindAll(character => character.isLord && !character.isPlayerCharacter);

        //�e�̎�^�[��
        foreach (CharacterController lordCharacter in lordCharacterList)
        {
            characterTurnUI.ShowCharacterTurnUI(lordCharacter);
            //�L�����N�^�[�̓o�p
            switch (lordCharacter.influence.territoryList.Count)
            {
                case 1:
                case 2:
                    while (lordCharacter.influence.characterList.Count <= 2 && lordCharacter.gold >= 9 && noneInfluence.characterList.Count != 0)
                    {
                        yield return StartCoroutine(NoneRandomCharacterAddInfluence(lordCharacter));
                    }
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                    while (lordCharacter.influence.characterList.Count <= 3 && lordCharacter.gold >= 9 && noneInfluence.characterList.Count != 0)
                    {
                        yield return StartCoroutine(NoneRandomCharacterAddInfluence(lordCharacter));
                    }
                    break;
                case 7:
                case 8:  
                case 9:
                case 10:
                    while (lordCharacter.influence.characterList.Count <= 4 && lordCharacter.gold >= 9 && noneInfluence.characterList.Count != 0)
                    {
                        yield return StartCoroutine(NoneRandomCharacterAddInfluence(lordCharacter));
                    }
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                    while (lordCharacter.influence.characterList.Count <= 5 && lordCharacter.gold >= 9 && noneInfluence.characterList.Count != 0)
                    {
                        yield return StartCoroutine(NoneRandomCharacterAddInfluence(lordCharacter));
                    }
                    break;
            }
            int beforeRank = (int)playerCharacter.rank;
            //�����̍������ɐg����ݒ�
            lordCharacter.influence.SetRankByFame();
            int afterRank = (int)playerCharacter.rank;

            if (afterRank > beforeRank)
            {
                Debug.Log("���i���܂���");
                dialogueUI.ShowElavationRankUI(playerCharacter);
                yield return new WaitUntil(() => !dialogueUI.IsDialogueVisible());
            }
            else if (afterRank < beforeRank)
            {
                Debug.Log("�~�i���܂���");
                dialogueUI.ShowDemotionRankUI(playerCharacter);
                yield return new WaitUntil(() => !dialogueUI.IsDialogueVisible());
            }

            yield return new WaitForSeconds(0.125f);
            characterTurnUI.HideCharacterTurnUI();
        }
        mapField.SetActive(false);
        cursor.gameObject.SetActive(false);
        phase = Phase.PlayerPersonalPhase;
        PhaseCalc();
    }

    void PlayerPersonalPhase()
    {
        Debug.Log("�v���C���[�l�t�F�[�Y�ł��B");
        titleFieldUI.ShowChangePersonalTurnText();
        ShowPersonalUI(playerCharacter);
    }

    IEnumerator OtherPersonalPhase()
    {
        Debug.Log("���L�����N�^�[�̌l�t�F�[�Y�ł��B");

        titleFieldUI.ShowChangePersonalTurnText();

        mapField.SetActive(true);
        List<CharacterController> otherCharacterList = characterList.FindAll(x => !x.isPlayerCharacter);
        
        foreach (CharacterController otherCharacter in otherCharacterList)
        {
            characterTurnUI.ShowCharacterTurnUI(otherCharacter);

            //�����̒l�ɉ����ď������͂�����
            if (otherCharacter.isLord == false && otherCharacter.influence != noneInfluence)
            {
                int leaveProbability = 0;
                leaveProbability = 99 - otherCharacter.loyalty;
                // 0����99�̊Ԃ̃����_���Ȓl�𐶐�����
                int randomValue = Random.Range(0, 100);
                // �����_���Ȓl������̊m���ȉ��ł���Ώ������͂�����
                if (randomValue < leaveProbability)
                {
                    // �������͂����鏈���������ɏ���
                    Debug.Log(otherCharacter.name + "�͏������͂�����܂�");
                    if (otherCharacter.influence == playerCharacter.influence)
                    {
                        if (playerCharacter.isLord == true)
                        {
                            dialogueUI.ShowLeaveInfluenceUI(otherCharacter);
                        }
                        // �_�C�A���O��������܂őҋ@
                        yield return new WaitUntil(() => !dialogueUI.IsDialogueVisible());
                    }
                    LeaveInfluence(otherCharacter);
                }
            }

            //���m�ٗp
            while (otherCharacter.soliderList.Count < 10 && otherCharacter.gold >= 2)
            {
                otherCharacter.soliderList.Add(Instantiate(constParam.soldierList.Find(c => c.soliderID == 1)));
                otherCharacter.gold -= 2;
            }
            //���m�P��
            //�̎�̏ꍇ
            if (otherCharacter.isLord == true)
            {
                while (otherCharacter.gold >= 15)
                {
                    foreach (SoldierController solider in otherCharacter.soliderList)
                    {
                        solider.Training(solider);
                    }
                    otherCharacter.gold -= 2;
                }
            }
            //�̎�ȊO�̏ꍇ
            else
            {
                while (otherCharacter.gold >= 7)
                {
                    foreach (SoldierController solider in otherCharacter.soliderList)
                    {
                        solider.Training(solider);
                    }
                    otherCharacter.gold -= 2;
                }
            }

            yield return new WaitForSeconds(0.125f);
            characterTurnUI.HideCharacterTurnUI();
        }

        mapField.SetActive(false);
        cursor.gameObject.SetActive(false);
        phase = Phase.BattlePhase;
        PhaseCalc();
    }

    void BattlePhase()
    {
        Debug.Log("�o�g���t�F�[�Y�ł��B");
        titleFieldUI.ShowChangeBattleTurnText();
        //�L�����N�^�[���X�g���V���b�t��
        ShuffleCharacterList();

        //characterList�̍ŏ��̗v�f���擾
        CharacterController firstCharacter = characterList.FirstOrDefault();
        if (firstCharacter == playerCharacter)
        {
            PlayerBattlePhase();
        }
        else
        {
            OtherBattlePhase(firstCharacter);
        }
    }

    public void PlayerBattlePhase()
    {
        Debug.Log("�v���C���[�o�g���t�F�[�Y�ł��B");
        battleTurnCharacter = playerCharacter;
        if (playerCharacter.influence != noneInfluence)
        {
            step = Step.Battle;
            defenceFlag = false;
            //battleManager.attackerWin = false;
            phase = Phase.PlayerBattlePhase;
            ShowBattleUI(playerCharacter);
        }
        else
        {
            NextCharacterBattlePhase(playerCharacter);
        }
    }

    public void OtherBattlePhase(CharacterController character)
    {
        battleManager.attackerRetreatFlag = false;
        battleManager.defenderRetreatFlag = false;
        step = Step.Battle;
        //battleManager.attackerWin = false;
        Debug.Log("OtherBattlePhase");
        phase = Phase.OtherBattlePhase;
        battleTurnCharacter = character;
        Debug.Log(character.name + "�̃^�[���ł��B");
        //�N�U���邩���f
        if (character.influence != noneInfluence && character.isLord == true && uniteCountryFlag == false)
        {
            //�܂��퓬���Ă��Ȃ��L�����N�^�[�����擾
            int battlefalseCharacterCount = character.influence.characterList.Count(c => !c.isBattle);
            switch (character.influence.characterList.Count)
            {
                case 1:
                case 2:
                    battleManager.isBattleEnd = true;
                    NextCharacterBattlePhase(character);
                    break;
                case 3:
                case 4:
                    if (battlefalseCharacterCount >= 2)
                    {
                        LordsOrderBattle(character);
                    }
                    else
                    {
                        battleManager.isBattleEnd = true;
                        NextCharacterBattlePhase(character);
                    }
                    break;
                case 5:
                case 6:
                    if (battlefalseCharacterCount >= 3)
                    {
                        LordsOrderBattle(character);
                    }
                    else
                    {
                        battleManager.isBattleEnd = true;
                        NextCharacterBattlePhase(character);
                    }
                    break;
            }
        }
        else
        {
            battleManager.isBattleEnd = true;
            NextCharacterBattlePhase(character);
        }
    }

    public void LordsOrderBattle(CharacterController character)
    {
      //�N�U����̓y�����߂�
                //character�̐��͂ɗאڂ��郉���_���ȗ̓y��N�U�ΏۂƂ���
        List<Territory> adjacentTerritory = character.influence.FindAdjacentTerritory();
        Territory randomTerritory = GetRandomTerritory(adjacentTerritory);
        while (randomTerritory.influence == character.influence || randomTerritory.influence == noneInfluence)
        {
            randomTerritory = GetRandomTerritory(adjacentTerritory);
        }
        territoryManager.territory = randomTerritory;
        territoryManager.influence = randomTerritory.influence;
        Debug.Log("�N�U����̓y��" + randomTerritory.position);

        //�N�U��̕������m�F����
            //�N�U��ň�ԋ����L����(���)���擾
        int soliderHPMax = 0;
        int soliderHPSum = 0;
        
        foreach (CharacterController chara in territoryManager.territory.influence.characterList)
        {
            soliderHPSum = 0;
            foreach (SoldierController solider in chara.soliderList)
            {
                soliderHPSum += solider.hp;
            }
            if (soliderHPSum > soliderHPMax)
            {
                soliderHPMax = soliderHPSum;
            }
        }

        //�N�U������L���������߂�
            //�N�U����������L�������擾
        List<CharacterController> attackableCharacterList = character.influence.characterList.FindAll(character => character.isAttackable);
        foreach (CharacterController chara in character.influence.characterList)
        {
            soliderHPSum = 0;
            foreach (SoldierController solider in chara.soliderList)
            {
                soliderHPSum += solider.hp;
            }
            if (soliderHPSum < soliderHPMax)
            {
                attackableCharacterList.Remove(chara);
            }
        }
            //�N�U����������L����������ꍇ�͂��̃L�������o��������
        if (attackableCharacterList.Count != 0)
        {
            System.Random random3 = new System.Random();
            CharacterController randomAttackCharacter = attackableCharacterList[random3.Next(attackableCharacterList.Count)];
            StopAllCoroutines();
            StartCoroutine(BattlePrepare(randomAttackCharacter, soliderHPMax));
        }
        //������������L���������Ȃ��ꍇ
        else
        {
            attackableCharacterList = character.influence.characterList.FindAll(character => character.isAttackable);
            //��ԋ����L������������
            System.Random random4 = new System.Random();
            CharacterController randomDefenceCharacter = attackableCharacterList[random4.Next(attackableCharacterList.Count)];
            CharacterController strongestCharacter = randomDefenceCharacter;

            int soliderStrongestHPSum = 0;
            int soliderHPSum2 = 0;
            foreach (CharacterController chara in attackableCharacterList)
            {
                soliderHPSum2 = 0;
                foreach (SoldierController solider in chara.soliderList)
                {
                    soliderHPSum2 += solider.hp;
                    if (soliderHPSum2 > soliderStrongestHPSum)
                    {
                        strongestCharacter = chara;
                        soliderStrongestHPSum = soliderHPSum2;
                    }
                }

            }

            StopAllCoroutines();
            StartCoroutine(BattlePrepare(strongestCharacter, soliderStrongestHPSum));
        }
    }

    public IEnumerator BattlePrepare(CharacterController attackCharacter, int attackSoliderHPSum)
    {   
        CharacterController defenderCharacter;

        battleManager.isBattleEnd = false;
        defenceFlag = false;

        //�N�U���ꂽ�̓y���v���C���[���́@���@�v���C���[���̎�̏ꍇ
        if (territoryManager.territory.influence == playerCharacter.influence && playerCharacter.isLord == true)
        {
            defenceFlag = true;

            battleManager.attackerCharacter = attackCharacter;

            TitleFieldUI.instance.titleFieldText.text = "      �G�R�̐N�U���󂯂܂���";
            StartCoroutine(ShowAttackedTerritory(attackCharacter));
            yield return new WaitForSeconds(2);

            dialogueUI.ShowAttackedUI();
            yield return new WaitUntil(() => !dialogueUI.IsDialogueVisible());

            mapField.SetActive(false);
            cursor.gameObject.SetActive(false);

            TitleFieldUI.instance.titleFieldText.text = "      �h�q������I�����Ă�������";
            characterIndexMenu.SetActive(true);
            characterIndexUI.ShowCharacterIndexUI(territoryManager.territory.influence.characterList);
            attackedCharacterUI.ShowAttackedCharacterUI(battleManager.attackerCharacter);
            abandonUI.ShowAbandonUI();
            landformInformationUI.ShowLandformInformationUI();
        }
        //�v���C���[���̎�ł͂Ȃ��ꍇ
        else
        {
            //�h�q���Ő퓬�\�ȃL�����N�^�[������ꍇ
            bool canAttack = territoryManager.territory.influence.characterList.Exists(c => c.isAttackable);
            if (canAttack)
            {
                //�N�U�����������L������h��L�����ɑI�� /�@�����L���������Ȃ��ꍇ�͈�ԋ����L������h��L�����ɑI��
                defenderCharacter = SelectDefenceCharacter(attackSoliderHPSum);

                if (attackCharacter == playerCharacter)
                {
                    TitleFieldUI.instance.titleFieldText.text = "      �o�����߂�����܂���";

                    StartCoroutine(ShowAttackedTerritory(attackCharacter));
                    yield return new WaitForSeconds(2);

                    dialogueUI.ShowBattleOrderUI();
                    yield return new WaitUntil(() => !dialogueUI.IsDialogueVisible());

                    battleUI.ShowBattleUI(playerCharacter, defenderCharacter, territoryManager.territory);
                    battleManager.StartBattle(playerCharacter, defenderCharacter);
                }
                else if (defenderCharacter == playerCharacter)
                {
                    TitleFieldUI.instance.titleFieldText.text = "      �o�����߂�����܂���";

                    StartCoroutine(ShowAttackedTerritory(attackCharacter));
                    yield return new WaitForSeconds(2);

                    dialogueUI.ShowBattleOrderUI();
                    yield return new WaitUntil(() => !dialogueUI.IsDialogueVisible());

                    battleUI.ShowBattleUI(attackCharacter, playerCharacter, territoryManager.territory);
                    battleManager.StartBattle(attackCharacter, playerCharacter);                    
                }
                else
                {
                    //�퓬�O�̉�ʕ\��
                    if (attackCharacter.influence == playerCharacter.influence || defenderCharacter.influence == playerCharacter.influence)
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      ���� VS �G�@�퓬�I";
                    }
                    else
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      �G VS �G�@�퓬�I";
                    }
                    mapField.SetActive(true);
                    cursor.gameObject.SetActive(true);

                    // �J�[�\���̈ʒu��ݒ�
                    RectTransform territoryRectTransform = territoryManager.territory.GetComponent<RectTransform>();
                    cursor.SetPosition(territoryRectTransform);
                    //cursor.transform.position = territoryManager.territory.position;

                    vsImageUI.SetPosition(territoryRectTransform);
                    //vsImageUI.transform.position = influenceManager.territory.position;

                    battleDetailUI.ShowBattleDetailUI(attackCharacter, defenderCharacter);
                    StartCoroutine(BlinkCursor(1.0f));
                    yield return new WaitForSeconds(1.0f);

                    //�퓬���{�@�퓬����ʕ\��
                    battleManager.attackerCharacter = attackCharacter;
                    battleManager.defenderCharacter = defenderCharacter;
                    SoundManager.instance.PlayBattleSE();
                    while (battleManager.attackerRetreatFlag == false && battleManager.defenderRetreatFlag == false)
                    {
                        battleManager.SoliderBattle(attackCharacter, defenderCharacter);
                        battleManager.SoliderBattle(defenderCharacter, attackCharacter);
                        battleManager.IsAliveCheckSolider(attackCharacter, defenderCharacter);
                        battleManager.RetreatCheck(attackCharacter, defenderCharacter);
                        battleManager.BattleEndCheck(attackCharacter, defenderCharacter);
                        battleDetailUI.ShowBattleDetailUI(attackCharacter, defenderCharacter);
                        vsImageUI.gameObject.SetActive(!vsImageUI.gameObject.activeSelf); // VS�C���[�W�̕\���E��\����؂�ւ���
                        yield return new WaitForSeconds(0.05f);
                    }

                    //�퓬��̉�ʂ�\��                    
                    StartCoroutine(battleManager.ShowEndBattle());
                    yield return new WaitForSeconds(battleManager.battleAfterWaitTime);

                    battleManager.CheckExtinct(defenderCharacter.influence);

                    //���̏����ֈڍs
                    battleManager.CheckAttackableCharacterInInfluence();
                }
            }
            //�퓬�\�ȃL�����N�^�[�����Ȃ��ꍇ�͖������ŏ���
            else
            {
                Debug.Log("�������ŏ���");
                attackCharacter.isAttackable = false;

                territoryUIOnMouse.ChangeTerritoryByBattle(attackCharacter.influence);
                battleManager.isBattleEnd = true;

                //���̏����ֈڍs
                battleManager.CheckAttackableCharacterInInfluence();
            }
        }
    }

    public CharacterController SelectDefenceCharacter(int attackSoliderHPSum)
    {
        Debug.Log("SelectDefenceCharacter");
        Debug.Log(territoryManager.territory.influence.influenceName);
        CharacterController defenderCharacter;
        //�h��L�������擾
        //�N�U�����������L�������擾
        List<CharacterController> attackableCharacterList = territoryManager.territory.influence.characterList.FindAll(character => character.isAttackable);
        foreach (CharacterController chara in territoryManager.territory.influence.characterList)
        {
            int soliderHPSum = 0;
            foreach (SoldierController solider in chara.soliderList)
            {
                soliderHPSum += solider.hp;
            }
            if (soliderHPSum < attackSoliderHPSum)
            {
                attackableCharacterList.Remove(chara);
            }
        }
        //�N�U�����������L����������ꍇ�͂��̃L�������o��������
        if (attackableCharacterList.Count != 0)
        {
            foreach (CharacterController chara in attackableCharacterList)
            {
                Debug.Log(chara.name);
            }
            System.Random random3 = new System.Random();
            CharacterController randomDefenceCharacter = attackableCharacterList[random3.Next(attackableCharacterList.Count)];
            defenderCharacter = randomDefenceCharacter;
            Debug.Log(defenderCharacter.name);
        }
        //�N�U�����������L���������Ȃ��ꍇ�͈�ԋ����L�������o��������
        else
        {
            attackableCharacterList = territoryManager.territory.influence.characterList.FindAll(character => character.isAttackable);
            //��ԋ����L������������
            System.Random random4 = new System.Random();
            CharacterController randomDefenceCharacter = attackableCharacterList[random4.Next(attackableCharacterList.Count)];
            CharacterController strongestCharacter = randomDefenceCharacter;

            int soliderStrongestHPSum = 0;

            foreach (CharacterController chara in attackableCharacterList)
            {
                int soliderHPSum = 0;
                foreach (SoldierController solider in chara.soliderList)
                {
                    soliderHPSum += solider.hp;
                    if (soliderHPSum > soliderStrongestHPSum)
                    {
                        strongestCharacter = chara;
                        soliderStrongestHPSum = soliderHPSum;
                    }
                }

            }
            defenderCharacter = strongestCharacter;
            Debug.Log(defenderCharacter.name);
        }
        return defenderCharacter;
    }

    IEnumerator ShowAttackedTerritory(CharacterController attackCharacter)
    {
        mapField.SetActive(true);
        cursor.gameObject.SetActive(true);

        // �J�[�\���̈ʒu��ݒ�
        RectTransform territoryRectTransform = territoryManager.territory.GetComponent<RectTransform>();
        cursor.SetPosition(territoryRectTransform);

        //cursor.transform.position = territoryManager.territory.position;
        StartCoroutine(BlinkCursor(2));
        characterDetailUI.ShowCharacterDetailUI(attackCharacter);
        yield return new WaitForSeconds(2);
    }

    // �J�[�\���_�ł̃R���[�`��
    public IEnumerator BlinkCursor(float blinkTime)
    {
        float addTime = 0f;
        while (addTime < blinkTime)
        {
            cursor.gameObject.SetActive(!cursor.gameObject.activeSelf); // �J�[�\���̕\���E��\����؂�ւ���
            yield return new WaitForSeconds(0.25f);
            addTime += 0.25f;
        }
    }

    public IEnumerator BlinkTerritory(float blinkTime, CharacterController attackerChara, CharacterController defenderChara, Territory territory)
    {
        float addTime = 0f;
        //SpriteRenderer territorySpriteRenderer = territory.GetComponent<SpriteRenderer>();
        Image territoryImage = territory.GetComponent<Image>();

        while (addTime < blinkTime)
        {
            // �̓y�ɍU�����͂̉摜��ݒ�
            territoryImage.sprite = attackerChara.influence.influenceImage;
            yield return new WaitForSeconds(0.1f);
            addTime += 0.1f;

            //territorySpriteRenderer.sprite = attackerChara.influence.influenceImage;
            //yield return new WaitForSeconds(0.1f);
            //addTime += 0.1f;

            // �̓y�ɖh�䐨�͂̉摜��ݒ�
            territoryImage.sprite = defenderChara.influence.influenceImage;
            yield return new WaitForSeconds(0.1f);
            addTime += 0.1f;

            //territorySpriteRenderer.sprite = defenderChara.influence.influenceImage;
            //yield return new WaitForSeconds(0.1f);
            //addTime += 0.1f;
        }

        //�ŏI�I�ɍU�������͂̉摜��ݒ�
        territoryImage.sprite = attackerChara.influence.influenceImage;
        yield return new WaitForSeconds(0.5f);
    }

    public void NextCharacterBattlePhase(CharacterController character)
    {
        Debug.Log("NextCharacterBattlePhase");
        //�L�����N�^�[���X�g����Ō�̃L�����N�^�[���擾
        CharacterController lastCharacter = characterList.LastOrDefault();

        //�Ō�̃^�[���̃L�����N�^�[�ł͂Ȃ��ꍇ
        if (character != lastCharacter)
        {
            // �L�����N�^�[���X�g��������œn���ꂽcharacter�̃C���f�b�N�X���擾
            int currentIndex = characterList.IndexOf(character);
            // ���̃L�����N�^�[�̃C���f�b�N�X���v�Z
            int nextIndex = (currentIndex + 1) % characterList.Count;
            // ���̃L�����N�^�[���擾
            CharacterController nextCharacter = characterList[nextIndex];
            if (nextCharacter == playerCharacter)
            {
                PlayerBattlePhase();
            }
            else
            {
                OtherBattlePhase(nextCharacter);
            }
        }
        //�Ō�̃^�[���̃L�����N�^�[�̏ꍇ
        else
        {
            Debug.Log("�Ō�̃L�����N�^�[�^�[�����I�����܂����B");
            if (uniteCountryFlag == true)
            {
                //���ꏈ��
                ShowUniteUI(uniteInfluence);
            }
            else
            {
                phase = Phase.SetupPhase;
                PhaseCalc();
            }
        }
    }

    private Territory GetRandomTerritory(List<Territory> territoryList)
    {
        if (territoryList.Count == 0)
        {
            return null;
        }
        // ���X�g���烉���_���ɃC���f�b�N�X��I��
        int randomIndex = Random.Range(0, territoryList.Count);

        // �I�����ꂽ�����_���ȗ̓y��Ԃ�
        return territoryList[randomIndex];
    }

    public IEnumerator NoneRandomCharacterAddInfluence(CharacterController lordCharacter)
    {
        //None�ɏ�������L�����N�^�[���X�g�̎擾
        //noneInfluenceCharacters = noneInfluence.characterList;

        //�������̃����_���ȃL�����N�^�[���擾
        System.Random random = new System.Random();
        CharacterController randomNoneCharacter = noneInfluence.characterList[random.Next(noneInfluence.characterList.Count)];

        if (randomNoneCharacter == playerCharacter)
        {
            characterTurnUI.HideCharacterTurnUI();
            influenceUI.ShowInfluenceUI(lordCharacter.influence);
            
            yesNoUI.ShowEmployedYesNoUI(lordCharacter);
            //yesNoUI����\���ɂȂ�܂őҋ@
            yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());

            if (yesNoUI.IsYes())
            {
                //�v���C���[�L�����N�^�[�𐨗͂֏���
                randomNoneCharacter.influence.RemoveCharacter(randomNoneCharacter);
                lordCharacter.influence.AddCharacter(randomNoneCharacter);
                lordCharacter.gold -= 9;

                dialogueUI.ShowEmployedUI(lordCharacter);
                yield return new WaitUntil(() => !dialogueUI.IsDialogueVisible());
                influenceUI.HideInfluenceUI();
            }
        }
        else
        {
            //�����_���L�����N�^�[�𐨗͂֏���
            randomNoneCharacter.influence.RemoveCharacter(randomNoneCharacter);
            lordCharacter.influence.AddCharacter(randomNoneCharacter);
            lordCharacter.gold -= 9;
        }

        // �������I���������Ƃ��Ăяo�����ɒʒm����
        yield return null;
        //None�ɏ�������L�����N�^�[���X�g�̍X�V
        //noneInfluenceCharacters = noneInfluence.characterList;
    }

    public void LeaveInfluence(CharacterController leaveCharacter)
    {
        //�Ǖ�����L�����N�^�[��艺�̐g���̃L�������擾 
        List<CharacterController> lowerRankCharacters = leaveCharacter.influence.characterList.FindAll(c => (int)c.rank < (int)leaveCharacter.rank);

        //�L�����N�^�[��Ǖ�
        leaveCharacter.influence.RemoveCharacter(leaveCharacter);

        if (lowerRankCharacters != null)
        {
            //�Ǖ������L�����N�^�[��艺�̐g���̃L���������i�@����%�A�g�����v�Z
            foreach (CharacterController lowerRankCharacter in lowerRankCharacters)
            {
                lowerRankCharacter.rank = (Rank)((int)lowerRankCharacter.rank + 1);
                lowerRankCharacter.CalcSalary();
                lowerRankCharacter.CalcLoyalty();
            }
        }
            
        //�Ǖ������L�����N�^�[�𖳏����ɉ���
        noneInfluence.AddCharacter(leaveCharacter);
    }

    private void ShuffleCharacterList()
    {
        System.Random random = new System.Random();
        characterList = characterList.OrderBy(x => random.Next()).ToList();
    }

    public CharacterController GetNextCharacter(CharacterController currentCharacter)
    {
        int currentIndex = characterList.IndexOf(currentCharacter);
        int nextIndex = (currentIndex + 1) % characterList.Count;
        return characterList[nextIndex];
    }

    public void ShowFadeUI()
    {
        mapField.SetActive(true);
        detailsUI.ShowDetailUI();
    }

    public void HideFadeUI()
    {
        mapField.SetActive(false);
        cursor.gameObject.SetActive(false);
        detailsUI.HideDetailUI();
    }

    public void ShowLordUI(CharacterController playerCharacter)
    {
        titleFieldUI.ShowPlayerLordPhase();
        characterMenuUI.ShowCharacterMenuUI(playerCharacter, playerCharacter.influence);
        characterDetailUI.ShowCharacterDetailUI(playerCharacter);
    }

    public void ShowPersonalUI(CharacterController playerCharacter)
    {
        titleFieldUI.ShowPlayerPersonalPhase();
        personalMenuUI.ShowPersonalMenuUI(playerCharacter);
        influenceUI.ShowInfluenceUI(playerCharacter.influence);
    }

    public void ShowBattleUI(CharacterController playerCharacter)
    {
        titleFieldUI.ShowPlayerBattlePhase();
        battleMenuUI.ShowBattleMenuUI(playerCharacter, playerCharacter.influence);
        characterDetailUI.ShowCharacterDetailUI(playerCharacter);
    }

    void ShowUniteUI(Influence influence)
    {
        TitleFieldUI.instance.titleFieldText.text = "      ���ꂳ��܂���!!";
        mapField.SetActive(true);
        influenceUI.ShowInfluenceUI(influence);
    }
    
    public void MouseDown1ToBack()
    {
        if (phase == Phase.CharacterChoicePhase)
        {
            if (Input.GetMouseButtonDown(1) && characterIndexMenu.gameObject.activeSelf)
            {
                SoundManager.instance.PlayCancelSE();
                characterIndexMenu.gameObject.SetActive(false);
                characterIndexUI.HideCharacterIndexUI();
                characterDetailUI.gameObject.SetActive(false);

                mapField.gameObject.SetActive(true);
            }
        }

        if (phase == Phase.PlayerLordPhase)
        {
            if (Input.GetMouseButtonDown(1) && mapField.gameObject.activeSelf)
            {
                SoundManager.instance.PlayCancelSE();
                mapField.gameObject.SetActive(false);
                cursor.gameObject.SetActive(false);
                influenceOnMapUI.HideInfluenceOnMapUI();
                ShowLordUI(playerCharacter);
            }
            if (Input.GetMouseButtonDown(1) && characterIndexMenu.gameObject.activeSelf)
            {
                SoundManager.instance.PlayCancelSE();
                characterIndexMenu.gameObject.SetActive(false);
                characterIndexUI.HideCharacterIndexUI();
                characterDetailUI.gameObject.SetActive(false);

                if (step == Step.Search || step == Step.Appointment || step == Step.Banishment)
                {
                    ShowLordUI(playerCharacter);
                }
                else
                {
                    mapField.gameObject.SetActive(true);
                }
            }
        }

        if (phase == Phase.PlayerPersonalPhase)
        {
            if (Input.GetMouseButtonDown(1) && mapField.gameObject.activeSelf)
            {
                SoundManager.instance.PlayCancelSE();
                mapField.gameObject.SetActive(false);
                cursor.gameObject.SetActive(false);
                influenceOnMapUI.HideInfluenceOnMapUI();
                ShowPersonalUI(playerCharacter);
            }
            if (Input.GetMouseButtonDown(1) && characterIndexMenu.gameObject.activeSelf)
            {
                SoundManager.instance.PlayCancelSE();
                characterIndexMenu.gameObject.SetActive(false);
                characterIndexUI.HideCharacterIndexUI();
                characterDetailUI.gameObject.SetActive(false);

                mapField.gameObject.SetActive(true);
            }
        }

        if (phase == Phase.PlayerBattlePhase)
        {
            if (Input.GetMouseButtonDown(1) && mapField.gameObject.activeSelf)
            {
                SoundManager.instance.PlayCancelSE();
                mapField.gameObject.SetActive(false);
                cursor.gameObject.SetActive(false);
                influenceOnMapUI.HideInfluenceOnMapUI();
                ShowBattleUI(playerCharacter);
            }
            if (Input.GetMouseButtonDown(1) && characterIndexMenu.gameObject.activeSelf)
            {
                SoundManager.instance.PlayCancelSE();
                characterIndexMenu.gameObject.SetActive(false);
                characterIndexUI.HideCharacterIndexUI();
                characterDetailUI.gameObject.SetActive(false);

                mapField.gameObject.SetActive(true);
            }
        }
    }
}
