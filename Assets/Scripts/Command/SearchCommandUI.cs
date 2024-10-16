using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SearchCommandUI : MonoBehaviour
{
    private Color originalUIColor; // 元の背景色を保持する変数

    [SerializeField] TextMeshProUGUI searchText;
    public GameMain gameManager;

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // 元の背景色を保持
            originalUIColor = image.color;
        }
    }

    public void ShowSearchCommandUI()
    {
        ChangeBackgroundColor(originalUIColor);
        searchText.color = Color.white; // 白色に変更
        this.gameObject.SetActive(true);

        //領地数に応じた探索文字の非表示設定
        switch (gameManager.playerCharacter.influence.territoryList.Count)
        {
            case 1:
            case 2:
                if (gameManager.playerCharacter.influence.characterList.Count >= 3 || gameManager.playerCharacter.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//非表示
                }
                break;
            case 3:
            case 4:
            case 5:
            case 6:
                if (gameManager.playerCharacter.influence.characterList.Count >= 4 || gameManager.playerCharacter.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//非表示
                }
                break;
            case 7:
            case 8:
            case 9:
            case 10:
                if (gameManager.playerCharacter.influence.characterList.Count >= 5 || gameManager.playerCharacter.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//非表示
                }
                break;
            case 11:
            case 12:
            case 13:
            case 14:
                if (gameManager.playerCharacter.influence.characterList.Count >= 6 || gameManager.playerCharacter.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//非表示
                }
                break;
        }
    }

    //背景色を変更
    private void ChangeBackgroundColor(Color color)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }
}
