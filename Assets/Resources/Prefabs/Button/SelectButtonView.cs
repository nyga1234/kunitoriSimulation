using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;
using DG.Tweening.Core.Easing;
using Unity.VisualScripting;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(EventTrigger))]
public class SelectButtonView : MonoBehaviour
{
    private Button _button;
    public Button Button => _button;

    [SerializeField] TitleFieldUI titleFieldUI;
    [SerializeField] ConsumptionMoneyUI consumptionMoneyUI;

    private Color originalColor; // ���̔w�i�F��ێ�����ϐ�

    void Awake()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // ���̔w�i�F��ێ�
            originalColor = image.color;
        }

        _button = GetComponent<Button>();
        EventTrigger eventTrigger = gameObject.GetComponent<EventTrigger>();

        { /* Select */
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.Select;
            entry.callback.AddListener(OnSelectEvent);
            eventTrigger.triggers.Add(entry);
        }

        //{ /* Deselect */
        //    EventTrigger.Entry entry = new EventTrigger.Entry();
        //    entry.eventID = EventTriggerType.Deselect;
        //    entry.callback.AddListener(OnDeselectEvent);
        //    eventTrigger.triggers.Add(entry);
        //}

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

        {/* On Click */
            _button.onClick.AddListener(() =>
            {
                EventSystem.current.SetSelectedGameObject(null);
            });

        }
    }

    private void OnSelectEvent(BaseEventData baseEvent)
    {
        // �{�^�����Ƃ̏������X�C�b�`���Ŏ��s
        switch (gameObject.name)
        {
            case "Information":
                titleFieldUI.ShowInformationText();
                consumptionMoneyUI.ShowConsumptionText(0);
                break;
            case "Appointment":
                titleFieldUI.ShowAppointmentText();
                consumptionMoneyUI.ShowConsumptionText(0);
                break;
            case "Search":
                titleFieldUI.ShowSearchText();
                consumptionMoneyUI.ShowConsumptionText(9);
                break;
            case "Banishment":
                titleFieldUI.ShowBanishmentText();
                consumptionMoneyUI.ShowConsumptionText(0);
                break;
            case "Alliance":
                titleFieldUI.ShowAllianceText();
                consumptionMoneyUI.ShowConsumptionText(5);
                break;
            case "Laureate":
                titleFieldUI.ShowLaureateText();
                consumptionMoneyUI.ShowConsumptionText(0);
                break;
            case "Recruit":
                titleFieldUI.ShowRecruitText();
                consumptionMoneyUI.ShowConsumptionText(2);
                break;
            case "Training":
                titleFieldUI.ShowTrainingText();
                consumptionMoneyUI.ShowConsumptionText(2);
                break;
            case "Enter":
                if (GameMain.instance.playerCharacter.influence == GameMain.instance.noneInfluence)
                {
                    titleFieldUI.ShowEnterText();
                    consumptionMoneyUI.ShowConsumptionText(0);
                }
                break;
            case "Vagabond":
                if (GameMain.instance.playerCharacter.isLord == true || GameMain.instance.playerCharacter.influence == GameMain.instance.noneInfluence)
                {
                    return;
                }
                else
                {
                    titleFieldUI.ShowVagabondText();
                    consumptionMoneyUI.ShowConsumptionText(0);
                }
                break;
            case "Rebellion":
                if (GameMain.instance.playerCharacter.isLord == true || GameMain.instance.playerCharacter.influence == GameMain.instance.noneInfluence)
                {
                    return;
                }
                else
                {
                    titleFieldUI.ShowRebellionText();
                    consumptionMoneyUI.ShowConsumptionText(0);
                }
                break;
            case "Attack":
                if (GameMain.instance.playerCharacter.isLord)
                {
                    titleFieldUI.ShowAttackText();
                }
                else
                {
                    TitleFieldUI.instance.titleFieldText.text = "      �Ǝ����f�ŐN�U";
                }
                consumptionMoneyUI.ShowConsumptionText(3);
                break;
            case "Provoke":
                titleFieldUI.ShowProvokeText();
                consumptionMoneyUI.ShowConsumptionText(9);
                break;
            case "Subdue":
                titleFieldUI.ShowSubdueText();
                consumptionMoneyUI.ShowConsumptionText(3);
                break;
            case "Function":
                titleFieldUI.ShowFunctionText();
                consumptionMoneyUI.ShowConsumptionText(0);
                break;
            case "End":
                titleFieldUI.ShowEndText();
                consumptionMoneyUI.ShowConsumptionText(0);
                break;
            default:
                break;
        }

        SoundManager.instance.PlayCursorSE();
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    //private void OnDeselectEvent(BaseEventData baseEvent)
    //{
        
    //}

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
        if (_button.interactable == false)
        {
            return;
        }

        EventSystem.current.SetSelectedGameObject(null);

        // UI�̔w�i�F�����ɖ߂�
        ChangeBackgroundColor(originalColor);
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
}