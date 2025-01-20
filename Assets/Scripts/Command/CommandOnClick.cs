using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using static GameMain;
using Cysharp.Threading.Tasks;

public class CommandOnClick : MonoBehaviour
{
    [Header("Param")]
    [SerializeField] UtilityParamObject constParam;
    [SerializeField] UtilityParamObject varParam;

    [Header("UI")]
    [SerializeField] CharacterMenuUI characterMenuUI;
    [SerializeField] PersonalMenuUI personalMenuUI;
    [SerializeField] BattleMenuUI battleMenuUI;
    [SerializeField] GameObject characterSearchMenu;
    [SerializeField] InfluenceUI influenceUI;
    [SerializeField] GameObject mapField;
    [SerializeField] GameObject functionUI;

    //CharacterMenuUI
    public void OnPointerClickInformation()
    {
        GameMain.instance.step = Step.Information;

        characterMenuUI.HideCharacterMenuUI();
        GameMain.instance.CharacterDetailUI.HideCharacterDetailUI();

        mapField.gameObject.SetActive(true);
    }

    public void OnPointerClickAppointment()
    {
        GameMain.instance.step = Step.Appointment;

        characterMenuUI.HideCharacterMenuUI();
        GameMain.instance.CharacterDetailUI.HideCharacterDetailUI();

        TitleFieldUI.instance.titleFieldText.text = "�I�������L�����N�^�[�����i";

        GameMain.instance.CharacterIndexUI.ShowCharacterIndexUI(GameMain.instance.playerCharacter.influence.characterList);
    }

