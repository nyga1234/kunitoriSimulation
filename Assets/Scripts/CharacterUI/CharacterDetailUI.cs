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
        CharacterForceText.text = "[�퓬] " + character.characterModel.force.ToString();
        CharacterInteliText.text = "[�q�d] " + character.characterModel.inteli.ToString();
        CharacterTactText.text = "[��r] " + character.characterModel.tact.ToString();
        CharacterFameText.text = "[����] " + character.characterModel.fame.ToString();
        CharacterAmbitionText.text = "[��S] " + character.characterModel.ambition.ToString();
        if(character.characterModel.isLord == true || character.influence.influenceName == "NoneInfluence")
        {
            CharacterLoyaltyText.text = "[����] --";
        }
        else
        {
            CharacterLoyaltyText.text = "[����] " + character.characterModel.loyalty.ToString();
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
        SoliderController imagelSolider = Instantiate(imageSolidefPrefab, field, false);
        imagelSolider.ShowImageSoliderUI(solider);
    }

    public void HideCharacterDetailUI()
    {
        gameObject.SetActive(false);
    }
}
