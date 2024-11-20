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
                //SoundManager.instance.PlayClickSE();
                EventSystem.current.SetSelectedGameObject(null);
            });
        }

        { /* Select */
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener(OnSelectEvent);
            eventTrigger.triggers.Add(entry);
        }

        { /* Deselect */
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Deselect;
            entry.callback.AddListener(OnDeselectEvent);
            eventTrigger.triggers.Add(entry);
        }

        { /* Enter */
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerEnter;
            entry.callback.AddListener(OnPointerEnterEvent);
            eventTrigger.triggers.Add(entry);
        }

        { /* Exit */
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerExit;
            entry.callback.AddListener(OnPointerExitEvent);
            eventTrigger.triggers.Add(entry);
        }
    }

    private void OnSelectEvent(BaseEventData baseEvent)
    {
        SoundManager.instance.PlayCursorSE();
        ChangeBackgroundColor(Color.gray);
        characterDetailUI.ShowCharacterDetailUI(characterController);
    }

    private void OnDeselectEvent(BaseEventData baseEvent)
    {
        //Debug.Log("OnDeselectEvent");
        // UI‚Ì”wŒiF‚ğŒ³‚É–ß‚·
        ChangeBackgroundColor(originalColor);
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
        //Debug.Log("OnPointerExitEvent");
        if (_button.interactable == false)
        {
            return;
        }

        EventSystem.current.SetSelectedGameObject(null);
    }

    //”wŒiF‚ğ•ÏX
    private void ChangeBackgroundColor(Color color)
    {
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
