using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OnClickBackButton : MonoBehaviour
{
    [SerializeField] Transform mapField;
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject mainField;
    [SerializeField] GameObject characterDetailUI;
    [SerializeField] GameObject backToCharaMenuButton;

    public void BackToCharaMenuButton()
    {
        this.gameObject.SetActive(false);
        mapField.gameObject.SetActive(false);
        //プレイヤーキャラクターの取得
        CharacterController playerCharacter = gameManager.characterList.Find(character => character.characterModel.isPlayerCharacter);
        gameManager.ShowLordUI(playerCharacter);
    }

    public void BackToPersonalMenuButton()
    {
        this.gameObject.SetActive(false);
        mapField.gameObject.SetActive(false);
        //プレイヤーキャラクターの取得
        CharacterController playerCharacter = gameManager.characterList.Find(character => character.characterModel.isPlayerCharacter);
        gameManager.ShowPersonalUI(playerCharacter);
    }

    public void BackToMapFeildButton()
    {
        this.gameObject.SetActive(false);
        mainField.gameObject.SetActive(false);
        characterDetailUI.gameObject.SetActive(false);

        mapField.gameObject.SetActive(true);
        backToCharaMenuButton.gameObject.SetActive(true);
    }
}
