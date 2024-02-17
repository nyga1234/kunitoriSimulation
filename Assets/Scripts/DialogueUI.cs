using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] Text dialogueText;

    private bool isDialogueVisible = false;

    public void ShowLeaveInfluenceUI(CharacterController character)
    {
        // ダイアログを表示する処理
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = character.characterModel.name + "が勢力を去りました";
    }

    public void ShowEnterInfluenceUI()
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "採用されました";
    }

    public void ShowSuccessAppointmentUI()
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "登用に成功しました";
    }

    public void ShowEmployedUI(CharacterController lordCharacter)
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = lordCharacter.characterModel.name + "軍へ加入しました";
    }

    public void ShowSuccessBanishmentUI()
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "追放に成功しました";
    }

    public void ShowSuccessVagabondUI()
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "勢力を去りました";
    }

    public void ShowElavationRankUI(CharacterController character)
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = character.characterModel.rank + "に昇格しました";
    }

    public void ShowDemotionRankUI(CharacterController character)
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = character.characterModel.rank + "に降格しました";
    }

    public void ShowAttackedUI()
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "防衛部隊を選択してください";
    }

    public void ShowBattleOrderUI()
    {
        this.gameObject.SetActive(true);
        isDialogueVisible = true;
        dialogueText.text = "戦闘を開始します";
    }

    public void HideDialogueUI()
    {
        // ダイアログを非表示にする処理
        this.gameObject.SetActive(false);
        isDialogueVisible = false;
    }

    public bool IsDialogueVisible()
    {
        return isDialogueVisible;
    }
}
