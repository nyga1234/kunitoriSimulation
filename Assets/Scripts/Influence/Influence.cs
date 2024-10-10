using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

public class Influence : MonoBehaviour
{
    public string influenceName;
    public Sprite influenceImage;
    public List<CharacterController> characterList;
    public List<Territory> territoryList;

    private void Awake()
    {
        this.characterList = new List<CharacterController>();
        this.territoryList = new List<Territory>();
    }

    public void Init(string influenceName, Sprite influenceImage)
    {
        this.influenceName = influenceName;
        this.influenceImage = influenceImage;
    }

    public int territorySum;
    public int goldSum;
    public int characterSum;
    public int soliderSum;
    public int forceSum;

    public bool IsAttackableTerritory(Territory territory)
    {
        bool canAttackaTerritory = false;

        List<Territory>  attackableTerritoryList = FindAdjacentTerritory();

        foreach (Territory attackableTerritory in attackableTerritoryList)
        {
            if (attackableTerritory.position == territory.position)
            {
                canAttackaTerritory = true;
            }
        }
        return canAttackaTerritory;
    }

    public List<Territory> FindAdjacentTerritory()
    {
        int spaceTerritory = 100;
        List<Territory> attackableTerritoryList = new List<Territory>();
        Vector2[] directions = {
            new Vector2(spaceTerritory, 0),   // �E
            new Vector2(-spaceTerritory, 0),  // ��
            new Vector2(0, -spaceTerritory),   // ��
            new Vector2(0, spaceTerritory)   // ��
        };
        foreach (Territory influenceTerritory in this.territoryList)
        {
            Territory rightTerritory = GameMain.instance.allTerritoryList.Find(x => x.position == influenceTerritory.position + directions[0]);
            if (rightTerritory != null)
            {
                attackableTerritoryList.Add(rightTerritory);
            }
            Territory leftTerritory = GameMain.instance.allTerritoryList.Find(x => x.position == influenceTerritory.position + directions[1]);
            if (leftTerritory != null)
            {
                attackableTerritoryList.Add(leftTerritory);
            }
            Territory downTerritory = GameMain.instance.allTerritoryList.Find(x => x.position == influenceTerritory.position + directions[2]);
            if (downTerritory != null)
            {
                attackableTerritoryList.Add(downTerritory);
            }
            Territory upTerritory = GameMain.instance.allTerritoryList.Find(x => x.position == influenceTerritory.position + directions[3]);
            if (upTerritory != null)
            {
                attackableTerritoryList.Add(upTerritory);
            }
        }
        return attackableTerritoryList;
    }

    // �����̍������ɐg����ݒ�(�̎�ȊO)
    public void SetRankByFame()
    {
        // �̎�̃����N�͗̎�ŌŒ�
        //characterList.Find(c => c.characterModel.isLord).characterModel.rank = Rank.�̎�;

        // �̎�ȊO�̃L�����N�^�[�𖼐��̍������Ƀ\�[�g
        List<CharacterController> sortedCharacters = characterList
            .Where(c => !c.characterModel.isLord)
            .OrderByDescending(c => c.characterModel.fame)
            .ToList();

        // �g�������ɐݒ�
        for (int i = 0; i < sortedCharacters.Count; i++)
        {
            switch (i)
            {
                case 0:
                    sortedCharacters[i].characterModel.rank = Rank.�⍲;
                    break;
                case 1:
                    sortedCharacters[i].characterModel.rank = Rank.�叫;
                    break;
                case 2:
                    sortedCharacters[i].characterModel.rank = Rank.����;
                    break;
                case 3:
                    sortedCharacters[i].characterModel.rank = Rank.����;
                    break;
                case 4:
                    sortedCharacters[i].characterModel.rank = Rank.�y��;
                    break;
            }
        }
        SortCharacterByRank(characterList);
    }

    // �g���̍������Ƀ\�[�g
    public void SortCharacterByRank(List<CharacterController> characterList)
    {
        this.characterList = characterList.OrderByDescending(c => c.characterModel.rank).ToList();
    }

    //���͂�Territory��ǉ����郁�\�b�h
    public void AddTerritory(Territory territory)
    {
        territoryList.Add(territory);

        //�̓y�ɐ��͂̉摜��ݒ�
        Image territoryImage = territory.GetComponent<Image>();
        if (territoryImage != null && influenceImage != null)
        {
            territoryImage.sprite = influenceImage;
        }
        //SpriteRenderer territorySpriteRenderer = territory.GetComponent<SpriteRenderer>();
        //if (territorySpriteRenderer != null && influenceImage != null)
        //{
        //    territorySpriteRenderer.sprite = influenceImage;
        //}
    }

