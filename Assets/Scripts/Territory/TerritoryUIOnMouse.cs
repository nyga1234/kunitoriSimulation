using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UniRx;
using Cysharp.Threading.Tasks;

public class TerritoryUIOnMouse : MonoBehaviour
{
    [SerializeField] TerritoryManager territoryManager;
    [SerializeField] Cursor cursor;
    [SerializeField] TitleFieldUI titleFieldUI;
    [SerializeField] DialogueUI dialogueUI;
    [SerializeField] InfluenceUI influenceUI;
    [SerializeField] InfluenceOnMapUI influenceOnMapUI;
    [SerializeField] CharacterIndexUI characterIndexUI;
    [SerializeField] CharacterDetailUI characterDetailUI;
    [SerializeField] TerritoryGenerator territoryGenerator;
    [SerializeField] GameObject characterIndexMenu;
    [SerializeField] GameObject mapField;
    [SerializeField] BattleManager battleManager;
    [SerializeField] BattleUI battleUI;
    [SerializeField] GameObject backToCharaMenuButton;
    [SerializeField] GameObject backToMapFieldButton;

    Territory onPointEnterTerritory;

    private Button _button;
    public Button Button => _button;

    [SerializeField] private UtilityParamObject varParam;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.OnClickAsObservable().Subscribe(_ =>
        {
            OnPointerClickTerritory();
        });
    }

    private void Update()
    {
        if (GameMain.instance.playerCharacter == null)
        {
            return;
        }

        if (GameMain.instance.playerCharacter.influence != GameMain.instance.noneInfluence)
        {
            foreach (Territory roopTerritory in GameMain.instance.allTerritoryList)
            {
                if (roopTerritory.influence == GameMain.instance.playerCharacter.influence)
                {
                    roopTerritory.ShowHomeTerritory(true);
                }
                else
                {
                    roopTerritory.ShowHomeTerritory(false);
                }
            }
        }
        else
        {
            foreach (Territory roopTerritory in GameMain.instance.allTerritoryList)
            {
                roopTerritory.ShowHomeTerritory(false);
            }
        }
    }

    public void OnPointerEnterTerritory()
    {
        switch (GameMain.instance.step)
        {
            case GameMain.Step.Information:
            case GameMain.Step.Attack:
            case GameMain.Step.Choice:
            case GameMain.Step.Enter:
                cursor.gameObject.SetActive(true);
                cursor.SetPosition(transform as RectTransform); // カーソルをアンカーポジションに移動

                SoundManager.instance.PlayMapOnCursorSE();
                
                Territory influenceTerritory = this.GetComponent<Territory>();
                influenceOnMapUI.ShowInfluenceOnMapUI(influenceTerritory.influence, influenceTerritory);
                break;
        }

        onPointEnterTerritory = this.GetComponent<Territory>();
    }

    public async void OnPointerClickTerritory()
    {
        switch (GameMain.instance.step)
        {
            //情報ステップ || キャラクター選択ステップ
            case GameMain.Step.Information:
            case GameMain.Step.Choice:
                SoundManager.instance.PlayClickSE();

                //クリックした領土を設定
                territoryManager.territory = onPointEnterTerritory;

                // Mapと勢力情報を非表示
                mapField.gameObject.SetActive(false);
                cursor.gameObject.SetActive(false);
                influenceOnMapUI.HideInfluenceOnMapUI();

                //キャラクター情報を表示
                characterIndexMenu.SetActive(true);
                characterIndexUI.ShowCharacterIndexUI(onPointEnterTerritory.influence.characterList);
                break;

            //仕官ステップ
            case GameMain.Step.Enter:
                SoundManager.instance.PlayClickSE();

                if (onPointEnterTerritory.influence == GameMain.instance.noneInfluence)
                {
                    TitleFieldUI.instance.titleFieldText.text = "      空き領土です";
                    return;
                }
                else
                {
                    //クリックした領土を設定
                    territoryManager.territory = onPointEnterTerritory;
                    cursor.gameObject.SetActive(false);

                    await SceneController.LoadAsync("UIConfirm");
                    varParam.ConfirmText = "仕官しますか？";
                    // OKまたはCancelボタンがクリックされるのを待機
                    await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);

                    if (varParam.IsConfirm == true)
                    {
                        GameMain.instance.noneInfluence.RemoveCharacter(GameMain.instance.playerCharacter);
                        territoryManager.territory.influence.AddCharacter(GameMain.instance.playerCharacter);

                        mapField.gameObject.SetActive(false);
                        cursor.gameObject.SetActive(false);
                        influenceOnMapUI.HideInfluenceOnMapUI();

                        GameMain.instance.ShowPersonalUI(GameMain.instance.playerCharacter);
                        await SceneController.LoadAsync("UIDialogue");
                        varParam.DialogueText = "採用されました";
                    }
                }
                break;

            //侵攻ステップ
            case GameMain.Step.Attack:
                SoundManager.instance.PlayClickSE();

                territoryManager.territory = onPointEnterTerritory;//クリックした領土を設定
                territoryManager.influence = onPointEnterTerritory.influence;//クリックした勢力を設定

                if (territoryManager.influence == GameMain.instance.playerCharacter.influence)
                {
                    titleFieldUI.titleFieldText.text = "     自国領土です";
                    return;
                }
                else if (territoryManager.influence == GameMain.instance.noneInfluence)
                {
                    titleFieldUI.titleFieldText.text = "     空き領土です";
                    return;
                }
                else if (GameMain.instance.playerCharacter.influence.IsAttackableTerritory(this.onPointEnterTerritory) == false)
                {
                    titleFieldUI.titleFieldText.text = "     隣接していません";
                    return;
                }
                else
                {
                    if (GameMain.instance.playerCharacter.isLord == true)
                    {
                        titleFieldUI.titleFieldText.text = "     侵攻させる部隊を選択してください";

                        // 勢力情報を非表示にする
                        influenceOnMapUI.HideInfluenceOnMapUI();
                        mapField.gameObject.SetActive(false);
                        cursor.gameObject.SetActive(false);

                        //侵攻キャラクター選択画面へ
                        characterIndexMenu.SetActive(true);
                        characterIndexUI.ShowCharacterIndexUI(GameMain.instance.playerCharacter.influence.characterList);
                    }
                    else
                    {
                        AttackBattle();
                    }
                }
                break;
        }
    }

    /// <summary>
    /// 領主ではないプレイヤーを独断で侵攻させる
    /// </summary>
    private async void AttackBattle()
    {
        await SceneController.LoadAsync("UIConfirm");
        varParam.ConfirmText = "侵攻しますか？";
        await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);

        if (varParam.IsConfirm == true)
        {
            //防衛可能なキャラクターがいるか
            bool canAttack = territoryManager.territory.influence.characterList.Exists(c => c.isAttackable);
            if (canAttack)
            {
                //防衛キャラを取得
                CharacterController defenceCharacter = GameMain.instance.SelectDefenceCharacter(GameMain.instance.playerCharacter);

                //戦闘へ進む
                influenceOnMapUI.HideInfluenceOnMapUI();
                mapField.gameObject.SetActive(false);
                cursor.gameObject.SetActive(false);

                battleUI.ShowBattleUI(GameMain.instance.playerCharacter, defenceCharacter, territoryManager.territory);
                battleManager.StartBattle(GameMain.instance.playerCharacter, defenceCharacter);
            }
            //防衛キャラがいない場合の処理を各必要がある
        }
    }

    public void InfluenceCalcSum(Influence influence)
    {
        influence.UpdateSums(influence);
    }

    public void ChangeTerritoryByBattle(Influence influence)
    {
        //領土に勝利した勢力を設定
        territoryManager.territory.influence = influence;
        //勝利した勢力に領土を設定
        influence.AddTerritory(territoryManager.territory);
        //敗北した勢力から領土を除外
        territoryManager.influence.RemoveTerritory(territoryManager.territory);
        //influenceList.Find(x => x.InfluenceType == influence.InfluenceType)?.AddTerritory(this.territory);

        if (influence.territoryList.Count == GameMain.instance.territoryCouont)
        {
            GameMain.instance.uniteCountryFlag = true;
            GameMain.instance.uniteInfluence = influence;
        }
    }
}
