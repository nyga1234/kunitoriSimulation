using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//g•ª
public enum Rank2
{
    ˜Qm = 0,
    y« = 1,
    ­« = 2,
    ’†« = 3,
    ‘å« = 4,
    •â² = 5,
    —Ìå = 6,
}

[CreateAssetMenu(fileName = "Character", menuName = "CreateCharacter")]
public class Character : ScriptableObject
{
    public Sprite icon;
    public new string name;
    public int force; //í“¬
    public int inteli;//’q–d
    public int tact;//è˜r
    public Rank rank;//g•ª
    public int fame;//–¼º
    public int ambition;//–ìS
    public int gold;//‚¨‹à
    public bool isLord;//—Ìå‚©«ŒR‚©
    public bool isPlayerCharacter;

    private int loyalty;//’‰½
    public int Loyalty { get { return loyalty; } set { loyalty = value; } }
    private int salary;//‹‹—¿%
    public int Salary { get { return salary; } set { salary = value; } }
    private int conflict;//“GˆÓ
    public int Conflict { get { return conflict; } set { conflict = value; } }
    private bool isAttackable = true;
    public bool IsAttackable { get { return isAttackable; } set { isAttackable = value; } }
    private bool isBattle = false;
    public bool IsBattle { get { return isBattle; } set { isBattle = value; } }
    //[SerializeField] Sprite icon;
    //public Sprite Icon { get { return icon; } set { icon = value; } }

    //[SerializeField] new string name;
    //public string Name { get { return name; } set { name = value; } }

    //[SerializeField] int force;//í“¬
    //public int Force { get { return force; } set { force = value; } }

    //[SerializeField] int inteli;//’q–d
    //public int Inteli { get { return inteli; } set { inteli = value; } }

    //[SerializeField] int tact;//è˜r
    //public int Tact { get { return tact; } set { tact = value; } }

    //[SerializeField] Rank rank;//g•ª
    //public Rank Rank { get { return rank; } set { rank = value; } }

    //[SerializeField] int fame;//–¼º
    //public int Fame { get { return fame; } set { fame = value; } }

    //[SerializeField] int ambition;//–ìS
    //public int Ambition { get { return ambition; } set { ambition = value; } }

    //[SerializeField] int gold;//‚¨‹à
    //public int Gold { get { return gold; } set { gold = value; } }

    //[SerializeField] bool isLord;//—Ìå‚©«ŒR‚©
    //public bool IsLord { get { return isLord; } set { isLord = value; } }

    //[SerializeField] bool isPlayerCharacter;
    //public bool IsPlayerCharacter { get { return isPlayerCharacter; } set { isPlayerCharacter = value; } }
}
