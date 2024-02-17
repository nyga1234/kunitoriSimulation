using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoliderView : MonoBehaviour
{
    [SerializeField] Text hpText;
    [SerializeField] Image iconImage;
    [SerializeField] Image hpSlider;
    [SerializeField] GameObject status;

    public void ShowSoliderUI(SoliderModel soliderModel, bool Attack)
    {
        hpText.text = soliderModel.hp.ToString();
        iconImage.sprite = soliderModel.icon;
        hpSlider.fillAmount = (float)soliderModel.hp / (float)soliderModel.maxHP;
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

        if(Attack != true)
        {
            // iconImageを水平方向に反転
            iconImage.transform.localScale = new Vector3(-1, 1, 1);
            // iconImageのx座標を-1倍にする
            iconImage.transform.position = new Vector3(iconImage.transform.position.x - 120, iconImage.transform.position.y, iconImage.transform.position.z);
            //ステータス画面のx座標を-1倍にする
            status.transform.position = new Vector3(status.transform.position.x + 80, status.transform.position.y, status.transform.position.z);
        }
    }
}
