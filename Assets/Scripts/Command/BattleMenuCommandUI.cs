using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenuCommandUI : MonoBehaviour
{
    [SerializeField] AttackCommandUI attackCommandUI;

    public void ShowBattleMenuCommandUI()
    {
        gameObject.SetActive(true);
        attackCommandUI.ShowAttackCommandUI();
    }
}
