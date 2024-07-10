using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BanishmentCommandUI : MonoBehaviour
{
    private Color originalUIColor; // Œ³‚Ì”wŒiF‚ğ•Û‚·‚é•Ï”
    [SerializeField] TextMeshProUGUI banishmentText;

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

    public void ShowBanishmentCommandUI()
    {
        ChangeBackgroundColor(originalUIColor);
        banishmentText.color = Color.white; // ”’F‚É•ÏX
        this.gameObject.SetActive(true);
        if (GameManager.instance.playerCharacter.influence.characterList.Count == 1)
        {
            banishmentText.color = new Color32(122, 122, 122, 255);
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
