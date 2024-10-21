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
        Debug.Log("ダメージは" + damage);
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
            // レベルアップの条件を追加
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

        // レベルアップ時にアイコンを変更
        UpdateSoldierOnLevelUp(solider);
    }

    private void UpdateSoldierOnLevelUp(SoldierController solider)
    {
        // SoliderEntityList/Solider フォルダから新しいアイコンを読み込む
        SoldierController soldierEntity = Resources.Load<SoldierController>("SoldierEntityList/Soldier" + solider.lv);

        // 新しいアイコンを設定
        solider.icon = soldierEntity.icon;

        //ステータスをレベルアップ
        solider.maxHP = soldierEntity.maxHP;
        solider.at = soldierEntity.at;
        solider.force = soldierEntity.force;
    }
}
