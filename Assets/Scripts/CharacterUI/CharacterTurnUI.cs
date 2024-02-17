using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTurnUI : MonoBehaviour
{
    [SerializeField] Image characterIcon;
    [SerializeField] Text charaRankText;
    [SerializeField] Text charaNameText;

    public void ShowCharacterTurnUI(CharacterController character)
    {
        gameObject.SetActive(true);

        characterIcon.sprite = character.characterModel.icon;

        charaRankText.text = character.characterModel.rank.ToString();
        charaNameText.text = character.characterModel.name;
    }

    public void HideCharacterTurnUI()
    {
        gameObject.SetActive(false);
    }
}
