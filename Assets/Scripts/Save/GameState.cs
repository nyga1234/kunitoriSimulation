using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public int turnCount;
    public GameManager.Phase phase;
    public GameManager.Step step;
    public List<CharacterData> characters;
}

[System.Serializable]
public class CharacterData
{
    public int characterId;
    public Vector3 position;
    public string influenceName;
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
}