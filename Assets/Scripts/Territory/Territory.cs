using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Territory : MonoBehaviour
{
    public enum AttackTerritoryType
    {
        desert,//砂漠
        wilderness,//荒野
        plain,//平原
        forest,//森
        fort,//砦
    }
    public AttackTerritoryType attackTerritoryType;

    public enum DefenceTerritoryType
    {
        desert,//砂漠
        wilderness,//荒野
        plain,//平原
        forest,//森
        fort,//砦
    }
    public DefenceTerritoryType defenceTerritoryType;

    //public Territory attackTerritory;
    //public Territory defenceTerritory;

    public Vector2 position;

    public Influence influence;
    [SerializeField] GameObject homeTerritory;

    public void ShowHomeTerritory(bool isActibe)
    {
        homeTerritory.SetActive(isActibe);
    }

    public void SetAttackTerritoryType(Territory territory)
    {
        // Enumの要素数を取得
        int enumLength = Enum.GetValues(typeof(AttackTerritoryType)).Length;

        // ランダムなインデックスを生成
        int randomIndex = Random.Range(0, enumLength);

        // ランダムなタイプを設定
        territory.attackTerritoryType = (AttackTerritoryType)randomIndex;
    }

    public void SetDefenceTerritoryType(Territory territory)
    {
        // Enumの要素数を取得
        int enumLength = Enum.GetValues(typeof(DefenceTerritoryType)).Length;

        // ランダムなインデックスを生成
        int randomIndex = Random.Range(0, enumLength);

        // ランダムなタイプを設定
        territory.defenceTerritoryType = (DefenceTerritoryType)randomIndex;
    }

    //public void ChangeColor(Color newColor)
    //{
    //    // SpriteRendererを取得
    //    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

    //    // SpriteRendererが存在する場合、色を変更
    //    if (spriteRenderer != null)
    //    {
    //        spriteRenderer.color = newColor;
    //    }
    //    else
    //    {
    //        Debug.LogError("SpriteRendererが見つかりませんでした。");
    //    }
    //}
}
