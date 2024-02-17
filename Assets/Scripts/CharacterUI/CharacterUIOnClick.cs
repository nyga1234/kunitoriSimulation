using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using static GameManager;

public class CharacterUIOnClick : MonoBehaviour
{
    public GameManager gameManager;
    public BattleManager battleManager;
    public InflueneceManager influenceManager;

    [SerializeField] YesNoUI yesNoUI;
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] GameObject characterIndexMenu;
    [SerializeField] CharacterIndexUI characterIndexUI;
    [SerializeField] BattleUI battleUI;
    [SerializeField] BattleDetailUI battleDetailUI;
    [SerializeField] CharacterDetailUI characterDetailUI;
    [SerializeField] GameObject mapField;

    [SerializeField] CharacterController clickedCharacter;

    private CharacterController defenceCharacter;

    public bool clickedFlag = false;

    IEnumerator WaitForSelectCharacter()
    {
        yesNoUI.ShowCharacterSelectUI();
        //yesNoUI����\���ɂȂ�܂őҋ@
        yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());
        clickedFlag = false;

        if (yesNoUI.IsYes())
        {
            clickedCharacter.characterModel.isPlayerCharacter = true;
            GameManager.instance.playerCharacter = clickedCharacter;

            characterIndexMenu.gameObject.SetActive(false);
            characterIndexUI.HideCharacterIndexUI();
            characterDetailUI.gameObject.SetActive(false);

            GameManager.instance.phase = Phase.SetupPhase;
            GameManager.instance.PhaseCalc();
        }
        else
        {
            characterIndexUI.ShowCharacterIndexUI(influenceManager.territory.influence.characterList);
        }
    }

    IEnumerator WaitForSearchCharacter()
    {
        yesNoUI.ShowSearchYesNoUI();
        //yesNoUI����\���ɂȂ�܂őҋ@
        yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());
        clickedFlag = false;

        if (yesNoUI.IsYes())
        {
            dialogueUI.ShowSuccessAppointmentUI();
            yield return new WaitUntil(() => !dialogueUI.IsDialogueVisible());

            clickedCharacter.influence.RemoveCharacter(clickedCharacter);
            gameManager.playerCharacter.influence.AddCharacter(clickedCharacter);

            characterIndexMenu.gameObject.SetActive(false);
            characterIndexUI.HideCharacterIndexUI();
            characterDetailUI.gameObject.SetActive(false);

            gameManager.ShowLordUI(gameManager.playerCharacter);
        }
        else
        {
            characterIndexUI.ShowCharacterIndexUI(characterIndexUI.indexCharacterList);
        }
    }

    IEnumerator WaitForBanishmentCharacter()
    {
        yesNoUI.ShowBanishmentYesNoUI();
        //yesNoUI����\���ɂȂ�܂őҋ@
        yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());
        clickedFlag = false;

        if (yesNoUI.IsYes())
        {
            dialogueUI.ShowSuccessBanishmentUI();
            yield return new WaitUntil(() => !dialogueUI.IsDialogueVisible());

            GameManager.instance.LeaveInfluence(clickedCharacter);

            characterIndexMenu.gameObject.SetActive(false);
            characterIndexUI.HideCharacterIndexUI();
            characterDetailUI.gameObject.SetActive(false);

            gameManager.ShowLordUI(gameManager.playerCharacter);
        }
        else
        {
            characterIndexUI.ShowCharacterIndexUI(GameManager.instance.playerCharacter.influence.characterList);
        }
    }

    IEnumerator WaitForDefenceBattle()
    {
        yesNoUI.ShowBattleCharacterSelectYesNoUI();
        //yesNoUI����\���ɂȂ�܂őҋ@
        yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());
        clickedFlag = false;

        if (yesNoUI.IsYes())
        {
            GameManager.instance.step = Step.Battle;
            HideCharacterIndex();
            if (clickedCharacter == GameManager.instance.playerCharacter)
            {
                battleUI.ShowBattleUI(battleManager.attackerCharacter, clickedCharacter, influenceManager.territory);
                battleManager.StartBattle(battleManager.attackerCharacter, clickedCharacter);
            }
            else
            {
                battleManager.DefenceBattle(battleManager.attackerCharacter, clickedCharacter);
            }
        }
        else
        {
            characterIndexUI.ShowCharacterIndexUI(influenceManager.territory.influence.characterList);
        }
    }

    IEnumerator WaitForAttackBattle()
    {
        yesNoUI.ShowBattleCharacterSelectYesNoUI();
        //yesNoUI����\���ɂȂ�܂őҋ@
        yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());
        clickedFlag = false;

        if (yesNoUI.IsYes())
        {
            GameManager.instance.step = Step.Battle;
            HideCharacterIndex();

            if (clickedCharacter == GameManager.instance.playerCharacter)
            {
                battleUI.ShowBattleUI(clickedCharacter, defenceCharacter, influenceManager.territory);
                battleManager.StartBattle(clickedCharacter, defenceCharacter);
            }
            else
            {
                battleManager.AttackBattle(clickedCharacter, defenceCharacter);
            }
        }
        else
        {
            characterIndexUI.ShowCharacterIndexUI(GameManager.instance.playerCharacter.influence.characterList);
        }
    }

    //�w�i�F��ύX
    private void ChangeBackgroundColor(Color color)
    {
        Debug.Log("�w�i�F�ύX");
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }

    public void OnPointerClickCharacter()
    {
        ChangeBackgroundColor(Color.gray);
        
        if (Input.GetMouseButtonUp(0))
        {
            //�L�����N�^�[�I���X�e�b�v
            if (GameManager.instance.step == Step.Choice)
            {
                clickedFlag = true;
                StartCoroutine(WaitForSelectCharacter());
            }
            //�T���X�e�b�v
            if (GameManager.instance.step == Step.Search)
            {
                clickedFlag = true;
                StartCoroutine(WaitForSearchCharacter());
            }
            //�C���X�e�b�v
            else if (GameManager.instance.step == Step.Appointment)
            {
                // �N���b�N���ꂽ�L�����N�^�[�̐g�����擾
                Rank clickedRank = clickedCharacter.characterModel.rank;
                // ���̐g�����v�Z
                Rank newRank = (Rank)((int)clickedRank + 1);
                // �g�����̎�̏ꍇ�͏������s��Ȃ�
                if (clickedRank == Rank.�̎� || newRank == Rank.�̎�)
                {
                    TitleFieldUI.instance.titleFieldText.text = "      �̎�͕ύX�ł��܂���";
                    return;
                }
                else
                {
                    TitleFieldUI.instance.titleFieldText.text = "      ���i�������L�����N�^�[���N���b�N";
                    // ���̐g���̃L�����N�^�[��T��
                    CharacterController characterToSwap = clickedCharacter.influence.characterList.Find(c => c.characterModel.rank == newRank);
                    // ����ւ�
                    if (characterToSwap != null)
                    {
                        SwapCharacters(clickedCharacter, characterToSwap);
                    }
                }
                // �L�����N�^�[���X�g��g���̍������Ƀ\�[�g
                clickedCharacter.influence.SortCharacterByRank(clickedCharacter.influence.characterList);
                //UI�X�V
                characterIndexUI.ShowCharacterIndexUI(GameManager.instance.playerCharacter.influence.characterList);
                characterDetailUI.ShowCharacterDetailUI(clickedCharacter);
            }
            //�Ǖ��X�e�b�v
            else if (GameManager.instance.step == Step.Banishment)
            {
                if (clickedCharacter.characterModel.isLord != true)
                {
                    clickedFlag = true;
                    StartCoroutine(WaitForBanishmentCharacter());
                }
                else
                {
                    TitleFieldUI.instance.titleFieldText.text = "      �̎�͒Ǖ��ł��܂���";
                    return;
                }
            }
            //�퓬�X�e�b�v
            else if (GameManager.instance.step == Step.Attack || GameManager.instance.step == Step.Battle)
            {
                //�v���C���[���͂��h�q���̏ꍇ
                if (GameManager.instance.defenceFlag == true)
                {
                    if (clickedCharacter.characterModel.isAttackable == true)
                    {
                        clickedFlag = true;
                        StartCoroutine(WaitForDefenceBattle());
                    }
                    else
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      �N�U�ς݂ł�";
                        return;
                    }
                }
                //�v���C���[���͂��N�U���̏ꍇ
                else
                {
                    if (clickedCharacter.characterModel.isAttackable == true)
                    {
                        battleManager.isBattleEnd = false;

                        //��������͂���퓬�\�ȃL�����N�^�[�������_���Ɏ擾
                        List<CharacterController> defenceCharacterList = influenceManager.influence.characterList.FindAll(c => c.characterModel.isAttackable);
                        System.Random random = new System.Random();
                        defenceCharacter = defenceCharacterList[random.Next(defenceCharacterList.Count)];

                        clickedFlag = true;
                        StartCoroutine(WaitForAttackBattle());
                    }
                    else
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      �N�U�ς݂ł�";
                        return;
                    }
                }
            }
        }
    }

    // �L�����N�^�[�����ւ��郁�\�b�h
    private void SwapCharacters(CharacterController clickedCharacter, CharacterController characterToSwap)
    {
        // �L�����N�^�[�̐g�������ւ�
        Rank tempRank = clickedCharacter.characterModel.rank;
        clickedCharacter.characterModel.rank = characterToSwap.characterModel.rank;
        characterToSwap.characterModel.rank = tempRank;

        clickedCharacter.CalcLoyalty();
        characterToSwap.CalcLoyalty();
    }

    public void SetCharacterController(CharacterController character)
    {
        clickedCharacter = character;
    }

    public void HideCharacterIndex()
    {
        characterIndexMenu.gameObject.SetActive(false);
        characterIndexUI.HideCharacterIndexUI();
        characterDetailUI.gameObject.SetActive(false);
    }
}
