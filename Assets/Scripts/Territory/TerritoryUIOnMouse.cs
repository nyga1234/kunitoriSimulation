using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TerritoryUIOnMouse : MonoBehaviour
{
    [SerializeField] TerritoryManager territoryManager;
    public Button yesButton; // Buttonを参照するための変数

    [SerializeField] Cursor cursor;
    [SerializeField] TitleFieldUI titleFieldUI;
    [SerializeField] YesNoUI yesNoUI;
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

    //public Influence influence;
    //public Territory territory;

    List<Influence> influenceList = new List<Influence>();
    List<Influence> blueInfluenceList = new List<Influence>();
    List<Influence> redInfluenceList = new List<Influence>();
    List<Influence> noneInfluenceList = new List<Influence>();

    private bool isSoundPlayed = false; // 音が再生されたかどうかを示すフラグ
    Territory beforeTerritory = null;

    private void Start()
    {
        Button btn = yesButton.GetComponent<Button>(); // Buttonコンポーネントを取得
        btn.onClick.AddListener(TaskOnClick); // クリック時にTaskOnClickメソッドを呼び出す
    }

    void TaskOnClick()
    {
        if (yesNoUI.IsYes()) // もしyesNoUIが"Yes"に設定されている場合
        {
            if (GameMain.instance.step == GameMain.Step.Enter)
            {
                GameMain.instance.noneInfluence.RemoveCharacter(GameMain.instance.playerCharacter);
                territoryManager.territory.influence.AddCharacter(GameMain.instance.playerCharacter);

                mapField.gameObject.SetActive(false);
                cursor.gameObject.SetActive(false);
                influenceOnMapUI.HideInfluenceOnMapUI();

                GameMain.instance.ShowPersonalUI(GameMain.instance.playerCharacter);
                dialogueUI.ShowEnterInfluenceUI();
            }

        }
    }

    IEnumerator WaitForAttackBattle()
    {
        yesNoUI.ShowAttackYesNoUI();
        //yesNoUIが非表示になるまで待機
        yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());

        if (yesNoUI.IsYes())
        {
            // 勢力情報非表示
            influenceOnMapUI.HideInfluenceOnMapUI();
            mapField.gameObject.SetActive(false);
            cursor.gameObject.SetActive(false);

            int attackSoliderHPSum = 0;
            foreach (SoliderController solider in GameMain.instance.playerCharacter.soliderList)
            {
                attackSoliderHPSum += solider.soliderModel.hp;
            }

            Debug.Log(territoryManager.territory.influence.influenceName);
            Debug.Log(territoryManager.influence.influenceName);
            Debug.Log(territoryManager.territory.influence.influenceName);
            CharacterController defenceCharacter = GameMain.instance.SelectDefenceCharacter(attackSoliderHPSum);

            battleUI.ShowBattleUI(GameMain.instance.playerCharacter, defenceCharacter, territoryManager.territory);
            battleManager.StartBattle(GameMain.instance.playerCharacter, defenceCharacter);
        }
    }

    Territory onPointEnterTerritory;

    public void OnPointerEnterTerritory()
    {
        if (!yesNoUI.IsYesNoVisible())
        {
            if (GameMain.instance.step == GameMain.Step.Information || GameMain.instance.step == GameMain.Step.Attack || GameMain.instance.step == GameMain.Step.Choice || GameMain.instance.step == GameMain.Step.Enter)
            {
                if (!yesNoUI.gameObject.activeSelf)
                {
                    cursor.gameObject.SetActive(true);
                }

                SoundManager.instance.PlayMapOnCursorSE();
                //if (isSoundPlayed == false)
                //{
                //    if (beforeTerritory != null)
                //    {
                //        SoundManager.instance.PlayMapOnCursorSE();
                //        isSoundPlayed = true;
                //    }
                //}
                cursor.SetPosition(transform as RectTransform); // カーソルをアンカーポジションに移動
                //cursor.SetPosition(transform);

                Territory influenceTerritory = this.GetComponent<Territory>();

                if (beforeTerritory != influenceTerritory)
                {
                    isSoundPlayed = false;
                    beforeTerritory = influenceTerritory;
                }
                else
                {
                    isSoundPlayed = true;
                }

                influenceOnMapUI.ShowInfluenceOnMapUI(influenceTerritory.influence, influenceTerritory);
            }
            
            onPointEnterTerritory = this.GetComponent<Territory>();

        }
    }

    public void OnPointerClickTerritory()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (!yesNoUI.IsYesNoVisible())
            {
                if (onPointEnterTerritory != null)
                {
                    //情報ステップ
                    if (/*Input.GetMouseButtonUp(0) && */GameMain.instance.step == GameMain.Step.Information || /*Input.GetMouseButtonDown(0) && */GameMain.instance.step == GameMain.Step.Choice)
                    {
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
                    }
                    //仕官ステップ
                    else if (GameMain.instance.step == GameMain.Step.Enter)
                    {
                        if (/*Input.GetMouseButtonDown(0) && */!yesNoUI.gameObject.activeSelf)
                        {
                            if (onPointEnterTerritory.influence == GameMain.instance.noneInfluence)
                            {
                                TitleFieldUI.instance.titleFieldText.text = "      空き領土です";
                            }
                            else
                            {
                                //クリックした領土を設定
                                territoryManager.territory = onPointEnterTerritory;
                                cursor.gameObject.SetActive(false);
                                yesNoUI.ShowEnterUI();
                            }
                        }
                        if (yesNoUI.gameObject.activeSelf)
                        {
                            TaskOnClick();
                        }
                    }
                    //侵攻ステップ
                    else if (/*Input.GetMouseButtonDown(0) && */GameMain.instance.step == GameMain.Step.Attack)
                    {
                        //クリックした領土を設定
                        territoryManager.territory = onPointEnterTerritory;
                        //クリックした勢力を設定（CharacterUIOnClickで防衛側のキャラを取得するために設定）
                        territoryManager.influence = onPointEnterTerritory.influence;

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
                            if (GameMain.instance.playerCharacter.characterModel.isLord == true)
                            {
                                SoundManager.instance.PlayClickSE();
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
                                Debug.Log(onPointEnterTerritory.influence.influenceName);
                                Debug.Log(territoryManager.influence.influenceName);
                                Debug.Log(territoryManager.territory.influence.influenceName);
                                StartCoroutine(WaitForAttackBattle()); ;
                            }
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (GameMain.instance.playerCharacter == null)
        {
            return;
        }
        else if (GameMain.instance.playerCharacter.influence != GameMain.instance.noneInfluence)
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
