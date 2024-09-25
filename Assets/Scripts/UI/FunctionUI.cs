using UnityEngine;
using UnityEngine.UI;

public class FunctionUI : MonoBehaviour
{
    [SerializeField] private Button maskButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button quitButton;

    [SerializeField] private SaveLoadUI saveLoadUI;

    private void Start()
    {
        //閉じる
        maskButton.onClick.AddListener(() => this.gameObject.SetActive(false));

        // セーブボタンが押された場合
        saveButton.onClick.AddListener(() =>
        {
            saveLoadUI.gameObject.SetActive(true);  // SaveLoadUIを表示
            saveLoadUI.SetMode(true);  // セーブモードを設定
            this.gameObject.SetActive(false);
        });

        // ロードボタンが押された場合
        loadButton.onClick.AddListener(() =>
        {
            saveLoadUI.gameObject.SetActive(true); // SaveLoadUIを表示
            saveLoadUI.SetMode(false); // ロードモードを設定
            this.gameObject.SetActive(false);
        });
        //閉じる
        closeButton.onClick.AddListener(() => this.gameObject.SetActive(false));

        //ゲーム終了
        quitButton.onClick.AddListener(QuitGame);
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
