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
        Character1NameText.text = character.name;
        Character1ForceText.text = character.force.ToString();
        Character1InteliText.text = character.inteli.ToString();
        Character1TactText.text = character.tact.ToString();
        Character1RankText.text = character.rank.ToString();
        Character1FameText.text = character.fame.ToString();
        Character1AmbitionText.text = character.ambition.ToString();
        Character1LoyaltyText.text = character.ambition.ToString();
        Character1SalaryText.text = character.ambition.ToString();
    }

    public void HideCharacterUI()
    {
        gameObject.SetActive(false);
    }
}
