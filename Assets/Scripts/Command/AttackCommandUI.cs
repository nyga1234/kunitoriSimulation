using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackCommandUI : MonoBehaviour
{
    private Color originalUIColor; // 元の背景色を保持する変数

    [SerializeField] TextMeshProUGUI attackText;
    public GameManager gameManager;

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // 元の背景色を保持
            originalUIColor = image.color;
        }
    }

    public void ShowAttackCommandUI()
    {
        ChangeBackgroundColor(originalUIColor);
        attackText.color = Color.white; // 白色に変更
        this.gameObject.SetActive(true);

        if (GameManager.instance.playerCharacter.characterModel.gold < 3)
        {
            attackText.color = new Color32(122, 122, 122, 255);
        }

        if (GameManager.instance.playerCharacter.characterModel.isLord == false && GameManager.instance.playerCharacter.characterModel.isAttackable == false)
        {
            attackText.color = new Color32(122, 122, 122, 255);
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
