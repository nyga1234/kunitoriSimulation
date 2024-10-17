using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


public class CommandOnMouse : MonoBehaviour
{
    public GameMain gameManager;

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
        SoundManager.instance.PlayCursorSE();
        titleFieldUI.ShowInformationText();
        consumptionMoneyUI.ShowConsumptionText(0);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterAppointment()
    {
        SoundManager.instance.PlayCursorSE();
        titleFieldUI.ShowAppointmentText();
        consumptionMoneyUI.ShowConsumptionText(0);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterSearch()
    {
        SoundManager.instance.PlayCursorSE();
        titleFieldUI.ShowSearchText();
        consumptionMoneyUI.ShowConsumptionText(9);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterBanishment()
    {
        SoundManager.instance.PlayCursorSE();
        titleFieldUI.ShowBanishmentText();
        consumptionMoneyUI.ShowConsumptionText(0);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterAlliance()
    {
        SoundManager.instance.PlayCursorSE();
        titleFieldUI.ShowAllianceText();
        consumptionMoneyUI.ShowConsumptionText(5);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterLaureate()
    {
        SoundManager.instance.PlayCursorSE();
        titleFieldUI.ShowLaureateText();
        consumptionMoneyUI.ShowConsumptionText(0);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterRecruit()
    {
        SoundManager.instance.PlayCursorSE();
        titleFieldUI.ShowRecruitText();
        consumptionMoneyUI.ShowConsumptionText(2);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterTraining()
    {
        SoundManager.instance.PlayCursorSE();
        titleFieldUI.ShowTrainingText();
        consumptionMoneyUI.ShowConsumptionText(2);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterEnter()
    {
        if (GameMain.instance.playerCharacter.influence == GameMain.instance.noneInfluence)
        {
            SoundManager.instance.PlayCursorSE();
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
        if (gameManager.playerCharacter.isLord == true || GameMain.instance.playerCharacter.influence == GameMain.instance.noneInfluence)
        {
            return;
        }
        else
        {
            SoundManager.instance.PlayCursorSE();
            titleFieldUI.ShowVagabondText();
            consumptionMoneyUI.ShowConsumptionText(0);
            // UI�̔w�i�F���D�F�ɕύX
            ChangeBackgroundColor(Color.gray);
        }
    }
    
    public void OnPointerEnterRebellion()
    {
        if (gameManager.playerCharacter.isLord == true || GameMain.instance.playerCharacter.influence == GameMain.instance.noneInfluence)
        {
            return;
        }
        else 
        {
            SoundManager.instance.PlayCursorSE();
            titleFieldUI.ShowRebellionText();
            consumptionMoneyUI.ShowConsumptionText(0);
            // UI�̔w�i�F���D�F�ɕύX
            ChangeBackgroundColor(Color.gray);
        }
    }

    public void OnPointerEnterAttack()
    {
        if (GameMain.instance.playerCharacter.isLord == true)
        {
            titleFieldUI.ShowAttackText();
        }
        else
        {
            TitleFieldUI.instance.titleFieldText.text = "      �Ǝ����f�ŐN�U";
        }

        //���ʏ���
        SoundManager.instance.PlayCursorSE();
        consumptionMoneyUI.ShowConsumptionText(3);
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterProvoke()
    {
        SoundManager.instance.PlayCursorSE();
        titleFieldUI.ShowProvokeText();
        consumptionMoneyUI.ShowConsumptionText(9);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterSubdue()
    {
        SoundManager.instance.PlayCursorSE();
        titleFieldUI.ShowSubdueText();
        consumptionMoneyUI.ShowConsumptionText(3);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterFunction()
    {
        SoundManager.instance.PlayCursorSE();
        titleFieldUI.ShowFunctionText();
        consumptionMoneyUI.ShowConsumptionText(0);
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerEnterEnd()
    {
        SoundManager.instance.PlayCursorSE();
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
