using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SearchCommandUI : MonoBehaviour
{
    private Color originalUIColor; // ���̔w�i�F��ێ�����ϐ�

    [SerializeField] TextMeshProUGUI searchText;
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

    public void ShowSearchCommandUI()
    {
        ChangeBackgroundColor(originalUIColor);
        searchText.color = Color.white; // ���F�ɕύX
        this.gameObject.SetActive(true);

        //�̒n���ɉ������T�������̔�\���ݒ�
        switch (gameManager.playerCharacter.influence.territoryList.Count)
        {
            case 1:
            case 2:
                if (gameManager.playerCharacter.influence.characterList.Count >= 3 || gameManager.playerCharacter.characterModel.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//��\��
                }
                break;
            case 3:
            case 4:
            case 5:
            case 6:
                if (gameManager.playerCharacter.influence.characterList.Count >= 4 || gameManager.playerCharacter.characterModel.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//��\��
                }
                break;
            case 7:
            case 8:
            case 9:
            case 10:
                if (gameManager.playerCharacter.influence.characterList.Count >= 5 || gameManager.playerCharacter.characterModel.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//��\��
                }
                break;
            case 11:
            case 12:
            case 13:
            case 14:
                if (gameManager.playerCharacter.influence.characterList.Count >= 6 || gameManager.playerCharacter.characterModel.gold < 9)
                {
                    searchText.color = new Color32(122, 122, 122, 255);//��\��
                }
                break;
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
