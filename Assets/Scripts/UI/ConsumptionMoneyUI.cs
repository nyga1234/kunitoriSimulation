using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConsumptionMoneyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI consumptionMoneyText;

    public void ShowConsumptionText(int money)
    {
        //gameObject.SetActive(true);
        consumptionMoneyText.text = "è¡îÔã‡ " + money;
    }
}
