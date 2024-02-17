using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static GameManager;

public class Character2UI : MonoBehaviour
{
    [SerializeField] Text CharacterNameText;//���O
    [SerializeField] Text CharacterForceText;//�퓬
    [SerializeField] Text CharacterInteliText;//�q�d
    [SerializeField] Text CharacterTactText;//��r
    [SerializeField] Text CharacterRankText;//�g��
    [SerializeField] Text CharacterFameText;//����
    [SerializeField] Text CharacterAmbitionText;//��S
    [SerializeField] Text CharacterLoyaltyText;//����
    [SerializeField] Text CharacterSalaryText;//����%

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

    public void ShowCharacterUI(CharacterController character)
    {
        ChangeTextColorWhite();
        ChangeBackgroundColor(originalColor);
        if (character.characterModel.isAttackable == false)
        {
            CharacterNameText.color = new Color32(122, 122, 122, 255);
            CharacterForceText.color = new Color32(122, 122, 122, 255);
            CharacterInteliText.color = new Color32(122, 122, 122, 255);
            CharacterTactText.color = new Color32(122, 122, 122, 255);
            CharacterRankText.color = new Color32(122, 122, 122, 255);
            CharacterFameText.color = new Color32(122, 122, 122, 255);
            CharacterAmbitionText.color = new Color32(122, 122, 122, 255);
            CharacterLoyaltyText.color = new Color32(122, 122, 122, 255);
            CharacterSalaryText.color = new Color32(122, 122, 122, 255);
        }
        gameObject.SetActive(true);
        CharacterNameText.text = character.characterModel.name;
        CharacterForceText.text = character.characterModel.force.ToString();
        CharacterInteliText.text = character.characterModel.inteli.ToString();
        CharacterTactText.text = character.characterModel.tact.ToString();
        CharacterRankText.text = character.characterModel.rank.ToString();
        CharacterFameText.text = character.characterModel.fame.ToString();
        CharacterAmbitionText.text = character.characterModel.ambition.ToString();
        if (character.characterModel.isLord == true || character.influence.influenceName == "NoneInfluence" || character == GameManager.instance.playerCharacter)
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
