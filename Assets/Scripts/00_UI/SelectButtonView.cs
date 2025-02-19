using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(EventTrigger))]
public class SelectButtonView : MonoBehaviour
{
    private Button _button;
    public Button Button => _button;

    [SerializeField] ConsumptionMoneyUI consumptionMoneyUI;

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
            _button.onClick.AddListener(() => {
                SoundManager.instance.PlayClickSE();
                EventSystem.current.SetSelectedGameObject(null);
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
        // ボタンごとの処理をスイッチ文で実行
        switch (gameObject.name)
        {
            case "Information":
                TitleFieldUI.instance.ShowInformationText();
                consumptionMoneyUI.ShowConsumptionText(0);
                break;
            case "Appointment":
                TitleFieldUI.instance.ShowAppointmentText();
                consumptionMoneyUI.ShowConsumptionText(0);
                break;
            case "Search":
                TitleFieldUI.instance.ShowSearchText();
                consumptionMoneyUI.ShowConsumptionText(9);
                break;
            case "Banishment":
                TitleFieldUI.instance.ShowBanishmentText();
                consumptionMoneyUI.ShowConsumptionText(0);
                break;
            case "Alliance":
                TitleFieldUI.instance.ShowAllianceText();
                consumptionMoneyUI.ShowConsumptionText(5);
                break;
            case "Laureate":
                TitleFieldUI.instance.ShowLaureateText();
                consumptionMoneyUI.ShowConsumptionText(0);
                break;
            case "Recruit":
                TitleFieldUI.instance.ShowRecruitText();
                consumptionMoneyUI.ShowConsumptionText(2);
                break;
            case "Training":
                TitleFieldUI.instance.ShowTrainingText();
                consumptionMoneyUI.ShowConsumptionText(2);
                break;
            case "Enter":
                if (GameMain.instance.playerCharacter.influence != GameMain.instance.noneInfluence)
                {
                    return;
                }
                TitleFieldUI.instance.ShowEnterText();
                consumptionMoneyUI.ShowConsumptionText(0);
                break;
            case "Vagabond":
                if (GameMain.instance.playerCharacter.isLord == true || GameMain.instance.playerCharacter.influence == GameMain.instance.noneInfluence)
                {
                    return;
                }
                TitleFieldUI.instance.ShowVagabondText();
                consumptionMoneyUI.ShowConsumptionText(0);
                break;
            case "Rebellion":
                if (GameMain.instance.playerCharacter.isLord == true || GameMain.instance.playerCharacter.influence == GameMain.instance.noneInfluence)
                {
                    return;
                }
                TitleFieldUI.instance.ShowRebellionText();
                consumptionMoneyUI.ShowConsumptionText(0);
                break;
            case "Attack":
                if (GameMain.instance.playerCharacter.isLord)
                {
                    TitleFieldUI.instance.ShowAttackText();
                }
                else
                {
                    TitleFieldUI.instance.titleFieldText.text = "      独自判断で侵攻";
                }
                consumptionMoneyUI.ShowConsumptionText(3);
                break;
            case "Provoke":
                TitleFieldUI.instance.ShowProvokeText();
                consumptionMoneyUI.ShowConsumptionText(9);
                break;
            case "Subdue":
                TitleFieldUI.instance.ShowSubdueText();
                consumptionMoneyUI.ShowConsumptionText(3);
                break;
            case "Function":
                TitleFieldUI.instance.ShowFunctionText();
                consumptionMoneyUI.ShowConsumptionText(0);
                break;
            case "End":
                TitleFieldUI.instance.ShowEndText();
                consumptionMoneyUI.ShowConsumptionText(0);
                break;
            default:
                break;
        }

        //共通処理
        SoundManager.instance.PlayCursorSE();
        ChangeBackgroundColor(Color.gray);
    }

    private void OnDeselectEvent(BaseEventData baseEvent)
    {
        //Debug.Log("OnDeselectEvent");
        // UIの背景色を元に戻す
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

    //背景色を変更
    private void ChangeBackgroundColor(Color color)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }
}