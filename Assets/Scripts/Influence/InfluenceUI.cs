using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InfluenceUI : MonoBehaviour
{
    [SerializeField] Sprite charaUI;

    [SerializeField] Image lordImage;
    [SerializeField] TextMeshProUGUI lordNameText;
    [SerializeField] TextMeshProUGUI territorySumText;
    [SerializeField] TextMeshProUGUI goldSumText;
    [SerializeField] TextMeshProUGUI characterSumText;
    [SerializeField] TextMeshProUGUI soliderSumText;
    [SerializeField] TextMeshProUGUI forceSumText;
    [SerializeField] TextMeshProUGUI allianceText;

    public void ShowInfluenceUI(Influence influence)
    {
        gameObject.SetActive(true);

        CharacterController lordCharacter = influence.characterList.Find(character => character.isLord);
        
        if (lordCharacter != null)
        {
            lordImage.sprite = lordCharacter.icon;
            lordNameText.text = "�̎� " + lordCharacter.name;

            GameMain.instance.TerritoryUIOnMouse.InfluenceCalcSum(influence);
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
            characterSumText.text = "[����] --" ;
            soliderSumText.text = "[����] --";
            forceSumText.text = "[���] --";
        }
    }

    public void HideInfluenceUI()
    {
        gameObject.SetActive(false);
    }
}
