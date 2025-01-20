using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameMain;

public class MouseDown1ToBack : MonoBehaviour
{
    void Update()
    {
        if (!SceneController.instance.Stack.Contains("UIConfirm") &&Å@!SceneController.instance.Stack.Contains("UIDialogue"))
        {
            OnMouse1Click();
        }
    }

    public void OnMouse1Click()
    {
        if (GameMain.instance.phase == Phase.CharacterChoicePhase)
        {
            if (Input.GetMouseButtonDown(1) && GameMain.instance.CharacterIndexUI.gameObject.activeSelf)
            {
                SoundManager.instance.PlayCancelSE();
                GameMain.instance.CharacterIndexUI.HideCharacterIndexUI();
                GameMain.instance.CharacterDetailUI.gameObject.SetActive(false);

                GameMain.instance.MapField.gameObject.SetActive(true);
            }
        }

        if (GameMain.instance.phase == Phase.PlayerLordPhase)
        {
            if (Input.GetMouseButtonDown(1) && GameMain.instance.MapField.gameObject.activeSelf)
            {
                SoundManager.instance.PlayCancelSE();
                GameMain.instance.MapField.gameObject.SetActive(false);
                GameMain.instance.Cursor.gameObject.SetActive(false);
                GameMain.instance.InfluenceOnMapUI.HideInfluenceOnMapUI();
                GameMain.instance.ShowLordUI(GameMain.instance.playerCharacter);
            }
            if (Input.GetMouseButtonDown(1) && GameMain.instance.CharacterIndexUI.gameObject.activeSelf)
            {
                SoundManager.instance.PlayCancelSE();
                GameMain.instance.CharacterIndexUI.HideCharacterIndexUI();
                GameMain.instance.CharacterDetailUI.gameObject.SetActive(false);

                if (GameMain.instance.step == Step.Search || GameMain.instance.step == Step.Appointment || GameMain.instance.step == Step.Banishment)
                {
                    GameMain.instance.ShowLordUI(GameMain.instance.playerCharacter);
                }
                else
                {
                    GameMain.instance.MapField.gameObject.SetActive(true);
                }
            }
        }

        if (GameMain.instance.phase == Phase.PlayerPersonalPhase)
        {
            if (Input.GetMouseButtonDown(1) && GameMain.instance.MapField.gameObject.activeSelf)
            {
                SoundManager.instance.PlayCancelSE();
                GameMain.instance.MapField.gameObject.SetActive(false);
                GameMain.instance.Cursor.gameObject.SetActive(false);
                GameMain.instance.InfluenceOnMapUI.HideInfluenceOnMapUI();
                GameMain.instance.ShowPersonalUI(GameMain.instance.playerCharacter);
            }
            if (Input.GetMouseButtonDown(1) && GameMain.instance.CharacterIndexUI.gameObject.activeSelf)
            {
                SoundManager.instance.PlayCancelSE();
                GameMain.instance.CharacterIndexUI.HideCharacterIndexUI();
                GameMain.instance.CharacterDetailUI.gameObject.SetActive(false);

                GameMain.instance.MapField.gameObject.SetActive(true);
            }
        }

        if (GameMain.instance.phase == Phase.PlayerBattlePhase)
        {
            if (Input.GetMouseButtonDown(1) && GameMain.instance.MapField.gameObject.activeSelf)
            {
                SoundManager.instance.PlayCancelSE();
                GameMain.instance.MapField.gameObject.SetActive(false);
                GameMain.instance.Cursor.gameObject.SetActive(false);
                GameMain.instance.InfluenceOnMapUI.HideInfluenceOnMapUI();
                GameMain.instance.ShowBattleUI(GameMain.instance.playerCharacter);
            }
            if (Input.GetMouseButtonDown(1) && GameMain.instance.CharacterIndexUI.gameObject.activeSelf)
            {
                SoundManager.instance.PlayCancelSE();
                GameMain.instance.CharacterIndexUI.HideCharacterIndexUI();
                GameMain.instance.CharacterDetailUI.gameObject.SetActive(false);

                GameMain.instance.MapField.gameObject.SetActive(true);
            }
        }
    }
}
