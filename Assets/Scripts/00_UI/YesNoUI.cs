using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class YesNoUI : MonoBehaviour
{
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;
    [SerializeField] GameObject mapField;

    [SerializeField] InfluenceOnMapUI influenceOnMapUI;
    [SerializeField] DialogueUI dialogueUI;

    [SerializeField] TextMeshProUGUI YesNoText;

    private bool Yes = false;
    private bool isYesNoVisible = false;

    //public void ShowEnterUI()
    //{
    //    SoundManager.instance.PlalyYesNoUISE();
    //    isYesNoVisible = true;
    //    Yes = false;
    //    this.gameObject.SetActive(true);
    //    YesNoText.text = "仕官しますか？";
    //}

    //public void ShowCharacterSelectUI()
    //{
    //    SoundManager.instance.PlalyYesNoUISE();
    //    isYesNoVisible = true;
    //    Yes = false;
    //    this.gameObject.SetActive(true);
    //    YesNoText.text = "このキャラクターでプレイしますか？";
    //}

    //public void ShowSearchYesNoUI()
    //{
    //    SoundManager.instance.PlalyYesNoUISE();
    //    isYesNoVisible = true;
    //    Yes = false;
    //    this.gameObject.SetActive(true);
    //    YesNoText.text = "登用しますか？";
    //}

    public void ShowEmployedYesNoUI(CharacterController lordCharacter)
    {
        SoundManager.instance.PlalyYesNoUISE();
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = lordCharacter.name + "軍から加入依頼です。加入しますか？";
    }

    //public void ShowBanishmentYesNoUI()
    //{
    //    SoundManager.instance.PlalyYesNoUISE();
    //    isYesNoVisible = true;
    //    Yes = false;
    //    this.gameObject.SetActive(true);
    //    YesNoText.text = "追放しますか？";
    //}

    //public void ShowVagabondYesNoUI()
    //{
    //    SoundManager.instance.PlalyYesNoUISE();
    //    isYesNoVisible = true;
    //    Yes = false;
    //    this.gameObject.SetActive(true);
    //    YesNoText.text = "勢力を去りますか？";
    //}

    public void ShowAttackYesNoUI()
    {
        SoundManager.instance.PlalyYesNoUISE();
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "侵攻しますか？";
    }

    //public void ShowBattleCharacterSelectYesNoUI()
    //{
    //    SoundManager.instance.PlalyYesNoUISE();
    //    isYesNoVisible = true;
    //    Yes = false;
    //    this.gameObject.SetActive(true);
    //    YesNoText.text = "よろしいですか？";
    //}

    public void ShowAbandonYesNoUI()
    {
        SoundManager.instance.PlalyYesNoUISE();
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "戦闘を放棄しますか？";
    }

    public void ShowEndYesNoUI()
    {
        SoundManager.instance.PlalyYesNoUISE();
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "終了しますか？";
    }

    public void YesButton()
    {
        SoundManager.instance.PlayYesSE();
        isYesNoVisible = false;
        this.gameObject.SetActive(false);
        Yes = true;
    }

    public void NoButtone()
    {
        SoundManager.instance.PlayCancelSE();
        isYesNoVisible = false;
        this.gameObject.SetActive(false);
        Yes = false;
    }

    public bool IsYes()
    {
        return Yes;
    }

    //public void HideDialogueUI()
    //{
    //    // ダイアログを非表示にする処理
    //    this.gameObject.SetActive(false);
    //    isDialogueVisible = false;
    //}

    public bool IsYesNoVisible()
    {
        return isYesNoVisible;
    }
}
