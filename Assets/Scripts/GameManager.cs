using System.Collections;
using System.Collections.Generic;
//using UnityEditor.UIElements;
using UnityEngine;
using System;
using UnityEngine.TextCore.Text;
//using UnityEditor.Build.Reporting;
using System.Runtime.CompilerServices;
using System.Linq;
using Random = UnityEngine.Random;
using Unity.VisualScripting;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
//using static UnityEditor.PlayerSettings;
using UnityEngine.UI;
//using UnityEngine.WSA;

public class GameManager : MonoBehaviour
{
    [SerializeField] LoadingUI loadingUI;
    [SerializeField] Cursor cursor;
    [SerializeField] VSImageUI vsImageUI;
    [SerializeField] CharacterController characterPrefab;
    [SerializeField] SoliderController soliderPrefab;

    public TerritoryGenerator territoryGenerator;
    
    [SerializeField] TitleFieldUI titleFieldUI;
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] YesNoUI yesNoUI;

    [SerializeField] BattleManager battleManager;
    //[SerializeField] InflueneceManager influenceManager;
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
    [SerializeField] CharacterSearchUI characterSearchUI;
    [SerializeField] DetailUI detailsUI;
    [SerializeField] CharacterDetailUI characterDetailUI;
    [SerializeField] InfluenceUI influenceUI;
    [SerializeField] InfluenceOnMapUI influenceOnMapUI;
    [SerializeField] BattleUI battleUI;
    [SerializeField] BattleDetailUI battleDetailUI;
    [SerializeField] GameObject mapField;
    [SerializeField] GameObject characterIndexMenu;
    [SerializeField] GameObject characterSearchMenu;

    public List<CharacterController> characterList;
    public CharacterController playerCharacter;

    // �L�����N�^�[���ƂɈقȂ镺�m���X�g�������߂̎������쐬
    //private Dictionary<CharacterController, List<SoliderController>> characterSoliderDict = new Dictionary<CharacterController, List<SoliderController>>();

    public List<Influence> influenceList;
    public List<Territory> allTerritoryList;

    //public Influence greenInfluence;
    //public Influence blueInfluence;
    //public Influence redInfluence;
    //public Influence yellowInfluence;
    public Influence noneInfluence;

    //private List<CharacterController> noneInfluenceCharacters;

    public CharacterController battleTurnCharacter;
    private CharacterController defenceChara;

    public bool defenceFlag;
    public int turnCount;

    public int territoryCouont;
    public Influence uniteInfluence;
    public bool uniteCountryFlag = false;

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

    //�V���O���g�����i�ǂ�����ł��A�N�Z�X�ł���悤�ɂ���j
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    [SerializeField] private Territory plainTerritorPrefab;
    private void Start()
    {
        StartGame();
        //Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
        //Vector3 worldCenter = Camera.main.ScreenToWorldPoint(screenCenter);
        //worldCenter.z = 0f;  // 2D�Q�[���̏ꍇ��Z����0�ɂ���

        //Instantiate(plainTerritorPrefab);
    }

    private void Update()
    {
        if (!yesNoUI.gameObject.activeSelf && !dialogueUI.gameObject.activeSelf)
        {
            MouseDown1ToBack();
        }
    }

    void StartGame()
    {
        // ���͂ɑΉ�����摜�t�@�C����
        string blackFlagPath = "Flags/BlackFlag";
        string blueFlagPath = "Flags/BlueFlag";
        string pinkFlagPath = "Flags/PinkFlag";
        string purpleFlagPath = "Flags/PurpleFlag";
        string yellowFlagPath = "Flags/YellowFlag";
        string GreyFlagPath = "Flags/GreyFlag";

        // Resources.Load���g�p���ăX�v���C�g��ǂݍ���
        Sprite blackInfluenceSprite = Resources.Load<Sprite>(blackFlagPath);
        Sprite blueInfluenceSprite = Resources.Load<Sprite>(blueFlagPath);
        Sprite pinkInfluenceSprite = Resources.Load<Sprite>(pinkFlagPath);
        Sprite purplenfluenceSprite = Resources.Load<Sprite>(purpleFlagPath);
        Sprite yellowfluenceSprite = Resources.Load<Sprite>(yellowFlagPath);
        Sprite noneInfluenceSprite = Resources.Load<Sprite>(GreyFlagPath);

        // �L�����N�^�[�̐���
        //�̎�
        CharacterController serugius = CreateCharacter(1);
        CharacterController victor = CreateCharacter(2);
        CharacterController arisia = CreateCharacter(3);
        CharacterController rourenthius = CreateCharacter(4);
        CharacterController feodoora = CreateCharacter(25);
        //�̎�z��
        CharacterController eresuthia = CreateCharacter(5);
        CharacterController renius = CreateCharacter(6);
        CharacterController peruseus = CreateCharacter(7);
        CharacterController amerieru = CreateCharacter(8);
        CharacterController karisutaana = CreateCharacter(9);
        CharacterController mariseruda = CreateCharacter(10);
        CharacterController venethia = CreateCharacter(11);
        CharacterController siguma = CreateCharacter(12);
        CharacterController jovannni = CreateCharacter(26);
        CharacterController simon = CreateCharacter(27);
        //������
        CharacterController sofuronia = CreateCharacter(13);
        CharacterController ferisithi = CreateCharacter(14);
        CharacterController rainnharuto = CreateCharacter(15);
        CharacterController ferics = CreateCharacter(16);
        CharacterController veronica = CreateCharacter(17);
        CharacterController reo = CreateCharacter(18);
        CharacterController marukus = CreateCharacter(19);
        CharacterController reira = CreateCharacter(20);
        CharacterController marissa = CreateCharacter(21);
        CharacterController nataasya = CreateCharacter(22);
        CharacterController iruma = CreateCharacter(23);
        CharacterController riisya = CreateCharacter(24);
        CharacterController oriannna = CreateCharacter(29);
        CharacterController garahaddo = CreateCharacter(30);
        CharacterController aressandoro = CreateCharacter(31);
        CharacterController akuserion = CreateCharacter(28);
        CharacterController dhionyusios = CreateCharacter(32);
        CharacterController tadeus = CreateCharacter(33);
        CharacterController siruvietto = CreateCharacter(34);
        CharacterController ruben = CreateCharacter(35);

        //�̎僊�X�g�̎擾
        List<CharacterController> lordCharacterList = characterList.FindAll(character => character.characterModel.isLord);

        //���͂̍쐬
        foreach (CharacterController character in lordCharacterList)
        {
            if (character == victor)
            {
                CreateInfluence(character, blackInfluenceSprite);
            }
            else if (character == serugius)
            {
                CreateInfluence(character, blueInfluenceSprite);
            }
            else if (character == arisia)
            {
                CreateInfluence(character, pinkInfluenceSprite);
            }
            else if (character == rourenthius)
            {
                CreateInfluence(character, purplenfluenceSprite);
            }
            else if (character == feodoora)
            {
                CreateInfluence(character, yellowfluenceSprite);
            }
        }
        //���������͂̍쐬
        noneInfluence = new GameObject("NoneInfluence").AddComponent<Influence>();
        noneInfluence.Init("NoneInfluence", noneInfluenceSprite);
        influenceList.Add(noneInfluence);
        // NoneInfluenceCharacters�̏�����
        //noneInfluenceCharacters = noneInfluence.characterList;

        //�L�����N�^�[�𐨗͂֏���������
        foreach (Influence influence in influenceList)
        {
            if (influence.influenceName == "�Z���M�E�X")
            {
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
            else if (influence.influenceName == "NoneInfluence")
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

        //Int�^�̕��m���X�g
        List<int> soliderIntList =  new List<int>() { 4, 4, 4, 4, 4, 3, 3, 3, 3, 3 };
        List<int> soliderIntList2 = new List<int>() { 3, 3, 3, 3, 3, 2, 2, 2, 2, 2 };
        List<int> soliderIntList3 = new List<int>() { 2, 2, 2, 2, 2, 1, 1, 1, 1, 1 };
        List<int> soliderIntList4 = new List<int>() { 1, 1, 1, 1, 1 };

        //�L�����N�^�[���ɕ��m������������
        foreach (CharacterController character in characterList)
        {
            if (character == serugius || character == victor || character == arisia || character == rourenthius || character == feodoora)
            {
                AssignSoldierListToCharacter(character, soliderIntList);
            }
            else if(character == eresuthia || character == peruseus || character == karisutaana || character == venethia || character == jovannni)
            {
                AssignSoldierListToCharacter(character, soliderIntList2);
            }
            else if (character == renius || character == amerieru || character == mariseruda || character == siguma || character == simon)
            {
                AssignSoldierListToCharacter(character, soliderIntList3);
            }
            else
            {
                AssignSoldierListToCharacter(character, soliderIntList4);
            }
        }

        allTerritoryList = territoryGenerator.Generate(influenceList);

        phase = Phase.CharacterChoicePhase;
        PhaseCalc();
    }

    public CharacterController CreateCharacter(int characterId)
    {
        // �L�����N�^�[�̐���
        CharacterController newCharacter = Instantiate(characterPrefab);
        newCharacter.gameObject.SetActive(false);
        newCharacter.Init(characterId);
        //newCharacter.characterModel.rank = rank;
        //���������L�����N�^�[���L�����N�^�[���X�g�ɒǉ�
        characterList.Add(newCharacter);
        return newCharacter;
    }

    public Influence CreateInfluence(CharacterController character, Sprite influenceImage)
    {
        Influence newInfluence = new GameObject(character.characterModel.name).AddComponent<Influence>();
        newInfluence.Init(character.characterModel.name, influenceImage);
        influenceList.Add(newInfluence);
        return newInfluence;
    }

    public void AssignSoldierListToCharacter(CharacterController character, List<int> soldierIDList)
    {
        // �L�����N�^�[�ɑΉ����镺�m���X�g���쐬
        List<SoliderController> characterSoldierList = new List<SoliderController>();

        // �Ή����镺�mID�ŕ��m�����������ă��X�g�ɒǉ�
        foreach (int soldierID in soldierIDList)
        {
            SoliderController soldier = Instantiate(soliderPrefab);
            soldier.Init(soldierID);
            soldier.gameObject.SetActive(false);
            characterSoldierList.Add(soldier);
        }
        // ���m���X�g���L�����N�^�[�ɐݒ�
        character.soliderList = characterSoldierList;
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

            //case Phase.PlayerBattlePhase:
            //    PlayerBattlePhase();
            //    break;

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
        //StartCoroutine(loadingUI.MoveSlider());
        
        //yield return StartCoroutine(uiFade.Fade(true));
        //yield return StartCoroutine(uiFade.Fade(false));

        turnCount++;

        //�S�ẴL�����N�^�[���U���\�ɐݒ�
        foreach (CharacterController character in characterList)
        {
            character.characterModel.isAttackable = true;
        }

        //�S�ẴL�����N�^�[�̃o�g���t���O��false�ɐݒ�
        foreach (CharacterController character in characterList)
        {
            character.characterModel.isBattle = false;
        }

        //�S�Ă̕��m��HP����
        foreach (CharacterController character in characterList)
        {
            foreach (SoliderController solider in character.soliderList)
            {
                solider.soliderModel.hp = solider.soliderModel.maxHP;
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
                    character.characterModel.gold += Mathf.RoundToInt(influenceIncome * character.characterModel.salary / 100.0f);
                }
            }
            else
            {
                foreach (CharacterController character in influence.characterList)
                {
                    character.characterModel.gold += Mathf.RoundToInt(character.characterModel.tact / 100.0f * 10.0f);
                }
            }
        }

        phase = Phase.PlayerLordPhase;
        PhaseCalc();
    }

    void PlayerLordPhase()
    {
        if (playerCharacter.characterModel.isLord == true)
        {
            Debug.Log("�v���C���[�̎�t�F�[�Y�ł��B");

            titleFieldUI.ShowChangeLordTurnText();
            ShowLordUI(playerCharacter);
        }
        else
        {
            GameManager.instance.step = Step.End;

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
        List<CharacterController> lordCharacterList = characterList.FindAll(character => character.characterModel.isLord && !character.characterModel.isPlayerCharacter);

        //�e�̎�^�[��
        foreach (CharacterController lordCharacter in lordCharacterList)
        {
            characterTurnUI.ShowCharacterTurnUI(lordCharacter);
            //�L�����N�^�[�̓o�p
            switch (lordCharacter.influence.territoryList.Count)
            {
                case 1:
                case 2:
                    while (lordCharacter.influence.characterList.Count <= 2 && lordCharacter.characterModel.gold >= 9 && noneInfluence.characterList.Count != 0)
                    {
                        yield return StartCoroutine(NoneRandomCharacterAddInfluence(lordCharacter));
                    }
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                    while (lordCharacter.influence.characterList.Count <= 3 && lordCharacter.characterModel.gold >= 9 && noneInfluence.characterList.Count != 0)
                    {
                        yield return StartCoroutine(NoneRandomCharacterAddInfluence(lordCharacter));
                    }
                    break;
                case 7:
                case 8:  
                case 9:
                case 10:
                    while (lordCharacter.influence.characterList.Count <= 4 && lordCharacter.characterModel.gold >= 9 && noneInfluence.characterList.Count != 0)
                    {
                        yield return StartCoroutine(NoneRandomCharacterAddInfluence(lordCharacter));
                    }
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                    while (lordCharacter.influence.characterList.Count <= 5 && lordCharacter.characterModel.gold >= 9 && noneInfluence.characterList.Count != 0)
                    {
                        yield return StartCoroutine(NoneRandomCharacterAddInfluence(lordCharacter));
                    }
                    break;
            }
            int beforeRank = (int)playerCharacter.characterModel.rank;
            //�����̍������ɐg����ݒ�
            lordCharacter.influence.SetRankByFame();
            int afterRank = (int)playerCharacter.characterModel.rank;

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
        List<CharacterController> otherCharacterList = characterList.FindAll(x => !x.characterModel.isPlayerCharacter);
        
        foreach (CharacterController otherCharacter in otherCharacterList)
        {
            characterTurnUI.ShowCharacterTurnUI(otherCharacter);

            //�����̒l�ɉ����ď������͂�����
            if (otherCharacter.characterModel.isLord == false && otherCharacter.influence != noneInfluence)
            {
                int leaveProbability = 0;
                leaveProbability = 99 - otherCharacter.characterModel.loyalty;
                // 0����99�̊Ԃ̃����_���Ȓl�𐶐�����
                int randomValue = Random.Range(0, 100);
                // �����_���Ȓl������̊m���ȉ��ł���Ώ������͂�����
                if (randomValue < leaveProbability)
                {
                    // �������͂����鏈���������ɏ���
                    Debug.Log(otherCharacter.characterModel.name + "�͏������͂�����܂�");
                    if (otherCharacter.influence == playerCharacter.influence)
                    {
                        if (playerCharacter.characterModel.isLord == true)
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
            while (otherCharacter.soliderList.Count < 10 && otherCharacter.characterModel.gold >= 2)
            {
                SoliderController solider = Instantiate(soliderPrefab);
                solider.Init(1);
                solider.gameObject.SetActive(false);
                otherCharacter.soliderList.Add(solider);
                otherCharacter.characterModel.gold -= 2;
            }
            //���m�P��
            //�̎�̏ꍇ
            if (otherCharacter.characterModel.isLord == true)
            {
                while (otherCharacter.characterModel.gold >= 15)
                {
                    foreach (SoliderController solider in otherCharacter.soliderList)
                    {
                        solider.soliderModel.Training(solider);
                    }
                    otherCharacter.characterModel.gold -= 2;
                }
            }
            //�̎�ȊO�̏ꍇ
            else
            {
                while (otherCharacter.characterModel.gold >= 7)
                {
                    foreach (SoliderController solider in otherCharacter.soliderList)
                    {
                        solider.soliderModel.Training(solider);
                    }
                    otherCharacter.characterModel.gold -= 2;
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
        Debug.Log(character.characterModel.name + "�̃^�[���ł��B");
        //�N�U���邩���f
        if (character.influence != noneInfluence && character.characterModel.isLord == true && uniteCountryFlag == false)
        {
            //�܂��퓬���Ă��Ȃ��L�����N�^�[�����擾
            int battlefalseCharacterCount = character.influence.characterList.Count(c => !c.characterModel.isBattle);
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
        while (randomTerritory.influence == character.influence || randomTerritory.influence == GameManager.instance.noneInfluence)
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
            foreach (SoliderController solider in chara.soliderList)
            {
                soliderHPSum += solider.soliderModel.hp;
            }
            if (soliderHPSum > soliderHPMax)
            {
                soliderHPMax = soliderHPSum;
            }
        }

        //�N�U������L���������߂�
            //�N�U����������L�������擾
        List<CharacterController> attackableCharacterList = character.influence.characterList.FindAll(character => character.characterModel.isAttackable);
        foreach (CharacterController chara in character.influence.characterList)
        {
            soliderHPSum = 0;
            foreach (SoliderController solider in chara.soliderList)
            {
                soliderHPSum += solider.soliderModel.hp;
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
            attackableCharacterList = character.influence.characterList.FindAll(character => character.characterModel.isAttackable);
            //��ԋ����L������������
            System.Random random4 = new System.Random();
            CharacterController randomDefenceCharacter = attackableCharacterList[random4.Next(attackableCharacterList.Count)];
            CharacterController strongestCharacter = randomDefenceCharacter;

            int soliderStrongestHPSum = 0;
            int soliderHPSum2 = 0;
            foreach (CharacterController chara in attackableCharacterList)
            {
                soliderHPSum2 = 0;
                foreach (SoliderController solider in chara.soliderList)
                {
                    soliderHPSum2 += solider.soliderModel.hp;
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
        if (territoryManager.territory.influence == playerCharacter.influence && playerCharacter.characterModel.isLord == true)
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
            bool canAttack = territoryManager.territory.influence.characterList.Exists(c => c.characterModel.isAttackable);
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
                    cursor.transform.position = territoryManager.territory.position;
                    vsImageUI.SetPosition(territoryManager.territory.transform as RectTransform);
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
                attackCharacter.characterModel.isAttackable = false;

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
        List<CharacterController> attackableCharacterList = territoryManager.territory.influence.characterList.FindAll(character => character.characterModel.isAttackable);
        foreach (CharacterController chara in territoryManager.territory.influence.characterList)
        {
            int soliderHPSum = 0;
            foreach (SoliderController solider in chara.soliderList)
            {
                soliderHPSum += solider.soliderModel.hp;
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
                Debug.Log(chara.characterModel.name);
            }
            System.Random random3 = new System.Random();
            CharacterController randomDefenceCharacter = attackableCharacterList[random3.Next(attackableCharacterList.Count)];
            defenderCharacter = randomDefenceCharacter;
            Debug.Log(defenderCharacter.characterModel.name);
        }
        //�N�U�����������L���������Ȃ��ꍇ�͈�ԋ����L�������o��������
        else
        {
            attackableCharacterList = territoryManager.territory.influence.characterList.FindAll(character => character.characterModel.isAttackable);
            //��ԋ����L������������
            System.Random random4 = new System.Random();
            CharacterController randomDefenceCharacter = attackableCharacterList[random4.Next(attackableCharacterList.Count)];
            CharacterController strongestCharacter = randomDefenceCharacter;

            int soliderStrongestHPSum = 0;

            foreach (CharacterController chara in attackableCharacterList)
            {
                int soliderHPSum = 0;
                foreach (SoliderController solider in chara.soliderList)
                {
                    soliderHPSum += solider.soliderModel.hp;
                    if (soliderHPSum > soliderStrongestHPSum)
                    {
                        strongestCharacter = chara;
                        soliderStrongestHPSum = soliderHPSum;
                    }
                }

            }
            defenderCharacter = strongestCharacter;
            Debug.Log(defenderCharacter.characterModel.name);
        }
        return defenderCharacter;
    }

    IEnumerator ShowAttackedTerritory(CharacterController attackCharacter)
    {
        mapField.SetActive(true);
        cursor.gameObject.SetActive(true);
        cursor.transform.position = territoryManager.territory.position;
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
                lordCharacter.characterModel.gold -= 9;

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
            lordCharacter.characterModel.gold -= 9;
        }

        // �������I���������Ƃ��Ăяo�����ɒʒm����
        yield return null;
        //None�ɏ�������L�����N�^�[���X�g�̍X�V
        //noneInfluenceCharacters = noneInfluence.characterList;
    }

    public void LeaveInfluence(CharacterController leaveCharacter)
    {
        //�Ǖ�����L�����N�^�[��艺�̐g���̃L�������擾 
        List<CharacterController> lowerRankCharacters = leaveCharacter.influence.characterList.FindAll(c => (int)c.characterModel.rank < (int)leaveCharacter.characterModel.rank);

        //�L�����N�^�[��Ǖ�
        leaveCharacter.influence.RemoveCharacter(leaveCharacter);

        if (lowerRankCharacters != null)
        {
            //�Ǖ������L�����N�^�[��艺�̐g���̃L���������i�@����%�A�g�����v�Z
            foreach (CharacterController lowerRankCharacter in lowerRankCharacters)
            {
                lowerRankCharacter.characterModel.rank = (Rank)((int)lowerRankCharacter.characterModel.rank + 1);
                lowerRankCharacter.CalcSalary();
                lowerRankCharacter.CalcLoyalty();
            }
        }
            
        //�Ǖ������L�����N�^�[�𖳏����ɉ���
        GameManager.instance.noneInfluence.AddCharacter(leaveCharacter);
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
