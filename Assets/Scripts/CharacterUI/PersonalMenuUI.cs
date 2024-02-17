using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PersonalMenuUI : MonoBehaviour
{
    [SerializeField] Text moneyText;

    [SerializeField] Image characterImage;

    [SerializeField] Text rankText;
    [SerializeField] Text characterNameText;

    [SerializeField] Text goldText;
    [SerializeField] Text forceText;
    [SerializeField] Text inteliText;
    [SerializeField] Text tactText;
    [SerializeField] Text fameText;
    [SerializeField] Text ambitionText;
    [SerializeField] Text loyalityText;

    [SerializeField] Text recruitText;
    [SerializeField] Text trainingText;
    [SerializeField] Text enterText;
    [SerializeField] Text vagabondText;
    [SerializeField] Text rebellionText;

    [SerializeField] Transform SoliderListField;
    [SerializeField] SoliderController personalSolidefPrefab;

    public void ShowPersonalMenuUI(CharacterController character)
    {
        gameObject.SetActive(true);

        recruitText.color = Color.white; // ���F�ɕύX
        trainingText.color = Color.white; // ���F�ɕύX
        enterText.color = Color.white; // ���F�ɕύX
        vagabondText.color = Color.white; // ���F�ɕύX
        rebellionText.color = Color.white; // ���F�ɕύX
        if (character.soliderList.Count >= 10 || character.characterModel.gold <= 1)
        {
            recruitText.color = new Color32(122, 122, 122, 255);
        }
        if (character.characterModel.gold <= 1)
        {
            trainingText.color = new Color32(122, 122, 122, 255);
        }
        if (character.influence != GameManager.instance.noneInfluence)
        {
            enterText.color = new Color32(255, 255, 255, 0);
        }
        if (character.characterModel.isLord == true || character.influence == GameManager.instance.noneInfluence)
        {
            vagabondText.color = new Color32(255, 255, 255, 0);
        }
        if (character.characterModel.isLord == true || character.influence == GameManager.instance.noneInfluence)
        {
            rebellionText.color = new Color32(255, 255, 255, 0);
        }

        moneyText.text = "���� " + character.characterModel.gold.ToString();

        characterImage.sprite = character.characterModel.icon;

        rankText.text = character.characterModel.rank.ToString();
        characterNameText.text = character.characterModel.name;

        goldText.text = "[������] " + character.characterModel.gold.ToString();
        forceText.text = "[�퓬] " + character.characterModel.force.ToString();
        inteliText.text = "[�q�d] " + character.characterModel.inteli.ToString();
        tactText.text = "[��r] " + character.characterModel.tact.ToString();
        fameText.text = "[����] " + character.characterModel.fame.ToString();
        ambitionText.text = "[��S] " + character.characterModel.ambition.ToString();
        if (character.characterModel.isLord == true || character.influence.influenceName == "NoneInfluence" || character == GameManager.instance.playerCharacter)
        {
            loyalityText.text = "[����] --";
        }
        else
        {
            loyalityText.text = "[����] " + character.characterModel.loyalty.ToString();
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
        SoliderController personalSolider = Instantiate(personalSolidefPrefab, field, false);
        personalSolider.ShowPersonalSoliderUI(solider);
    }

    public void HidePersonalMenuUI()
    {
        gameObject.SetActive(false);
    }
}
