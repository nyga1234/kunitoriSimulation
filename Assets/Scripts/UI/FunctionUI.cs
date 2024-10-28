using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Cysharp.Threading.Tasks;

public class FunctionUI : MonoBehaviour
{
    [SerializeField] private Button maskButton;
    [SerializeField] private Button saveButton;
    //[SerializeField] private Button loadButton;
    [SerializeField] private Button configButton;
    [SerializeField] private Button titleButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private UtilityParamObject varParam;

    private void Start()
    {
        maskButton.onClick.AddListener(() => this.gameObject.SetActive(false));

        saveButton.onClick.AsObservable().Subscribe(async _ =>
        {
            SaveLoadManager.IsSaving = true;
            await SceneController.LoadAsync("UISaveLoad");
        });

        //loadButton.onClick.AsObservable().Subscribe(async _ =>
        //{
        //    SaveLoadManager.IsSaving = false;
        //    await SceneController.LoadAsync("UISaveLoad");
        //});

        titleButton.OnClickAsObservable().Subscribe(async _ => {
            varParam.ConfirmText = "タイトルへ戻りますか？";
            await SceneController.LoadAsync("UIConfirm");
            await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);
            if (varParam.IsConfirm == true)
            {
                await GameManager.instance.ChangeScene("GameMain", "Title");
            }
        });

        quitButton.OnClickAsObservable().Subscribe(async _ =>{
            varParam.ConfirmText = "ゲームを終了しますか？";
            await SceneController.LoadAsync("UIConfirm");
            await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);
            if (varParam.IsConfirm == true)
            {
                QuitGame();
            }
        });

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
