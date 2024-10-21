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
            new Vector2(spaceTerritory, 0),   // 右
            new Vector2(-spaceTerritory, 0),  // 左
            new Vector2(0, -spaceTerritory),   // 下
            new Vector2(0, spaceTerritory)   // 上
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

    // 名声の高い順に身分を設定(領主以外)
    public void SetRankByFame()
    {
        // 領主のランクは領主で固定
        //characterList.Find(c => c.isLord).rank = Rank.領主;

        // 領主以外のキャラクターを名声の高い順にソート
        List<CharacterController> sortedCharacters = characterList
            .Where(c => !c.isLord)
            .OrderByDescending(c => c.fame)
            .ToList();

        // 身分を順に設定
        for (int i = 0; i < sortedCharacters.Count; i++)
        {
            switch (i)
            {
                case 0:
                    sortedCharacters[i].rank = Rank.補佐;
                    break;
                case 1:
                    sortedCharacters[i].rank = Rank.大将;
                    break;
                case 2:
                    sortedCharacters[i].rank = Rank.中将;
                    break;
                case 3:
                    sortedCharacters[i].rank = Rank.少将;
                    break;
                case 4:
                    sortedCharacters[i].rank = Rank.准将;
                    break;
            }
        }
        SortCharacterByRank(characterList);
    }

    //勢力にTerritoryを追加するメソッド
    public void AddTerritory(Territory territory)
    {
        territoryList.Add(territory);

        //領土に勢力の画像を設定
        Image territoryImage = territory.GetComponent<Image>();
        if (territoryImage != null && influenceImage != null)
        {
            territoryImage.sprite = influenceImage;
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
                    character.rank = Rank.領主;
                    break;
                case 2:
                    character.rank = Rank.補佐;
                    break;
                case 3:
                    character.rank = Rank.大将;
                    break;
                case 4:
                    character.rank = Rank.中将;
                    break;
                case 5:
                    character.rank = Rank.少将;
                    break;
                case 6:
                    character.rank = Rank.准将;
                    break;
            }
            SortCharacterByRank(characterList);
        }

        if (character.influence.influenceName != "NoneInfluence")
        {
            //キャラクターの給料%を設定
            character.CalcSalary();
            //キャラクターの忠誠を設定
            if (!character.isLord)
            {
                character.CalcLoyalty();
            }
        }
    }

    // 身分の高い順にソート
    public void SortCharacterByRank(List<CharacterController> characterList)
    {
        this.characterList = characterList.OrderByDescending(c => c.rank).ToList();
    }

    // キャラクターを勢力から除外するメソッド
    public void RemoveCharacter(CharacterController character)
    {
        characterList.Remove(character);
        character.rank = Rank.浪士;
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

    // コピーコンストラクタの追加
    public Influence(Influence original)
    {
        influenceName = original.influenceName;
        influenceImage = original.influenceImage;
        territorySum = original.territorySum;
        goldSum = original.goldSum;
        characterSum = original.characterSum;
        soliderSum = original.soliderSum;
        forceSum = original.forceSum;

        // リストを新しく作成してコピー
        characterList = new List<CharacterController>(original.characterList);
        territoryList = new List<Territory>(original.territoryList);
    }

}
