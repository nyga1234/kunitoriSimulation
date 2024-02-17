using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class YesNoUI : MonoBehaviour
{
    [SerializeField] InflueneceManager influeneceManager;
    [SerializeField] GameObject mapField;

    [SerializeField] InfluenceOnMapUI influenceOnMapUI;
    [SerializeField] DialogueUI dialogueUI;

    [SerializeField] Text YesNoText;

    private bool Yes = false;
    private bool isYesNoVisible = false;

    public void ShowEnterUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "仕官しますか？";
    }

    public void ShowCharacterSelectUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "このキャラクターでプレイしますか？";
    }

    public void ShowSearchYesNoUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "登用しますか？";
    }

    public void ShowEmployedYesNoUI(CharacterController lordCharacter)
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = lordCharacter.characterModel.name + "軍から加入依頼です。加入しますか？";
    }

    public void ShowBanishmentYesNoUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "追放しますか？";
    }

    public void ShowVagabondYesNoUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "勢力を去りますか？";
    }

    public void ShowAttackYesNoUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "侵攻しますか？";
    }

    public void ShowBattleCharacterSelectYesNoUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "よろしいですか？";
    }

    public void ShowAbandonYesNoUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "戦闘を放棄しますか？";
    }

    public void ShowEndYesNoUI()
    {
        isYesNoVisible = true;
        Yes = false;
        this.gameObject.SetActive(true);
        YesNoText.text = "終了しますか？";
    }

    public void YesButton()
    {
        isYesNoVisible = false;
        this.gameObject.SetActive(false);
        Yes = true;
    }

    public void NoButtone()
    {
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
