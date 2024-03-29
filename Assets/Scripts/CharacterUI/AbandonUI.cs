using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbandonUI : MonoBehaviour
{
    private Color originalColor; // 元の背景色を保持する変数

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
            // 元の背景色を保持
            originalColor = image.color;
        }
    }

    public void OnPointerEnterAbandon()
    {
        // UIの背景色を灰色に変更
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerExit()
    {
        if (clickedFlag == false)
        {
            // UIの背景色を元に戻す
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
        //yesNoUIが非表示になるまで待機
        yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());
        clickedFlag = false;

        // UIの背景色を元に戻す
        ChangeBackgroundColor(originalColor);

        if (yesNoUI.IsYes())
        {
            characterIndexMenu.gameObject.SetActive(false);
            characterIndexUI.HideCharacterIndexUI();
            characterDetailUI.gameObject.SetActive(false);

            battleManager.AbandonBattle();
        }
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
