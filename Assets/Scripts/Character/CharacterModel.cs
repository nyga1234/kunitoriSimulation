using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel
{
    public int characterId; // ’Ç‰Á
    public Sprite icon;
    public string name;
    public int force; //í“¬
    public int inteli;//’q–d
    public int tact;//è˜r
    public int fame;//–¼º
    public int ambition;//–ìS
    public int loyalty;//’‰½
    public int salary;//‹‹—¿%
    public int conflict;//“GˆÓ
    public Rank rank; //g•ª
    public int gold;//‚¨‹à

    public bool isLord;//—Ìå‚©«ŒR‚©
    public bool isPlayerCharacter;
    public bool isAttackable;
    public bool isBattle;

    public CharacterModel(int characterID)
    {
        CharacterEntity characterEntity = Resources.Load<CharacterEntity>("CharacterEntityList/Character" + characterID);
        this.characterId = characterID;
        this.icon = characterEntity.icon;
        this.name = characterEntity.name;
        this.force = characterEntity.force;
        this.inteli = characterEntity.inteli;
        this.tact = characterEntity.tact;
        this.fame = characterEntity.fame;
        this.ambition = characterEntity.ambition;
        this.rank = Rank.˜Qm;
        this.gold = characterEntity.gold;

        this.isLord = characterEntity.isLord;
        this.isPlayerCharacter = characterEntity.isPlayerCharacter;
        this.isAttackable = true;
        this.isBattle = false;
    }
}
