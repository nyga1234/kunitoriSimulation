//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//////�g��
////public enum Rank
////{
////    �Q�m = 0,
////    �y�� = 1,
////    ���� = 2,
////    ���� = 3,
////    �叫 = 4,
////    �⍲ = 5,
////    �̎� = 6,
////}

////[CreateAssetMenu(fileName = "CharacterModel", menuName = "CreateCharacterModel")]
////public class CharacterModel : ScriptableObject
////{
////    public Sprite icon;
////    public new string name;
////    public int force; //�퓬
////    public int inteli;//�q�d
////    public int tact;//��r
////    public Rank rank;//�g��
////    public int fame;//����
////    public int ambition;//��S
////    public int gold;//����
////    public bool isLord;//�̎傩���R��
////    public bool isPlayerCharacter;

////    public int loyalty;//����
////    public int salary;//����%
////    public int conflict;//�G��
////    public bool isAttackable = true;
////    public bool isBattle = false;

////    //private int loyalty;//����
////    //public int Loyalty { get { return loyalty; } set { loyalty = value; } }
////    //private int salary;//����%
////    //public int Salary { get { return salary; } set { salary = value; } }
////    //private int conflict;//�G��
////    //public int Conflict { get { return conflict; } set { conflict = value; } }
////    //private bool isAttackable = true;
////    //public bool IsAttackable { get { return isAttackable; } set { isAttackable = value; } }
////    //private bool isBattle = false;
////    //public bool IsBattle { get { return isBattle; } set { isBattle = value; } }
////    //[SerializeField] Sprite icon;
////    //public Sprite Icon { get { return icon; } set { icon = value; } }

////    //[SerializeField] new string name;
////    //public string Name { get { return name; } set { name = value; } }

////    //[SerializeField] int force;//�퓬
////    //public int Force { get { return force; } set { force = value; } }

////    //[SerializeField] int inteli;//�q�d
////    //public int Inteli { get { return inteli; } set { inteli = value; } }

////    //[SerializeField] int tact;//��r
////    //public int Tact { get { return tact; } set { tact = value; } }

////    //[SerializeField] Rank rank;//�g��
////    //public Rank Rank { get { return rank; } set { rank = value; } }

////    //[SerializeField] int fame;//����
////    //public int Fame { get { return fame; } set { fame = value; } }

////    //[SerializeField] int ambition;//��S
////    //public int Ambition { get { return ambition; } set { ambition = value; } }

////    //[SerializeField] int gold;//����
////    //public int Gold { get { return gold; } set { gold = value; } }

////    //[SerializeField] bool isLord;//�̎傩���R��
////    //public bool IsLord { get { return isLord; } set { isLord = value; } }

////    //[SerializeField] bool isPlayerCharacter;
////    //public bool IsPlayerCharacter { get { return isPlayerCharacter; } set { isPlayerCharacter = value; } }
////}

//public class CharacterModel
//{
//    public int characterId; // �ǉ�
//    public Sprite icon;
//    public string name;
//    public int force; //�퓬
//    public int inteli;//�q�d
//    public int tact;//��r
//    public int fame;//����
//    public int ambition;//��S
//    public int loyalty;//����
//    public int salary;//����%
//    public int conflict;//�G��
//    public Rank rank; //�g��
//    public int gold;//����

//    public bool isLord;//�̎傩���R��
//    public bool isPlayerCharacter;
//    public bool isAttackable;
//    public bool isBattle;

//    public CharacterModel(int characterID)
//    {
//        CharacterEntity characterEntity = Resources.Load<CharacterEntity>("CharacterEntityList/Character" + characterID);
//        this.characterId = characterID;
//        this.icon = characterEntity.icon;
//        this.name = characterEntity.name;
//        this.force = characterEntity.force;
//        this.inteli = characterEntity.inteli;
//        this.tact = characterEntity.tact;
//        this.fame = characterEntity.fame;
//        this.ambition = characterEntity.ambition;
//        this.rank = Rank.�Q�m;
//        this.gold = characterEntity.gold;

//        this.isLord = characterEntity.isLord;
//        this.isPlayerCharacter = characterEntity.isPlayerCharacter;
//        this.isAttackable = true;
//        this.isBattle = false;
//    }
//}
