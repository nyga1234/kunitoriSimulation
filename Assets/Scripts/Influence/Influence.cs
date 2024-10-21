using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "Influence", menuName = "CreateInfluence")]
public class Influence : ScriptableObject
{
    [Header("Constant Value")]
    public string influenceName;
    public Sprite influenceImage;

    [Header("Changing Value")]
    public int territorySum;
    public int goldSum;
    public int characterSum;
    public int soliderSum;
    public int forceSum;
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

    public void Initialize()
    {
        territorySum = 0;
        goldSum = 0;
        characterSum = 0;
        soliderSum = 0;
        forceSum = 0;
        characterList.Clear();
    }

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
        //characterList.Find(c => c.isLord).rank = Rank.�̎�;

        // �̎�ȊO�̃L�����N�^�[�𖼐��̍������Ƀ\�[�g
        List<CharacterController> sortedCharacters = characterList
            .Where(c => !c.isLord)
            .OrderByDescending(c => c.fame)
            .ToList();

        // �g�������ɐݒ�
        for (int i = 0; i < sortedCharacters.Count; i++)
        {
            switch (i)
            {
                case 0:
                    sortedCharacters[i].rank = Rank.�⍲;
                    break;
                case 1:
                    sortedCharacters[i].rank = Rank.�叫;
                    break;
                case 2:
                    sortedCharacters[i].rank = Rank.����;
                    break;
                case 3:
                    sortedCharacters[i].rank = Rank.����;
                    break;
                case 4:
                    sortedCharacters[i].rank = Rank.�y��;
                    break;
            }
        }
        SortCharacterByRank(characterList);
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
                    character.rank = Rank.�̎�;
                    break;
                case 2:
                    character.rank = Rank.�⍲;
                    break;
                case 3:
                    character.rank = Rank.�叫;
                    break;
                case 4:
                    character.rank = Rank.����;
                    break;
                case 5:
                    character.rank = Rank.����;
                    break;
                case 6:
                    character.rank = Rank.�y��;
                    break;
            }
            SortCharacterByRank(characterList);
        }

        if (character.influence.influenceName != "NoneInfluence")
        {
            //�L�����N�^�[�̋���%��ݒ�
            character.CalcSalary();
            //�L�����N�^�[�̒�����ݒ�
            if (!character.isLord)
            {
                character.CalcLoyalty();
            }
        }
    }

    // �g���̍������Ƀ\�[�g
    public void SortCharacterByRank(List<CharacterController> characterList)
    {
        this.characterList = characterList.OrderByDescending(c => c.rank).ToList();
    }

    // �L�����N�^�[�𐨗͂��珜�O���郁�\�b�h
    public void RemoveCharacter(CharacterController character)
    {
        characterList.Remove(character);
        character.rank = Rank.�Q�m;
        //character.RemoveInfluence();
    }

    public void UpdateSums(Influence influence)
    {
        this.territorySum = influence.territoryList.Count;
        this.characterSum = influence.characterList.Count;
        this.goldSum = influence.characterList.Sum(c => c.gold);
        this.soliderSum = influence.characterList.Sum(c => c.soliderList.Count);
        this.forceSum = influence.characterList.Sum(c => c.soliderList.Sum(s => s.force));
    }

    // �R�s�[�R���X�g���N�^�̒ǉ�
    public Influence(Influence original)
    {
        influenceName = original.influenceName;
        influenceImage = original.influenceImage;
        territorySum = original.territorySum;
        goldSum = original.goldSum;
        characterSum = original.characterSum;
        soliderSum = original.soliderSum;
        forceSum = original.forceSum;

        // ���X�g��V�����쐬���ăR�s�[
        characterList = new List<CharacterController>(original.characterList);
        territoryList = new List<Territory>(original.territoryList);
    }

}
