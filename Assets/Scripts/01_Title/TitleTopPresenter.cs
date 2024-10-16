using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using UnityEditor.VersionControl;
using System;

public class TitleTopPresenter : MonoBehaviour
{
    #region View
    [Header("View")]
    [SerializeField]
    private Button newGameButton;
    [SerializeField]
    private Button loadGameButton;
    [SerializeField]
    private Button endGameButton;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        #region View to Model
        newGameButton.onClick.AsObservable().Subscribe(_ => GameStart());

        //loadGameButton.onClick.AsObservable().Subscribe(async async => await SceneController.LoadAsync("UISaveLoad"));
        loadGameButton.onClick.AsObservable().Subscribe(async _ =>
        {
            SaveLoadManager.IsSaving = false;
            await SceneController.LoadAsync("UISaveLoad");
        });

        endGameButton.onClick.AsObservable().Subscribe(_ => GameEnd());
        #endregion  

        SceneController.instance.Stack.Add("Title");
    }

    private void OnDestroy()
    {
        SceneController.instance.Stack.Remove("Title");   
    }

    private void GameStart()
    {
        //await SceneController.LoadAsync(nameof(SaveLoadUI));
        
        GameManager.instance.ChangeScene("Title", "GameMain");
    }

    private void GameEnd()
    {
        Application.Quit();
    }

    //private async void OpneSaveLoadWindow()
    //{
    //    await SceneController.LoadAsync("UISaveLoad");
    //}
}
