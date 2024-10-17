using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class PersonalMenuUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;

    [SerializeField] Image characterImage;

    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] TextMeshProUGUI characterNameText;

    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI forceText;
    [SerializeField] TextMeshProUGUI inteliText;
    [SerializeField] TextMeshProUGUI tactText;
    [SerializeField] TextMeshProUGUI fameText;
    [SerializeField] TextMeshProUGUI ambitionText;
    [SerializeField] TextMeshProUGUI loyalityText;

    [SerializeField] TextMeshProUGUI recruitText;
    [SerializeField] TextMeshProUGUI trainingText;
    [SerializeField] TextMeshProUGUI enterText;
    [SerializeField] TextMeshProUGUI vagabondText;
    [SerializeField] TextMeshProUGUI rebellionText;

    [SerializeField] Transform SoliderListField;
    [SerializeField] GameObject personalSolidefPrefab;
    //[SerializeField] SoliderController personalSolidefPrefab;

    public void ShowPersonalMenuUI(CharacterController character)
    {
        gameObject.SetActive(true);

        recruitText.color = Color.white; // ���F�ɕύX
        trainingText.color = Color.white; // ���F�ɕύX
        enterText.color = Color.white; // ���F�ɕύX
        vagabondText.color = Color.white; // ���F�ɕύX
        rebellionText.color = Color.white; // ���F�ɕύX
        if (character.soliderList.Count >= 10 || character.gold <= 1)
        {
            recruitText.color = new Color32(122, 122, 122, 255);
        }
        if (character.gold <= 1)
        {
            trainingText.color = new Color32(122, 122, 122, 255);
        }
        if (character.influence != GameMain.instance.noneInfluence)
        {
            enterText.color = new Color32(255, 255, 255, 0);
        }
        if (character.isLord == true || character.influence == GameMain.instance.noneInfluence)
        {
            vagabondText.color = new Color32(255, 255, 255, 0);
        }
        if (character.isLord == true || character.influence == GameMain.instance.noneInfluence)
        {
            rebellionText.color = new Color32(255, 255, 255, 0);
        }

        moneyText.text = "���� " + character.gold.ToString();

        characterImage.sprite = character.icon;

        rankText.text = character.rank.ToString();
        characterNameText.text = character.name;

        goldText.text = "[������] " + character.gold.ToString();
        forceText.text = "[�퓬] " + character.force.ToString();
        inteliText.text = "[�q�d] " + character.inteli.ToString();
        tactText.text = "[��r] " + character.tact.ToString();
        fameText.text = "[����] " + character.fame.ToString();
        ambitionText.text = "[��S] " + character.ambition.ToString();
        if (character.isLord == true || character.influence.influenceName == "NoneInfluence" || character == GameMain.instance.playerCharacter)
        {
            loyalityText.text = "[����] --";
        }
        else
        {
            loyalityText.text = "[����] " + character.loyalty.ToString();
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
        var soldierObject = Instantiate(personalSolidefPrefab, field);
        soldierObject.GetComponent<SoldierImageView>().
            ShowSoldierStatus(
            solider.soliderModel.icon, 
            solider.soliderModel.hp.ToString(),
            solider.soliderModel.lv.ToString());
        //SoliderController personalSolider = Instantiate(personalSolidefPrefab, field, false);
        //personalSolider.ShowPersonalSoliderUI(solider);
    }

    public void HidePersonalMenuUI()
    {
        gameObject.SetActive(false);
    }
}
