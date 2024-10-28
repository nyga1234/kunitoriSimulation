using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using UniRx;
using TMPro;

public class ConfirmUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI confirmText;

    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;

    [SerializeField] private UtilityParamObject varParam;

    private void Start()
    {
        confirmText.text = varParam.ConfirmText;
        varParam.IsConfirm = null;

        okButton.OnClickAsObservable().Subscribe(async _ =>
        {
            varParam.IsConfirm = true;
            await  OnPressClose();
        });

        cancelButton.onClick.AsObservable().Subscribe(async _ =>
        {
            varParam.IsConfirm = false;
            await OnPressClose();
        });

        SceneController.instance.Stack.Add("UIConfirm");
    }

    private void OnDestroy()
    {
        SceneController.instance.Stack.Remove("UIConfirm");
    }

    public async UniTask OnPressClose() => await SceneController.UnloadAsync("UIConfirm");
}