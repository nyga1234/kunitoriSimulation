using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[DisallowMultipleComponent]
public class TitleTopModel : MonoBehaviour
{
    [SerializeField]
    //private UtilityParamObject _param;

    public void GameStart()
    {
        //await SceneController.LoadAsync(nameof(SaveLoadUI));
        GameManager.instance.ChangeScene("Title", "GameMain");
    }

    public void GameEnd()
    {
        Application.Quit();
    }

    //public async void OpenOptionWindow()
    //{
    //    await SceneController.LoadAsync("Option");
    //}

    //public async void OpenLicenseWindow()
    //{
    //    await SceneController.LoadAsync(nameof(License));
    //}
}