    // Territory�𐨗͂��珜�O���郁�\�b�h
    public void RemoveTerritory(Territory territory)
    {
        territoryList.Remove(territory);
    }

    // �L�����N�^�[�𐨗͂ɒǉ����郁�\�b�h
    public void AddCharacter(CharacterController character)
    {
        // �L�����N�^�[�𐨗͂ɒǉ�
        characterList.Add(character);
        character.SetInfluence(this);

        //�L�����N�^�[�̐g����ݒ�
        if (character.influence.influenceName != "NoneInfluence")
        {
            switch (this.characterList.Count)
            {
                case 1:
                    character.characterModel.rank = Rank.�̎�;
                    break;
                case 2:
                    character.characterModel.rank = Rank.�⍲;
                    break;
                case 3:
                    character.characterModel.rank = Rank.�叫;
                    break;
                case 4:
                    character.characterModel.rank = Rank.����;
                    break;
                case 5:
                    character.characterModel.rank = Rank.����;
                    break;
                case 6:
                    character.characterModel.rank = Rank.�y��;
                    break;
            }
            SortCharacterByRank(characterList);
        }

        if (character.influence.influenceName != "NoneInfluence")
        {
            //�L�����N�^�[�̋���%��ݒ�
            character.CalcSalary();
            //�L�����N�^�[�̒�����ݒ�
            if (!character.characterModel.isLord)
            {
                character.CalcLoyalty();
            }
        }
        ////�L�����N�^�[�̒�����ݒ�
        //if (character.influence.influenceName != "NoneInfluence" && character.characterModel.isLord != true)
        //{
        //    character.CalcLoyalty();
        //}
    }

    // �L�����N�^�[�𐨗͂��珜�O���郁�\�b�h
    public void RemoveCharacter(CharacterController character)
    {
        characterList.Remove(character);
        character.characterModel.rank = Rank.�Q�m;
        //character.RemoveInfluence();
    }

    //public int CalcTerritorySum(Influence influence)
    //{
    //    //this.territorySum = territoryList.Count(territory => territory.influence.InfluenceType == influenceType);
    //    this.territorySum = influence.territoryList.Count;
    //    return this.territorySum;
    //}

    //public void CalcGoldSum(List<CharacterController> characterList)
    //{
    //    int goldSum = 0;

    //    foreach (CharacterController character in characterList)
    //    {
    //        // �e�L�����N�^�[�̏����������v�ɉ��Z
    //        goldSum += character.characterModel.gold;
    //    }
    //    this.goldSum = goldSum;
    //}

    //public void CalcCharacterSum(List<CharacterController> characterList)
    //{
    //    this.characterSum = characterList.Count;
    //}

    //public void CalcSoliderSum(List<CharacterController> characterList)
    //{
    //    int soliderSum = 0;

    //    foreach (CharacterController character in characterList)
    //    {
    //        soliderSum += character.soliderList.Count;
    //    }
    //    this.soliderSum = soliderSum;
    //}

    //public void CalcForceSum(List<CharacterController> characterList)
    //{
    //    int forceSum = 0;

    //    foreach(CharacterController character in characterList)
    //    {
    //        foreach (SoliderController solider in character.soliderList)
    //        {
    //            forceSum += solider.soliderModel.force;
    //        }
    //    }
    //    this.forceSum = forceSum;
    //}

    public void UpdateSums(Influence influence)
    {
        this.territorySum = influence.territoryList.Count;
        this.characterSum = influence.characterList.Count;
        this.goldSum = influence.characterList.Sum(c => c.characterModel.gold);
        this.soliderSum = influence.characterList.Sum(c => c.soliderList.Count);
        this.forceSum = influence.characterList.Sum(c => c.soliderList.Sum(s => s.soliderModel.force));
    }

    //[CreateAssetMenu(fileName = "InfluenceData", menuName = "Game/InfluenceData")]
    //public class InfluenceData : ScriptableObject
    //{
    //    public string influenceName;
    //    public Sprite influenceSprite;
    //    public List<CharacterController> characters;
    //}
}
