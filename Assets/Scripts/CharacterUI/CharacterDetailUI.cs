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
        CharacterForceText.text = "[�퓬] " + character.force.ToString();
        CharacterInteliText.text = "[�q�d] " + character.inteli.ToString();
        CharacterTactText.text = "[��r] " + character.tact.ToString();
        CharacterFameText.text = "[����] " + character.fame.ToString();
        CharacterAmbitionText.text = "[��S] " + character.ambition.ToString();
        if(character.isLord == true || character.influence.influenceName == "NoneInfluence")
        {
            CharacterLoyaltyText.text = "[����] --";
        }
        else
        {
            CharacterLoyaltyText.text = "[����] " + character.loyalty.ToString();
        }
        
        ShowSoliderList(character.soliderList, SoliderListField);
    }

    void ShowSoliderList(List<SoliderController> soliderList, Transform field)
    {
        // ���ݕ\������Ă��镺�m���폜
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }

        // �V�������m���X�g���쐬
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
