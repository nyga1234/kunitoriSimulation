using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderController : MonoBehaviour
{
    public SoliderModel soliderModel;
    SoliderView soliderView;
    ImageSoliderView imageSoliderView;
    PersonalSoliderView personalSoliderView;
    BattleSoliderView battleSoliderView;

    //public CharacterController character;

    private void Awake()
    {
        soliderView = GetComponent<SoliderView>();
        imageSoliderView = GetComponent<ImageSoliderView>();
        personalSoliderView = GetComponent<PersonalSoliderView>();
        battleSoliderView = GetComponent<BattleSoliderView>();
    }

    public void Init(int soliderId)
    {
        soliderModel = new SoliderModel(soliderId);
    }

    public void ShowBattleSoliderUI(SoliderController solider, bool Attack)
    {
        soliderView.ShowSoliderUI(solider.soliderModel, Attack);
    }

    public void ShowImageSoliderUI(SoliderController solider)
    {
        imageSoliderView.ShowImageSoliderUI(solider.soliderModel);
    }

    public void ShowPersonalSoliderUI(SoliderController solider)
    {
        personalSoliderView.ShowPersonalSoliderUI(solider.soliderModel);
    }

    public void ShowBattleDetailSoliderUI(SoliderController solider, bool Attack)
    {
        battleSoliderView.ShowBattleSoliderUI(solider.soliderModel, Attack);
    }

    //public void CheckAlive()
    //{
    //    if (soliderModel.isAlive)
    //    {
    //        soliderView.ShowSoliderUI(soliderModel);
    //    }
    //    else
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    /*
    // キャラクターを設定するメソッド
    public void SetCharacter(CharacterController character)
    {
        this.character = character;
    }
    */
}
