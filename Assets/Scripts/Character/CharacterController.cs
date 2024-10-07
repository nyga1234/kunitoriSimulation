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

    // �L�����N�^�[�𐨗͂ɏ������郁�\�b�h
    public void SetInfluence(Influence influence)
    {
        this.influence = influence;
    }

    // �L�����N�^�[�𐨗͂��珜�O���郁�\�b�h
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

    //��������ɋ���%���v�Z
    public void CalcSalaryOnFame()
    {
        int totalSalary = 0;
        //�e�L�����N�^�[�̋����𖼐��Ɋ�Â��v�Z
        foreach (CharacterController character in influence.characterList)
        {
            character.characterModel.salary += (int)Mathf.Round(character.characterModel.fame * 0.5f);
        }
        //�L�����N�^�[���X�g�̃g�[�^���������擾
        totalSalary += influence.characterList.Sum(c => c.characterModel.salary);
        //�e�L�����N�^�[�̋������g�[�^����������v�Z
        foreach (CharacterController character in influence.characterList)
        {
            character.characterModel.salary = Mathf.RoundToInt((float)character.characterModel.salary / totalSalary * 100);
        }
    }

    public void CalcLoyalty()
    {
        //��S����ɒ������v�Z
        int characterLoyalty;
        characterLoyalty = 100 - characterModel.ambition;
        //�g������ɒ������v�Z
        switch (characterModel.rank)
        {
            case Rank.�⍲:
                characterLoyalty += 35;
                break;
            case Rank.�叫:
                characterLoyalty += 30;
                break;
            case Rank.����:
                characterLoyalty += 25;
                break;
            case Rank.����:
                characterLoyalty += 20;
                break;
            case Rank.�y��:
                characterLoyalty += 15;
                break;
        }
        //��������ɒ������v�Z
        characterLoyalty += characterModel.salary;
        //�̎�̎�r����ɒ������v�Z
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
