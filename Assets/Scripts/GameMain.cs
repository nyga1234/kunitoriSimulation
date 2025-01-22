using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using System;

public class GameMain : SingletonMonoBehaviour<GameMain>
{
    [SerializeField] private UtilityParamObject varParam;
    [SerializeField] UtilityParamObject constParam;
    public List<CharacterController> characterList;
    public List<Influence> influenceList;

    [SerializeField] Cursor cursor;
    [SerializeField] VSImageUI vsImageUI;
    [SerializeField] SoldierController soliderPrefab;
    [SerializeField] TitleFieldUI titleFieldUI;
    [SerializeField] BattleManager battleManager;
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;
    [SerializeField] CharacterTurnUI characterTurnUI;
    [SerializeField] CharacterMenuUI characterMenuUI;
    [SerializeField] PersonalMenuUI personalMenuUI;
    [SerializeField] BattleMenuUI battleMenuUI;
    [SerializeField] CharacterIndexUI characterIndexUI;
    [SerializeField] AbandonUI abandonUI;
    [SerializeField] AttackedCharacterUI attackedCharacterUI;
    [SerializeField] LandformInformationUI landformInformationUI;
    [SerializeField] DetailUI detailsUI;
    [SerializeField] CharacterDetailUI characterDetailUI;
    [SerializeField] InfluenceUI influenceUI;
    [SerializeField] InfluenceOnMapUI influenceOnMapUI;
    [SerializeField] BattleUI battleUI;
    [SerializeField] BattleDetailUI battleDetailUI;
    [SerializeField] GameObject mapField;
    [SerializeField] GameObject characterSearchMenu;
    public TerritoryGenerator territoryGenerator;
    public CharacterController playerCharacter;
    
    public List<Territory> allTerritoryList;
    public Influence noneInfluence;

    public CharacterController battleTurnCharacter;

    public bool defenceFlag;
    public int turnCount;
    public int territoryCouont;
    public Influence uniteInfluence; //統一した勢力
    public bool uniteCountryFlag = false;

    [SerializeField] Territory plainTerritorPrefab;
    [SerializeField] Transform parent;
    public List<Territory> initializeTerritoryList;

    public VSImageUI VSImageUI { get => vsImageUI; set => vsImageUI = value; }
    public CharacterIndexUI CharacterIndexUI { get => characterIndexUI; set => characterIndexUI = value; }
    public CharacterDetailUI CharacterDetailUI { get => characterDetailUI; set => characterDetailUI = value; }
    public GameObject MapField { get => mapField; set => mapField = value; }
    public Cursor Cursor { get => cursor; set => cursor = value; }
    public InfluenceOnMapUI InfluenceOnMapUI { get => influenceOnMapUI; set => influenceOnMapUI = value; }

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

        //キャラクターへ兵士を割り当て
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

    public async void PhaseCalc()
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
                await OtherLordPhase();
                break;

            case Phase.PlayerPersonalPhase:
                PlayerPersonalPhase();
                break;

            case Phase.OtherPersonalPhase:
                await OtherPersonalPhase();
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
                int territoryIncome = 15;
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

