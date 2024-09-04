using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Territory : MonoBehaviour
{
    public enum AttackTerritoryType
    {
        desert,//����
        wilderness,//�r��
        plain,//����
        forest,//�X
        fort,//��
    }
    public AttackTerritoryType attackTerritoryType;

    public enum DefenceTerritoryType
    {
        desert,//����
        wilderness,//�r��
        plain,//����
        forest,//�X
        fort,//��
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
        // Enum�̗v�f�����擾
        int enumLength = Enum.GetValues(typeof(AttackTerritoryType)).Length;

        // �����_���ȃC���f�b�N�X�𐶐�
        int randomIndex = Random.Range(0, enumLength);

        // �����_���ȃ^�C�v��ݒ�
        territory.attackTerritoryType = (AttackTerritoryType)randomIndex;
    }

    public void SetDefenceTerritoryType(Territory territory)
    {
        // Enum�̗v�f�����擾
        int enumLength = Enum.GetValues(typeof(DefenceTerritoryType)).Length;

        // �����_���ȃC���f�b�N�X�𐶐�
        int randomIndex = Random.Range(0, enumLength);

        // �����_���ȃ^�C�v��ݒ�
        territory.defenceTerritoryType = (DefenceTerritoryType)randomIndex;
    }

    //public void ChangeColor(Color newColor)
    //{
    //    // SpriteRenderer���擾
    //    SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

    //    // SpriteRenderer�����݂���ꍇ�A�F��ύX
    //    if (spriteRenderer != null)
    //    {
    //        spriteRenderer.color = newColor;
    //    }
    //    else
    //    {
    //        Debug.LogError("SpriteRenderer��������܂���ł����B");
    //    }
    //}
}
