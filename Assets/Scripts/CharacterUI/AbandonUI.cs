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

    private Color originalColor; // ���̔w�i�F��ێ�����ϐ�

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
            // ���̔w�i�F��ێ�
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
        // UI�̔w�i�F�����ɖ߂�
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

    //�w�i�F��ύX
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
        varParam.ConfirmText = "�퓬��������܂����H";
        // OK�܂���Cancel�{�^�����N���b�N�����̂�ҋ@
        await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);

        if (varParam.IsConfirm == true)
        {
            GameMain.instance.CharacterIndexUI.HideCharacterIndexUI();
            GameMain.instance.CharacterDetailUI.gameObject.SetActive(false);

            battleManager.AbandonBattle();
        }
    }
}
