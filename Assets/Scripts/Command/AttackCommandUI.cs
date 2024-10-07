using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackCommandUI : MonoBehaviour
{
    private Color originalUIColor; // Œ³‚Ì”wŒiF‚ğ•Û‚·‚é•Ï”

    [SerializeField] TextMeshProUGUI attackText;
    public GameMain gameManager;

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // Œ³‚Ì”wŒiF‚ğ•Û
            originalUIColor = image.color;
        }
    }

    public void ShowAttackCommandUI()
    {
        ChangeBackgroundColor(originalUIColor);
        attackText.color = Color.white; // ”’F‚É•ÏX
        this.gameObject.SetActive(true);

        if (GameMain.instance.playerCharacter.characterModel.gold < 3)
        {
            attackText.color = new Color32(122, 122, 122, 255);
        }

        if (GameMain.instance.playerCharacter.characterModel.isLord == false && GameMain.instance.playerCharacter.characterModel.isAttackable == false)
        {
            attackText.color = new Color32(122, 122, 122, 255);
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
