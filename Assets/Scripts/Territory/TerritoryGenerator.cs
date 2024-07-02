using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Territory;

public class TerritoryGenerator : MonoBehaviour
{
    [SerializeField] Territory plainTerritorPrefab;
    [SerializeField] Transform parent;

    const int WIDTH = 7;
    const int HEIGHT = 3;

    const int screenWidth = 1920;
    const int screenHeight = 1080;

    public const int territorySpace = 100;

    public Territory greenTerritory;
    public Territory redTerritory;
    public Territory blueTerritory;
    public Territory yellowTerritory;
    public Territory blackTerritory;
    public Territory noneTerritory;

    public List<Territory> Generate(List<Influence> influenceList)
    {
        List<Territory> territoryList = new List<Territory>();

        //Vector2 screenOffset = new Vector2(screenWidth / 2, screenHeight / 2);
        Vector2 spaceOffset = new Vector2(WIDTH / 2 * territorySpace, HEIGHT / 2 * territorySpace);
        //Vector2 offset = new Vector2(0, 90);

        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                //Vector2  pos = new Vector2(x * territorySpace, y * territorySpace) + screenOffset - spaceOffset + offset;
                Vector2 pos = new Vector2(x * territorySpace, y * territorySpace) - spaceOffset;
                Territory newTerritory = Instantiate(plainTerritorPrefab, parent);
                RectTransform rectTransform = newTerritory.GetComponent<RectTransform>();
                rectTransform.anchoredPosition = pos;
                newTerritory.gameObject.SetActive(true);

                if (x <= 2 && y == 2 || x == 0 && y == 1)
                {
                    SetupTerritory(newTerritory, pos, influenceList, "���B�N�^�[");
                    territoryList.Add(newTerritory);
                    GameManager.instance.territoryCouont++;
                    //greenTerritory = Instantiate(plainTerritorPrefab, pos, Quaternion.identity, parent);
                    //greenTerritory.gameObject.SetActive(true);
                    //SetupTerritory(greenTerritory, pos, influenceList, "���B�N�^�[");
                    //territoryList.Add(greenTerritory);
                    //GameManager.instance.territoryCouont++;
                }
                else if (x <= 2 && y == 0 || x == 1 && y == 1)
                {
                    SetupTerritory(newTerritory, pos, influenceList, "�A���V�A");
                    territoryList.Add(newTerritory);
                    GameManager.instance.territoryCouont++;

                }
                else if(x >= 4 && y == 2 || x == 5 && y == 1)
                {
                    SetupTerritory(newTerritory, pos, influenceList, "�Z���M�E�X");
                    territoryList.Add(newTerritory);
                    GameManager.instance.territoryCouont++;
                }
                else if (x == 2 && y == 1 || x == 3 && y == 0 || x == 3 && y == 2 || x == 4 && y == 1)
                {
                    SetupTerritory(newTerritory, pos, influenceList, "���[�����e�B�E�X");
                    territoryList.Add(newTerritory);
                    GameManager.instance.territoryCouont++;
                }
                else if (x >= 4 && y == 0 || x == 6 && y == 1)
                {
                    SetupTerritory(newTerritory, pos, influenceList, "�t�F�I�h�[��");
                    territoryList.Add(newTerritory);
                    GameManager.instance.territoryCouont++;
                }
                else
                {
                    SetupTerritory(newTerritory, pos, influenceList, "NoneInfluence");
                    territoryList.Add(newTerritory);
                }
            }
            //parent.gameObject.SetActive(false);
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
