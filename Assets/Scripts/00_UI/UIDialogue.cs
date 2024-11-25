using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class UIDialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI diaText;

    [SerializeField] private Button pushButton;

    [SerializeField] private UtilityParamObject varParam;

    private void Start()
    {
        SoundManager.instance.PlayDialogueSE();

        diaText.text = varParam.DialogueText;

        pushButton.OnClickAsObservable().Subscribe(async _ =>
        {
            await OnPressClose();
        });

        SceneController.instance.Stack.Add("UIDialogue");
    }

    private void OnDestroy()
    {
        SceneController.instance.Stack.Remove("UIDialogue");
    }

    public async UniTask OnPressClose() => await SceneController.UnloadAsync("UIDialogue");
}
