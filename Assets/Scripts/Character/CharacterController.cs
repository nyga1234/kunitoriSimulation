using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.TextCore.Text;

//�g��
public enum Rank
{
    �Q�m = 0,
    �y�� = 1,
    ���� = 2,
    ���� = 3,
    �叫 = 4,
    �⍲ = 5,
    �̎� = 6,
}

[CreateAssetMenu(fileName = "Character", menuName = "CreateCharacter")]
public class CharacterController : ScriptableObject
{
    [Header("Constant Value")]
    public int characterId;
    public Sprite icon;
    public new string name;
    public int force; //�퓬
    public int inteli;//�q�d
    public int tact;//��r
    public int ambition;//��S

    [Header("Changing Value")]
    public Rank rank;//�g��
    public int fame;//����
    public int gold;//����
    public int loyalty;//����
    public int salary;//����%
    public bool isLord;//�̎傩���R��
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

    //��������ɋ���%���v�Z
    public void CalcSalaryOnFame()
    {
        int totalSalary = 0;
        //�e�L�����N�^�[�̋����𖼐��Ɋ�Â��v�Z
        foreach (CharacterController character in influence.characterList)
        {
            character.salary += (int)Mathf.Round(character.fame * 0.5f);
        }
        //�L�����N�^�[���X�g�̃g�[�^���������擾
        totalSalary += influence.characterList.Sum(c => c.salary);
        //�e�L�����N�^�[�̋������g�[�^����������v�Z
        foreach (CharacterController character in influence.characterList)
        {
            character.salary = Mathf.RoundToInt((float)character.salary / totalSalary * 100);
        }
    }

    public void CalcLoyalty()
    {
        //��S����ɒ������v�Z
        int characterLoyalty;
        characterLoyalty = 100 - ambition;
        //�g������ɒ������v�Z
        switch (rank)
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
        characterLoyalty += salary;
        //�̎�̎�r����ɒ������v�Z
        CharacterController lordCharacter = influence.characterList.Find(character => character.rank == Rank.�̎�);
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
