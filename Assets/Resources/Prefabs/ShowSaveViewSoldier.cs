using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowSaveViewSoldier : MonoBehaviour
{
    [SerializeField] Image soliderIcon;

    public void ShowSoldierUI(Sprite sprite)
    {
        soliderIcon.sprite = sprite;
    }
}
