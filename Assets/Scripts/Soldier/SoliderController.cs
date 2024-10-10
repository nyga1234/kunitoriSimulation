using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoliderController : MonoBehaviour
{
    public SoliderModel soliderModel;
    public CharacterController character;

    //SoliderView soliderView;
    //ImageSoliderView imageSoliderView;
    //PersonalSoliderView personalSoliderView;
    //BattleSoliderView battleSoliderView;

    private void Awake()
    {
        //soliderView = GetComponent<SoliderView>();
        //imageSoliderView = GetComponent<ImageSoliderView>();
        //personalSoliderView = GetComponent<PersonalSoliderView>();
        //battleSoliderView = GetComponent<BattleSoliderView>();
    }

    public void Init(int soliderId, int soliderUniqueId)
    {
        soliderModel = new SoliderModel(soliderId, soliderUniqueId);
    }

    //public void ShowBattleSoliderUI(SoliderController solider, bool Attack)
    //{
    //    soliderView.ShowSoliderUI(solider.soliderModel);
    //}

    //public void ShowImageSoliderUI(SoliderController solider)
    //{
    //    imageSoliderView.ShowImageSoliderUI(solider.soliderModel);
    //}

    //public void ShowPersonalSoliderUI(SoliderController solider)
    //{
    //    personalSoliderView.ShowPersonalSoliderUI(solider.soliderModel);
    //}

    //public void ShowBattleDetailSoliderUI(SoliderController solider, bool Attack)
    //{
    //    battleSoliderView.ShowBattleSoliderUI(solider.soliderModel, Attack);
    //}
}
