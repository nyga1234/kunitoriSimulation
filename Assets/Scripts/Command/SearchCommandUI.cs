using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SearchCommandUI : MonoBehaviour
{
    private Color originalUIColor; // Œ³‚Ì”wŒiF‚ğ•Û‚·‚é•Ï”

    [SerializeField] TextMeshProUGUI searchText;
    public GameManager gameManager;

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // Œ³‚Ì”wŒiF‚ğ•Û
            originalUIColor = image.color;
        }
    }

    public void ShowSearchCommandUI()
    {
        ChangeBackgroundColor(originalUIColor);
        searchText.color = Color.white; // ”’F‚É•ÏX
        this.gameObject.SetActive(true);

        //—Ì’n”‚É‰‚¶‚½’Tõ•¶š‚Ì”ñ•\¦İ’è
        switch (gameManager.playerCharacter.influence.territoryList.Count)
        {
            case 1:
            case 2:
                if (gameManager.playerCharacter.influence.characterList.Count >= 3 || gameManager.playerCharacter.characterModel.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//”ñ•\¦
                }
                break;
            case 3:
            case 4:
            case 5:
            case 6:
                if (gameManager.playerCharacter.influence.characterList.Count >= 4 || gameManager.playerCharacter.characterModel.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//”ñ•\¦
                }
                break;
            case 7:
            case 8:
            case 9:
            case 10:
                if (gameManager.playerCharacter.influence.characterList.Count >= 5 || gameManager.playerCharacter.characterModel.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//”ñ•\¦
                }
                break;
            case 11:
            case 12:
            case 13:
            case 14:
                if (gameManager.playerCharacter.influence.characterList.Count >= 6 || gameManager.playerCharacter.characterModel.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//”ñ•\¦
                }
                break;
        }
    }

    //”wŒiF‚ğ•ÏX
    private void ChangeBackgroundColor(Color color)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }
}
