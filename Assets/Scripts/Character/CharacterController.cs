using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.TextCore.Text;

public class CharacterController : MonoBehaviour
{
    public CharacterModel characterModel;

    public Influence influence;

    public List<SoliderController> soliderList;

    public int soliderForceSum;

    public void Init(int characterID)
    {
        characterModel = new CharacterModel(characterID);
    }

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
                influence.characterList[0].characterModel.salary = 100;
                break;
            case 2:
                influence.characterList[0].characterModel.salary = 60;
                influence.characterList[1].characterModel.salary = 40;
                CalcSalaryOnFame();
                break;
            case 3:
                influence.characterList[0].characterModel.salary = 45;
                influence.characterList[1].characterModel.salary = 30;
                influence.characterList[2].characterModel.salary = 25;
                CalcSalaryOnFame();
                break;
            case 4:
                influence.characterList[0].characterModel.salary = 35;
                influence.characterList[1].characterModel.salary = 26;
                influence.characterList[2].characterModel.salary = 22;
                influence.characterList[3].characterModel.salary = 17;
                CalcSalaryOnFame();
                break;
            case 5:
                influence.characterList[0].characterModel.salary = 28;
                influence.characterList[1].characterModel.salary = 23;
                influence.characterList[2].characterModel.salary = 19;
                influence.characterList[3].characterModel.salary = 16;
                influence.characterList[4].characterModel.salary = 14;
                CalcSalaryOnFame();
                break;
            case 6:
                influence.characterList[0].characterModel.salary = 24;
                influence.characterList[1].characterModel.salary = 20;
                influence.characterList[2].characterModel.salary = 17;
                influence.characterList[3].characterModel.salary = 15;
                influence.characterList[4].characterModel.salary = 13;
                influence.characterList[5].characterModel.salary = 11;
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
            character.characterModel.salary += (int)Mathf.Round(character.characterModel.fame * 0.5f);
        }
        //キャラクターリストのトータル給料を取得
        totalSalary += influence.characterList.Sum(c => c.characterModel.salary);
        //各キャラクターの給料をトータル給料から計算
        foreach (CharacterController character in influence.characterList)
        {
            character.characterModel.salary = Mathf.RoundToInt((float)character.characterModel.salary / totalSalary * 100);
        }
    }

    public void CalcLoyalty()
    {
        //野心を基に忠誠を計算
        int characterLoyalty;
        characterLoyalty = 100 - characterModel.ambition;
        //身分を基に忠誠を計算
        switch (characterModel.rank)
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
        characterLoyalty += characterModel.salary;
        //領主の手腕を基に忠誠を計算
        CharacterController lordCharacter = influence.characterList.Find(character => character.characterModel.isLord);
        characterLoyalty += lordCharacter.characterModel.tact - 90;

        characterModel.loyalty = characterLoyalty;
        if (characterModel.loyalty >= 100)
        {
            characterModel.loyalty = 100;
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
