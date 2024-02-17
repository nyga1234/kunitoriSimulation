using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


public class CommandOnMouse : MonoBehaviour
{
    public GameManager gameManager;

    [SerializeField] CommandOnClick commandOnClick;
    [SerializeField] TitleFieldUI titleFieldUI;
    [SerializeField] ConsumptionMoneyUI consumptionMoneyUI;

    private Color originalColor; // ���̔w�i�F��ێ�����ϐ�

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // ���̔w�i�F��ێ�
            originalColor = image.color;
        }
    }

    public void OnPointerEnterInformation()
    {
        titleFieldUI.ShowInformationText();
        consumptionMoneyUI.ShowConsumptionText(0);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterAppointment()
    {
        titleFieldUI.ShowAppointmentText();
        consumptionMoneyUI.ShowConsumptionText(0);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterSearch()
    {
        titleFieldUI.ShowSearchText();
        consumptionMoneyUI.ShowConsumptionText(9);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterBanishment()
    {
        titleFieldUI.ShowBanishmentText();
        consumptionMoneyUI.ShowConsumptionText(0);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterAlliance()
    {
        titleFieldUI.ShowAllianceText();
        consumptionMoneyUI.ShowConsumptionText(5);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterLaureate()
    {
        titleFieldUI.ShowLaureateText();
        consumptionMoneyUI.ShowConsumptionText(0);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterRecruit()
    {
        titleFieldUI.ShowRecruitText();
        consumptionMoneyUI.ShowConsumptionText(2);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterTraining()
    {
        titleFieldUI.ShowTrainingText();
        consumptionMoneyUI.ShowConsumptionText(2);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterEnter()
    {
        if (GameManager.instance.playerCharacter.influence == GameManager.instance.noneInfluence)
        {
            titleFieldUI.ShowEnterText();
            consumptionMoneyUI.ShowConsumptionText(0);
            // UI�̔w�i�F���D�F�ɕύX
            ChangeBackgroundColor(Color.gray);
        }
        else
        {
            return;
        }
    }

    public void OnPointerEnterVagabond()
    {
        if (gameManager.playerCharacter.characterModel.isLord == true || GameManager.instance.playerCharacter.influence == GameManager.instance.noneInfluence)
        {
            return;
        }
        else
        {
            titleFieldUI.ShowVagabondText();
            consumptionMoneyUI.ShowConsumptionText(0);
            // UI�̔w�i�F���D�F�ɕύX
            ChangeBackgroundColor(Color.gray);
        }
    }
    
    public void OnPointerEnterRebellion()
    {
        if (gameManager.playerCharacter.characterModel.isLord == true || GameManager.instance.playerCharacter.influence == GameManager.instance.noneInfluence)
        {
            return;
        }
        else 
        {
            titleFieldUI.ShowRebellionText();
            consumptionMoneyUI.ShowConsumptionText(0);
            // UI�̔w�i�F���D�F�ɕύX
            ChangeBackgroundColor(Color.gray);
        }
    }

    public void OnPointerEnterAttack()
    {
        if (GameManager.instance.playerCharacter.characterModel.isLord == true)
        {
            titleFieldUI.ShowAttackText();
        }
        else
        {
            TitleFieldUI.instance.titleFieldText.text = "      �Ǝ����f�ŐN�U";
        }
        
        //���ʏ���
        consumptionMoneyUI.ShowConsumptionText(3);
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterProvoke()
    {
        titleFieldUI.ShowProvokeText();
        consumptionMoneyUI.ShowConsumptionText(9);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterSubdue()
    {
        titleFieldUI.ShowSubdueText();
        consumptionMoneyUI.ShowConsumptionText(3);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterFunction()
    {
        titleFieldUI.ShowFunctionText();
        consumptionMoneyUI.ShowConsumptionText(0);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterEnd()
    {
        titleFieldUI.ShowEndText();
        consumptionMoneyUI.ShowConsumptionText(0);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerExit()
    {
        if (commandOnClick.clickedFlag == false)
        {
            // UI�̔w�i�F�����ɖ߂�
            ChangeBackgroundColor(originalColor);
        }
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
