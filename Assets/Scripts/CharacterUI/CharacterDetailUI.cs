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
    //[SerializeField] SoliderController imageSolidefPrefab;
    [SerializeField] GameObject imageSolidefPrefab;

    public void ShowCharacterDetailUI(CharacterController character)
    {
        gameObject.SetActive(true);
        characterIcon.sprite = character.icon;
        CharacterNameText.text = character.name;
        CharacterForceText.text = "[êÌì¨] " + character.force.ToString();
        CharacterInteliText.text = "[íqñd] " + character.inteli.ToString();
        CharacterTactText.text = "[éËòr] " + character.tact.ToString();
        CharacterFameText.text = "[ñºê∫] " + character.fame.ToString();
        CharacterAmbitionText.text = "[ñÏêS] " + character.ambition.ToString();
        if(character.isLord == true || character.influence.influenceName == "NoneInfluence")
        {
            CharacterLoyaltyText.text = "[íâêΩ] --";
        }
        else
        {
            CharacterLoyaltyText.text = "[íâêΩ] " + character.loyalty.ToString();
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
        //SoliderController imagelSolider = Instantiate(imageSolidefPrefab, field, false);
        //imagelSolider.ShowImageSoliderUI(solider);
        var soldierObject = Instantiate(imageSolidefPrefab, field);
        soldierObject.GetComponent<SoldierImageView>().
            ShowSoldierHP(solider.soliderModel.icon, solider.soliderModel.hp.ToString());
    }

    public void HideCharacterDetailUI()
    {
        gameObject.SetActive(false);
    }
}
