using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoldierImageView : MonoBehaviour
{
    [SerializeField] Image soliderIcon;

    public void ShowSoldierImage(Sprite sprite, bool Attack)
    {
        soliderIcon.sprite = sprite;

        if (!Attack)
        {
            soliderIcon.transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f);
        }
    }
}
