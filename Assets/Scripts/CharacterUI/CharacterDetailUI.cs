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
        CharacterForceText.text = "[戦闘] " + character.characterModel.force.ToString();
        CharacterInteliText.text = "[智謀] " + character.characterModel.inteli.ToString();
        CharacterTactText.text = "[手腕] " + character.characterModel.tact.ToString();
        CharacterFameText.text = "[名声] " + character.characterModel.fame.ToString();
        CharacterAmbitionText.text = "[野心] " + character.characterModel.ambition.ToString();
        if(character.characterModel.isLord == true || character.influence.influenceName == "NoneInfluence")
        {
            CharacterLoyaltyText.text = "[忠誠] --";
        }
        else
        {
            CharacterLoyaltyText.text = "[忠誠] " + character.characterModel.loyalty.ToString();
        }
        
        ShowSoliderList(character.soliderList, SoliderListField);
    }

    void ShowSoliderList(List<SoliderController> soliderList, Transform field)
    {
        // 現在表示されている兵士を削除
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }

        // 新しい兵士リストを作成
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
