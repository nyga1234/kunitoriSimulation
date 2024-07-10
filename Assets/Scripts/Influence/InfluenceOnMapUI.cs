using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfluenceOnMapUI : MonoBehaviour
{
    //public InflueneceManager influeneceManager;
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;

    [SerializeField] Sprite charaUI;

    [SerializeField] Image lordImage;
    [SerializeField] Image attackTerritoryTypeImage;
    [SerializeField] Image defenceTerritoryTypeImage;
    [SerializeField] TextMeshProUGUI lordNameText;
    [SerializeField] TextMeshProUGUI territorySumText;
    [SerializeField] TextMeshProUGUI goldSumText;
    [SerializeField] TextMeshProUGUI characterSumText;
    [SerializeField] TextMeshProUGUI soliderSumText;
    [SerializeField] TextMeshProUGUI forceSumText;
    [SerializeField] TextMeshProUGUI allianceText;

    public void ShowInfluenceOnMapUI(Influence influence, Territory territory)
    {
        gameObject.SetActive(true);

        CharacterController lordCharacter = influence.characterList.Find(character => character.characterModel.isLord);

        SetBattleFieldUI(territory);

        if (lordCharacter != null)
        {
            lordImage.sprite = lordCharacter.characterModel.icon;
            lordNameText.text = "�̎� " + lordCharacter.characterModel.name;

            territoryUIOnMouse.InfluenceCalcSum(influence);
            territorySumText.text = "[�̐�] " + influence.territorySum.ToString();
            goldSumText.text = "[���v]" + influence.goldSum.ToString();
            characterSumText.text = "[����] " + influence.characterSum.ToString();
            soliderSumText.text = "[����] " + influence.soliderSum.ToString();
            forceSumText.text = "[���] " + influence.forceSum.ToString();
        }
        else
        {
            lordImage.sprite = charaUI;
            lordNameText.text = "������ ";
            territorySumText.text = "[�̐�] --";
            goldSumText.text = "[���v] --";
            characterSumText.text = "[����] --";
            soliderSumText.text = "[����] --";
            forceSumText.text = "[���] --";
        }
    }

    void SetBattleFieldUI(Territory territory)
    {
        //�n�`�ɑΉ�����摜�t�@�C����
        string desert = "Landforms/desert";
        string wilderness = "Landforms/wilderness";
        string plain = "Landforms/plain";
        string forest = "Landforms/forest";
        string fort = "Landforms/fort";

        // Resources.Load���g�p���ăX�v���C�g��ǂݍ���
        Sprite desertSprite = Resources.Load<Sprite>(desert);
        Sprite wildernessSprite = Resources.Load<Sprite>(wilderness);
        Sprite plainSprite = Resources.Load<Sprite>(plain);
        Sprite forestSprite = Resources.Load<Sprite>(forest);
        Sprite fortSprite = Resources.Load<Sprite>(fort);

        switch (territory.attackTerritoryType)
        {
            case Territory.AttackTerritoryType.desert:
                attackTerritoryTypeImage.sprite = desertSprite;
                break;
            case Territory.AttackTerritoryType.wilderness:
                attackTerritoryTypeImage.sprite = wildernessSprite;
                break;
            case Territory.AttackTerritoryType.plain:
                attackTerritoryTypeImage.sprite = plainSprite;
                break;
            case Territory.AttackTerritoryType.forest:
                attackTerritoryTypeImage.sprite = forestSprite;
                break;
            case Territory.AttackTerritoryType.fort:
                attackTerritoryTypeImage.sprite = fortSprite;
                break;
        }

        switch (territory.defenceTerritoryType)
        {
            case Territory.DefenceTerritoryType.desert:
                defenceTerritoryTypeImage.sprite = desertSprite;
                break;
            case Territory.DefenceTerritoryType.wilderness:
                defenceTerritoryTypeImage.sprite = wildernessSprite;
                break;
            case Territory.DefenceTerritoryType.plain:
                defenceTerritoryTypeImage.sprite = plainSprite;
                break;
            case Territory.DefenceTerritoryType.forest:
                defenceTerritoryTypeImage.sprite = forestSprite;
                break;
            case Territory.DefenceTerritoryType.fort:
                defenceTerritoryTypeImage.sprite = fortSprite;
                break;
        }

    }

    public void HideInfluenceOnMapUI()
    {
        gameObject.SetActive(false);
    }
}
