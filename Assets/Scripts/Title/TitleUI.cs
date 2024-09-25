using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button loadGameButton;
    [SerializeField] private Button exitButton;

    //[SerializeField] private SaveSlotUI saveslotUI;
    //[SerializeField] private LoadSlotUI loadslotUI;

    private void Start()
    {
        //NewGame
        newGameButton.onClick.AddListener(() => SceneManager.LoadScene("Main"));
        //ロードスロットUI表示
        loadGameButton.onClick.AddListener(() => SceneManager.LoadScene("UISaveLoad"));
        //ゲーム終了
        exitButton.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
