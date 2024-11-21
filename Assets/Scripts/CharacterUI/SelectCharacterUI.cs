using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(EventTrigger))]
public class SelectCharacterUI : MonoBehaviour
{
    private Button _button;
    public Button Button => _button;

    [SerializeField] CharacterDetailUI characterDetailUI;

    private CharacterController characterController;
    private Color originalColor; // Œ³‚Ì”wŒiF‚ğ•Û‚·‚é•Ï”

    void Awake()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // Œ³‚Ì”wŒiF‚ğ•Û
            originalColor = image.color;
        }

        _button = GetComponent<Button>();
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();

        { /* On Click */
            _button.onClick.AddListener(() =>
            {
                Debug.Log("ƒNƒŠƒbƒN‚³‚ê‚Ü‚µ‚½");
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
    /// EventTrigger‚ÉƒCƒxƒ“ƒg‚ğ“o˜^‚·‚é‹¤’Êˆ—
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
        characterDetailUI.ShowCharacterDetailUI(characterController);
    }

    private void OnDeselectEvent(BaseEventData baseEvent)
    {
        Debug.Log("OnDeselectEvent");
        // UI‚Ì”wŒiF‚ğŒ³‚É–ß‚·
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

    //”wŒiF‚ğ•ÏX
    private void ChangeBackgroundColor(Color color)
    {
        Debug.Log($"”wŒiF‚ğ•ÏX{color}");
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
