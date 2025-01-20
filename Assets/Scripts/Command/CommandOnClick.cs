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

        TitleFieldUI.instance.titleFieldText.text = "選択したキャラクターを昇格";

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
                        TitleFieldUI.instance.titleFieldText.text = "      配下の将数が上限です";
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
                        TitleFieldUI.instance.titleFieldText.text = "      配下の将数が上限です";
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
                        TitleFieldUI.instance.titleFieldText.text = "      配下の将数が上限です";
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
                        TitleFieldUI.instance.titleFieldText.text = "      配下の将数が上限です";
                    }
                    break;
            }
        }
        else
        {
            TitleFieldUI.instance.titleFieldText.text = "      資金が足りません";
        }
    }

    public void OnPointerClickBanishment()
    {
        if (GameMain.instance.playerCharacter.influence.characterList.Count != 1)
        {
            GameMain.instance.step = Step.Banishment;

            characterMenuUI.HideCharacterMenuUI();
            GameMain.instance.CharacterDetailUI.HideCharacterDetailUI();

            TitleFieldUI.instance.titleFieldText.text = "      クリックで追放";
            GameMain.instance.CharacterIndexUI.ShowCharacterIndexUI(GameMain.instance.playerCharacter.influence.characterList);
        }
        else
        {
            TitleFieldUI.instance.titleFieldText.text = "      領主は追放できません";
            return;
        }
    }

    public async void OnPointerClickLordEnd()
    {
        await SceneController.LoadAsync("UIConfirm");
        varParam.ConfirmText = "ターンを終了しますか？";
        // OKまたはCancelボタンがクリックされるのを待機
        await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);

        if (varParam.IsConfirm == true)
        {
            GameMain.instance.step = Step.End;

            characterMenuUI.HideCharacterMenuUI();
            GameMain.instance.CharacterDetailUI.HideCharacterDetailUI();

            //GameMainのフェーズを進める
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
            TitleFieldUI.instance.titleFieldText.text = "      雇用可能な兵士は10人までです";
            return;
        }
        else if (GameMain.instance.playerCharacter.gold < 2)
        {
            TitleFieldUI.instance.titleFieldText.text = "      資金が足りません";
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

            TitleFieldUI.instance.titleFieldText.text = "      仕官先を選択";
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
            TitleFieldUI.instance.titleFieldText.text = "      資金が足りません";
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
            varParam.ConfirmText = "所属勢力を去りますか？";
            // OKまたはCancelボタンがクリックされるのを待機
            await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);

            if (varParam.IsConfirm == true)
            {
                //所属勢力を去る
                GameMain.instance.LeaveInfluence(GameMain.instance.playerCharacter);

                GameMain.instance.ShowPersonalUI(GameMain.instance.playerCharacter);

                await SceneController.LoadAsync("UIDialogue");
                varParam.DialogueText = "所属勢力を去りました";
            }
        }
    }

    public async void OnPointerClickPersonalEnd()
    {
        await SceneController.LoadAsync("UIConfirm");
        varParam.ConfirmText = "ターンを終了しますか？";
        // OKまたはCancelボタンがクリックされるのを待機
        await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);

        if (varParam.IsConfirm == true)
        {
            GameMain.instance.step = Step.End;

            personalMenuUI.HidePersonalMenuUI();
            influenceUI.HideInfluenceUI();

            //GameMainのフェーズを進める
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
            TitleFieldUI.instance.titleFieldText.text = "      侵攻済みです";
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
            TitleFieldUI.instance.titleFieldText.text = "      資金が足りません";
            return;
        }
    }

    public async void OnPointerClickBattleEnd()
    {
        await SceneController.LoadAsync("UIConfirm");
        varParam.ConfirmText = "ターンを終了しますか？";
        // OKまたはCancelボタンがクリックされるのを待機
        await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);

        if (varParam.IsConfirm == true)
        {
            GameMain.instance.step = Step.End;

            battleMenuUI.HideBattleMenuUI();
            GameMain.instance.CharacterDetailUI.HideCharacterDetailUI();

            bool isPlayerLast = GameMain.instance.characterList.LastOrDefault() == GameMain.instance.playerCharacter;
            if (isPlayerLast)
            {
                Debug.Log("プレイヤーは最後です。");
                //プレイヤー領主フェーズへ進める
                GameMain.instance.phase = Phase.PlayerLordPhase;
                GameMain.instance.PhaseCalc();
            }
            else
            {
                Debug.Log("プレイヤーは最後ではありません。");
                CharacterController playerNextCharacter = GameMain.instance.GetNextCharacter(GameMain.instance.playerCharacter);
                GameMain.instance.OtherBattlePhase(playerNextCharacter);
            }
        }
    }

    //共通処理
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

        //Noneに所属するキャラクターリストからランダムに6名取得
        List<CharacterController> noneInfluenceCharacters = GameMain.instance.noneInfluence.characterList;
        System.Random random = new System.Random();
        List<CharacterController> shuffledList = noneInfluenceCharacters.OrderBy(x => random.Next()).ToList();
        // 先頭から6つの要素を取得
        List<CharacterController> randomCharacters = shuffledList.Take(6).ToList();

        TitleFieldUI.instance.titleFieldText.text = "      配下に加えたいキャラをクリックで追加";
        GameMain.instance.CharacterIndexUI.ShowCharacterIndexUI(randomCharacters);
    }
}
