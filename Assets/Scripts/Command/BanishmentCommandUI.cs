using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BanishmentCommandUI : MonoBehaviour
{
    private Color originalUIColor; // 元の背景色を保持する変数
    [SerializeField] TextMeshProUGUI banishmentText;

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

    public void ShowBanishmentCommandUI()
    {
        ChangeBackgroundColor(originalUIColor);
        banishmentText.color = Color.white; // 白色に変更
        this.gameObject.SetActive(true);
        if (GameMain.instance.playerCharacter.influence.characterList.Count == 1)
        {
            banishmentText.color = new Color32(122, 122, 122, 255);
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
