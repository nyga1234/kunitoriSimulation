using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    [SerializeField] Text Character1NameText;//���O
    [SerializeField] Text Character1ForceText;//�퓬
    [SerializeField] Text Character1InteliText;//�q�d
    [SerializeField] Text Character1TactText;//��r
    [SerializeField] Text Character1RankText;//�g��
    [SerializeField] Text Character1FameText;//����
    [SerializeField] Text Character1AmbitionText;//��S
    [SerializeField] Text Character1LoyaltyText;//����
    [SerializeField] Text Character1SalaryText;//����%

    public void ShowCharacterUI(CharacterController character)
    {
        gameObject.SetActive(true);
        Character1NameText.text = character.characterModel.name;
        Character1ForceText.text = character.characterModel.force.ToString();
        Character1InteliText.text = character.characterModel.inteli.ToString();
        Character1TactText.text = character.characterModel.tact.ToString();
        Character1RankText.text = character.characterModel.rank.ToString();
        Character1FameText.text = character.characterModel.fame.ToString();
        Character1AmbitionText.text = character.characterModel.ambition.ToString();
        Character1LoyaltyText.text = character.characterModel.ambition.ToString();
        Character1SalaryText.text = character.characterModel.ambition.ToString();
    }

    public void HideCharacterUI()
    {
        gameObject.SetActive(false);
    }
}
