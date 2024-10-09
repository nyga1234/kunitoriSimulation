using System.Collections.Generic;
using UnityEngine;
using static Territory;

[System.Serializable]
public class GameState
{
    public int turnCount;
    public GameMain.Phase phase;
    public GameMain.Step step;
    public List<CharacterData> characters;
    public int playerCharacterId;
    public List<InfluenceData> influences;
    //public CharacterController playerCharacter;
}

[System.Serializable]
public class CharacterData
{
    public Sprite icon;
    public string name;
    public int characterId;
    public int force;
    public int inteli;
    public int tact;
    public int fame;
    public int ambition;
    public int loyalty;
    public int salary;
    public int conflict;
    public Rank rank;
    public int gold;
    public bool isLord;
    public bool isPlayerCharacter;
    public bool isAttackable;
    public bool isBattle;
    public string influenceName;
    public List<SoliderData> soliders = new List<SoliderData>();
}

[System.Serializable]
public class InfluenceData
{
    public string influenceName;
    public List<int> characterIds;
    public List<TerritoryData> territories = new List<TerritoryData>();
}

[System.Serializable]
public class SoliderData
{
    public int soliderID;
    public int hp;
    public int df;
    public int maxHP;
    public int at;
    public int force;
    public Sprite icon;
    public int lv;
    public int experience;
    public bool isAlive;
    public int uniqueID;
}

[System.Serializable]
public class TerritoryData
{
    public AttackTerritoryType attackTerritoryType;
    public DefenceTerritoryType defenceTerritoryType;
    public Vector2 position;
    //public Influence influence;
    public string influenceName;
}