using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI dialogueText;

    private bool isDialogueVisible = false;

    public void ShowLeaveInfluenceUI(CharacterController character)
    {
        SoundManager.instance.PlayDialogueSE();
        // ダイアログを表示する処理
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = character.name + "が勢力を去りました";
    }

    public void ShowEnterInfluenceUI()
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "採用されました";
    }

    public void ShowSuccessAppointmentUI()
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "登用に成功しました";
    }

    public void ShowEmployedUI(CharacterController lordCharacter)
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = lordCharacter.name + "軍へ加入しました";
    }

    public void ShowSuccessBanishmentUI()
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "追放に成功しました";
    }

    public void ShowSuccessVagabondUI()
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "勢力を去りました";
    }

    public void ShowElavationRankUI(CharacterController character)
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = character.rank + "に昇格しました";
    }

    public void ShowDemotionRankUI(CharacterController character)
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = character.rank + "に降格しました";
    }

    public void ShowAttackedUI()
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "防衛部隊を選択してください";
    }

    public void ShowBattleOrderUI()
    {
        SoundManager.instance.PlayDialogueSE();
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "戦闘を開始します";
    }

    public void HideDialogueUI()
    {
        SoundManager.instance.PlayClickSE();
        // ダイアログを非表示にする処理
        this.gameObject.SetActive(false);
        isDialogueVisible = false;
    }

    public bool IsDialogueVisible()
    {
        return isDialogueVisible;
    }
}
