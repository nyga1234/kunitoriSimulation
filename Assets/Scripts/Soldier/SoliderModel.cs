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
        SoldierEntity soldierEntity = Resources.Load<SoldierEntity>("SoldierEntityList/Soldier" + soliderId);
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
        //ダメージの少数部分を取得
        float decimalPart = damageFloat % 1;
        //0から1の間でランダムな少数を生成
        float random = Random.Range(0f, 1f);

        //少数部分の確立に応じて小数点を切り上げ、切り捨て
        if (random <= decimalPart)
        {
            //ランダムな値より大きい場合は小数点以下を切り上げ
            damageInt = Mathf.CeilToInt(damageFloat);
        }
        else
        {
            //ランダムな値より小さい場合は小数点以下を切り捨て
            damageInt = Mathf.FloorToInt(damageFloat);
            if (damageInt == 0)
            {
                damageInt = 1;
            }
        }
        //ディフェンス側がダメージを受ける
        defenceSolider.soliderModel.Damage(damageInt);
    }

    public void CounterAttack(CharacterController attackChara, CharacterController defenceChara, SoliderController attackSolider, Territory territory)
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
        //ダメージの少数部分を取得
        float decimalPart = damageFloat % 1;
        //0から1の間でランダムな少数を生成
        float random = Random.Range(0f, 1f);

        //少数部分の確立に応じて小数点を切り上げ、切り捨て
        if (random <= decimalPart)
        {
            //ランダムな値より大きい場合は小数点以下を切り上げ
            damageInt = Mathf.CeilToInt(damageFloat);
        }
        else
        {
            //ランダムな値より小さい場合は小数点以下を切り捨て
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
            // レベルアップの条件を追加
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

        // レベルアップ時にアイコンを変更
        UpdateSoldierOnLevelUp(solider);
    }

    private void UpdateSoldierOnLevelUp(SoliderController solider)
    {
        // SoliderEntityList/Solider フォルダから新しいアイコンを読み込む
        SoldierEntity soldierEntity = Resources.Load<SoldierEntity>("SoldierEntityList/Soldier" + solider.soliderModel.lv);

        // 新しいアイコンを設定
        solider.soliderModel.icon = soldierEntity.icon;

        //ステータスをレベルアップ
        solider.soliderModel.maxHP = soldierEntity.maxHP;
        solider.soliderModel.at = soldierEntity.at;
        solider.soliderModel.force = soldierEntity.force;
    }
}
