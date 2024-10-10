//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;

//public class SoliderView : MonoBehaviour
//{
//    [SerializeField] TextMeshProUGUI hpText;
//    [SerializeField] Image iconImage;
//    [SerializeField] Image hpSlider;

//    public void ShowSoliderUI(SoliderModel soliderModel)
//    {
//        hpText.text = soliderModel.hp.ToString();
//        iconImage.sprite = soliderModel.icon;
//        hpSlider.fillAmount = (float)soliderModel.hp / (float)soliderModel.maxHP;
//        if (hpSlider.fillAmount <= 0.6)
//        {
//            hpSlider.color = Color.green;
//        }
//        if (hpSlider.fillAmount <= 0.4)
//        {
//            hpSlider.color = Color.yellow;
//        }
//        if (hpSlider.fillAmount <= 0.2)
//        {
//            hpSlider.color = Color.red;
//        }
//    }
//}
