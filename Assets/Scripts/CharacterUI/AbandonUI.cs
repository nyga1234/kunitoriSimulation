using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbandonUI : MonoBehaviour
{
    private Color originalColor; // ���̔w�i�F��ێ�����ϐ�

    [SerializeField] GameObject characterIndexMenu;
    [SerializeField] CharacterIndexUI characterIndexUI;
    [SerializeField] CharacterDetailUI characterDetailUI;
    [SerializeField] YesNoUI yesNoUI;
    [SerializeField] BattleManager battleManager;

    private bool clickedFlag = false;

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
    }

    public void OnPointerEnterAbandon()
    {
        // UI�̔w�i�F���D�F�ɕύX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerExit()
    {
        if (clickedFlag == false)
        {
            // UI�̔w�i�F�����ɖ߂�
            ChangeBackgroundColor(originalColor);
        }
    }

    public void OnPointerClickAbandon()
    {
        if (Input.GetMouseButtonUp(0))
        {
            clickedFlag = true;
            StartCoroutine(WaitForAbandon());
        }       
    }

    IEnumerator WaitForAbandon()
    {
        yesNoUI.ShowAbandonYesNoUI();
        //yesNoUI����\���ɂȂ�܂őҋ@
        yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());
        clickedFlag = false;

        // UI�̔w�i�F�����ɖ߂�
        ChangeBackgroundColor(originalColor);

        if (yesNoUI.IsYes())
        {
            characterIndexMenu.gameObject.SetActive(false);
            characterIndexUI.HideCharacterIndexUI();
            characterDetailUI.gameObject.SetActive(false);

            battleManager.AbandonBattle();
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
