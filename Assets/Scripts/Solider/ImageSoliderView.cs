using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageSoliderView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI soliderHPText;
    [SerializeField] Image soliderIcon;

    public void ShowImageSoliderUI(SoliderModel soliderModel)
    {
        soliderHPText.text = soliderModel.hp.ToString();
        soliderIcon.sprite = soliderModel.icon;
    }
}