    public void OnPointerClickSearch()
    {
        if (GameMain.instance.playerCharacter.gold >= 9)
        {
            switch (GameMain.instance.playerCharacter.influence.territoryList.Count)
            {
                case 1:
                case 2:
                    if (GameMain.instance.playerCharacter.influence.characterList.Count <= 2)
                    {
                        CharacterSearch();
                    }
                    else
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      �z���̏���������ł�";
                    }
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                    if (GameMain.instance.playerCharacter.influence.characterList.Count <= 3)
                    {
                        CharacterSearch();
                    }
                    else
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      �z���̏���������ł�";
                    }
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                    if (GameMain.instance.playerCharacter.influence.characterList.Count <= 4)
                    {
                        CharacterSearch();
                    }
                    else
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      �z���̏���������ł�";
                    }
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                    if (GameMain.instance.playerCharacter.influence.characterList.Count <= 5)
                    {
                        CharacterSearch();
                    }
                    else
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      �z���̏���������ł�";
                    }
                    break;
            }
        }
        else
        {
            TitleFieldUI.instance.titleFieldText.text = "      ����������܂���";
        }
    }

    public void OnPointerClickBanishment()
    {
        if (GameMain.instance.playerCharacter.influence.characterList.Count != 1)
        {
            GameMain.instance.step = Step.Banishment;

            characterMenuUI.HideCharacterMenuUI();
            GameMain.instance.CharacterDetailUI.HideCharacterDetailUI();

            TitleFieldUI.instance.titleFieldText.text = "      �N���b�N�ŒǕ�";
            GameMain.instance.CharacterIndexUI.ShowCharacterIndexUI(GameMain.instance.playerCharacter.influence.characterList);
        }
        else
        {
            TitleFieldUI.instance.titleFieldText.text = "      �̎�͒Ǖ��ł��܂���";
            return;
        }
    }

    public async void OnPointerClickLordEnd()
    {
        await SceneController.LoadAsync("UIConfirm");
        varParam.ConfirmText = "�^�[�����I�����܂����H";
        // OK�܂���Cancel�{�^�����N���b�N�����̂�ҋ@
        await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);

        if (varParam.IsConfirm == true)
        {
            GameMain.instance.step = Step.End;

            characterMenuUI.HideCharacterMenuUI();
            GameMain.instance.CharacterDetailUI.HideCharacterDetailUI();

            //GameMain�̃t�F�[�Y��i�߂�
            GameMain.instance.phase = Phase.OtherLordPhase;
            GameMain.instance.PhaseCalc();
        }
    }

    //PersonalMenuUI
    public void OnPointerClickPersonalInformation()
    {
        GameMain.instance.step = Step.Information;

        personalMenuUI.HidePersonalMenuUI();
        influenceUI.HideInfluenceUI();

        mapField.gameObject.SetActive(true);
    }

    public void OnPointerClickSoliderRecruit()
    {
        if (GameMain.instance.playerCharacter.soliderList.Count < 10 && GameMain.instance.playerCharacter.gold >= 2)
        {
            GameMain.instance.playerCharacter.soliderList.Add(Instantiate(constParam.soldierList.Find(c => c.soliderID == 1)));
            GameMain.instance.playerCharacter.gold -= 2;
            personalMenuUI.ShowPersonalMenuUI(GameMain.instance.playerCharacter);
            influenceUI.ShowInfluenceUI(GameMain.instance.playerCharacter.influence);
        }
        else if (GameMain.instance.playerCharacter.soliderList.Count >= 10)
        {
            TitleFieldUI.instance.titleFieldText.text = "      �ٗp�\�ȕ��m��10�l�܂łł�";
            return;
        }
        else if (GameMain.instance.playerCharacter.gold < 2)
        {
            TitleFieldUI.instance.titleFieldText.text = "      ����������܂���";
            return;
        }
    }

    public void OnPointerClickEnter()
    {
        if (GameMain.instance.playerCharacter.influence == GameMain.instance.noneInfluence)
        {
            GameMain.instance.step = Step.Enter;

            personalMenuUI.HidePersonalMenuUI();
            influenceUI.HideInfluenceUI();

            TitleFieldUI.instance.titleFieldText.text = "      �d�����I��";
            mapField.gameObject.SetActive(true);
        }
        else
        {
            return;
        }
    }

    public void OnPointerClickSoliderTraining()
    {
        if (GameMain.instance.playerCharacter.gold >= 2)
        {
            //SoundManager.instance.PlayTrainingSE();
            foreach (SoldierController solider in GameMain.instance.playerCharacter.soliderList)
            {
                solider.Training(solider);
            }
            GameMain.instance.playerCharacter.gold -= 2;
            personalMenuUI.ShowPersonalMenuUI(GameMain.instance.playerCharacter);
            influenceUI.ShowInfluenceUI(GameMain.instance.playerCharacter.influence);
        }
        else
        {
            TitleFieldUI.instance.titleFieldText.text = "      ����������܂���";
            return;
        }
    }

    public async void OnPointerClickVagabond()
    {
        if (GameMain.instance.playerCharacter.isLord == true || GameMain.instance.playerCharacter.influence == GameMain.instance.noneInfluence)
        {
            return;
        }
        else
        {
            await SceneController.LoadAsync("UIConfirm");
            varParam.ConfirmText = "�������͂�����܂����H";
            // OK�܂���Cancel�{�^�����N���b�N�����̂�ҋ@
            await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);

            if (varParam.IsConfirm == true)
            {
                //�������͂�����
                GameMain.instance.LeaveInfluence(GameMain.instance.playerCharacter);

                GameMain.instance.ShowPersonalUI(GameMain.instance.playerCharacter);

                await SceneController.LoadAsync("UIDialogue");
                varParam.DialogueText = "�������͂�����܂���";
            }
        }
    }

    public async void OnPointerClickPersonalEnd()
    {
        await SceneController.LoadAsync("UIConfirm");
        varParam.ConfirmText = "�^�[�����I�����܂����H";
        // OK�܂���Cancel�{�^�����N���b�N�����̂�ҋ@
        await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);

        if (varParam.IsConfirm == true)
        {
            GameMain.instance.step = Step.End;

            personalMenuUI.HidePersonalMenuUI();
            influenceUI.HideInfluenceUI();

            //GameMain�̃t�F�[�Y��i�߂�
            GameMain.instance.phase = Phase.OtherPersonalPhase;
            GameMain.instance.PhaseCalc();
        }
    }

    //BattleMenuUI
    public void OnPointerClickBattleInformation()
    {
        GameMain.instance.step = Step.Information;

        battleMenuUI.HideBattleMenuUI();
        GameMain.instance.CharacterDetailUI.HideCharacterDetailUI();

        mapField.gameObject.SetActive(true);
    }

    public void OnPointerClickRebellion()
    {
        if (GameMain.instance.playerCharacter.isLord == true || GameMain.instance.playerCharacter.influence == GameMain.instance.noneInfluence)
        {
            return;
        }
    }

    public void OnPointerClickAttack()
    {
        if (GameMain.instance.playerCharacter.isLord == false && GameMain.instance.playerCharacter.isAttackable == false)
        {
            TitleFieldUI.instance.titleFieldText.text = "      �N�U�ς݂ł�";
            return;
        }


        if (GameMain.instance.playerCharacter.gold >= 3)
        {
            GameMain.instance.step = Step.Attack;

            battleMenuUI.HideBattleMenuUI();
            GameMain.instance.CharacterDetailUI.HideCharacterDetailUI();

            mapField.gameObject.SetActive(true);
        }
        else
        {
            TitleFieldUI.instance.titleFieldText.text = "      ����������܂���";
            return;
        }
    }

    public async void OnPointerClickBattleEnd()
    {
        await SceneController.LoadAsync("UIConfirm");
        varParam.ConfirmText = "�^�[�����I�����܂����H";
        // OK�܂���Cancel�{�^�����N���b�N�����̂�ҋ@
        await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);

        if (varParam.IsConfirm == true)
        {
            GameMain.instance.step = Step.End;

            battleMenuUI.HideBattleMenuUI();
            GameMain.instance.CharacterDetailUI.HideCharacterDetailUI();

            bool isPlayerLast = GameMain.instance.characterList.LastOrDefault() == GameMain.instance.playerCharacter;
            if (isPlayerLast)
            {
                Debug.Log("�v���C���[�͍Ō�ł��B");
                //�v���C���[�̎�t�F�[�Y�֐i�߂�
                GameMain.instance.phase = Phase.PlayerLordPhase;
                GameMain.instance.PhaseCalc();
            }
            else
            {
                Debug.Log("�v���C���[�͍Ō�ł͂���܂���B");
                CharacterController playerNextCharacter = GameMain.instance.GetNextCharacter(GameMain.instance.playerCharacter);
                GameMain.instance.OtherBattlePhase(playerNextCharacter);
            }
        }
    }

    //���ʏ���
    public void OnPointerClickFunction()
    {
        functionUI.SetActive(true);
    }

    public void CharacterSearch()
    {
        GameMain.instance.step = Step.Search;

        GameMain.instance.playerCharacter.gold -= 9;
        characterMenuUI.HideCharacterMenuUI();
        GameMain.instance.CharacterDetailUI.HideCharacterDetailUI();

        //None�ɏ�������L�����N�^�[���X�g���烉���_����6���擾
        List<CharacterController> noneInfluenceCharacters = GameMain.instance.noneInfluence.characterList;
        System.Random random = new System.Random();
        List<CharacterController> shuffledList = noneInfluenceCharacters.OrderBy(x => random.Next()).ToList();
        // �擪����6�̗v�f���擾
        List<CharacterController> randomCharacters = shuffledList.Take(6).ToList();

        TitleFieldUI.instance.titleFieldText.text = "      �z���ɉ��������L�������N���b�N�Œǉ�";
        GameMain.instance.CharacterIndexUI.ShowCharacterIndexUI(randomCharacters);
    }
}
