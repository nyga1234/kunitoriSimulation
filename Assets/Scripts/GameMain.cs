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
    public TerritoryGenerator territoryGenerator;

    public List<SoldierController> allSoliderList;
    public CharacterController playerCharacter;
    
    public List<Territory> allTerritoryList;
    public Influence noneInfluence;

    public CharacterController battleTurnCharacter;

    public bool defenceFlag;
    public int turnCount;
    public int territoryCouont;
    public Influence uniteInfluence; //統一した勢力
    public bool uniteCountryFlag = false;
    private int soliderUniqueId = 0;

    [SerializeField] Territory plainTerritorPrefab;
    [SerializeField] Transform parent;
    private List<Territory> initializeTerritoryList;

    //フェーズの管理
    public enum Phase
    {
        CharacterChoicePhase,//キャラ選択フェーズ
        SetupPhase,//セットアップフェーズ
        PlayerLordPhase,//プレイヤー領主フェーズ(プレイヤーが領主の場合のみ）
        OtherLordPhase,//他領主フェーズ
        PlayerPersonalPhase,//プレイヤー個人フェーズ
        OtherPersonalPhase,//他個人フェーズ
        BattlePhase,//バトルフェーズ
        PlayerBattlePhase,//プレイヤーバトルフェーズ
        OtherBattlePhase,//他バトルフェーズ
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

    //無所属勢力名
    private string noneInfluenceName = "NoneInfluence";

    // 勢力に対応する画像ファイル名
    private string blackFlagPath = "Flags/BlackFlag";
    private string blueFlagPath = "Flags/BlueFlag";
    private string pinkFlagPath = "Flags/PinkFlag";
    private string purpleFlagPath = "Flags/PurpleFlag";
    private string yellowFlagPath = "Flags/YellowFlag";
    private string greyFlagPath = "Flags/GreyFlag";

    // Resources.Loadを使用してスプライトを読み込む
    Sprite blackInfluenceSprite;
    Sprite blueInfluenceSprite;
    Sprite pinkInfluenceSprite;
    Sprite purplenfluenceSprite;
    Sprite yellowfluenceSprite;
    Sprite noneInfluenceSprite;

    private void Start()
    {
        Initialize();
        //StartGame();


        if (!SaveLoadManager.HasSaveData(0))
        {
            Debug.Log("ゲームを最初から開始します");
            StartGame();
        }
        else
        {
            Debug.Log("ゲームを途中から開始します");
            LoadGame(1);
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
        //foreach (CharacterController character in characterList)
        //{
        //    character.Initialize();
        //}

        //CharacterController serugius = characterList.Find(c => c.characterId == 1);
        //serugius.isLord = true;
        //CharacterController victor = characterList.Find(c => c.characterId == 2);
        //victor.isLord = true;
        //CharacterController arisia = characterList.Find(c => c.characterId == 3);
        //arisia.isLord = true;
        //CharacterController rourenthius = characterList.Find(c => c.characterId == 4);
        //rourenthius.isLord = true;
        //CharacterController feodoora = characterList.Find(c => c.characterId == 25);
        //feodoora.isLord = true;

        //foreach (Influence influence in influenceList)
        //{
        //    influence.Initialize();
        //}

        ////勢力の生成
        //// Resources.Loadを使用してスプライトを読み込む
        //blackInfluenceSprite = Resources.Load<Sprite>(blackFlagPath);
        //blueInfluenceSprite = Resources.Load<Sprite>(blueFlagPath);
        //pinkInfluenceSprite = Resources.Load<Sprite>(pinkFlagPath);
        //purplenfluenceSprite = Resources.Load<Sprite>(purpleFlagPath);
        //yellowfluenceSprite = Resources.Load<Sprite>(yellowFlagPath);
        //noneInfluenceSprite = Resources.Load<Sprite>(greyFlagPath);

        ////無所属勢力の作成
        //noneInfluence = new GameObject(noneInfluenceName).AddComponent<Influence>();
        //noneInfluence.Init(noneInfluenceName, noneInfluenceSprite);
        //influenceList.Add(noneInfluence);

        ////領主リストの取得
        //List<CharacterController> lordCharacterList = characterList.FindAll(character => character.isLord);

        ////領主に応じた勢力を作成
        //foreach (CharacterController character in lordCharacterList)
        //{
        //    if (character == victor)
        //    {
        //        CreateInfluence(character, blackInfluenceSprite);
        //    }
        //    else if (character == serugius)
        //    {
        //        CreateInfluence(character, blueInfluenceSprite);
        //    }
        //    else if (character == arisia)
        //    {
        //        CreateInfluence(character, pinkInfluenceSprite);
        //    }
        //    else if (character == rourenthius)
        //    {
        //        CreateInfluence(character, purplenfluenceSprite);
        //    }
        //    else if (character == feodoora)
        //    {
        //        CreateInfluence(character, yellowfluenceSprite);
        //    }
        //}

        initializeTerritoryList = territoryGenerator.InitializeTerritory();
        //territoryGenerator.GenerateTerritory(territoryGenerator.InitializeTerritory(), influenceList);
        //allTerritoryList = territoryGenerator.Generate(influenceList);
    }

    private void StartGame()
    {
        //allTerritoryList = territoryGenerator.GenerateTerritory(initializeTerritoryList, influenceList);

        //foreach (Influence influence in constParam.influenceList)
        //{
        //    Influence newInfluence = new Influence(influence);

        //    this.influenceList.Add(newInfluence);

        //    foreach (CharacterController character in influence.characterList)
        //    {
        //        CharacterController newCharacter = new CharacterController(character);

        //        this.characterList.Add(newCharacter);
        //    }
        //}

        

        

        //領主
        CharacterController serugius = characterList.Find(c => c.characterId == 1);
        CharacterController victor = characterList.Find(c => c.characterId == 2);
        CharacterController arisia = characterList.Find(c => c.characterId == 3);
        CharacterController rourenthius = characterList.Find(c => c.characterId == 4);
        CharacterController feodoora = characterList.Find(c => c.characterId == 25);
        //領主配下
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
        //無所属
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

        ////給料%と忠誠を計算
        //List<CharacterController> lordCharacterList = characterList.FindAll(character => character.influence.influenceName != "NoneInfluence");
        //foreach (CharacterController character in lordCharacterList)
        //{
        //    //キャラクターの給料%を設定
        //    character.CalcSalary();
        //    //キャラクターの忠誠を設定
        //    if (!character.isLord)
        //    {
        //        character.CalcLoyalty();
        //    }
        //}


        //キャラクターを勢力へ所属させる
        foreach (Influence influence in influenceList)
        {
            if (influence.influenceName == "セルギウス")
            {
                Debug.Log("キャラを勢力へ所属");
                influence.AddCharacter(serugius);
                influence.AddCharacter(eresuthia);
                influence.AddCharacter(renius);
            }
            else if (influence.influenceName == "ヴィクター")
            {
                influence.AddCharacter(victor);
                influence.AddCharacter(peruseus);
                influence.AddCharacter(amerieru);

            }
            else if (influence.influenceName == "アリシア")
            {
                influence.AddCharacter(arisia);
                influence.AddCharacter(karisutaana);
                influence.AddCharacter(mariseruda);
            }
            else if (influence.influenceName == "ローレンティウス")
            {
                influence.AddCharacter(rourenthius);
                influence.AddCharacter(venethia);
                influence.AddCharacter(siguma);
            }
            else if (influence.influenceName == "フェオドーラ")
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

        //Int型の兵士リスト
        List<int> soliderIntList =  new List<int>() { 4, 4, 4, 4, 4, 3, 3, 3, 3, 3 };
        List<int> soliderIntList2 = new List<int>() { 3, 3, 3, 3, 3, 2, 2, 2, 2, 2 };
        List<int> soliderIntList3 = new List<int>() { 2, 2, 2, 2, 2, 1, 1, 1, 1, 1 };
        List<int> soliderIntList4 = new List<int>() { 1, 1, 1, 1, 1 };

        ////キャラクター毎に兵士を所属させる
        //foreach (CharacterController character in characterList)
        //{
        //    if (character == serugius || character == victor || character == arisia || character == rourenthius || character == feodoora)
        //    {
        //        AssignSoldierListToCharacter(character, soliderIntList);
        //    }
        //    else if(character == eresuthia || character == peruseus || character == karisutaana || character == venethia || character == jovannni)
        //    {
        //        AssignSoldierListToCharacter(character, soliderIntList2);
        //    }
        //    else if (character == renius || character == amerieru || character == mariseruda || character == siguma || character == simon)
        //    {
        //        AssignSoldierListToCharacter(character, soliderIntList3);
        //    }
        //    else
        //    {
        //        AssignSoldierListToCharacter(character, soliderIntList4);
        //    }
        //}

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

        //allTerritoryList = territoryGenerator.Generate(influenceList);
        allTerritoryList = territoryGenerator.GenerateTerritory(initializeTerritoryList, influenceList);

        phase = Phase.CharacterChoicePhase;
        PhaseCalc();
    }

    //public Influence CreateInfluence(CharacterController character, Sprite influenceImage)
    //{
    //    Influence newInfluence = new GameObject(character.name).AddComponent<Influence>();
    //    newInfluence.Init(character.name, influenceImage);
    //    influenceList.Add(newInfluence);
    //    newInfluence.transform.SetParent(Influence, false);
    //    return newInfluence;
    //}

    public void AssignSoldierListToCharacter(CharacterController character, List<int> soldierIDList)
    {
        //character.soliderList.Clear();
        // 対応する兵士IDで兵士を初期化してリストに追加
        foreach (int soldierID in soldierIDList)
        {
            //SoliderController soldier = Instantiate(soliderPrefab, Solider);
            //soldier.Init(soldierID, CreateSoliderUniqueID());
            //soldier.gameObject.SetActive(false);
            //character.soliderList.Add(soldier);
            //allSoliderList.Add(soldier);
        }
    }

    public int CreateSoliderUniqueID()
    {
        return soliderUniqueId++;
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
        //StartCoroutine(loadingUI.MoveSlider());
        
        //yield return StartCoroutine(uiFade.Fade(true));
        //yield return StartCoroutine(uiFade.Fade(false));

        turnCount++;

        //全てのキャラクターを攻撃可能に設定
        foreach (CharacterController character in characterList)
        {
            character.isAttackable = true;
        }

        //全てのキャラクターのバトルフラグをfalseに設定
        foreach (CharacterController character in characterList)
        {
            character.isBattle = false;
        }

        //全ての兵士のHPを回復
        foreach (CharacterController character in characterList)
        {
            foreach (SoldierController solider in character.soliderList)
            {
                solider.hp = solider.maxHP;
            }
        }

        //名声に応じた身分の変更

        //領土数に応じた収入をキャラクターに分配
        foreach (Influence influence in influenceList)
        {
            if (influence != noneInfluence)
            {
                int territoryIncome = 30;
                int influenceIncome = 0;//値を初期化
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
            Debug.Log("プレイヤー領主フェーズです。");

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
        Debug.Log("他キャラクターの領主フェーズです。");

        titleFieldUI.ShowChangeLordTurnText();

        mapField.SetActive(true);
        //他キャラクターの領主リストの取得
        List<CharacterController> lordCharacterList = characterList.FindAll(character => character.isLord && !character.isPlayerCharacter);

        //各領主ターン
        foreach (CharacterController lordCharacter in lordCharacterList)
        {
            characterTurnUI.ShowCharacterTurnUI(lordCharacter);
            //キャラクターの登用
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
            //名声の高い順に身分を設定
            lordCharacter.influence.SetRankByFame();
            int afterRank = (int)playerCharacter.rank;

            if (afterRank > beforeRank)
            {
                Debug.Log("昇格しました");
                dialogueUI.ShowElavationRankUI(playerCharacter);
                yield return new WaitUntil(() => !dialogueUI.IsDialogueVisible());
            }
            else if (afterRank < beforeRank)
            {
                Debug.Log("降格しました");
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
        Debug.Log("プレイヤー個人フェーズです。");
        titleFieldUI.ShowChangePersonalTurnText();
        ShowPersonalUI(playerCharacter);
    }

    IEnumerator OtherPersonalPhase()
    {
        Debug.Log("他キャラクターの個人フェーズです。");

        titleFieldUI.ShowChangePersonalTurnText();

        mapField.SetActive(true);
        List<CharacterController> otherCharacterList = characterList.FindAll(x => !x.isPlayerCharacter);
        
        foreach (CharacterController otherCharacter in otherCharacterList)
        {
            characterTurnUI.ShowCharacterTurnUI(otherCharacter);

            //忠誠の値に応じて所属勢力を去る
            if (otherCharacter.isLord == false && otherCharacter.influence != noneInfluence)
            {
                int leaveProbability = 0;
                leaveProbability = 99 - otherCharacter.loyalty;
                // 0から99の間のランダムな値を生成する
                int randomValue = Random.Range(0, 100);
                // ランダムな値が所定の確率以下であれば所属勢力を去る
                if (randomValue < leaveProbability)
                {
                    // 所属勢力を去る処理をここに書く
                    Debug.Log(otherCharacter.name + "は所属勢力を去ります");
                    if (otherCharacter.influence == playerCharacter.influence)
                    {
                        if (playerCharacter.isLord == true)
                        {
                            dialogueUI.ShowLeaveInfluenceUI(otherCharacter);
                        }
                        // ダイアログが閉じられるまで待機
                        yield return new WaitUntil(() => !dialogueUI.IsDialogueVisible());
                    }
                    LeaveInfluence(otherCharacter);
                }
            }

            //兵士雇用
            while (otherCharacter.soliderList.Count < 10 && otherCharacter.gold >= 2)
            {
                otherCharacter.soliderList.Add(Instantiate(constParam.soldierList.Find(c => c.soliderID == 1)));
                otherCharacter.gold -= 2;
                //SoliderController solider = Instantiate(soliderPrefab);
                //solider.Init(1, CreateSoliderUniqueID());
                //solider.gameObject.SetActive(false);
                //otherCharacter.soliderList.Add(solider);
                //allSoliderList.Add(solider);
                //otherCharacter.gold -= 2;
            }
            //兵士訓練
            //領主の場合
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
            //領主以外の場合
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
        Debug.Log("バトルフェーズです。");
        titleFieldUI.ShowChangeBattleTurnText();
        //キャラクターリストをシャッフル
        ShuffleCharacterList();

        //characterListの最初の要素を取得
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
        Debug.Log("プレイヤーバトルフェーズです。");
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
        Debug.Log(character.name + "のターンです。");
        //侵攻するか判断
        if (character.influence != noneInfluence && character.isLord == true && uniteCountryFlag == false)
        {
            //まだ戦闘していないキャラクター数を取得
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
      //侵攻する領土を決める
                //characterの勢力に隣接するランダムな領土を侵攻対象とする
        List<Territory> adjacentTerritory = character.influence.FindAdjacentTerritory();
        Territory randomTerritory = GetRandomTerritory(adjacentTerritory);
        while (randomTerritory.influence == character.influence || randomTerritory.influence == noneInfluence)
        {
            randomTerritory = GetRandomTerritory(adjacentTerritory);
        }
        territoryManager.territory = randomTerritory;
        territoryManager.influence = randomTerritory.influence;
        Debug.Log("侵攻する領土は" + randomTerritory.position);

        //侵攻先の部隊を確認する
            //侵攻先で一番強いキャラ(戦力)を取得
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

        //侵攻させるキャラを決める
            //侵攻先よりも強いキャラを取得
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
            //侵攻先よりも強いキャラがいる場合はそのキャラを出撃させる
        if (attackableCharacterList.Count != 0)
        {
            System.Random random3 = new System.Random();
            CharacterController randomAttackCharacter = attackableCharacterList[random3.Next(attackableCharacterList.Count)];
            StopAllCoroutines();
            StartCoroutine(BattlePrepare(randomAttackCharacter, soliderHPMax));
        }
        //相手よりも強いキャラがいない場合
        else
        {
            attackableCharacterList = character.influence.characterList.FindAll(character => character.isAttackable);
            //一番強いキャラを初期化
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

        //侵攻された領土がプレイヤー勢力　かつ　プレイヤーが領主の場合
        if (territoryManager.territory.influence == playerCharacter.influence && playerCharacter.isLord == true)
        {
            defenceFlag = true;

            battleManager.attackerCharacter = attackCharacter;

            TitleFieldUI.instance.titleFieldText.text = "      敵軍の侵攻を受けました";
            StartCoroutine(ShowAttackedTerritory(attackCharacter));
            yield return new WaitForSeconds(2);

            dialogueUI.ShowAttackedUI();
            yield return new WaitUntil(() => !dialogueUI.IsDialogueVisible());

            mapField.SetActive(false);
            cursor.gameObject.SetActive(false);

            TitleFieldUI.instance.titleFieldText.text = "      防衛部隊を選択してください";
            characterIndexMenu.SetActive(true);
            characterIndexUI.ShowCharacterIndexUI(territoryManager.territory.influence.characterList);
            attackedCharacterUI.ShowAttackedCharacterUI(battleManager.attackerCharacter);
            abandonUI.ShowAbandonUI();
            landformInformationUI.ShowLandformInformationUI();
        }
        //プレイヤーが領主ではない場合
        else
        {
            //防衛側で戦闘可能なキャラクターがいる場合
            bool canAttack = territoryManager.territory.influence.characterList.Exists(c => c.isAttackable);
            if (canAttack)
            {
                //侵攻側よりも強いキャラを防御キャラに選択 /　強いキャラがいない場合は一番強いキャラを防御キャラに選択
                defenderCharacter = SelectDefenceCharacter(attackSoliderHPSum);

                if (attackCharacter == playerCharacter)
                {
                    TitleFieldUI.instance.titleFieldText.text = "      出撃命令が下りました";

                    StartCoroutine(ShowAttackedTerritory(attackCharacter));
                    yield return new WaitForSeconds(2);

                    dialogueUI.ShowBattleOrderUI();
                    yield return new WaitUntil(() => !dialogueUI.IsDialogueVisible());

                    battleUI.ShowBattleUI(playerCharacter, defenderCharacter, territoryManager.territory);
                    battleManager.StartBattle(playerCharacter, defenderCharacter);
                }
                else if (defenderCharacter == playerCharacter)
                {
                    TitleFieldUI.instance.titleFieldText.text = "      出撃命令が下りました";

                    StartCoroutine(ShowAttackedTerritory(attackCharacter));
                    yield return new WaitForSeconds(2);

                    dialogueUI.ShowBattleOrderUI();
                    yield return new WaitUntil(() => !dialogueUI.IsDialogueVisible());

                    battleUI.ShowBattleUI(attackCharacter, playerCharacter, territoryManager.territory);
                    battleManager.StartBattle(attackCharacter, playerCharacter);                    
                }
                else
                {
                    //戦闘前の画面表示
                    if (attackCharacter.influence == playerCharacter.influence || defenderCharacter.influence == playerCharacter.influence)
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      味方 VS 敵　戦闘！";
                    }
                    else
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      敵 VS 敵　戦闘！";
                    }
                    mapField.SetActive(true);
                    cursor.gameObject.SetActive(true);

                    // カーソルの位置を設定
                    RectTransform territoryRectTransform = territoryManager.territory.GetComponent<RectTransform>();
                    cursor.SetPosition(territoryRectTransform);
                    //cursor.transform.position = territoryManager.territory.position;

                    vsImageUI.SetPosition(territoryRectTransform);
                    //vsImageUI.transform.position = influenceManager.territory.position;

                    battleDetailUI.ShowBattleDetailUI(attackCharacter, defenderCharacter);
                    StartCoroutine(BlinkCursor(1.0f));
                    yield return new WaitForSeconds(1.0f);

                    //戦闘実施　戦闘中画面表示
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
                        vsImageUI.gameObject.SetActive(!vsImageUI.gameObject.activeSelf); // VSイメージの表示・非表示を切り替える
                        yield return new WaitForSeconds(0.05f);
                    }

                    //戦闘後の画面を表示                    
                    StartCoroutine(battleManager.ShowEndBattle());
                    yield return new WaitForSeconds(battleManager.battleAfterWaitTime);

                    battleManager.CheckExtinct(defenderCharacter.influence);

                    //次の処理へ移行
                    battleManager.CheckAttackableCharacterInInfluence();
                }
            }
            //戦闘可能なキャラクターがいない場合は無条件で勝利
            else
            {
                Debug.Log("無条件で勝利");
                attackCharacter.isAttackable = false;

                territoryUIOnMouse.ChangeTerritoryByBattle(attackCharacter.influence);
                battleManager.isBattleEnd = true;

                //次の処理へ移行
                battleManager.CheckAttackableCharacterInInfluence();
            }
        }
    }

    public CharacterController SelectDefenceCharacter(int attackSoliderHPSum)
    {
        Debug.Log("SelectDefenceCharacter");
        Debug.Log(territoryManager.territory.influence.influenceName);
        CharacterController defenderCharacter;
        //防御キャラを取得
        //侵攻側よりも強いキャラを取得
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
        //侵攻側よりも強いキャラがいる場合はそのキャラを出撃させる
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
        //侵攻側よりも強いキャラがいない場合は一番強いキャラを出撃させる
        else
        {
            attackableCharacterList = territoryManager.territory.influence.characterList.FindAll(character => character.isAttackable);
            //一番強いキャラを初期化
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

        // カーソルの位置を設定
        RectTransform territoryRectTransform = territoryManager.territory.GetComponent<RectTransform>();
        cursor.SetPosition(territoryRectTransform);

        //cursor.transform.position = territoryManager.territory.position;
        StartCoroutine(BlinkCursor(2));
        characterDetailUI.ShowCharacterDetailUI(attackCharacter);
        yield return new WaitForSeconds(2);
    }

    // カーソル点滅のコルーチン
    public IEnumerator BlinkCursor(float blinkTime)
    {
        float addTime = 0f;
        while (addTime < blinkTime)
        {
            cursor.gameObject.SetActive(!cursor.gameObject.activeSelf); // カーソルの表示・非表示を切り替える
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
            // 領土に攻撃勢力の画像を設定
            territoryImage.sprite = attackerChara.influence.influenceImage;
            yield return new WaitForSeconds(0.1f);
            addTime += 0.1f;

            //territorySpriteRenderer.sprite = attackerChara.influence.influenceImage;
            //yield return new WaitForSeconds(0.1f);
            //addTime += 0.1f;

            // 領土に防御勢力の画像を設定
            territoryImage.sprite = defenderChara.influence.influenceImage;
            yield return new WaitForSeconds(0.1f);
            addTime += 0.1f;

            //territorySpriteRenderer.sprite = defenderChara.influence.influenceImage;
            //yield return new WaitForSeconds(0.1f);
            //addTime += 0.1f;
        }

        //最終的に攻撃側勢力の画像を設定
        territoryImage.sprite = attackerChara.influence.influenceImage;
        yield return new WaitForSeconds(0.5f);
    }

    public void NextCharacterBattlePhase(CharacterController character)
    {
        Debug.Log("NextCharacterBattlePhase");
        //キャラクターリストから最後のキャラクターを取得
        CharacterController lastCharacter = characterList.LastOrDefault();

        //最後のターンのキャラクターではない場合
        if (character != lastCharacter)
        {
            // キャラクターリストから引数で渡されたcharacterのインデックスを取得
            int currentIndex = characterList.IndexOf(character);
            // 次のキャラクターのインデックスを計算
            int nextIndex = (currentIndex + 1) % characterList.Count;
            // 次のキャラクターを取得
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
        //最後のターンのキャラクターの場合
        else
        {
            Debug.Log("最後のキャラクターターンが終了しました。");
            if (uniteCountryFlag == true)
            {
                //統一処理
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
        // リストからランダムにインデックスを選択
        int randomIndex = Random.Range(0, territoryList.Count);

        // 選択されたランダムな領土を返す
        return territoryList[randomIndex];
    }

    public IEnumerator NoneRandomCharacterAddInfluence(CharacterController lordCharacter)
    {
        //Noneに所属するキャラクターリストの取得
        //noneInfluenceCharacters = noneInfluence.characterList;

        //無所属のランダムなキャラクターを取得
        System.Random random = new System.Random();
        CharacterController randomNoneCharacter = noneInfluence.characterList[random.Next(noneInfluence.characterList.Count)];

        if (randomNoneCharacter == playerCharacter)
        {
            characterTurnUI.HideCharacterTurnUI();
            influenceUI.ShowInfluenceUI(lordCharacter.influence);
            
            yesNoUI.ShowEmployedYesNoUI(lordCharacter);
            //yesNoUIが非表示になるまで待機
            yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());

            if (yesNoUI.IsYes())
            {
                //プレイヤーキャラクターを勢力へ所属
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
            //ランダムキャラクターを勢力へ所属
            randomNoneCharacter.influence.RemoveCharacter(randomNoneCharacter);
            lordCharacter.influence.AddCharacter(randomNoneCharacter);
            lordCharacter.gold -= 9;
        }

        // 処理が終了したことを呼び出し元に通知する
        yield return null;
        //Noneに所属するキャラクターリストの更新
        //noneInfluenceCharacters = noneInfluence.characterList;
    }

    public void LeaveInfluence(CharacterController leaveCharacter)
    {
        //追放するキャラクターより下の身分のキャラを取得 
        List<CharacterController> lowerRankCharacters = leaveCharacter.influence.characterList.FindAll(c => (int)c.rank < (int)leaveCharacter.rank);

        //キャラクターを追放
        leaveCharacter.influence.RemoveCharacter(leaveCharacter);

        if (lowerRankCharacters != null)
        {
            //追放したキャラクターより下の身分のキャラを昇格　給料%、身分を計算
            foreach (CharacterController lowerRankCharacter in lowerRankCharacters)
            {
                lowerRankCharacter.rank = (Rank)((int)lowerRankCharacter.rank + 1);
                lowerRankCharacter.CalcSalary();
                lowerRankCharacter.CalcLoyalty();
            }
        }
            
        //追放したキャラクターを無所属に加入
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
        TitleFieldUI.instance.titleFieldText.text = "      統一されました!!";
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

    public void SaveGame(int slot)
    {
        GameState gameState = new GameState
        {
            turnCount = this.turnCount,
            phase = this.phase,
            step = this.step,
            characters = new List<CharacterData>(),
            playerCharacterId = playerCharacter.characterId,
            influences = new List<InfluenceData>(),
        };

        // キャラクターデータの収集
        foreach (var character in characterList)
        {
            CharacterData charData = new CharacterData
            {
                icon = character.icon,
                name = character.name,
                characterId = character.characterId,
                force = character.force,
                inteli = character.inteli,
                tact = character.tact,
                fame = character.fame,
                ambition = character.ambition,
                loyalty = character.loyalty,
                salary = character.salary,
                rank = character.rank,
                gold = character.gold,
                isLord = character.isLord,
                isPlayerCharacter = character.isPlayerCharacter,
                isAttackable = character.isAttackable,
                isBattle = character.isBattle,

                influenceName = character.influence != null ? character.influence.influenceName : "None",
            };
            // 兵士データの収集
            foreach (var solider in character.soliderList)
            {
                SoliderData soliderData = new SoliderData
                {
                    soliderID = solider.soliderID,
                    hp = solider.hp,
                    df = solider.df,
                    maxHP = solider.maxHP,
                    at = solider.at,
                    force = solider.force,
                    icon = solider.icon,
                    lv = solider.lv,
                    experience = solider.experience,
                    isAlive = solider.isAlive,
                    uniqueID = solider.uniqueID
                };
                // 兵士データをキャラクターデータに追加
                charData.soliders.Add(soliderData);
            }

            gameState.characters.Add(charData); 
        }

        // 勢力データの収集
        foreach (var influence in influenceList)
        {
            InfluenceData influenceData = new InfluenceData
            {
                influenceName = influence.influenceName,
                characterIds = influence.characterList.Select(c => c.characterId).ToList()
            };
            //領土データの収集
            foreach (var territory in influence.territoryList)
            {
                TerritoryData territoryData = new TerritoryData
                {
                    attackTerritoryType = territory.attackTerritoryType,
                    defenceTerritoryType = territory.defenceTerritoryType,
                    position = territory.position,
                    //influence = territory.influence,
                    influenceName = territory.influence != null ? territory.influence.influenceName : null,
                };
                influenceData.territories.Add(territoryData);
                //Debug.Log(territoryData.influenceName);
            }
            gameState.influences.Add(influenceData);
        }

        SaveLoadManager.SaveGame(gameState, slot);
    }

    public void LoadGame(int slot)
    {
        GameState gameState = SaveLoadManager.LoadGame(slot);
        if (gameState != null)
        {
            this.turnCount = gameState.turnCount;
            this.phase = gameState.phase;
            this.step = gameState.step;

            // キャラクターの復元
            foreach (var charData in gameState.characters)
            {
                CharacterController character = characterList.Find(c => c.characterId == charData.characterId);

                character.icon = charData.icon;
                character.name = charData.name;
                character.force = charData.force;
                character.inteli = charData.inteli;
                character.tact = charData.tact;
                character.fame = charData.fame;
                character.ambition = charData.ambition;
                character.loyalty = charData.loyalty;
                character.salary = charData.salary;
                character.rank = charData.rank;
                character.gold = charData.gold;
                character.isLord = charData.isLord;
                character.isPlayerCharacter = charData.isPlayerCharacter;
                character.isAttackable = charData.isAttackable;
                character.isBattle = charData.isBattle;
                character.influence = influenceList.Find(i => i.influenceName == charData.influenceName);

                // 兵士の復元
                foreach (var soliderData in charData.soliders)
                {
                    SoldierController newSoldier = Instantiate(constParam.soldierList.Find(c => c.soliderID == soliderData.soliderID));
                    //constParam.soldierList
                    //SoldierController solider = Instantiate(soliderPrefab, Solider);
                    //solider.Init(soliderData.soliderID, soliderData.uniqueID);
                    newSoldier.hp = soliderData.hp;
                    newSoldier.df = soliderData.df;
                    newSoldier.maxHP = soliderData.maxHP;
                    newSoldier.at = soliderData.at;
                    newSoldier.force = soliderData.force;
                    newSoldier.icon = soliderData.icon;
                    newSoldier.lv = soliderData.lv;
                    newSoldier.experience = soliderData.experience;
                    newSoldier.isAlive = soliderData.isAlive;

                    character.soliderList.Add(newSoldier);
                    allSoliderList.Add(newSoldier);
                }

            }
            //プレイヤーキャラクターの復元
            playerCharacter = characterList.Find(x => x.characterId == gameState.playerCharacterId);

            // 勢力の復元
            foreach (var influenceData in gameState.influences)
            {
                Influence influence = influenceList.Find(i => i.influenceName == influenceData.influenceName);

                //勢力にキャラクターを所属させる&&キャラクターを勢力に所属させる
                foreach (int characterId in influenceData.characterIds)
                {
                    CharacterController character = characterList.Find(c => c.characterId == characterId);
                    if (character != null)
                    {
                        character.SetInfluence(influence);
                        influence.AddCharacter(character);
                    }
                }

                //領土の復元
                foreach (var territoryData in influenceData.territories)
                {
                    Territory territory = initializeTerritoryList.Find(t => t.position == territoryData.position);
                    if (territory != null)
                    {
                        //Influence influ = influenceList.Find(i => i.influenceName == territoryData.influenceName);
                        territoryGenerator.SetupTerritory(territory, territory.position, influenceList, territoryData.influenceName);
                        allTerritoryList.Add(territory);
                    }
                }
            }

            // ゲームの状態を更新
            PhaseCalc();
        }
    }
}
