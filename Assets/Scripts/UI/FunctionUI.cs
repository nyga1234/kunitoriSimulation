using UnityEngine;
using UnityEngine.UI;

public class FunctionUI : MonoBehaviour
{
    [SerializeField] private Button maskButton;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button quitButton;

    private void Start()
    {
        maskButton.onClick.AddListener(() => this.gameObject.SetActive(false));
        saveButton.onClick.AddListener(() => GameManager.instance.SaveGame());
        loadButton.onClick.AddListener(() => GameManager.instance.LoadGame());
        closeButton.onClick.AddListener(() => this.gameObject.SetActive(false));
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
