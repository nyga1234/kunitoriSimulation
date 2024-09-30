//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using static GameManager;

//public class PlayerInput : MonoBehaviour
//{
//    [SerializeField] Cursor cursor;
//    [SerializeField] VSImageUI vsImageUI;
//    [SerializeField] CharacterController characterPrefab;
//    [SerializeField] SoliderController soliderPrefab;
//    [SerializeField] TitleFieldUI titleFieldUI;
//    [SerializeField] DialogueUI dialogueUI;
//    [SerializeField] YesNoUI yesNoUI;
//    [SerializeField] BattleManager battleManager;
//    [SerializeField] TerritoryManager territoryManager;
//    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;
//    [SerializeField] CharacterTurnUI characterTurnUI;
//    [SerializeField] CharacterMenuUI characterMenuUI;
//    [SerializeField] PersonalMenuUI personalMenuUI;
//    [SerializeField] BattleMenuUI battleMenuUI;
//    [SerializeField] CharacterIndexUI characterIndexUI;
//    [SerializeField] AbandonUI abandonUI;
//    [SerializeField] AttackedCharacterUI attackedCharacterUI;
//    [SerializeField] LandformInformationUI landformInformationUI;
//    [SerializeField] CharacterSearchUI characterSearchUI;
//    [SerializeField] DetailUI detailsUI;
//    [SerializeField] CharacterDetailUI characterDetailUI;
//    [SerializeField] InfluenceUI influenceUI;
//    [SerializeField] InfluenceOnMapUI influenceOnMapUI;
//    [SerializeField] BattleUI battleUI;
//    [SerializeField] BattleDetailUI battleDetailUI;
//    [SerializeField] GameObject mapField;
//    [SerializeField] GameObject characterIndexMenu;
//    [SerializeField] GameObject characterSearchMenu;
//    public TerritoryGenerator territoryGenerator;

//    public void MouseDown1ToBack()
//    {
//        if (GameManager.instance.phase == Phase.CharacterChoicePhase)
//        {
//            if (Input.GetMouseButtonDown(1) && characterIndexMenu.gameObject.activeSelf)
//            {
//                SoundManager.instance.PlayCancelSE();
//                characterIndexMenu.gameObject.SetActive(false);
//                characterIndexUI.HideCharacterIndexUI();
//                characterDetailUI.gameObject.SetActive(false);

//                mapField.gameObject.SetActive(true);
//            }
//        }

//        if (GameManager.instance.phase == Phase.PlayerLordPhase)
//        {
//            if (Input.GetMouseButtonDown(1) && mapField.gameObject.activeSelf)
//            {
//                SoundManager.instance.PlayCancelSE();
//                mapField.gameObject.SetActive(false);
//                cursor.gameObject.SetActive(false);
//                influenceOnMapUI.HideInfluenceOnMapUI();
//                ShowLordUI(playerCharacter);
//            }
//            if (Input.GetMouseButtonDown(1) && characterIndexMenu.gameObject.activeSelf)
//            {
//                SoundManager.instance.PlayCancelSE();
//                characterIndexMenu.gameObject.SetActive(false);
//                characterIndexUI.HideCharacterIndexUI();
//                characterDetailUI.gameObject.SetActive(false);

//                if (step == Step.Search || step == Step.Appointment || step == Step.Banishment)
//                {
//                    ShowLordUI(playerCharacter);
//                }
//                else
//                {
//                    mapField.gameObject.SetActive(true);
//                }
//            }
//        }

//        if (GameManager.instance.phase == Phase.PlayerPersonalPhase)
//        {
//            if (Input.GetMouseButtonDown(1) && mapField.gameObject.activeSelf)
//            {
//                SoundManager.instance.PlayCancelSE();
//                mapField.gameObject.SetActive(false);
//                cursor.gameObject.SetActive(false);
//                influenceOnMapUI.HideInfluenceOnMapUI();
//                ShowPersonalUI(playerCharacter);
//            }
//            if (Input.GetMouseButtonDown(1) && characterIndexMenu.gameObject.activeSelf)
//            {
//                SoundManager.instance.PlayCancelSE();
//                characterIndexMenu.gameObject.SetActive(false);
//                characterIndexUI.HideCharacterIndexUI();
//                characterDetailUI.gameObject.SetActive(false);

//                mapField.gameObject.SetActive(true);
//            }
//        }

//        if (GameManager.instance.phase == Phase.PlayerBattlePhase)
//        {
//            if (Input.GetMouseButtonDown(1) && mapField.gameObject.activeSelf)
//            {
//                SoundManager.instance.PlayCancelSE();
//                mapField.gameObject.SetActive(false);
//                cursor.gameObject.SetActive(false);
//                influenceOnMapUI.HideInfluenceOnMapUI();
//                ShowBattleUI(playerCharacter);
//            }
//            if (Input.GetMouseButtonDown(1) && characterIndexMenu.gameObject.activeSelf)
//            {
//                SoundManager.instance.PlayCancelSE();
//                characterIndexMenu.gameObject.SetActive(false);
//                characterIndexUI.HideCharacterIndexUI();
//                characterDetailUI.gameObject.SetActive(false);

//                mapField.gameObject.SetActive(true);
//            }
//        }
//    }
//}
