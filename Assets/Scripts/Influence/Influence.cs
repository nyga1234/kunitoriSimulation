using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
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
        List<Territory> attackableTerritoryList = new List<Territory>();
        foreach (Territory influenceTerritory in this.territoryList)
        {
            Territory rightTerritory = GameManager.instance.allTerritoryList.Find(x => x.position == influenceTerritory.position + Vector2.right);
            if (rightTerritory != null)
            {
                attackableTerritoryList.Add(rightTerritory);
            }
            Territory leftTerritory = GameManager.instance.allTerritoryList.Find(x => x.position == influenceTerritory.position + Vector2.left);
            if (leftTerritory != null)
            {
                attackableTerritoryList.Add(leftTerritory);
            }
            Territory downTerritory = GameManager.instance.allTerritoryList.Find(x => x.position == influenceTerritory.position + Vector2.down);
            if (downTerritory != null)
            {
                attackableTerritoryList.Add(downTerritory);
            }
            Territory upTerritory = GameManager.instance.allTerritoryList.Find(x => x.position == influenceTerritory.position + Vector2.up);
            if (upTerritory != null)
            {
                attackableTerritoryList.Add(upTerritory);
            }
        }

        return attackableTerritoryList;
    }

    // 名声の高い順に身分を設定(領主以外)
    public void SetRankByFame()
    {
        // 領主のランクは領主で固定
        //characterList.Find(c => c.characterModel.isLord).characterModel.rank = Rank.領主;

        // 領主以外のキャラクターを名声の高い順にソート
        List<CharacterController> sortedCharacters = characterList
            .Where(c => !c.characterModel.isLord)
            .OrderByDescending(c => c.characterModel.fame)
            .ToList();

        // 身分を順に設定
        for (int i = 0; i < sortedCharacters.Count; i++)
        {
            switch (i)
            {
                case 0:
                    sortedCharacters[i].characterModel.rank = Rank.補佐;
                    break;
                case 1:
                    sortedCharacters[i].characterModel.rank = Rank.大将;
                    break;
                case 2:
                    sortedCharacters[i].characterModel.rank = Rank.中将;
                    break;
                case 3:
                    sortedCharacters[i].characterModel.rank = Rank.少将;
                    break;
                case 4:
                    sortedCharacters[i].characterModel.rank = Rank.准将;
                    break;
            }
        }
        SortCharacterByRank(characterList);
    }

    // 身分の高い順にソート
    public void SortCharacterByRank(List<CharacterController> characterList)
    {
        this.characterList = characterList.OrderByDescending(c => c.characterModel.rank).ToList();
    }

    //勢力にTerritoryを追加するメソッド
    public void AddTerritory(Territory territory)
    {
        territoryList.Add(territory);

        // 領土に勢力の画像を設定
        SpriteRenderer territorySpriteRenderer = territory.GetComponent<SpriteRenderer>();
        if (territorySpriteRenderer != null && influenceImage != null)
        {
            territorySpriteRenderer.sprite = influenceImage;
        }
    }

    // Territoryを勢力から除外するメソッド
    public void RemoveTerritory(Territory territory)
    {
        territoryList.Remove(territory);
    }

    // キャラクターを勢力に追加するメソッド
    public void AddCharacter(CharacterController character)
    {
        // キャラクターを勢力に追加
        characterList.Add(character);
        character.SetInfluence(this);

        //キャラクターの身分を設定
        if (character.influence.influenceName != "NoneInfluence")
        {
            switch (this.characterList.Count)
            {
                case 1:
                    character.characterModel.rank = Rank.領主;
                    break;
                case 2:
                    character.characterModel.rank = Rank.補佐;
                    break;
                case 3:
                    character.characterModel.rank = Rank.大将;
                    break;
                case 4:
                    character.characterModel.rank = Rank.中将;
                    break;
                case 5:
                    character.characterModel.rank = Rank.少将;
                    break;
                case 6:
                    character.characterModel.rank = Rank.准将;
                    break;
            }
            SortCharacterByRank(characterList);
        }
        //キャラクターの給料%を設定
        if (character.influence.influenceName != "NoneInfluence")
        {
            character.CalcSalary();
        }

        //キャラクターの忠誠を設定
        if (character.influence.influenceName != "NoneInfluence" && character.characterModel.isLord != true)
        {
            character.CalcLoyalty();
        }
    }

    // キャラクターを勢力から除外するメソッド
    public void RemoveCharacter(CharacterController character)
    {
        characterList.Remove(character);
        character.characterModel.rank = Rank.浪士;
        //character.RemoveInfluence();
    }

    public int CalcTerritorySum(Influence influence)
    {
        //this.territorySum = territoryList.Count(territory => territory.influence.InfluenceType == influenceType);
        this.territorySum = influence.territoryList.Count;
        return this.territorySum;
    }

    public void CalcGoldSum(List<CharacterController> characterList)
    {
        int goldSum = 0;

        foreach (CharacterController character in characterList)
        {
            // 各キャラクターの所持金を合計に加算
            goldSum += character.characterModel.gold;
        }
        this.goldSum = goldSum;
    }

    public void CalcCharacterSum(List<CharacterController> characterList)
    {
        this.characterSum = characterList.Count;
    }

    public void CalcSoliderSum(List<CharacterController> characterList)
    {
        int soliderSum = 0;

        foreach (CharacterController character in characterList)
        {
            soliderSum += character.soliderList.Count;
        }
        this.soliderSum = soliderSum;
    }

    public void CalcForceSum(List<CharacterController> characterList)
    {
        int forceSum = 0;

        foreach(CharacterController character in characterList)
        {
            foreach (SoliderController solider in character.soliderList)
            {
                forceSum += solider.soliderModel.force;
            }
        }
        this.forceSum = forceSum;
    }
}
