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
        //yesNoUIが非表示になるまで待機
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
        //yesNoUIが非表示になるまで待機
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
        //yesNoUIが非表示になるまで待機
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
        //yesNoUIが非表示になるまで待機
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
        //yesNoUIが非表示になるまで待機
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

    //背景色を変更
    private void ChangeBackgroundColor(Color color)
    {
        Debug.Log("背景色変更");
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
            //キャラクター選択ステップ
            if (GameManager.instance.step == Step.Choice)
            {
                clickedFlag = true;
                StartCoroutine(WaitForSelectCharacter());
            }
            //探索ステップ
            if (GameManager.instance.step == Step.Search)
            {
                clickedFlag = true;
                StartCoroutine(WaitForSearchCharacter());
            }
            //任命ステップ
            else if (GameManager.instance.step == Step.Appointment)
            {
                // クリックされたキャラクターの身分を取得
                Rank clickedRank = clickedCharacter.characterModel.rank;
                // 一つ上の身分を計算
                Rank newRank = (Rank)((int)clickedRank + 1);
                // 身分が領主の場合は処理を行わない
                if (clickedRank == Rank.領主 || newRank == Rank.領主)
                {
                    TitleFieldUI.instance.titleFieldText.text = "      領主は変更できません";
                    return;
                }
                else
                {
                    TitleFieldUI.instance.titleFieldText.text = "      昇格したいキャラクターをクリック";
                    // 一つ上の身分のキャラクターを探す
                    CharacterController characterToSwap = clickedCharacter.influence.characterList.Find(c => c.characterModel.rank == newRank);
                    // 入れ替え
                    if (characterToSwap != null)
                    {
                        SwapCharacters(clickedCharacter, characterToSwap);
                    }
                }
                // キャラクターリストを身分の高い順にソート
                clickedCharacter.influence.SortCharacterByRank(clickedCharacter.influence.characterList);
                //UI更新
                characterIndexUI.ShowCharacterIndexUI(GameManager.instance.playerCharacter.influence.characterList);
                characterDetailUI.ShowCharacterDetailUI(clickedCharacter);
            }
            //追放ステップ
            else if (GameManager.instance.step == Step.Banishment)
            {
                if (clickedCharacter.characterModel.isLord != true)
                {
                    clickedFlag = true;
                    StartCoroutine(WaitForBanishmentCharacter());
                }
                else
                {
                    TitleFieldUI.instance.titleFieldText.text = "      領主は追放できません";
                    return;
                }
            }
            //戦闘ステップ
            else if (GameManager.instance.step == Step.Attack || GameManager.instance.step == Step.Battle)
            {
                //プレイヤー勢力が防衛側の場合
                if (GameManager.instance.defenceFlag == true)
                {
                    if (clickedCharacter.characterModel.isAttackable == true)
                    {
                        clickedFlag = true;
                        StartCoroutine(WaitForDefenceBattle());
                    }
                    else
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      侵攻済みです";
                        return;
                    }
                }
                //プレイヤー勢力が侵攻側の場合
                else
                {
                    if (clickedCharacter.characterModel.isAttackable == true)
                    {
                        battleManager.isBattleEnd = false;

                        //守備側勢力から戦闘可能なキャラクターをランダムに取得
                        List<CharacterController> defenceCharacterList = influenceManager.influence.characterList.FindAll(c => c.characterModel.isAttackable);
                        System.Random random = new System.Random();
                        defenceCharacter = defenceCharacterList[random.Next(defenceCharacterList.Count)];

                        clickedFlag = true;
                        StartCoroutine(WaitForAttackBattle());
                    }
                    else
                    {
                        TitleFieldUI.instance.titleFieldText.text = "      侵攻済みです";
                        return;
                    }
                }
            }
        }
    }

    // キャラクターを入れ替えるメソッド
    private void SwapCharacters(CharacterController clickedCharacter, CharacterController characterToSwap)
    {
        // キャラクターの身分を入れ替え
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
