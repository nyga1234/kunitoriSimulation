using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System;
using static GameMain;
using UnityEngine.EventSystems;

public class CommandOnClick : MonoBehaviour
{
    [SerializeField] YesNoUI yesNoUI;
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] CharacterMenuUI characterMenuUI;
    [SerializeField] PersonalMenuUI personalMenuUI;
    [SerializeField] BattleMenuUI battleMenuUI;
    [SerializeField] GameObject characterIndexMenu;
    [SerializeField] GameObject characterSearchMenu;
    [SerializeField] CharacterIndexUI characterIndexUI;
    [SerializeField] CharacterSearchUI characterSearchUI;
    [SerializeField] CharacterDetailUI characterDetailUI;
    [SerializeField] InfluenceUI influenceUI;
    [SerializeField] GameObject mapField;
    [SerializeField] GameObject backToCharaMenuButton;
    [SerializeField] GameObject backToPersonalMenuButton;
    [SerializeField] GameObject functionUI;

    public GameMain gameManager;

    [SerializeField] SoliderController soliderPrefab;

    public bool clickedFlag = false;

    private Color originalColor; // ���̔w�i�F��ێ�����ϐ�

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // ���̔w�i�F��ێ�
            originalColor = image.color;
        }
    }

    public void OnPointerClickInformation()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SoundManager.instance.PlayClickSE();
            GameMain.instance.step = Step.Information;

            characterMenuUI.HideCharacterMenuUI();
            characterDetailUI.HideCharacterDetailUI();

            mapField.gameObject.SetActive(true);
            //backToCharaMenuButton.gameObject.SetActive(true);

            // �N���b�N���ꂽ��w�i�F�����ɖ߂�
            ChangeBackgroundColor(originalColor);
        }
    }

    public void OnPointerClickPersonalInformation()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SoundManager.instance.PlayClickSE();
            GameMain.instance.step = Step.Information;

            personalMenuUI.HidePersonalMenuUI();
            influenceUI.HideInfluenceUI();

            mapField.gameObject.SetActive(true);
            //backToPersonalMenuButton.gameObject.SetActive(true);

            // �N���b�N���ꂽ��w�i�F�����ɖ߂�
            ChangeBackgroundColor(originalColor);
        }
    }

    public void OnPointerClickBattleInformation()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SoundManager.instance.PlayClickSE();
            GameMain.instance.step = Step.Information;

            battleMenuUI.HideBattleMenuUI();
            characterDetailUI.HideCharacterDetailUI();

            mapField.gameObject.SetActive(true);
            //backToPersonalMenuButton.gameObject.SetActive(true);

            // �N���b�N���ꂽ��w�i�F�����ɖ߂�
            ChangeBackgroundColor(originalColor);
        }
    }

    public void OnPointerClickAppointment()
    {
        if (Input.GetMouseButtonUp(0))
        {
            SoundManager.instance.PlayClickSE();
            GameMain.instance.step = Step.Appointment;

            characterMenuUI.HideCharacterMenuUI();
            characterDetailUI.HideCharacterDetailUI();

            TitleFieldUI.instance.titleFieldText.text = "      ���i�������L�����N�^�[���N���b�N";
            characterIndexMenu.SetActive(true);
            characterIndexUI.ShowCharacterIndexUI(GameMain.instance.playerCharacter.influence.characterList);
        }
    }

    public void OnPointerClickSearch()
    {
        if (Input.GetMouseButtonUp(0))
        {
            switch (gameManager.playerCharacter.influence.territoryList.Count)
            {
                case 1:
                case 2:
                    if (gameManager.playerCharacter.characterModel.gold >= 9)
                    {
                        if (gameManager.playerCharacter.influence.characterList.Count <= 2)
                        {
                            CharacterSearch();
                        }
                        else
                        {
                            TitleFieldUI.instance.titleFieldText.text = "      �z���̏���������ł�";
                        }
                    }
                    else
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      ����������܂���";
                    }
                    break;
                case 3:
                case 4: 
                case 5:
                case 6:
                    if (gameManager.playerCharacter.characterModel.gold >= 9)
                    {
                        if (gameManager.playerCharacter.influence.characterList.Count <= 3)
                        {
                            CharacterSearch();
                        }
                        else
                        {
                            TitleFieldUI.instance.titleFieldText.text = "      �z���̏���������ł�";
                        }
                    }
                    else
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      ����������܂���";
                    }
                    break;
                case 7:
                case 8:
                case 9:
                case 10:
                    if (gameManager.playerCharacter.characterModel.gold >= 9)
                    {
                        if (gameManager.playerCharacter.influence.characterList.Count <= 4)
                        {
                            CharacterSearch();
                        }
                        else
                        {
                            TitleFieldUI.instance.titleFieldText.text = "      �z���̏���������ł�";
                        }
                    }
                    else
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      ����������܂���";
                    }
                    break;
                case 11:
                case 12:
                case 13:
                case 14:
                    if (gameManager.playerCharacter.characterModel.gold >= 9)
                    {
                        if (gameManager.playerCharacter.influence.characterList.Count <= 5)
                        {
                            CharacterSearch();
                        }
                        else
                        {
                            TitleFieldUI.instance.titleFieldText.text = "      �z���̏���������ł�";
                        }
                    }
                    else
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      ����������܂���";
                    }
                    break;
            }
        }
    }

    public void OnPointerClickBanishment()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (GameMain.instance.playerCharacter.influence.characterList.Count != 1)
            {
                SoundManager.instance.PlayClickSE();
                GameMain.instance.step = Step.Banishment;

                characterMenuUI.HideCharacterMenuUI();
                characterDetailUI.HideCharacterDetailUI();

                TitleFieldUI.instance.titleFieldText.text = "      �N���b�N�ŒǕ�";
                characterIndexMenu.SetActive(true);
                characterIndexUI.ShowCharacterIndexUI(GameMain.instance.playerCharacter.influence.characterList);
            }
            else
            {
                TitleFieldUI.instance.titleFieldText.text = "      �̎�͒Ǖ��ł��܂���";
                return;
            }
        }
    }

    public void OnPointerClickSoliderRecruit()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (gameManager.playerCharacter.soliderList.Count < 10 && gameManager.playerCharacter.characterModel.gold >= 2)
            {
                SoundManager.instance.PlayRecruitSE();
                SoliderController solider = Instantiate(soliderPrefab);
                solider.Init(1, GameMain.instance.CreateSoliderUniqueID());
                solider.gameObject.SetActive(false);
                gameManager.playerCharacter.soliderList.Add(solider);
                gameManager.allSoliderList.Add(solider);
                gameManager.playerCharacter.characterModel.gold -= 2;
                personalMenuUI.ShowPersonalMenuUI(gameManager.playerCharacter);
                influenceUI.ShowInfluenceUI(gameManager.playerCharacter.influence);
            }
            else if (gameManager.playerCharacter.soliderList.Count >= 10)
            {
                TitleFieldUI.instance.titleFieldText.text = "      �ٗp�\�ȕ��m��10�l�܂łł�";
                return;
            }
            else if (gameManager.playerCharacter.characterModel.gold < 2)
            {
                TitleFieldUI.instance.titleFieldText.text = "      ����������܂���";
                return;
            }
        }
    }

    public void OnPointerClickSoliderTraining()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (gameManager.playerCharacter.characterModel.gold >= 2)
            {
                SoundManager.instance.PlayTrainingSE();
                foreach (SoliderController solider in gameManager.playerCharacter.soliderList)
                {
                    solider.soliderModel.Training(solider);
                }
                gameManager.playerCharacter.characterModel.gold -= 2;
                personalMenuUI.ShowPersonalMenuUI(gameManager.playerCharacter);
                influenceUI.ShowInfluenceUI(gameManager.playerCharacter.influence);
            }
            else
            {
                TitleFieldUI.instance.titleFieldText.text = "      ����������܂���";
                return;
            }
        }
    }

    public void OnPointerClickEnter()
    {
        if (GameMain.instance.playerCharacter.influence == GameMain.instance.noneInfluence)
        {
            if (Input.GetMouseButtonUp(0))
            {
                SoundManager.instance.PlayClickSE();
                GameMain.instance.step = Step.Enter;

                personalMenuUI.HidePersonalMenuUI();
                influenceUI.HideInfluenceUI();

                TitleFieldUI.instance.titleFieldText.text = "      �d�����I��";
                mapField.gameObject.SetActive(true);

                // �N���b�N���ꂽ��w�i�F�����ɖ߂�
                ChangeBackgroundColor(originalColor);
            }
        }
        else
        {
            return;
        }
    }

    public void OnPointerClickVagabond()
    {
        if(gameManager.playerCharacter.characterModel.isLord == true || GameMain.instance.playerCharacter.influence == GameMain.instance.noneInfluence)
        {
            return;
        }
        else
        {
            clickedFlag = true;
            StartCoroutine(WaitForVagabond());
        }
    }

    IEnumerator WaitForVagabond()
    {
        yesNoUI.ShowVagabondYesNoUI();
        //yesNoUI����\���ɂȂ�܂őҋ@
        yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());
        clickedFlag = false;

        //�w�i�F�����ɖ߂�
        ChangeBackgroundColor(originalColor);

        if (yesNoUI.IsYes())
        {
            //�������͂�����
            GameMain.instance.LeaveInfluence(GameMain.instance.playerCharacter);

            GameMain.instance.ShowPersonalUI(GameMain.instance.playerCharacter);

            dialogueUI.ShowSuccessVagabondUI();
        }
    }

    public void OnPointerClickRebellion()
    {
        if (gameManager.playerCharacter.characterModel.isLord == true || GameMain.instance.playerCharacter.influence == GameMain.instance.noneInfluence)
        {
            return;
        }
    }

    public void OnPointerClickAttack()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (GameMain.instance.playerCharacter.characterModel.isLord == false && GameMain.instance.playerCharacter.characterModel.isAttackable == false)
            {
                TitleFieldUI.instance.titleFieldText.text = "      �N�U�ς݂ł�";
                return;
            }


            if (GameMain.instance.playerCharacter.characterModel.gold >= 3)
            {
                SoundManager.instance.PlayClickSE();
                GameMain.instance.step = Step.Attack;

                battleMenuUI.HideBattleMenuUI();
                characterDetailUI.HideCharacterDetailUI();

                mapField.gameObject.SetActive(true);
            }
            else
            {
                TitleFieldUI.instance.titleFieldText.text = "      ����������܂���";
                return;
            }
        }
    }

    public void OnPointerClickFunction()
    {
        if (Input.GetMouseButtonUp(0))
        {
            functionUI.SetActive(true);
        }
    }

    public void OnPointerClickLordEnd()
    {
        if (Input.GetMouseButtonUp(0))
        {
            clickedFlag = true;
            StartCoroutine(WaitForLordEnd());
        }
    }

    IEnumerator WaitForLordEnd()
    {
        yesNoUI.ShowEndYesNoUI();
        //yesNoUI����\���ɂȂ�܂őҋ@
        yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());
        clickedFlag = false;

        //�w�i�F�����ɖ߂�
        ChangeBackgroundColor(originalColor);

        if (yesNoUI.IsYes())
        {
            GameMain.instance.step = Step.End;

            characterMenuUI.HideCharacterMenuUI();
            characterDetailUI.HideCharacterDetailUI();

            //GameMain�̃t�F�[�Y��i�߂�
            gameManager.phase = Phase.OtherLordPhase;
            gameManager.PhaseCalc();
        }
    }

    public void OnPointerClickPersonalEnd()
    {
        if (Input.GetMouseButtonUp(0))
        {
            clickedFlag = true;
            StartCoroutine(WaifForPersonalEnd());
        }
    }

    IEnumerator WaifForPersonalEnd()
    {
        yesNoUI.ShowEndYesNoUI();
        //yesNoUI����\���ɂȂ�܂őҋ@
        yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());
        clickedFlag = false;

        //�w�i�F�����ɖ߂�
        ChangeBackgroundColor(originalColor);

        if (yesNoUI.IsYes())
        {
            GameMain.instance.step = Step.End;

            personalMenuUI.HidePersonalMenuUI();
            influenceUI.HideInfluenceUI();

            //GameMain�̃t�F�[�Y��i�߂�
            gameManager.phase = Phase.OtherPersonalPhase;
            gameManager.PhaseCalc();
        }
    }

    public void OnPointerClickBattleEnd()
    {
        if (Input.GetMouseButtonUp(0))
        {
            clickedFlag = true;
            StartCoroutine(WaitForBattleEnd());
        }
    }

    IEnumerator WaitForBattleEnd()
    {
        yesNoUI.ShowEndYesNoUI();
        //yesNoUI����\���ɂȂ�܂őҋ@
        yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());
        clickedFlag = false;

        //�w�i�F�����ɖ߂�
        ChangeBackgroundColor(originalColor);

        if (yesNoUI.IsYes())
        {
            GameMain.instance.step = Step.End;

            battleMenuUI.HideBattleMenuUI();
            characterDetailUI.HideCharacterDetailUI();

            // �N���b�N���ꂽ��w�i�F�����ɖ߂�
            ChangeBackgroundColor(originalColor);

            bool isPlayerLast = GameMain.instance.characterList.LastOrDefault() == GameMain.instance.playerCharacter;
            if (isPlayerLast)
            {
                Debug.Log("�v���C���[�͍Ō�ł��B");
                //�v���C���[�̎�t�F�[�Y�֐i�߂�
                gameManager.phase = Phase.PlayerLordPhase;
                gameManager.PhaseCalc();
            }
            else
            {
                Debug.Log("�v���C���[�͍Ō�ł͂���܂���B");
                CharacterController playerNextCharacter = GameMain.instance.GetNextCharacter(GameMain.instance.playerCharacter);
                GameMain.instance.OtherBattlePhase(playerNextCharacter);
            }
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

    public void CharacterSearch()
    {
        SoundManager.instance.PlayClickSE();
        GameMain.instance.step = Step.Search;

        gameManager.playerCharacter.characterModel.gold -= 9;
        characterMenuUI.HideCharacterMenuUI();
        characterDetailUI.HideCharacterDetailUI();

        //None�ɏ�������L�����N�^�[���X�g���烉���_����6���擾
        List<CharacterController> noneInfluenceCharacters = GameMain.instance.noneInfluence.characterList;
        System.Random random = new System.Random();
        List<CharacterController> shuffledList = noneInfluenceCharacters.OrderBy(x => random.Next()).ToList();
        // �擪����6�̗v�f���擾
        List<CharacterController> randomCharacters = shuffledList.Take(6).ToList();

        TitleFieldUI.instance.titleFieldText.text = "      �z���ɉ��������L�������N���b�N�Œǉ�";
        characterIndexMenu.SetActive(true);
        characterIndexUI.ShowCharacterIndexUI(randomCharacters);
    }
}
