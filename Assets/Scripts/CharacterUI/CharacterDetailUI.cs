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
        CharacterForceText.text = "[戦闘] " + character.force.ToString();
        CharacterInteliText.text = "[智謀] " + character.inteli.ToString();
        CharacterTactText.text = "[手腕] " + character.tact.ToString();
        CharacterFameText.text = "[名声] " + character.fame.ToString();
        CharacterAmbitionText.text = "[野心] " + character.ambition.ToString();
        if(character.isLord == true || character.influence.influenceName == "NoneInfluence")
        {
            CharacterLoyaltyText.text = "[忠誠] --";
        }
        else
        {
            CharacterLoyaltyText.text = "[忠誠] " + character.loyalty.ToString();
        }
        
        ShowSoliderList(character.soliderList, SoliderListField);
    }

    void ShowSoliderList(List<SoldierController> soliderList, Transform field)
    {
        // 現在表示されている兵士を削除
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }

        // 新しい兵士リストを作成
        foreach (SoldierController solider in soliderList)
        {
            ShowSolider(solider, field);
        }
    }

    void ShowSolider(SoldierController solider, Transform field)
    {
        //SoliderController imagelSolider = Instantiate(imageSolidefPrefab, field, false);
        //imagelSolider.ShowImageSoliderUI(solider);
        var soldierObject = Instantiate(imageSolidefPrefab, field);
        soldierObject.GetComponent<SoldierImageView>().
            ShowSoldierHP(solider.icon, solider.hp.ToString());
    }

    public void HideCharacterDetailUI()
    {
        gameObject.SetActive(false);
    }
}
