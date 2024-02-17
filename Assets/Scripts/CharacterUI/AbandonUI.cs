using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbandonUI : MonoBehaviour
{
    private Color originalColor; // Œ³‚Ì”wŒiF‚ğ•Û‚·‚é•Ï”

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
            // Œ³‚Ì”wŒiF‚ğ•Û
            originalColor = image.color;
        }
    }

    public void OnPointerEnterAbandon()
    {
        // UI‚Ì”wŒiF‚ğŠDF‚É•ÏX
        ChangeBackgroundColor(Color.gray);
    }

    public void OnPointerExit()
    {
        if (clickedFlag == false)
        {
            // UI‚Ì”wŒiF‚ğŒ³‚É–ß‚·
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
        //yesNoUI‚ª”ñ•\¦‚É‚È‚é‚Ü‚Å‘Ò‹@
        yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());
        clickedFlag = false;

        // UI‚Ì”wŒiF‚ğŒ³‚É–ß‚·
        ChangeBackgroundColor(originalColor);

        if (yesNoUI.IsYes())
        {
            characterIndexMenu.gameObject.SetActive(false);
            characterIndexUI.HideCharacterIndexUI();
            characterDetailUI.gameObject.SetActive(false);

            battleManager.AbandonBattle();
        }
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
}
