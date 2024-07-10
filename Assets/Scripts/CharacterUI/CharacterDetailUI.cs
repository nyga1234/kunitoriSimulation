using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDetailUI : MonoBehaviour
{
    [SerializeField] Image characterIcon;
    [SerializeField] TextMeshProUGUI CharacterNameText;
    [SerializeField] TextMeshProUGUI CharacterForceText;
    [SerializeField] TextMeshProUGUI CharacterInteliText;
    [SerializeField] TextMeshProUGUI CharacterTactText;
    [SerializeField] TextMeshProUGUI CharacterFameText;
    [SerializeField] TextMeshProUGUI CharacterAmbitionText;
    [SerializeField] TextMeshProUGUI CharacterLoyaltyText;

    [SerializeField] Transform SoliderListField;
    [SerializeField] SoliderController imageSolidefPrefab;

    public void ShowCharacterDetailUI(CharacterController character)
    {
        gameObject.SetActive(true);
        characterIcon.sprite = character.characterModel.icon;
        CharacterNameText.text = character.characterModel.name;
        CharacterForceText.text = "[êÌì¨] " + character.characterModel.force.ToString();
        CharacterInteliText.text = "[íqñd] " + character.characterModel.inteli.ToString();
        CharacterTactText.text = "[éËòr] " + character.characterModel.tact.ToString();
        CharacterFameText.text = "[ñºê∫] " + character.characterModel.fame.ToString();
        CharacterAmbitionText.text = "[ñÏêS] " + character.characterModel.ambition.ToString();
        if(character.characterModel.isLord == true || character.influence.influenceName == "NoneInfluence")
        {
            CharacterLoyaltyText.text = "[íâêΩ] --";
        }
        else
        {
            CharacterLoyaltyText.text = "[íâêΩ] " + character.characterModel.loyalty.ToString();
        }
        
        ShowSoliderList(character.soliderList, SoliderListField);
    }

    void ShowSoliderList(List<SoliderController> soliderList, Transform field)
    {
        // åªç›ï\é¶Ç≥ÇÍÇƒÇ¢ÇÈï∫émÇçÌèú
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }

        // êVÇµÇ¢ï∫émÉäÉXÉgÇçÏê¨
        foreach (SoliderController solider in soliderList)
        {
            ShowSolider(solider, field);
        }
    }

    void ShowSolider(SoliderController solider, Transform field)
    {
        SoliderController imagelSolider = Instantiate(imageSolidefPrefab, field, false);
        imagelSolider.ShowImageSoliderUI(solider);
    }

    public void HideCharacterDetailUI()
    {
        gameObject.SetActive(false);
    }
}
