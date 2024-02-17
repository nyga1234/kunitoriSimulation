using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Territory;

public class TerritoryGenerator : MonoBehaviour
{
    //[SerializeField] Territory greenTerritorPrefab;
    //[SerializeField] Territory blueTerritorPrefab;
    //[SerializeField] Territory redTerritorPrefab;
    //[SerializeField] Territory noneTerritorPrefab;
    [SerializeField] Territory plainTerritorPrefab;
    [SerializeField] Transform mapField;

    const int WIDTH = 7;
    const int HEIGHT = 3;

    public Territory greenTerritory;
    public Territory redTerritory;
    public Territory blueTerritory;
    public Territory yellowTerritory;
    public Territory blackTerritory;
    public Territory noneTerritory;

    public List<Territory> Generate(List<Influence> influenceList)
    {
        List<Territory> territoryList = new List<Territory>();
        //Territory greenTerritory = null;
        //Territory blueTerritory = null;
        //Territory redTerritory = null;
        //Territory noneTerritory = null;

        Vector2 offset = new Vector2((float)(WIDTH / 2), (float)(HEIGHT / 2 - 1));
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                Vector2 pos = new Vector2(x, y) - offset;
                if (x <= 2 && y == 2 || x == 0 && y == 1)
                {
                    greenTerritory = Instantiate(plainTerritorPrefab, pos, Quaternion.identity, mapField);
                    SetupTerritory(greenTerritory, pos, influenceList, "���B�N�^�[");
                    territoryList.Add(greenTerritory);
                    GameManager.instance.territoryCouont++;
                }
                else if (x <= 2 && y == 0 || x == 1 && y == 1)
                {
                    redTerritory = Instantiate(plainTerritorPrefab, pos, Quaternion.identity, mapField);
                    SetupTerritory(redTerritory, pos, influenceList, "�A���V�A");
                    territoryList.Add(redTerritory);
                    GameManager.instance.territoryCouont++;
                }
                else if(x >= 4 && y == 2 || x == 5 && y == 1)
                {
                    blueTerritory = Instantiate(plainTerritorPrefab, pos, Quaternion.identity, mapField);
                    SetupTerritory(blueTerritory, pos, influenceList, "�Z���M�E�X");
                    territoryList.Add(blueTerritory);
                    GameManager.instance.territoryCouont++;
                }
                else if (x == 2 && y == 1 || x == 3 && y == 0 || x == 3 && y == 2 || x == 4 && y == 1)
                {
                    yellowTerritory = Instantiate(plainTerritorPrefab, pos, Quaternion.identity, mapField);
                    SetupTerritory(yellowTerritory, pos, influenceList, "���[�����e�B�E�X");
                    territoryList.Add(yellowTerritory);
                    GameManager.instance.territoryCouont++;
                }
                else if (x >= 4 && y == 0 || x == 6 && y == 1)
                {
                    blackTerritory = Instantiate(plainTerritorPrefab, pos, Quaternion.identity, mapField);
                    SetupTerritory(blackTerritory, pos, influenceList, "�t�F�I�h�[��");
                    territoryList.Add(blackTerritory);
                    GameManager.instance.territoryCouont++;
                }
                else
                {
                    noneTerritory = Instantiate(plainTerritorPrefab, pos, Quaternion.identity, mapField);
                    SetupTerritory(noneTerritory, pos, influenceList, "NoneInfluence");
                    territoryList.Add(noneTerritory);
                }
            }
            mapField.gameObject.SetActive(false);
        }
        return territoryList;
    }

    private void SetupTerritory(Territory territory, Vector2 position, List<Influence> influenceList, string influenceName)
    {
        //�̓y�ɍ��W��ݒ�
        territory.position = new Vector2(position.x, position.y);
        //�̓y�ɐ��͂�ݒ�
        territory.influence = influenceList.Find(influence => influence.influenceName == influenceName);

        //���͂ɗ̓y��ݒ�
        //influenceList.Find(influence => influence.influenceName == influenceName)?.AddTerritory(territory);
        Influence foundInfluence = influenceList.Find(influence => influence.influenceName == influenceName);
        if (foundInfluence != null)
        {
            foundInfluence.AddTerritory(territory);
        }

        //�U���̓y��ݒ�
        territory.SetAttackTerritoryType(territory);
        //�h��̓y��ݒ�
        territory.SetDefenceTerritoryType(territory);
    }
}
