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
        //����
        maskButton.onClick.AddListener(() => this.gameObject.SetActive(false));

        // �Z�[�u�{�^���������ꂽ�ꍇ
        saveButton.onClick.AddListener(() =>
        {
            saveLoadUI.gameObject.SetActive(true);  // SaveLoadUI��\��
            saveLoadUI.SetMode(true);  // �Z�[�u���[�h��ݒ�
            this.gameObject.SetActive(false);
        });

        // ���[�h�{�^���������ꂽ�ꍇ
        loadButton.onClick.AddListener(() =>
        {
            saveLoadUI.gameObject.SetActive(true); // SaveLoadUI��\��
            saveLoadUI.SetMode(false); // ���[�h���[�h��ݒ�
            this.gameObject.SetActive(false);
        });
        //����
        closeButton.onClick.AddListener(() => this.gameObject.SetActive(false));

        //�Q�[���I��
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
