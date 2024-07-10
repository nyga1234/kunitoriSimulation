using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackedCharacterUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CharacterNameText;//���O
    [SerializeField] TextMeshProUGUI CharacterForceText;//�퓬
    [SerializeField] TextMeshProUGUI CharacterInteliText;//�q�d
    [SerializeField] TextMeshProUGUI CharacterTactText;//��r
    [SerializeField] TextMeshProUGUI CharacterRankText;//�g��
    [SerializeField] TextMeshProUGUI CharacterFameText;//����
    [SerializeField] TextMeshProUGUI CharacterAmbitionText;//��S
    [SerializeField] TextMeshProUGUI CharacterLoyaltyText;//����
    [SerializeField] TextMeshProUGUI CharacterSalaryText;//����%

    private Color originalColor; // ���̔w�i�F��ێ�����ϐ�

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // ���̔w�i�F��ێ�
            originalColor = image.color;
        }
    }

    public void ShowAttackedCharacterUI(CharacterController character)
    {
        this.gameObject.SetActive(true);

        ChangeTextColorWhite();

        CharacterNameText.text = character.characterModel.name;
        CharacterForceText.text = character.characterModel.force.ToString();
        CharacterInteliText.text = character.characterModel.inteli.ToString();
        CharacterTactText.text = character.characterModel.tact.ToString();
        //CharacterRankText.text = character.characterModel.rank.ToString();
        CharacterFameText.text = character.characterModel.fame.ToString();
        CharacterAmbitionText.text = character.characterModel.ambition.ToString();
        if (character.characterModel.isLord == true || character.influence.influenceName == "NoneInfluence")
        {
            CharacterLoyaltyText.text = "--";
        }
        else
        {
            CharacterLoyaltyText.text = character.characterModel.loyalty.ToString();
        }
        if (character.influence.influenceName == "NoneInfluence")
        {
            CharacterSalaryText.text = "--";
        }
        else
        {
            CharacterSalaryText.text = character.characterModel.salary.ToString();
        }

        CharacterUIOnMouse onMouseComponent = this.gameObject.GetComponent<CharacterUIOnMouse>();
        if (onMouseComponent != null)
        {
            onMouseComponent.SetCharacterController(character);
        }
    }

    public void HideCharacterUI()
    {
        ChangeBackgroundColor(originalColor);
        gameObject.SetActive(false);
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

    private void ChangeTextColorWhite()
    {
        CharacterNameText.color = Color.white;
        CharacterForceText.color = Color.white;
        CharacterInteliText.color = Color.white;
        CharacterTactText.color = Color.white;
        CharacterRankText.color = Color.white;
        CharacterFameText.color = Color.white;
        CharacterAmbitionText.color = Color.white;
        CharacterLoyaltyText.color = Color.white;
        CharacterSalaryText.color = Color.white;
    }
}
