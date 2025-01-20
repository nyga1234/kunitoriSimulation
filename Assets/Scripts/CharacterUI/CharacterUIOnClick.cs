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
                Debug.LogWarning("未知のゲームステップです。");
                break;
        }
    }

    private async void HandleCharacterSelection(CharacterController character)
    {
        if (await ShowConfirmationDialog("このキャラクターでプレイしますか？"))
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
        if (await ShowConfirmationDialog("登用しますか？"))
        {
            await ShowDialogue("登用に成功しました");
            TransferCharacterToPlayer(character);
        }
    }

    private void HandleCharacterAppointment(CharacterController character)
    {
        if (Rank.領主 == (Rank)(int)character.rank + 1 || character.rank == Rank.領主)
        {
            ShowTemporaryMessage("領主は変更できません");
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
            ShowTemporaryMessage("昇格可能なキャラクターがいません");
        }
    }

    private async void HandleCharacterBanishment(CharacterController character)
    {
        if (character.isLord)
        {
            ShowTemporaryMessage("領主は追放できません");
            return;
        }

        if (await ShowConfirmationDialog("追放しますか？"))
        {
            await ShowDialogue("追放に成功しました");
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
            ShowTemporaryMessage("侵攻済みです");
        }
    }

    private async void StartDefenceBattle(CharacterController character)
    {
        if (await ShowConfirmationDialog("よろしいですか？"))
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
    /// 領主（プレイヤー）が出撃キャラを選択して侵攻を開始する
    /// </summary>
    /// <param name="character"></param>
    private async void StartAttackBattle(CharacterController character)
    {
        if (await ShowConfirmationDialog("よろしいですか？"))
        {
            GameMain.instance.step = Step.Battle;
            HideAllCharacterUI();

            //防衛側で戦闘可能なキャラクターがいる場合
            bool canAttack = varParam.Territory.influence.characterList.Exists(c => c.isAttackable);
            if (canAttack)
            {
                //防衛キャラを取得
                CharacterController defenceCharacter = GameMain.instance.SelectDefenceCharacter(character);

                //侵攻させるキャラにプレイヤーを選択した場合
                if (character == GameMain.instance.playerCharacter)
                {
                    battleUI.ShowBattleUI(character, defenceCharacter, varParam.Territory);
                    battleManager.StartBattle(character, defenceCharacter);
                }
                //プレイヤー以外を選択した場合
                else
                {
                    battleManager.AttackBattle(character, defenceCharacter);
                }
            }
            //防衛キャラがいない場合の処理を各必要がある
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
