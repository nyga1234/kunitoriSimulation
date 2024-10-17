using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using static GameMain;

public class CharacterUIOnClick : MonoBehaviour
{
    public GameMain gameManager;
    public BattleManager battleManager;
    //public InflueneceManager influenceManager;
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;
    [SerializeField] TerritoryManager territoryManager;

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
            clickedCharacter.isPlayerCharacter = true;
            GameMain.instance.playerCharacter = clickedCharacter;

            characterIndexMenu.gameObject.SetActive(false);
            characterIndexUI.HideCharacterIndexUI();
            characterDetailUI.gameObject.SetActive(false);

            GameMain.instance.phase = Phase.SetupPhase;
            GameMain.instance.PhaseCalc();
        }
        else
        {
            characterIndexUI.ShowCharacterIndexUI(territoryManager.territory.influence.characterList);
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

            GameMain.instance.LeaveInfluence(clickedCharacter);

            characterIndexMenu.gameObject.SetActive(false);
            characterIndexUI.HideCharacterIndexUI();
            characterDetailUI.gameObject.SetActive(false);

            gameManager.ShowLordUI(gameManager.playerCharacter);
        }
        else
        {
            characterIndexUI.ShowCharacterIndexUI(GameMain.instance.playerCharacter.influence.characterList);
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
            GameMain.instance.step = Step.Battle;
            HideCharacterIndex();
            if (clickedCharacter == GameMain.instance.playerCharacter)
            {
                battleUI.ShowBattleUI(battleManager.attackerCharacter, clickedCharacter, territoryManager.territory);
                battleManager.StartBattle(battleManager.attackerCharacter, clickedCharacter);
            }
            else
            {
                battleManager.DefenceBattle(battleManager.attackerCharacter, clickedCharacter);
            }
        }
        else
        {
            characterIndexUI.ShowCharacterIndexUI(territoryManager.territory.influence.characterList);
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
            GameMain.instance.step = Step.Battle;
            HideCharacterIndex();

            if (clickedCharacter == GameMain.instance.playerCharacter)
            {
                battleUI.ShowBattleUI(clickedCharacter, defenceCharacter, territoryManager.territory);
                battleManager.StartBattle(clickedCharacter, defenceCharacter);
            }
            else
            {
                battleManager.AttackBattle(clickedCharacter, defenceCharacter);
            }
        }
        else
        {
            characterIndexUI.ShowCharacterIndexUI(GameMain.instance.playerCharacter.influence.characterList);
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
            if (GameMain.instance.step == Step.Choice)
            {
                clickedFlag = true;
                StartCoroutine(WaitForSelectCharacter());
            }
            //�T���X�e�b�v
            if (GameMain.instance.step == Step.Search)
            {
                clickedFlag = true;
                StartCoroutine(WaitForSearchCharacter());
            }
            //�C���X�e�b�v
            else if (GameMain.instance.step == Step.Appointment)
            {
                // �N���b�N���ꂽ�L�����N�^�[�̐g�����擾
                Rank clickedRank = clickedCharacter.rank;
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
                    SoundManager.instance.PlayClickSE();
                    TitleFieldUI.instance.titleFieldText.text = "      ���i�������L�����N�^�[���N���b�N";
                    // ���̐g���̃L�����N�^�[��T��
                    CharacterController characterToSwap = clickedCharacter.influence.characterList.Find(c => c.rank == newRank);
                    // ����ւ�
                    if (characterToSwap != null)
                    {
                        SwapCharacters(clickedCharacter, characterToSwap);
                    }
                }
                // �L�����N�^�[���X�g��g���̍������Ƀ\�[�g
                clickedCharacter.influence.SortCharacterByRank(clickedCharacter.influence.characterList);
                //UI�X�V
                characterIndexUI.ShowCharacterIndexUI(GameMain.instance.playerCharacter.influence.characterList);
                characterDetailUI.ShowCharacterDetailUI(clickedCharacter);
            }
            //�Ǖ��X�e�b�v
            else if (GameMain.instance.step == Step.Banishment)
            {
                if (clickedCharacter.isLord != true)
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
            else if (GameMain.instance.step == Step.Attack || GameMain.instance.step == Step.Battle)
            {
                //�v���C���[���͂��h�q���̏ꍇ
                if (GameMain.instance.defenceFlag == true)
                {
                    if (clickedCharacter.isAttackable == true)
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
                    if (clickedCharacter.isAttackable == true)
                    {
                        battleManager.isBattleEnd = false;

                        //��������͂���퓬�\�ȃL�����N�^�[�������_���Ɏ擾
                        List<CharacterController> defenceCharacterList = territoryManager.influence.characterList.FindAll(c => c.isAttackable);
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
        Rank tempRank = clickedCharacter.rank;
        clickedCharacter.rank = characterToSwap.rank;
        characterToSwap.rank = tempRank;

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
