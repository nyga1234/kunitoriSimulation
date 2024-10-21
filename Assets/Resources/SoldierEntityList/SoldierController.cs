using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Soldier Param", fileName = "Soldier")]
public class SoldierController : ScriptableObject
{
    //public SoliderModel soliderModel;
    //public CharacterController character;

    [Header("Initial Set Value")]
    public int soliderID;
    public Sprite icon;
    public int lv;
    public int maxHP;
    public int hp;
    public int at;
    public int df;
    public int force;
    
    [Header("Later Set Value")]
    public int experience;
    public bool isAlive;
    public int uniqueID;

    //public void Init(int soliderId, int soliderUniqueId)
    //{
    //    soliderModel = new SoliderModel(soliderId, soliderUniqueId);
    //}

    void Damage(int damage)
    {
        Debug.Log("�_���[�W��" + damage);
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            isAlive = false;
        }
    }

    public void Attack(CharacterController attackChara, CharacterController defenceChara, SoldierController defenceSolider, Territory territory)
    {
        float attackSoliderAT = at * attackChara.force;

        float defenceSoliderDF = 0.0f;
        switch (territory.defenceTerritoryType)
        {
            case Territory.DefenceTerritoryType.desert:
                defenceSoliderDF = defenceChara.inteli * 0.5f;
                break;
            case Territory.DefenceTerritoryType.wilderness:
                defenceSoliderDF = defenceChara.inteli * 0.75f;
                break;
            case Territory.DefenceTerritoryType.plain:
                defenceSoliderDF = defenceChara.inteli * 1.0f;
                break;
            case Territory.DefenceTerritoryType.forest:
                defenceSoliderDF = defenceChara.inteli * 1.25f;
                break;
            case Territory.DefenceTerritoryType.fort:
                defenceSoliderDF = defenceChara.inteli * 1.5f;
                break;
        }

        int damageInt;
        float damageFloat = attackSoliderAT / defenceSoliderDF;
        //�_���[�W�̏����������擾
        float decimalPart = damageFloat % 1;
        //0����1�̊ԂŃ����_���ȏ����𐶐�
        float random = Random.Range(0f, 1f);

        //���������̊m���ɉ����ď����_��؂�グ�A�؂�̂�
        if (random <= decimalPart)
        {
            //�����_���Ȓl���傫���ꍇ�͏����_�ȉ���؂�グ
            damageInt = Mathf.CeilToInt(damageFloat);
        }
        else
        {
            //�����_���Ȓl��菬�����ꍇ�͏����_�ȉ���؂�̂�
            damageInt = Mathf.FloorToInt(damageFloat);
            if (damageInt == 0)
            {
                damageInt = 1;
            }
        }
        //�f�B�t�F���X�����_���[�W���󂯂�
        defenceSolider.Damage(damageInt);
    }

    public void CounterAttack(CharacterController attackChara, CharacterController defenceChara, SoldierController attackSolider, Territory territory)
    {
        float defenceSoliderAT = at * defenceChara.force;

        float attackSoliderDF = 0.0f;
        switch (territory.attackTerritoryType)
        {
            case Territory.AttackTerritoryType.desert:
                attackSoliderDF = attackChara.inteli * 0.8f;
                break;
            case Territory.AttackTerritoryType.wilderness:
                attackSoliderDF = attackChara.inteli * 0.9f;
                break;
            case Territory.AttackTerritoryType.plain:
                attackSoliderDF = attackChara.inteli * 1.0f;
                break;
            case Territory.AttackTerritoryType.forest:
                attackSoliderDF = attackChara.inteli * 1.1f;
                break;
            case Territory.AttackTerritoryType.fort:
                attackSoliderDF = attackChara.inteli * 1.2f;
                break;
        }

        int damageInt;
        float damageFloat = defenceSoliderAT / attackSoliderDF;
        //�_���[�W�̏����������擾
        float decimalPart = damageFloat % 1;
        //0����1�̊ԂŃ����_���ȏ����𐶐�
        float random = Random.Range(0f, 1f);

        //���������̊m���ɉ����ď����_��؂�グ�A�؂�̂�
        if (random <= decimalPart)
        {
            //�����_���Ȓl���傫���ꍇ�͏����_�ȉ���؂�グ
            damageInt = Mathf.CeilToInt(damageFloat);
        }
        else
        {
            //�����_���Ȓl��菬�����ꍇ�͏����_�ȉ���؂�̂�
            damageInt = Mathf.FloorToInt(damageFloat);
            if (damageInt == 0)
            {
                damageInt = 1;
            }
        }
        attackSolider.Damage(damageInt);
    }

    public void Training(SoldierController solider)
    {
        solider.experience += 10;

        if (solider.lv >= 8)
        {
            return;
        }
        else
        {
            // ���x���A�b�v�̏�����ǉ�
            while (solider.experience >= 100)
            {
                LevelUP(solider);
                solider.experience -= 100;
            }
        }
    }

    public void LevelUP(SoldierController solider)
    {
        solider.lv += 1;

        // ���x���A�b�v���ɃA�C�R����ύX
        UpdateSoldierOnLevelUp(solider);
    }

    private void UpdateSoldierOnLevelUp(SoldierController solider)
    {
        // SoliderEntityList/Solider �t�H���_����V�����A�C�R����ǂݍ���
        SoldierController soldierEntity = Resources.Load<SoldierController>("SoldierEntityList/Soldier" + solider.lv);

        // �V�����A�C�R����ݒ�
        solider.icon = soldierEntity.icon;

        //�X�e�[�^�X�����x���A�b�v
        solider.maxHP = soldierEntity.maxHP;
        solider.at = soldierEntity.at;
        solider.force = soldierEntity.force;
    }
}
