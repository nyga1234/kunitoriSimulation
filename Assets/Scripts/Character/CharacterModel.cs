using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel
{
    public int characterId; // �ǉ�
    public Sprite icon;
    public string name;
    public int force; //�퓬
    public int inteli;//�q�d
    public int tact;//��r
    public int fame;//����
    public int ambition;//��S
    public int loyalty;//����
    public int salary;//����%
    public int conflict;//�G��
    public Rank rank; //�g��
    public int gold;//����

    public bool isLord;//�̎傩���R��
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
        this.rank = Rank.�Q�m;
        this.gold = characterEntity.gold;

        this.isLord = characterEntity.isLord;
        this.isPlayerCharacter = characterEntity.isPlayerCharacter;
        this.isAttackable = true;
        this.isBattle = false;
    }
}
