using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderModel
{
    public int soliderID;
    public int hp;
    public int df;
    public int maxHP;
    public int at;
    public int force;
    public Sprite icon;
    public int lv;
    public int experience;
    public bool isAlive;
    public int uniqueID;

    public SoliderModel(int soliderId, int soliderUniqueId)
    {
        SoldierEntity soldierEntity = Resources.Load<SoldierEntity>("SoldierEntityList/Solider" + soliderId);
        this.soliderID = soliderId;
        this.hp = soldierEntity.hp;
        this.maxHP = soldierEntity.maxHP;
        this.at = soldierEntity.at;
        this.df = soldierEntity.df;
        this.force = soldierEntity.force;
        this.icon = soldierEntity.icon;
        this.lv = soldierEntity.lv;
        this.experience = soldierEntity.experience;
        this.isAlive = true;
        this.uniqueID = soliderUniqueId;
    }

    void Damage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            isAlive = false;
        }
    }

    public void Attack(CharacterController attackChara, CharacterController defenceChara, SoliderController defenceSolider, Territory territory)
    {
        float attackSoliderAT = at * attackChara.characterModel.force;

        float defenceSoliderDF = 0.0f;
        switch (territory.defenceTerritoryType)
        {
            case Territory.DefenceTerritoryType.desert:
                defenceSoliderDF = defenceChara.characterModel.inteli * 0.5f;
                break;
            case Territory.DefenceTerritoryType.wilderness:
                defenceSoliderDF = defenceChara.characterModel.inteli * 0.75f;
                break;
            case Territory.DefenceTerritoryType.plain:
                defenceSoliderDF = defenceChara.characterModel.inteli * 1.0f;
                break;
            case Territory.DefenceTerritoryType.forest:
                defenceSoliderDF = defenceChara.characterModel.inteli * 1.25f;
                break;
            case Territory.DefenceTerritoryType.fort:
                defenceSoliderDF = defenceChara.characterModel.inteli * 1.5f;
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
        defenceSolider.soliderModel.Damage(damageInt);
    }

    public void CounterAttack(CharacterController attackChara, CharacterController defenceChara, SoliderController attackSolider, Territory territory)
    {
        float defenceSoliderAT = at * defenceChara.characterModel.force;

        float attackSoliderDF = 0.0f;
        switch (territory.attackTerritoryType)
        {
            case Territory.AttackTerritoryType.desert:
                attackSoliderDF = attackChara.characterModel.inteli * 0.8f;
                break;
            case Territory.AttackTerritoryType.wilderness:
                attackSoliderDF = attackChara.characterModel.inteli * 0.9f;
                break;
            case Territory.AttackTerritoryType.plain:
                attackSoliderDF = attackChara.characterModel.inteli * 1.0f;
                break;
            case Territory.AttackTerritoryType.forest:
                attackSoliderDF = attackChara.characterModel.inteli * 1.1f;
                break;
            case Territory.AttackTerritoryType.fort:
                attackSoliderDF = attackChara.characterModel.inteli * 1.2f;
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
        attackSolider.soliderModel.Damage(damageInt);
    }

    public void Training(SoliderController solider)
    {
        solider.soliderModel.experience += 10;

        if (solider.soliderModel.lv >= 8)
        {
            return;
        }
        else
        {
            // ���x���A�b�v�̏�����ǉ�
            while (solider.soliderModel.experience >= 100)
            {
                LevelUP(solider);
                solider.soliderModel.experience -= 100;
            }
        }
    }

    public void LevelUP(SoliderController solider)
    {
        solider.soliderModel.lv += 1;

        // ���x���A�b�v���ɃA�C�R����ύX
        UpdateSoliderOnLevelUp(solider);
    }

    private void UpdateSoliderOnLevelUp(SoliderController solider)
    {
        // SoliderEntityList/Solider �t�H���_����V�����A�C�R����ǂݍ���
        SoldierEntity soliderEntity = Resources.Load<SoldierEntity>("SoldierEntityList/Solider" + solider.soliderModel.lv);

        // �V�����A�C�R����ݒ�
        solider.soliderModel.icon = soliderEntity.icon;

        //�X�e�[�^�X�����x���A�b�v
        solider.soliderModel.maxHP = soliderEntity.maxHP;
        solider.soliderModel.at = soliderEntity.at;
        solider.soliderModel.force = soliderEntity.force;
    }
}