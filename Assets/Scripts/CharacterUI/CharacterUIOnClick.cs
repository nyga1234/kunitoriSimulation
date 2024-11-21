using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using static GameMain;

public class CharacterUIOnClick : MonoBehaviour
{
    public GameMain gameManager;
    public BattleManager battleManager;

    [SerializeField] TerritoryManager territoryManager;
    [SerializeField] GameObject characterIndexMenu;
    [SerializeField] CharacterIndexUI characterIndexUI;
    [SerializeField] CharacterDetailUI characterDetailUI;

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
        if (character.rank == Rank.�̎�)
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
            battleManager.DefenceBattle(battleManager.attackerCharacter, character);
        }
    }

    private async void StartAttackBattle(CharacterController character)
    {
        if (await ShowConfirmationDialog("��낵���ł����H"))
        {
            GameMain.instance.step = Step.Battle;
            HideAllCharacterUI();

            List<CharacterController> defenceList = territoryManager.influence.characterList.FindAll(c => c.isAttackable);
            if (defenceList.Count > 0)
            {
                defenceCharacter = defenceList[Random.Range(0, defenceList.Count)];
                battleManager.AttackBattle(character, defenceCharacter);
            }
            else
            {
                ShowTemporaryMessage("�퓬�\�ȃL�����N�^�[�����܂���");
            }
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
        characterIndexUI.HideCharacterIndexUI();
        characterIndexUI.ShowCharacterIndexUI(GameMain.instance.playerCharacter.influence.characterList);
        characterDetailUI.ShowCharacterDetailUI(character);
    }

    private void HideAllCharacterUI()
    {
        characterIndexMenu.gameObject.SetActive(false);
        characterIndexUI.HideCharacterIndexUI();
        characterDetailUI.gameObject.SetActive(false);
    }

    private void ShowTemporaryMessage(string message)
    {
        TitleFieldUI.instance.titleFieldText.text = $"      {message}";
    }
}
