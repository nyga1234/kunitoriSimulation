using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PersonalSoliderView : MonoBehaviour
{
    [SerializeField] Text soliderHPText;
    [SerializeField] Text soliderLvText;
    [SerializeField] Image soliderIcon;

    public void ShowPersonalSoliderUI(SoliderModel soliderModel)
    {
        soliderHPText.text = "HP " + soliderModel.hp.ToString();
        soliderLvText.text = "Lv " + soliderModel.lv.ToString();
        soliderIcon.sprite = soliderModel.icon;
    }
}