    private async UniTask OtherLordPhase()
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
                        await NoneCharacterAddInfluence(lordCharacter);
                    }
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                    while (lordCharacter.influence.characterList.Count <= 3 && lordCharacter.gold >= 9 && noneInfluence.characterList.Count != 0)
                    {
                        await NoneCharacterAddInfluence(lordCharacter);
                    }
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                    while (lordCharacter.influence.characterList.Count <= 4 && lordCharacter.gold >= 9 && noneInfluence.characterList.Count != 0)
                    {
                        await NoneCharacterAddInfluence(lordCharacter);
                    }
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                case 15:
                    while (lordCharacter.influence.characterList.Count <= 5 && lordCharacter.gold >= 9 && noneInfluence.characterList.Count != 0)
                    {
                        await NoneCharacterAddInfluence(lordCharacter);
                    }
                    break;
            }

            int beforeRank = (int)playerCharacter.rank;
            //名声の高い順に身分を設定
            lordCharacter.influence.SetRankByFame();
            int afterRank = (int)playerCharacter.rank;

            if (afterRank > beforeRank)
            {
                await SceneController.LoadAsync("UIDialogue");
                varParam.DialogueText = playerCharacter.rank + "に昇格しました";
            }
            else if (afterRank < beforeRank)
            {
                await SceneController.LoadAsync("UIDialogue");
                varParam.DialogueText = playerCharacter.rank + "に降格しました";
            }

            await UniTask.Delay(125); // 125msの待機
            characterTurnUI.HideCharacterTurnUI();
        }
        mapField.SetActive(false);
        cursor.gameObject.SetActive(false);
        phase = Phase.PlayerPersonalPhase;
        PhaseCalc();
    }

    private async UniTask NoneCharacterAddInfluence(CharacterController lordCharacter)
    {
        Debug.Log("NoneCharacterAddInfluence");
        //無所属のランダムなキャラクターを取得
        System.Random random = new System.Random();
        CharacterController randomNoneCharacter = noneInfluence.characterList[random.Next(noneInfluence.characterList.Count)];

        if (randomNoneCharacter == playerCharacter)
        {
            characterTurnUI.HideCharacterTurnUI();
            influenceUI.ShowInfluenceUI(lordCharacter.influence);

            await SceneController.LoadAsync("UIConfirm");
            varParam.ConfirmText = "軍から加入依頼です。加入しますか？";
            // OKまたはCancelボタンがクリックされるのを待機
            await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);

            if (varParam.IsConfirm == true)
            {
                //プレイヤーキャラクターを勢力へ所属
                randomNoneCharacter.influence.RemoveCharacter(randomNoneCharacter);
                lordCharacter.influence.AddCharacter(randomNoneCharacter);
                lordCharacter.gold -= 9;

                await SceneController.LoadAsync("UIDialogue");
                varParam.DialogueText = lordCharacter.name + "軍へ加入しました";
            }
        }
        else
        {
            //ランダムキャラクターを勢力へ所属
            randomNoneCharacter.influence.RemoveCharacter(randomNoneCharacter);
            lordCharacter.influence.AddCharacter(randomNoneCharacter);
            lordCharacter.gold -= 9;
        }
    }

    void PlayerPersonalPhase()
    {
        Debug.Log("プレイヤー個人フェーズです。");
        titleFieldUI.ShowChangePersonalTurnText();
        ShowPersonalUI(playerCharacter);
    }

    private async UniTask OtherPersonalPhase()
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
                    Debug.Log(otherCharacter.name + "は所属勢力を去ります");
                    if (otherCharacter.influence == playerCharacter.influence && playerCharacter.isLord == true)
                    {
                        await SceneController.LoadAsync("UIDialogue");
                        varParam.DialogueText = otherCharacter.name + "が勢力を去りました";

                        // ダイアログが閉じられるまで待機
                        await UniTask.WaitUntil(() => varParam.IsDialogue.HasValue);
                    }
                    LeaveInfluence(otherCharacter);
                }
            }

            //兵士雇用
            while (otherCharacter.soliderList.Count < 10 && otherCharacter.gold >= 2)
            {
                otherCharacter.soliderList.Add(Instantiate(constParam.soldierList.Find(c => c.soliderID == 1)));
                otherCharacter.gold -= 2;
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

            await UniTask.Delay(125); // 125msの待機
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
                        NextCharacterBattlePhase(character);
                    }
                    break;
            }
        }
        else
        {
            NextCharacterBattlePhase(character);
        }
    }

    private async void LordsOrderBattle(CharacterController character)
    {
        // 隣接する侵攻可能な領土を取得
        List<Territory> adjacentTerritory = character.influence.FindAdjacentTerritory()
            .FindAll(territory => territory.influence != character.influence && territory.influence != noneInfluence);

        // 侵攻対象の領土をランダムに選択
        Territory targetTerritory = GetRandomTerritory(adjacentTerritory);

        // 選択した領土を TerritoryManager に設定
        varParam.Territory = targetTerritory;
        varParam.Influence = targetTerritory.influence;

        // 攻撃キャラクターを選択
        CharacterController attackCharacter = SelectAttackCharacter(character.influence, targetTerritory.influence);

        //バトル準備を開始
        StopAllCoroutines();
        await BattlePrepare(attackCharacter);
    }

    public async UniTask BattlePrepare(CharacterController attackCharacter)
    {
        CharacterController defenderCharacter;

        defenceFlag = false;

        //侵攻された領土がプレイヤー勢力　かつ　プレイヤーが領主の場合
        if (varParam.Territory.influence == playerCharacter.influence && playerCharacter.isLord == true)
        {
            defenceFlag = true;

            battleManager.attackerCharacter = attackCharacter;

            TitleFieldUI.instance.titleFieldText.text = "      敵軍の侵攻を受けました";
            characterDetailUI.ShowCharacterDetailUI(attackCharacter);
            await ShowAttackedTerritory(attackCharacter);

            await SceneController.LoadAsync("UIDialogue");
            varParam.DialogueText = "防衛部隊を選択してください";
            await UniTask.WaitUntil(() => varParam.IsDialogue.HasValue);

            mapField.SetActive(false);
            cursor.gameObject.SetActive(false);

            TitleFieldUI.instance.titleFieldText.text = "      防衛部隊を選択してください";
            characterIndexUI.ShowCharacterIndexUI(varParam.Territory.influence.characterList);
            attackedCharacterUI.ShowAttackedCharacterUI(battleManager.attackerCharacter);
            abandonUI.ShowAbandonUI();
            landformInformationUI.ShowLandformInformationUI();
        }
        //プレイヤーが領主ではない場合
        else
        {
            //防衛側で戦闘可能なキャラクターがいる場合
            bool canAttack = varParam.Territory.influence.characterList.Exists(c => c.isAttackable);
            if (canAttack)
            {
                defenderCharacter = SelectDefenceCharacter(attackCharacter);

                //戦闘前の画面表示
                await ShowBeforeBattle(attackCharacter, defenderCharacter);

                //AI同士の戦闘
                if (attackCharacter != playerCharacter & defenderCharacter != playerCharacter)
                {
                    //戦闘実施　戦闘中画面表示
                    await battleManager.AIBattle(attackCharacter, defenderCharacter);

                    //戦闘後の画面を表示                    
                    await battleManager.ShowEndBattle();

                    battleManager.CheckExtinct(defenderCharacter.influence);

                    //次の処理へ移行
                    battleManager.CheckAttackableCharacterInInfluence();
                }
            }
            //修正が必要
            //戦闘可能なキャラクターがいない場合は無条件で勝利
            else
            {
                Debug.Log("無条件で勝利");
                attackCharacter.isAttackable = false;

                territoryUIOnMouse.ChangeTerritoryByBattle(attackCharacter.influence);

                //次の処理へ移行
                battleManager.CheckAttackableCharacterInInfluence();
            }
        }
    }

    private async UniTask ShowBeforeBattle(CharacterController attackCharacter, CharacterController defenderCharacter)
    {
        if (attackCharacter == playerCharacter || defenderCharacter == playerCharacter)
        {
            TitleFieldUI.instance.titleFieldText.text = "      出撃命令が下りました";

            await ShowAttackedTerritory(attackCharacter);

            await SceneController.LoadAsync("UIDialogue");
            varParam.DialogueText = "戦闘を開始します";
            await UniTask.WaitUntil(() => varParam.IsDialogue.HasValue);

            battleUI.ShowBattleUI(attackCharacter, defenderCharacter, varParam.Territory);
            battleManager.StartBattle(attackCharacter, defenderCharacter);
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

            // カーソルと戦闘アイコンの位置を設定
            RectTransform territoryRectTransform = varParam.Territory.GetComponent<RectTransform>();
            cursor.SetPosition(territoryRectTransform);
            vsImageUI.SetPosition(territoryRectTransform);

            battleDetailUI.ShowBattleDetailUI(attackCharacter, defenderCharacter);
            await BlinkCursor(1.0f);
        }
    }

    /// <summary>
    /// 侵攻させるキャラクターを取得
    /// </summary>
    /// <param name="attackInfluence"></param>
    /// <param name="defenceInfluence"></param>
    /// <returns></returns>
    private CharacterController SelectAttackCharacter(Influence attackInfluence, Influence defenceInfluence)
    {
        //防衛側で一番強い戦力を取得
        CharacterController strongestDefenceCharacter = GetStrongestCharacter(defenceInfluence.characterList);
        int defenceCharaHPMax = strongestDefenceCharacter.CalcSoldierHPSum();

        // 攻撃可能かつ防衛キャラよりも強いキャラクターをフィルタリング
        List<CharacterController> attackableCharacterList = attackInfluence.characterList
            .FindAll(character => character.isAttackable && character.CalcSoldierHPSum() > defenceCharaHPMax);

        // フィルタリング結果に応じてキャラクターを選択
        CharacterController attackCharacter = attackableCharacterList.Count > 0
            ? GetRandomCharacter(attackableCharacterList) // 防衛キャラより強い攻撃キャラからランダム選択
            : GetStrongestCharacter(attackInfluence.characterList.FindAll(character => character.isAttackable)); // 攻撃可能なキャラの中で最強を選択

        return attackCharacter;
    }

    /// <summary>
    /// 防衛させるキャラクターを取得
    /// </summary>
    /// <param name="attackCharacter"></param>
    /// <returns></returns>
    public CharacterController SelectDefenceCharacter(CharacterController attackCharacter)
    {
        // 防衛可能なキャラクターを取得
        List<CharacterController> defendableCharacterList = varParam.Territory.influence.characterList
            .FindAll(character => character.isAttackable);

        // 侵攻キャラより強い防衛キャラを取得
        defendableCharacterList = defendableCharacterList
            .FindAll(defenceChara => defenceChara.CalcSoldierHPSum() >= attackCharacter.CalcSoldierHPSum());

        CharacterController defenderCharacter;

        // 強い防衛キャラがいる場合、ランダムに選択
        if (defendableCharacterList.Count > 0)
        {
            defenderCharacter = GetRandomCharacter(defendableCharacterList); ;
        }
        // 強い防衛キャラがいない場合、最強キャラを選択
        else
        {
            defenderCharacter = GetStrongestCharacter(
                varParam.Territory.influence.characterList.FindAll(character => character.isAttackable));
        }
        return defenderCharacter;
    }

    private async UniTask ShowAttackedTerritory(CharacterController attackCharacter)
    {
        mapField.SetActive(true);
        cursor.gameObject.SetActive(true);

        // カーソルの位置を設定
        RectTransform territoryRectTransform = varParam.Territory.GetComponent<RectTransform>();
        cursor.SetPosition(territoryRectTransform);

        await BlinkCursor(2);
        characterDetailUI.ShowCharacterDetailUI(attackCharacter);
    }

    // カーソル点滅のコルーチン
    public async UniTask BlinkCursor(float blinkTime)
    {
        float addTime = 0f;
        while (addTime < blinkTime)
        {
            cursor.gameObject.SetActive(!cursor.gameObject.activeSelf); // カーソルの表示・非表示を切り替える
            await UniTask.Delay(TimeSpan.FromSeconds(0.25f));
            addTime += 0.25f;
        }
    }

    public IEnumerator BlinkTerritory(float blinkTime, CharacterController attackerChara, CharacterController defenderChara, Territory territory)
    {
        float addTime = 0f;
        Image territoryImage = territory.GetComponent<Image>();

        while (addTime < blinkTime)
        {
            // 領土に攻撃勢力の画像を設定
            territoryImage.sprite = attackerChara.influence.influenceImage;
            yield return new WaitForSeconds(0.1f);
            addTime += 0.1f;

            // 領土に防御勢力の画像を設定
            territoryImage.sprite = defenderChara.influence.influenceImage;
            yield return new WaitForSeconds(0.1f);
            addTime += 0.1f;
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

    /// <summary>
    /// ランダムなキャラクターを取得するヘルパー関数
    /// </summary>
    /// <param name="characters"></param>
    /// <returns></returns>
    private CharacterController GetRandomCharacter(List<CharacterController> characters)
    {
        System.Random random = new System.Random();
        return characters[random.Next(characters.Count)];
    }

    /// <summary>
    /// 最強キャラを取得する
    /// </summary>
    /// <param name="characters"></param>
    /// <returns></returns>
    private CharacterController GetStrongestCharacter(List<CharacterController> characters)
    {
        return characters
            .OrderByDescending(chara => chara.CalcSoldierHPSum())
            .FirstOrDefault();
    }

    /// <summary>
    /// リストからランダムな領土を取得する
    /// </summary>
    /// <param name="territoryList"></param>
    /// <returns></returns>
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
}
