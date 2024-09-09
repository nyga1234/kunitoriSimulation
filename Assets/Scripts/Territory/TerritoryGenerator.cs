using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TerritoryGenerator : MonoBehaviour
{
    [SerializeField] Territory plainTerritorPrefab;
    [SerializeField] Transform parent;

    const int WIDTH = 7;
    const int HEIGHT = 3;

    public const int territorySpace = 100;

    private Vector2 spaceOffset = new Vector2(WIDTH / 2 * territorySpace, HEIGHT / 2 * territorySpace);

    public Territory greenTerritory;
    public Territory redTerritory;
    public Territory blueTerritory;
    public Territory yellowTerritory;
    public Territory blackTerritory;
    public Territory noneTerritory;

    public List<Territory> InitializeTerritory()
    {
        List<Territory> initialTerritoriese = new List<Territory>();

        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                Vector2 pos = new Vector2(x * territorySpace, y * territorySpace) - spaceOffset;

                Territory newTerritory = Instantiate(plainTerritorPrefab, parent);

                newTerritory.position = new Vector2(pos.x, pos.y);
                newTerritory.GetComponent<RectTransform>().anchoredPosition = pos;
                newTerritory.gameObject.SetActive(true);
                initialTerritoriese.Add(newTerritory);
            }
        }
        return initialTerritoriese;
    }

    // ���͂ɉ����ė̓y��ݒ�
    public List<Territory> GenerateTerritory(List<Territory> territoryList, List<Influence> influenceList)
    {
        List<Territory> generateTerritoryList = new List<Territory>();

        int index = 0;

        for (int x = 0; x < WIDTH; x++)
        {
            for (int y = 0; y < HEIGHT; y++)
            {
                Territory territory = territoryList[index];
                Vector2 pos = new Vector2(x * territorySpace, y * territorySpace) - spaceOffset;

                if (x <= 2 && y == 2 || x == 0 && y == 1)
                {
                    SetupTerritory(territory, pos, influenceList, "���B�N�^�[");
                    generateTerritoryList.Add(territory);
                    //GameManager.instance.territoryCouont++;
                }
                else if (x <= 2 && y == 0 || x == 1 && y == 1)
                {
                    SetupTerritory(territory, pos, influenceList, "�A���V�A");
                    generateTerritoryList.Add(territory);
                    //GameManager.instance.territoryCouont++;
                }
                else if (x >= 4 && y == 2 || x == 5 && y == 1)
                {
                    SetupTerritory(territory, pos, influenceList, "�Z���M�E�X");
                    generateTerritoryList.Add(territory);
                    //GameManager.instance.territoryCouont++;
                }
                else if (x == 2 && y == 1 || x == 3 && y == 0 || x == 3 && y == 2 || x == 4 && y == 1)
                {
                    SetupTerritory(territory, pos, influenceList, "���[�����e�B�E�X");
                    generateTerritoryList.Add(territory);
                    //GameManager.instance.territoryCouont++;
                }
                else if (x >= 4 && y == 0 || x == 6 && y == 1)
                {
                    SetupTerritory(territory, pos, influenceList, "�t�F�I�h�[��");
                    generateTerritoryList.Add(territory);
                    //GameManager.instance.territoryCouont++;
                }
                else
                {
                    SetupTerritory(territory, pos, influenceList, "NoneInfluence");
                    generateTerritoryList.Add(territory);
                }

                index++;
            }
        }
        return generateTerritoryList;
    }

    //public List<Territory> Generate(List<Influence> influenceList)
    //{
    //    List<Territory> territoryList = new List<Territory>();

    //    for (int x = 0; x < WIDTH; x++)
    //    {
    //        for (int y = 0; y < HEIGHT; y++)
    //        {
    //            Vector2 pos = new Vector2(x * territorySpace, y * territorySpace) - spaceOffset;
    //            Territory newTerritory = Instantiate(plainTerritorPrefab, parent);
    //            newTerritory.GetComponent<RectTransform>().anchoredPosition = pos;
    //            newTerritory.gameObject.SetActive(true);

    //            if (x <= 2 && y == 2 || x == 0 && y == 1)
    //            {
    //                SetupTerritory(newTerritory, pos, influenceList, "���B�N�^�[");
    //                territoryList.Add(newTerritory);
    //                GameManager.instance.territoryCouont++;
    //            }
    //            else if (x <= 2 && y == 0 || x == 1 && y == 1)
    //            {
    //                SetupTerritory(newTerritory, pos, influenceList, "�A���V�A");
    //                territoryList.Add(newTerritory);
    //                GameManager.instance.territoryCouont++;

    //            }
    //            else if(x >= 4 && y == 2 || x == 5 && y == 1)
    //            {
    //                SetupTerritory(newTerritory, pos, influenceList, "�Z���M�E�X");
    //                territoryList.Add(newTerritory);
    //                GameManager.instance.territoryCouont++;
    //            }
    //            else if (x == 2 && y == 1 || x == 3 && y == 0 || x == 3 && y == 2 || x == 4 && y == 1)
    //            {
    //                SetupTerritory(newTerritory, pos, influenceList, "���[�����e�B�E�X");
    //                territoryList.Add(newTerritory);
    //                GameManager.instance.territoryCouont++;
    //            }
    //            else if (x >= 4 && y == 0 || x == 6 && y == 1)
    //            {
    //                SetupTerritory(newTerritory, pos, influenceList, "�t�F�I�h�[��");
    //                territoryList.Add(newTerritory);
    //                GameManager.instance.territoryCouont++;
    //            }
    //            else
    //            {
    //                SetupTerritory(newTerritory, pos, influenceList, "NoneInfluence");
    //                territoryList.Add(newTerritory);
    //            }
    //        }
    //    }
    //    return territoryList;
    //}

    public void SetupTerritory(Territory territory, Vector2 position, List<Influence> influenceList, string influenceName)
    {
        //�̓y�����J�E���g
        GameManager.instance.territoryCouont++;

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
