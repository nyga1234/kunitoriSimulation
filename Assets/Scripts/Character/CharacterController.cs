using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.TextCore.Text;

//身分
public enum Rank
{
    浪士 = 0,
    准将 = 1,
    少将 = 2,
    中将 = 3,
    大将 = 4,
    補佐 = 5,
    領主 = 6,
}

[CreateAssetMenu(fileName = "Character", menuName = "CreateCharacter")]
public class CharacterController : ScriptableObject
{
    [Header("Constant Value")]
    public int characterId;
    public Sprite icon;
    public new string name;
    public int force; //戦闘
    public int inteli;//智謀
    public int tact;//手腕
    public int ambition;//野心

    [Header("Changing Value")]
    public Rank rank;//身分
    public int fame;//名声
    public int gold;//お金
    public int loyalty;//忠誠
    public int salary;//給料%
    public bool isLord;//領主か将軍か
    public bool isPlayerCharacter;
    public bool isAttackable = true;
    public bool isBattle = false;

    public Influence influence;
    public List<SoliderController> soliderList;
    public int soliderForceSum;

    //public void Init(int characterID)
    //{
    //    characterModel = new CharacterModel(characterID);
    //}

    // キャラクターを勢力に所属するメソッド
    public void SetInfluence(Influence influence)
    {
        this.influence = influence;
    }

    // キャラクターを勢力から除外するメソッド
    public void RemoveInfluence()
    {
        SetInfluence(GameMain.instance.noneInfluence);
    }

    public void AddSoliders(List<SoliderController> soliderList)
    {
        this.soliderList.AddRange(soliderList);
    }

    public void CalcSalary()
    {
        switch (influence.characterList.Count)
        {
            case 1:
                influence.characterList[0].salary = 100;
                break;
            case 2:
                influence.characterList[0].salary = 60;
                influence.characterList[1].salary = 40;
                CalcSalaryOnFame();
                break;
            case 3:
                influence.characterList[0].salary = 45;
                influence.characterList[1].salary = 30;
                influence.characterList[2].salary = 25;
                CalcSalaryOnFame();
                break;
            case 4:
                influence.characterList[0].salary = 35;
                influence.characterList[1].salary = 26;
                influence.characterList[2].salary = 22;
                influence.characterList[3].salary = 17;
                CalcSalaryOnFame();
                break;
            case 5:
                influence.characterList[0].salary = 28;
                influence.characterList[1].salary = 23;
                influence.characterList[2].salary = 19;
                influence.characterList[3].salary = 16;
                influence.characterList[4].salary = 14;
                CalcSalaryOnFame();
                break;
            case 6:
                influence.characterList[0].salary = 24;
                influence.characterList[1].salary = 20;
                influence.characterList[2].salary = 17;
                influence.characterList[3].salary = 15;
                influence.characterList[4].salary = 13;
                influence.characterList[5].salary = 11;
                CalcSalaryOnFame();
                break;
        }
    }

    //名声を基に給料%を計算
    public void CalcSalaryOnFame()
    {
        int totalSalary = 0;
        //各キャラクターの給料を名声に基づき計算
        foreach (CharacterController character in influence.characterList)
        {
            character.salary += (int)Mathf.Round(character.fame * 0.5f);
        }
        //キャラクターリストのトータル給料を取得
        totalSalary += influence.characterList.Sum(c => c.salary);
        //各キャラクターの給料をトータル給料から計算
        foreach (CharacterController character in influence.characterList)
        {
            character.salary = Mathf.RoundToInt((float)character.salary / totalSalary * 100);
        }
    }

    public void CalcLoyalty()
    {
        //野心を基に忠誠を計算
        int characterLoyalty;
        characterLoyalty = 100 - ambition;
        //身分を基に忠誠を計算
        switch (rank)
        {
            case Rank.補佐:
                characterLoyalty += 35;
                break;
            case Rank.大将:
                characterLoyalty += 30;
                break;
            case Rank.中将:
                characterLoyalty += 25;
                break;
            case Rank.少将:
                characterLoyalty += 20;
                break;
            case Rank.准将:
                characterLoyalty += 15;
                break;
        }
        //給料を基に忠誠を計算
        characterLoyalty += salary;
        //領主の手腕を基に忠誠を計算
        CharacterController lordCharacter = influence.characterList.Find(character => character.rank == Rank.領主);
        characterLoyalty += lordCharacter.tact - 90;

        loyalty = characterLoyalty;
        if (loyalty >= 100)
        {
            loyalty = 100;
        }
    }

    public void CalcSoliderForceSum()
    {
        foreach (SoliderController solider in soliderList)
        {
            soliderForceSum += solider.soliderModel.force;
        }
    }
}
