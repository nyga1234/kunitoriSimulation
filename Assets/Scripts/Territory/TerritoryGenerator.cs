using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Territory;

public class TerritoryGenerator : MonoBehaviour
{
    [SerializeField] Territory plainTerritorPrefab;
    [SerializeField] Transform mapField;

    const int WIDTH = 7;
    const int HEIGHT = 3;

    const int screenWidth = 1920;
    const int screenHeight = 1080;

    const int space = 100;

    public Territory greenTerritory;
    public Territory redTerritory;
    public Territory blueTerritory;
    public Territory yellowTerritory;
    public Territory blackTerritory;
    public Territory noneTerritory;

    public List<Territory> Generate(List<Influence> influenceList)
    {
        List<Territory> territoryList = new List<Territory>();

        //Vector2 offset = new Vector2((float)(WIDTH / 2), (float)(HEIGHT / 2 - 1));
        Vector2 screenOffset = new Vector2(screenWidth / 2, screenHeight / 2);
        Vector2 spaceOffset = new Vector2(WIDTH / 2 * space, HEIGHT / 2 * space);
        Vector2 offset = new Vector2(0, 90);
        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                //Vector2 pos = new Vector2(x, y) - offset;
                Vector2  pos = new Vector2(x * space, y * space) + screenOffset - spaceOffset + offset;
                if (x <= 2 && y == 2 || x == 0 && y == 1)
                {
                    greenTerritory = Instantiate(plainTerritorPrefab, pos, Quaternion.identity, mapField);
                    greenTerritory.gameObject.SetActive(true);
                    SetupTerritory(greenTerritory, pos, influenceList, "ヴィクター");
                    territoryList.Add(greenTerritory);
                    GameManager.instance.territoryCouont++;
                }
                else if (x <= 2 && y == 0 || x == 1 && y == 1)
                {
                    redTerritory = Instantiate(plainTerritorPrefab, pos, Quaternion.identity, mapField);
                    redTerritory.gameObject.SetActive(true);
                    SetupTerritory(redTerritory, pos, influenceList, "アリシア");
                    territoryList.Add(redTerritory);
                    GameManager.instance.territoryCouont++;
                }
                else if(x >= 4 && y == 2 || x == 5 && y == 1)
                {
                    blueTerritory = Instantiate(plainTerritorPrefab, pos, Quaternion.identity, mapField);
                    blueTerritory.gameObject.SetActive(true);
                    SetupTerritory(blueTerritory, pos, influenceList, "セルギウス");
                    territoryList.Add(blueTerritory);
                    GameManager.instance.territoryCouont++;
                }
                else if (x == 2 && y == 1 || x == 3 && y == 0 || x == 3 && y == 2 || x == 4 && y == 1)
                {
                    yellowTerritory = Instantiate(plainTerritorPrefab, pos, Quaternion.identity, mapField);
                    yellowTerritory.gameObject.SetActive(true);
                    SetupTerritory(yellowTerritory, pos, influenceList, "ローレンティウス");
                    territoryList.Add(yellowTerritory);
                    GameManager.instance.territoryCouont++;
                }
                else if (x >= 4 && y == 0 || x == 6 && y == 1)
                {
                    blackTerritory = Instantiate(plainTerritorPrefab, pos, Quaternion.identity, mapField);
                    blackTerritory.gameObject.SetActive(true);
                    SetupTerritory(blackTerritory, pos, influenceList, "フェオドーラ");
                    territoryList.Add(blackTerritory);
                    GameManager.instance.territoryCouont++;
                }
                else
                {
                    noneTerritory = Instantiate(plainTerritorPrefab, pos, Quaternion.identity, mapField);
                    noneTerritory.gameObject.SetActive(true);
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
        //領土に座標を設定
        territory.position = new Vector2(position.x, position.y);
        //領土に勢力を設定
        territory.influence = influenceList.Find(influence => influence.influenceName == influenceName);

        //勢力に領土を設定
        //influenceList.Find(influence => influence.influenceName == influenceName)?.AddTerritory(territory);
        Influence foundInfluence = influenceList.Find(influence => influence.influenceName == influenceName);
        if (foundInfluence != null)
        {
            foundInfluence.AddTerritory(territory);
        }

        //攻撃領土を設定
        territory.SetAttackTerritoryType(territory);
        //防御領土を設定
        territory.SetDefenceTerritoryType(territory);
    }
}
