using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoliderEntity", menuName = "Create SoliderEntity")]
public class SoliderEntity : ScriptableObject
{
    //public int soliderID;
    public int hp;
    public int maxHP;
    public int at;
    public int df;
    public int force;
    public int lv;
    public int experience;
    public Sprite icon;
}
