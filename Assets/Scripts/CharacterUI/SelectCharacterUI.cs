using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static GameMain;
using UnityEngine.TextCore.Text;
using UnityEditor.VersionControl;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(EventTrigger))]
public class SelectCharacterUI : MonoBehaviour
{
    private Button _button;
    public Button Button => _button;

    private CharacterController characterController;
    private Color originalColor; // 元の背景色を保持する変数

    void Awake()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // 元の背景色を保持
            originalColor = image.color;
        }

        _button = GetComponent<Button>();
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();

        { /* On Click */
            _button.onClick.AddListener(() =>
            {
                Debug.Log("クリックされました");
                //SoundManager.instance.PlayClickSE();
                //EventSystem.current.SetSelectedGameObject(null);
            });
        }

        AddEvent(eventTrigger, EventTriggerType.Select, OnSelectEvent);
        AddEvent(eventTrigger, EventTriggerType.PointerEnter, OnPointerEnterEvent);
        AddEvent(eventTrigger, EventTriggerType.PointerExit, OnPointerExitEvent);
        AddEvent(eventTrigger, EventTriggerType.Deselect, OnDeselectEvent);
    }

    /// <summary>
    /// EventTriggerにイベントを登録する共通処理
    /// </summary>
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
        GameMain.instance.CharacterDetailUI.ShowCharacterDetailUI(characterController);

        switch (GameMain.instance.step)
        {
            case Step.Appointment:
                TitleFieldUI.instance.titleFieldText.text = "選択したキャラクターを昇格";
                break;

            //default:
            //    Debug.LogWarning("未知のゲームステップです。");
            //    break;
        }
    }

    private void OnDeselectEvent(BaseEventData baseEvent)
    {
        Debug.Log("OnDeselectEvent");
        // UIの背景色を元に戻す
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

    //背景色を変更
    private void ChangeBackgroundColor(Color color)
    {
        Debug.Log($"背景色を変更{color}");
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }

    public void SetCharacterController(CharacterController character)
    {
        characterController = character;
    }
}
