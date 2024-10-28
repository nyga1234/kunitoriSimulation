using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;

public class FunctionUI : MonoBehaviour
{
    [SerializeField] private Button maskButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button configButton;
    [SerializeField] private Button titleButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button closeButton;

    private void Start()
    {
        maskButton.onClick.AddListener(() => this.gameObject.SetActive(false));

        saveButton.onClick.AsObservable().Subscribe(async _ =>
        {
            SaveLoadManager.IsSaving = true;
            await SceneController.LoadAsync("UISaveLoad");
        });

        loadButton.onClick.AsObservable().Subscribe(async _ =>
        {
            SaveLoadManager.IsSaving = false;
            await SceneController.LoadAsync("UISaveLoad");
        });

        titleButton.onClick.AsObservable().Subscribe(async _ => { await GameManager.instance.ChangeScene("GameMain", "Title"); });

        quitButton.onClick.AddListener(QuitGame);

        closeButton.onClick.AddListener(() => this.gameObject.SetActive(false));
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
