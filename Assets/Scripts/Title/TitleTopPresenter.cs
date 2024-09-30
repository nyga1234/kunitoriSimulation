using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;

public class TitleTopPresenter : MonoBehaviour
{
    [Header("Model")]
    [SerializeField]
    private TitleTopModel _model;

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
        newGameButton.onClick.AsObservable().Subscribe(_ => _model.GameStart());
        loadGameButton.onClick.AddListener(() => SceneManager.LoadScene("UISaveLoad"));
        endGameButton.onClick.AsObservable().Subscribe(_ => _model.GameEnd());
        #endregion  
    }

    //    [SerializeField] private Button newGameButton;
    //    [SerializeField] private Button loadGameButton;
    //    [SerializeField] private Button exitButton;

    //    private SaveLoadUI _saveLoadUI;
    //    public SaveLoadUI SaveLoadUI => _saveLoadUI;

    //    private void Start()
    //    {
    //        //NewGame
    //        newGameButton.onClick.AddListener(() => SceneManager.LoadScene("Main"));

    //        //ロードスロットUI表示
    //        loadGameButton.onClick.AddListener(() => {
    //            SceneManager.LoadScene("UISaveLoad");
    //            _saveLoadUI.SetMode(false); // ロードモードを設定
    //        });

    //        //ゲーム終了
    //        exitButton.onClick.AddListener(QuitGame);
    //    }

    //    private void QuitGame()
    //    {
    //#if UNITY_EDITOR
    //        UnityEditor.EditorApplication.isPlaying = false;
    //#else
    //        Application.Quit();
    //#endif
    //    }
}
