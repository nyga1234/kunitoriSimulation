using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InfluenceUI : MonoBehaviour
{
    //public InflueneceManager influeneceManager;
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;

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

        CharacterController lordCharacter = influence.characterList.Find(character => character.characterModel.isLord);
        
        if (lordCharacter != null)
        {
            lordImage.sprite = lordCharacter.characterModel.icon;
            lordNameText.text = "—Ìå " + lordCharacter.characterModel.name;

            territoryUIOnMouse.InfluenceCalcSum(influence);
            territorySumText.text = "[—Ì”] " + influence.territorySum.ToString();
            goldSumText.text = "[‹àŒv]" + influence.goldSum.ToString();
            characterSumText.text = "[«”] " + influence.characterSum.ToString();
            soliderSumText.text = "[•º”] " + influence.soliderSum.ToString();
            forceSumText.text = "[í—Í] " + influence.forceSum.ToString();
        }
        else
        {
            lordImage.sprite = charaUI;
            lordNameText.text = "–³Š‘® ";
            territorySumText.text = "[—Ì”] --";
            goldSumText.text = "[‹àŒv] --";
            characterSumText.text = "[«”] --" ;
            soliderSumText.text = "[•º”] --";
            forceSumText.text = "[í—Í] --";
        }
    }

    public void HideInfluenceUI()
    {
        gameObject.SetActive(false);
    }
}
