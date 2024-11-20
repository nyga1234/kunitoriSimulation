using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIndexUI : MonoBehaviour
{
    [SerializeField] Character1UI character1UI;
    [SerializeField] Character2UI character2UI;
    [SerializeField] Character3UI character3UI;
    [SerializeField] Character4UI character4UI;
    [SerializeField] Character5UI character5UI;
    [SerializeField] Character6UI character6UI;
    [SerializeField] AttackedCharacterUI attackedCharacterUI;
    [SerializeField] AbandonUI abandonUI;
    [SerializeField] LandformInformationUI landformInformationUI;

    public List<CharacterController> indexCharacterList;

    public void ShowCharacterIndexUI(List<CharacterController> characterList)
    {
        indexCharacterList = characterList;
        this.gameObject.SetActive(true);
        for (int i = 0; i < characterList.Count; i++)
        {
            if (i == 0)
            {
                character1UI.ShowCharacterUI(characterList[i]);
                //CharacterUIOnMouse onMouseComponent = character1UI.GetComponent<CharacterUIOnMouse>();
                SelectCharacterUI onMouseComponent = character1UI.GetComponent<SelectCharacterUI>();
                CharacterUIOnClick onClickComponent = character1UI.GetComponent<CharacterUIOnClick>();
                if (onMouseComponent != null || onClickComponent != null)
                {
                    onMouseComponent.SetCharacterController(characterList[i]);
                    onClickComponent.SetCharacterController(characterList[i]);
                }
            }
            else if (i == 1)
            {
                character2UI.ShowCharacterUI(characterList[i]);
                //CharacterUIOnMouse onMouseComponent2 = character2UI.GetComponent<CharacterUIOnMouse>();
                SelectCharacterUI onMouseComponent2 = character2UI.GetComponent<SelectCharacterUI>();
                CharacterUIOnClick onClickComponent2 = character2UI.GetComponent<CharacterUIOnClick>();
                if (onMouseComponent2 != null || onClickComponent2 != null)
                {
                    onMouseComponent2.SetCharacterController(characterList[i]);
                    onClickComponent2.SetCharacterController(characterList[i]);
                }
            }
            else if (i == 2)
            {
                character3UI.ShowCharacterUI(characterList[i]);
                //CharacterUIOnMouse onMouseComponent3 = character3UI.GetComponent<CharacterUIOnMouse>();
                SelectCharacterUI onMouseComponent3 = character3UI.GetComponent<SelectCharacterUI>();
                CharacterUIOnClick onClickComponent3 = character3UI.GetComponent<CharacterUIOnClick>();
                if (onMouseComponent3 != null || onClickComponent3 != null)
                {
                    onMouseComponent3.SetCharacterController(characterList[i]);
                    onClickComponent3.SetCharacterController(characterList[i]);
                }
            }
            else if (i == 3)
            {
                character4UI.ShowCharacterUI(characterList[i]);
                //CharacterUIOnMouse onMouseComponent4 = character4UI.GetComponent<CharacterUIOnMouse>();
                SelectCharacterUI onMouseComponent4 = character4UI.GetComponent<SelectCharacterUI>();
                CharacterUIOnClick onClickComponent4 = character4UI.GetComponent<CharacterUIOnClick>();
                if (onMouseComponent4 != null || onClickComponent4 != null)
                {
                    onMouseComponent4.SetCharacterController(characterList[i]);
                    onClickComponent4.SetCharacterController(characterList[i]);
                }
            }
            else if (i == 4)
            {
                character5UI.ShowCharacterUI(characterList[i]);
                //CharacterUIOnMouse onMouseComponent5 = character5UI.GetComponent<CharacterUIOnMouse>();
                SelectCharacterUI onMouseComponent5 = character5UI.GetComponent<SelectCharacterUI>();
                CharacterUIOnClick onClickComponent5 = character5UI.GetComponent<CharacterUIOnClick>();
                if (onMouseComponent5 != null || onClickComponent5 != null)
                {
                    onMouseComponent5.SetCharacterController(characterList[i]);
                    onClickComponent5.SetCharacterController(characterList[i]);
                }
            }
            else if (i == 5)
            {
                character6UI.ShowCharacterUI(characterList[i]);
                //CharacterUIOnMouse onMouseComponent6 = character6UI.GetComponent<CharacterUIOnMouse>();
                SelectCharacterUI onMouseComponent6 = character6UI.GetComponent<SelectCharacterUI>();
                CharacterUIOnClick onClickComponent6 = character6UI.GetComponent<CharacterUIOnClick>();
                if (onMouseComponent6 != null || onClickComponent6 != null)
                {
                    onMouseComponent6.SetCharacterController(characterList[i]);
                    onClickComponent6.SetCharacterController(characterList[i]);
                }
            }
        }
    }

    public void HideCharacterIndexUI()
    {
        this.gameObject.SetActive(false);
        character1UI.HideCharacterUI();
        character2UI.HideCharacterUI();
        character3UI.HideCharacterUI();
        character4UI.HideCharacterUI();
        character5UI.HideCharacterUI();
        character6UI.HideCharacterUI();
        attackedCharacterUI.HideCharacterUI();
        abandonUI.HideAbandonUI();
        landformInformationUI.HideLandformInformationUI();
    }
}
