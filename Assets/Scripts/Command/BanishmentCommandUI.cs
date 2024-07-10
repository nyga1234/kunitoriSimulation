using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BanishmentCommandUI : MonoBehaviour
{
    private Color originalUIColor; // ���̔w�i�F��ێ�����ϐ�
    [SerializeField] TextMeshProUGUI banishmentText;

    public GameManager gameManager;

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // ���̔w�i�F��ێ�
            originalUIColor = image.color;
        }
    }

    public void ShowBanishmentCommandUI()
    {
        ChangeBackgroundColor(originalUIColor);
        banishmentText.color = Color.white; // ���F�ɕύX
        this.gameObject.SetActive(true);
        if (GameManager.instance.playerCharacter.influence.characterList.Count == 1)
        {
            banishmentText.color = new Color32(122, 122, 122, 255);
        }
    }

    //�w�i�F��ύX
    private void ChangeBackgroundColor(Color color)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }
}
