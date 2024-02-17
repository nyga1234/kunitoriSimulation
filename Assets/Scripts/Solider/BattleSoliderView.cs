using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSoliderView : MonoBehaviour
{
    [SerializeField] Image soliderIcon;

    public void ShowBattleSoliderUI(SoliderModel soliderModel)
    {
        soliderIcon.sprite = soliderModel.icon;
    }
}
