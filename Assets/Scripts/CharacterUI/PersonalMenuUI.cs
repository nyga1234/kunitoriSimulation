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

        recruitText.color = Color.white; // 白色に変更
        trainingText.color = Color.white; // 白色に変更
        enterText.color = Color.white; // 白色に変更
        vagabondText.color = Color.white; // 白色に変更
        rebellionText.color = Color.white; // 白色に変更
        if (character.soliderList.Count >= 10 || character.characterModel.gold <= 1)
        {
            recruitText.color = new Color32(122, 122, 122, 255);
        }
        if (character.characterModel.gold <= 1)
        {
            trainingText.color = new Color32(122, 122, 122, 255);
        }
        if (character.influence != GameMain.instance.noneInfluence)
        {
            enterText.color = new Color32(255, 255, 255, 0);
        }
        if (character.characterModel.isLord == true || character.influence == GameMain.instance.noneInfluence)
        {
            vagabondText.color = new Color32(255, 255, 255, 0);
        }
        if (character.characterModel.isLord == true || character.influence == GameMain.instance.noneInfluence)
        {
            rebellionText.color = new Color32(255, 255, 255, 0);
        }

        moneyText.text = "資金 " + character.characterModel.gold.ToString();

        characterImage.sprite = character.characterModel.icon;

        rankText.text = character.characterModel.rank.ToString();
        characterNameText.text = character.characterModel.name;

        goldText.text = "[所持金] " + character.characterModel.gold.ToString();
        forceText.text = "[戦闘] " + character.characterModel.force.ToString();
        inteliText.text = "[智謀] " + character.characterModel.inteli.ToString();
        tactText.text = "[手腕] " + character.characterModel.tact.ToString();
        fameText.text = "[名声] " + character.characterModel.fame.ToString();
        ambitionText.text = "[野心] " + character.characterModel.ambition.ToString();
        if (character.characterModel.isLord == true || character.influence.influenceName == "NoneInfluence" || character == GameMain.instance.playerCharacter)
        {
            loyalityText.text = "[忠誠] --";
        }
        else
        {
            loyalityText.text = "[忠誠] " + character.characterModel.loyalty.ToString();
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
