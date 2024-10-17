using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterTurnUI : MonoBehaviour
{
    [SerializeField] Image characterIcon;
    [SerializeField] TextMeshProUGUI charaRankText;
    [SerializeField] TextMeshProUGUI charaNameText;

    public void ShowCharacterTurnUI(CharacterController character)
    {
        gameObject.SetActive(true);

        characterIcon.sprite = character.icon;

        charaRankText.text = character.rank.ToString();
        charaNameText.text = character.name;
    }

    public void HideCharacterTurnUI()
    {
        gameObject.SetActive(false);
    }
}
