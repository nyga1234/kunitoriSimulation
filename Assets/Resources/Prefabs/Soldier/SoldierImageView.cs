using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SoldierImageView : MonoBehaviour
{
    [SerializeField] Image soliderIcon;
    [SerializeField] TextMeshProUGUI soliderLvText;
    [SerializeField] TextMeshProUGUI soliderHPText;
    [SerializeField] Image hpSlider;

    public void ShowSoldierImage(Sprite sprite, bool Attack)
    {
        soliderIcon.sprite = sprite;

        if (!Attack)
        {
            soliderIcon.transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f);
        }
    }

    public void ShowSoldierHP(Sprite sprite, string hp)
    {
        soliderHPText.text = hp;
        soliderIcon.sprite = sprite;
    }

    public void ShowSoldierStatus(Sprite sprite, string hp, string lv)
    {
        soliderHPText.text = "HP " + hp;
        soliderLvText.text = "Lv " + lv;
        soliderIcon.sprite = sprite;
    }

    public void ShowBattleSoldier(Sprite sprite, int hp, int maxHP)
    {
        soliderHPText.text = hp.ToString();
        soliderIcon.sprite = sprite;
        hpSlider.fillAmount = (float)hp / (float)maxHP;
        if (hpSlider.fillAmount <= 0.6)
        {
            hpSlider.color = Color.green;
        }
        if (hpSlider.fillAmount <= 0.4)
        {
            hpSlider.color = Color.yellow;
        }
        if (hpSlider.fillAmount <= 0.2)
        {
            hpSlider.color = Color.red;
        }
    }
}
