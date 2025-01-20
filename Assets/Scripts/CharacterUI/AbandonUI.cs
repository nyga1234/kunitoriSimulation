using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(EventTrigger))]
public class AbandonUI : MonoBehaviour
{
    private Button _button;
    public Button Button => _button;

    private Color originalColor; // å≥ÇÃîwåiêFÇï€éùÇ∑ÇÈïœêî

    [SerializeField] BattleManager battleManager;

    [SerializeField] private UtilityParamObject varParam;

    public void ShowAbandonUI()
    {
        ChangeBackgroundColor(originalColor);
        this.gameObject.SetActive(true);
    }

    public void HideAbandonUI()
    {
        this.gameObject.SetActive(false);
    }

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // å≥ÇÃîwåiêFÇï€éù
            originalColor = image.color;
        }

        _button = GetComponent<Button>();
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();

        { /* On Click */
            _button.onClick.AddListener(() => {
                //SoundManager.instance.PlayClickSE();
                StartAbandonBattle();
            });
        }

        AddEvent(eventTrigger, EventTriggerType.Select, OnSelectEvent);
        AddEvent(eventTrigger, EventTriggerType.PointerEnter, OnPointerEnterEvent);
        AddEvent(eventTrigger, EventTriggerType.PointerExit, OnPointerExitEvent);
        AddEvent(eventTrigger, EventTriggerType.Deselect, OnDeselectEvent);
    }

    private void AddEvent(EventTrigger eventTrigger, EventTriggerType eventType, UnityEngine.Events.UnityAction<BaseEventData> callback)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry
        {
            eventID = eventType
        };
        entry.callback.AddListener(callback);
        eventTrigger.triggers.Add(entry);
    }

    private void OnSelectEvent(BaseEventData baseEvent)
    {
        Debug.Log("OnSelectEvent");
        SoundManager.instance.PlayCursorSE();
        ChangeBackgroundColor(Color.gray);
    }

    private void OnDeselectEvent(BaseEventData baseEvent)
    {
        Debug.Log("OnDeselectEvent");
        // UIÇÃîwåiêFÇå≥Ç…ñﬂÇ∑
        ChangeBackgroundColor(Color.black);
    }

    private void OnPointerEnterEvent(BaseEventData baseEvent)
    {
        if (_button.interactable == false)
        {
            return;
        }

        _button.Select();
    }

    private void OnPointerExitEvent(BaseEventData baseEvent)
    {
        Debug.Log("OnPointerExitEvent");
        if (_button.interactable == false)
        {
            return;
        }

        //EventSystem.current.SetSelectedGameObject(null);
    }

    //îwåiêFÇïœçX
    private void ChangeBackgroundColor(Color color)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }

    private async void StartAbandonBattle()
    {
        await SceneController.LoadAsync("UIConfirm");
        varParam.ConfirmText = "êÌì¨Çï˙ä¸ÇµÇ‹Ç∑Ç©ÅH";
        // OKÇ‹ÇΩÇÕCancelÉ{É^ÉìÇ™ÉNÉäÉbÉNÇ≥ÇÍÇÈÇÃÇë“ã@
        await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);

        if (varParam.IsConfirm == true)
        {
            GameMain.instance.CharacterIndexUI.HideCharacterIndexUI();
            GameMain.instance.CharacterDetailUI.gameObject.SetActive(false);

            battleManager.AbandonBattle();
        }
    }
}
