using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;

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
        loadGameButton.onClick.AddListener(() => SceneManager.LoadScene("UISaveLoad"));
        endGameButton.onClick.AsObservable().Subscribe(_ => GameEnd());
        #endregion  
    }

    public void GameStart()
    {
        //await SceneController.LoadAsync(nameof(SaveLoadUI));
        
        GameManager.instance.ChangeScene("Title", "GameMain");
    }

    public void GameEnd()
    {
        Application.Quit();
    }
}
