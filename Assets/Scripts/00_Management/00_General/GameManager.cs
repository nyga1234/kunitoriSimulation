using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cysharp.Threading.Tasks;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [Header("Initialize Scene Setting")]
    [SerializeField]
    private string _initializeLoadScene;

    [Header("Loading Canvas")]
    [SerializeField]
    private LoadingOverlay _loading;

    public LoadingOverlay Loading => _loading;

    [SerializeField] UtilityParamObject constParam;

    private SceneController _sceneController;

    public void Awake()
    {
        _sceneController = SceneController.instance;
    }

    public void Start()
    {
        // Load First Scene
        if (_initializeLoadScene != string.Empty && SceneController.LoadedSceneCount <= 1)
        {
            _sceneController.Open(_initializeLoadScene);
        }
    }

    public async UniTask ChangeScene(string before, string next)
    {
        await _loading.Display();
        await _sceneController.SwitchPrimaryScene(before, next);
    }

    public void SaveGame(int slot)
    {
        GameState gameState = new GameState
        {
            turnCount = GameMain.instance.turnCount,
            phase = GameMain.instance.phase,
            step = GameMain.instance.step,
            characters = new List<CharacterData>(),
            playerCharacterId = GameMain.instance.playerCharacter.characterId,
            influences = new List<InfluenceData>(),
        };

        // キャラクターデータの収集
        foreach (var character in GameMain.instance.characterList)
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
        foreach (var influence in GameMain.instance.influenceList)
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
        Debug.Log("LoadGame");
        GameState gameState = SaveLoadManager.LoadGame(slot);
        if (gameState != null)
        {
            GameMain.instance.turnCount = gameState.turnCount;
            GameMain.instance.phase = gameState.phase;
            GameMain.instance.step = gameState.step;

            // キャラクターの復元
            foreach (var charData in gameState.characters)
            {
                CharacterController character = GameMain.instance.characterList.Find(c => c.characterId == charData.characterId);

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
                character.influence = GameMain.instance.influenceList.Find(i => i.influenceName == charData.influenceName);

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
                }

            }
            //プレイヤーキャラクターの復元
            GameMain.instance.playerCharacter = GameMain.instance.characterList.Find(x => x.characterId == gameState.playerCharacterId);

            // 勢力の復元
            foreach (var influenceData in gameState.influences)
            {
                Influence influence = GameMain.instance.influenceList.Find(i => i.influenceName == influenceData.influenceName);

                //勢力にキャラクターを所属させる&&キャラクターを勢力に所属させる
                foreach (int characterId in influenceData.characterIds)
                {
                    CharacterController character = GameMain.instance.characterList.Find(c => c.characterId == characterId);
                    if (character != null)
                    {
                        character.SetInfluence(influence);
                        influence.AddCharacter(character);
                    }
                }

                //領土の復元
                foreach (var territoryData in influenceData.territories)
                {
                    Territory territory = GameMain.instance.initializeTerritoryList.Find(t => t.position == territoryData.position);
                    if (territory != null)
                    {
                        //Influence influ = influenceList.Find(i => i.influenceName == territoryData.influenceName);
                        GameMain.instance.territoryGenerator.SetupTerritory(territory, territory.position, GameMain.instance.influenceList, territoryData.influenceName);
                        GameMain.instance.allTerritoryList.Add(territory);
                    }
                }
            }

            // ゲームの状態を更新
            GameMain.instance.PhaseCalc();
        }
    }
}
