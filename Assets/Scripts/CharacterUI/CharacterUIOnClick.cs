using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using static GameMain;

public class CharacterUIOnClick : MonoBehaviour
{
    public GameMain gameManager;
    public BattleManager battleManager;

    [SerializeField] BattleUI battleUI;

    [SerializeField] private UtilityParamObject varParam;

    private CharacterController defenceCharacter;

    public void OnPointerClickCharacter(CharacterController character)
    {
        switch (GameMain.instance.step)
        {
            case Step.Choice:
                HandleCharacterSelection(character);
                break;

            case Step.Search:
                HandleCharacterSearch(character);
                break;

            case Step.Appointment:
                HandleCharacterAppointment(character);
                break;

            case Step.Banishment:
                HandleCharacterBanishment(character);
                break;

            case Step.Attack:
            case Step.Battle:
                HandleBattle(character);
                break;

            default:
                Debug.LogWarning("���m�̃Q�[���X�e�b�v�ł��B");
                break;
        }
    }

    private async void HandleCharacterSelection(CharacterController character)
    {
        if (await ShowConfirmationDialog("���̃L�����N�^�[�Ńv���C���܂����H"))
        {
            character.isPlayerCharacter = true;
            GameMain.instance.playerCharacter = character;

            HideAllCharacterUI();
            GameMain.instance.phase = Phase.SetupPhase;
            GameMain.instance.PhaseCalc();
        }
    }

    private async void HandleCharacterSearch(CharacterController character)
    {
        if (await ShowConfirmationDialog("�o�p���܂����H"))
        {
            await ShowDialogue("�o�p�ɐ������܂���");
            TransferCharacterToPlayer(character);
        }
    }

    private void HandleCharacterAppointment(CharacterController character)
    {
        if (Rank.�̎� == (Rank)(int)character.rank + 1 || character.rank == Rank.�̎�)
        {
            ShowTemporaryMessage("�̎�͕ύX�ł��܂���");
            return;
        }

        Rank newRank = (Rank)((int)character.rank + 1);
        CharacterController targetCharacter = character.influence.characterList.Find(c => c.rank == newRank);

        if (targetCharacter != null)
        {
            SwapCharacterRanks(character, targetCharacter);
            SortAndRefreshUI(character);
        }
        else
        {
            ShowTemporaryMessage("���i�\�ȃL�����N�^�[�����܂���");
        }
    }

    private async void HandleCharacterBanishment(CharacterController character)
    {
        if (character.isLord)
        {
            ShowTemporaryMessage("�̎�͒Ǖ��ł��܂���");
            return;
        }

        if (await ShowConfirmationDialog("�Ǖ����܂����H"))
        {
            await ShowDialogue("�Ǖ��ɐ������܂���");
            GameMain.instance.LeaveInfluence(character);
            HideAllCharacterUI();
            gameManager.ShowLordUI(gameManager.playerCharacter);
        }
    }

    private void HandleBattle(CharacterController character)
    {
        if (character.isAttackable)
        {
            if (GameMain.instance.defenceFlag)
            {
                StartDefenceBattle(character);
            }
            else
            {
                StartAttackBattle(character);
            }
        }
        else
        {
            ShowTemporaryMessage("�N�U�ς݂ł�");
        }
    }

    private async void StartDefenceBattle(CharacterController character)
    {
        if (await ShowConfirmationDialog("��낵���ł����H"))
        {
            GameMain.instance.step = Step.Battle;
            HideAllCharacterUI();
            if (character == GameMain.instance.playerCharacter)
            {
                battleUI.ShowBattleUI(battleManager.attackerCharacter, character, varParam.Territory);
                battleManager.StartBattle(battleManager.attackerCharacter, character);
            }
            else
            {
                battleManager.DefenceBattle(battleManager.attackerCharacter, character);
            }
                
        }
    }

    /// <summary>
    /// �̎�i�v���C���[�j���o���L������I�����ĐN�U���J�n����
    /// </summary>
    /// <param name="character"></param>
    private async void StartAttackBattle(CharacterController character)
    {
        if (await ShowConfirmationDialog("��낵���ł����H"))
        {
            GameMain.instance.step = Step.Battle;
            HideAllCharacterUI();

            //�h�q���Ő퓬�\�ȃL�����N�^�[������ꍇ
            bool canAttack = varParam.Territory.influence.characterList.Exists(c => c.isAttackable);
            if (canAttack)
            {
                //�h�q�L�������擾
                CharacterController defenceCharacter = GameMain.instance.SelectDefenceCharacter(character);

                //�N�U������L�����Ƀv���C���[��I�������ꍇ
                if (character == GameMain.instance.playerCharacter)
                {
                    battleUI.ShowBattleUI(character, defenceCharacter, varParam.Territory);
                    battleManager.StartBattle(character, defenceCharacter);
                }
                //�v���C���[�ȊO��I�������ꍇ
                else
                {
                    battleManager.AttackBattle(character, defenceCharacter);
                }
            }
            //�h�q�L���������Ȃ��ꍇ�̏������e�K�v������
        }
    }

    private async UniTask<bool> ShowConfirmationDialog(string message)
    {
        await SceneController.LoadAsync("UIConfirm");
        varParam.ConfirmText = message;
        await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);
        return varParam.IsConfirm == true;
    }

    private async UniTask ShowDialogue(string message)
    {
        await SceneController.LoadAsync("UIDialogue");
        varParam.DialogueText = message;
    }

    private void SwapCharacterRanks(CharacterController charA, CharacterController charB)
    {
        Rank tempRank = charA.rank;
        charA.rank = charB.rank;
        charB.rank = tempRank;

        charA.CalcLoyalty();
        charB.CalcLoyalty();
    }

    private void TransferCharacterToPlayer(CharacterController character)
    {
        character.influence.RemoveCharacter(character);
        gameManager.playerCharacter.influence.AddCharacter(character);

        HideAllCharacterUI();
        gameManager.ShowLordUI(gameManager.playerCharacter);
    }

    private void SortAndRefreshUI(CharacterController character)
    {
        character.influence.SortCharacterByRank(character.influence.characterList);
        GameMain.instance.CharacterIndexUI.HideCharacterIndexUI();
        GameMain.instance.CharacterIndexUI.ShowCharacterIndexUI(GameMain.instance.playerCharacter.influence.characterList);
        GameMain.instance.CharacterDetailUI.ShowCharacterDetailUI(character);
    }

    private void HideAllCharacterUI()
    {
        GameMain.instance.CharacterIndexUI.HideCharacterIndexUI();
        GameMain.instance.CharacterDetailUI.gameObject.SetActive(false);
    }

    private void ShowTemporaryMessage(string message)
    {
        TitleFieldUI.instance.titleFieldText.text = $"      {message}";
    }
}
