using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TerritoryUIOnMouse : MonoBehaviour
{
    //private InflueneceManager influeneceManager;

    //void Start()
    //{
    //    // InfluenceManagerをシーン内から見つけて取得
    //    influeneceManager = FindObjectOfType<InflueneceManager>();

    //    // InfluenceManagerが見つからなかった場合のエラーチェック
    //    if (influeneceManager == null)
    //    {
    //        Debug.LogError("InflueneceManagerが見つかりませんでした。シーンに配置されていることを確認してください。");
    //    }
    //}

    //public void OnpoineterEnter()
    //{
    //    Debug.Log("到達");
    //    influeneceManager.OnPointerEnterTerritory();
    //}

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

    public Influence influence;
    public Territory territory;

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
            if (GameManager.instance.step == GameManager.Step.Enter)
            {
                GameManager.instance.noneInfluence.RemoveCharacter(GameManager.instance.playerCharacter);
                territory.influence.AddCharacter(GameManager.instance.playerCharacter);

                mapField.gameObject.SetActive(false);
                influenceOnMapUI.HideInfluenceOnMapUI();

                GameManager.instance.ShowPersonalUI(GameManager.instance.playerCharacter);
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

            int attackSoliderHPSum = 0;
            foreach (SoliderController solider in GameManager.instance.playerCharacter.soliderList)
            {
                attackSoliderHPSum += solider.soliderModel.hp;
            }

            Debug.Log(territory.influence.influenceName);
            Debug.Log(this.influence.influenceName);
            Debug.Log(this.territory.influence.influenceName);
            CharacterController defenceCharacter = GameManager.instance.SelectDefenceCharacter(attackSoliderHPSum);

            battleUI.ShowBattleUI(GameManager.instance.playerCharacter, defenceCharacter, territory);
            battleManager.StartBattle(GameManager.instance.playerCharacter, defenceCharacter);
        }
    }

    public void OnPointerClickTerritory()
    {

    }

    public void OnPointerEnterTerritory()
    {
        if (!yesNoUI.IsYesNoVisible())
        {
            if (GameManager.instance.step == GameManager.Step.Information || GameManager.instance.step == GameManager.Step.Attack || GameManager.instance.step == GameManager.Step.Choice || GameManager.instance.step == GameManager.Step.Enter)
            {
                if (!yesNoUI.gameObject.activeSelf)
                {
                    cursor.gameObject.SetActive(true);
                }

                if (isSoundPlayed == false)
                {
                    if (beforeTerritory != null)
                    {
                        SoundManager.instance.PlayMapOnCursorSE();
                        isSoundPlayed = true;
                    }
                }
                cursor.SetPosition(transform);//カーソルをマウス位置へ移動

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
            
            Territory territory = this.GetComponent<Territory>();

            if (territory != null)
            {
                Debug.Log("到達");
                //情報ステップ
                if (Input.GetMouseButtonDown(0) && GameManager.instance.step == GameManager.Step.Information || Input.GetMouseButtonDown(0) && GameManager.instance.step == GameManager.Step.Choice)//左クリックしたら
                {
                    Debug.Log("到達");
                    SoundManager.instance.PlayClickSE();
                    //クリックした領土を設定
                    this.territory = territory;

                    // 勢力情報非表示
                    influenceOnMapUI.HideInfluenceOnMapUI();
                    mapField.gameObject.SetActive(false);

                    //キャラクター情報表示
                    characterIndexMenu.SetActive(true);
                    characterIndexUI.ShowCharacterIndexUI(territory.influence.characterList);
                }
                //仕官ステップ
                else if (GameManager.instance.step == GameManager.Step.Enter)
                {
                    if (Input.GetMouseButtonDown(0) && !yesNoUI.gameObject.activeSelf)
                    {
                        if (territory.influence == GameManager.instance.noneInfluence)
                        {
                            TitleFieldUI.instance.titleFieldText.text = "      空き領土です";
                        }
                        else
                        {
                            //クリックした領土を設定
                            this.territory = territory;
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
                else if (Input.GetMouseButtonDown(0) && GameManager.instance.step == GameManager.Step.Attack)
                {
                    //クリックした領土を設定
                    this.territory = territory;
                    //クリックした勢力を設定（CharacterUIOnClickで防衛側のキャラを取得するために設定）
                    this.influence = territory.influence;

                    if (this.influence == GameManager.instance.playerCharacter.influence)
                    {
                        titleFieldUI.titleFieldText.text = "     自国領土です";
                        return;
                    }
                    else if (this.influence == GameManager.instance.noneInfluence)
                    {
                        titleFieldUI.titleFieldText.text = "     空き領土です";
                        return;
                    }
                    else if (GameManager.instance.playerCharacter.influence.IsAttackableTerritory(this.territory) == false)
                    {
                        titleFieldUI.titleFieldText.text = "     隣接していません";
                        return;
                    }
                    else
                    {
                        if (GameManager.instance.playerCharacter.characterModel.isLord == true)
                        {
                            SoundManager.instance.PlayClickSE();
                            titleFieldUI.titleFieldText.text = "     侵攻させる部隊を選択してください";

                            // 勢力情報を非表示にする
                            influenceOnMapUI.HideInfluenceOnMapUI();
                            mapField.gameObject.SetActive(false);

                            //侵攻キャラクター選択画面へ
                            characterIndexMenu.SetActive(true);
                            characterIndexUI.ShowCharacterIndexUI(GameManager.instance.playerCharacter.influence.characterList);
                        }
                        else
                        {
                            Debug.Log(territory.influence.influenceName);
                            Debug.Log(this.influence.influenceName);
                            Debug.Log(this.territory.influence.influenceName);
                            StartCoroutine(WaitForAttackBattle()); ;
                        }
                    }
                }
            }
        }

        if (GameManager.instance.playerCharacter == null)
        {
            return;
        }
        else if (GameManager.instance.playerCharacter.influence != GameManager.instance.noneInfluence)
        {
            foreach (Territory territory in GameManager.instance.allTerritoryList)
            {
                if (territory.influence == GameManager.instance.playerCharacter.influence)
                {
                    territory.ShowHomeTerritory(true);
                }
                else
                {
                    territory.ShowHomeTerritory(false);
                }
            }
        }
        else
        {
            foreach (Territory territory in GameManager.instance.allTerritoryList)
            {
                territory.ShowHomeTerritory(false);
            }
        }
    }

    public void InfluenceCalcSum(Influence influence)
    {
        influence.CalcTerritorySum(influence);
        influence.CalcGoldSum(influence.characterList);
        influence.CalcCharacterSum(influence.characterList);
        influence.CalcSoliderSum(influence.characterList);
        influence.CalcForceSum(influence.characterList);
    }

    public void ChangeTerritoryByBattle(Influence influence)
    {
        //領土に勢力を設定
        this.territory.influence = influence;
        //勢力に領土を設定
        influence.AddTerritory(this.territory);
        this.influence.RemoveTerritory(this.territory);
        //influenceList.Find(x => x.InfluenceType == influence.InfluenceType)?.AddTerritory(this.territory);

        if (influence.territoryList.Count == GameManager.instance.territoryCouont)
        {
            GameManager.instance.uniteCountryFlag = true;
            GameManager.instance.uniteInfluence = influence;
        }
    }
}
