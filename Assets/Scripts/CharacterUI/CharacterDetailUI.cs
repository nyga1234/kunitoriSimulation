using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterDetailUI : MonoBehaviour
{
    [SerializeField] Image characterIcon;
    [SerializeField] Text CharacterNameText;
    [SerializeField] Text CharacterForceText;
    [SerializeField] Text CharacterInteliText;
    [SerializeField] Text CharacterTactText;
    [SerializeField] Text CharacterFameText;
    [SerializeField] Text CharacterAmbitionText;
    [SerializeField] Text CharacterLoyaltyText;

    [SerializeField] Transform SoliderListField;
    [SerializeField] SoliderController imageSolidefPrefab;

    public void ShowCharacterDetailUI(CharacterController character)
    {
        gameObject.SetActive(true);
        characterIcon.sprite = character.characterModel.icon;
        CharacterNameText.text = character.characterModel.name;
        CharacterForceText.text = "[í“¬] " + character.characterModel.force.ToString();
        CharacterInteliText.text = "[’q–d] " + character.characterModel.inteli.ToString();
        CharacterTactText.text = "[è˜r] " + character.characterModel.tact.ToString();
        CharacterFameText.text = "[–¼º] " + character.characterModel.fame.ToString();
        CharacterAmbitionText.text = "[–ìS] " + character.characterModel.ambition.ToString();
        if(character.characterModel.isLord == true || character.influence.influenceName == "NoneInfluence")
        {
            CharacterLoyaltyText.text = "[’‰½] --";
        }
        else
        {
            CharacterLoyaltyText.text = "[’‰½] " + character.characterModel.loyalty.ToString();
        }
        
        ShowSoliderList(character.soliderList, SoliderListField);
    }

    void ShowSoliderList(List<SoliderController> soliderList, Transform field)
    {
        // Œ»İ•\¦‚³‚ê‚Ä‚¢‚é•ºm‚ğíœ
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }

        // V‚µ‚¢•ºmƒŠƒXƒg‚ğì¬
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
