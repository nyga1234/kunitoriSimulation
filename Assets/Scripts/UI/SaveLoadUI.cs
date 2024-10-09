using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SaveLoadUI : MonoBehaviour
{
    [SerializeField] private Button maskButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject slotPrefab;  // スロットのPrefab
    [SerializeField] private Transform slotParent;   // スロットを配置する親オブジェクト
    [SerializeField] private ConfirmOverwriteUI confirmOverwriteUI;
    [SerializeField] GameObject imageSoliderPrefab;

    private bool isSaving = true; // セーブかロードかを判断するフラグ
    private List<GameObject> slotInstances = new List<GameObject>(); // 動的に生成されたスロットのインスタンス

    private int maxSlots = 3; // スロットの数（変更可能）

    private void Start()
    {
        // スロットの生成
        GenerateSlots();

        //閉じる（マスクボタン）
        maskButton.onClick.AddListener(() => this.gameObject.SetActive(false));

        //閉じる（閉じるボタン）
        closeButton.onClick.AddListener(() => this.gameObject.SetActive(false));

        // UIの初期化
        UpdateSlotUI();
    }

    // スロット数に応じてPrefabを生成
    private void GenerateSlots()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            GameObject slotInstance = Instantiate(slotPrefab, slotParent); // スロットPrefabを生成
            slotInstances.Add(slotInstance); // インスタンスをリストに追加

            SaveSlotView saveSlotView = slotInstance.GetComponent<SaveSlotView>();

            // ローカル変数を使用してスロット番号をキャプチャ
            int capturedIndex = i;

            // ボタンにクリックリスナーを追加
            saveSlotView.GetComponent<Button>().onClick.AddListener(() => OnSlotButtonClick(capturedIndex));
        }
    }


    private void UpdateSlotUI()
    {
        Debug.Log("UpdateSlotUI");
        for (int i = 0; i < maxSlots; i++)
        {
            SaveSlotView saveSlotView = slotInstances[i].GetComponent<SaveSlotView>();

            // セーブデータが存在する場合、そのデータを読み込む
            if (SaveLoadManager.HasSaveData(i))
            {
                // セーブデータをロード
                GameState gameState = SaveLoadManager.LoadGame(i);

                // スロットの情報を更新
                saveSlotView.SlotText.text = GetSaveText(gameState); // 保存された情報を表示

                var playerCharData = gameState.characters.FirstOrDefault(c => c.isPlayerCharacter);
                saveSlotView.PlayerImage.sprite = playerCharData.icon;

                //saveSlotView.PlayerImage.sprite = gameState.characters
                //    .FirstOrDefault(c => c.isPlayerCharacter)?.icon; // プレイヤーキャラクターのアイコン

                // プレイヤーキャラクターの兵士リストを表示

                if (playerCharData != null)
                {
                    ShowSoldierList(playerCharData.soliders, saveSlotView.SoldierListField);
                }
                //ShowSoliderList(GameMain.instance.playerCharacter.soliderList, saveSlotView.SoldierListField);
            }
            else
            {
                saveSlotView.SlotText.text = "空きスロット"; // セーブデータが無い場合
                saveSlotView.PlayerImage.sprite = null;
                //ShowSoldierList(new List<SoliderController>(), saveSlotView.SoldierListField);
            }
        }
    }

    // GameStateに基づいて表示テキストを取得する
    private string GetSaveText(GameState gameState)
    {
        string turnCount = gameState.turnCount.ToString() + "期";
        // プレイヤーキャラクターの情報を取得
        var playerCharData = gameState.characters.FirstOrDefault(c => c.isPlayerCharacter);
        string rank = playerCharData != null ? playerCharData.rank.ToString() :  null;
        string name = playerCharData != null ? playerCharData.name : null;

        return $"{turnCount} : {rank} {name}";
    }

    // セーブかロードかのモードをセット
    public void SetMode(bool saving)
    {
        isSaving = saving;
        //UpdateSlotUI();
    }

    //void ShowSoliderList(List<SoliderController> soliderList, Transform field)
    //{
    //    // 現在表示されている兵士を削除
    //    foreach (Transform child in field)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    // 新しい兵士リストを作成
    //    foreach (SoliderController solider in soliderList)
    //    {
    //        SoliderController battleSolider = Instantiate(imageSoliderPrefab, field);
    //        battleSolider.ShowBattleDetailSoliderUI(solider, true);
    //    }
    //}

    void ShowSoldierList(List<SoliderData> soldierList, Transform field)
    {
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }

        foreach (var soldierData in soldierList)
        {
            var soldierObject = Instantiate(imageSoliderPrefab, field);
            soldierObject.GetComponent<ShowSaveViewSoldier>().ShowSoldierUI(soldierData.icon);
        }
    }

    private void OnSlotButtonClick(int slot)
    {
        if (isSaving)
        {
            if (SaveLoadManager.HasSaveData(slot))
            {
                // 上書き確認UIを表示
                confirmOverwriteUI.Show(() =>
                {
                    GameMain.instance.SaveGame(slot); //Show()の引数に上書きセーブ処理を渡す
                });
            }
            else
            {
                // セーブ処理
                GameMain.instance.SaveGame(slot);
            }
        }
        else
        {
            // ロード処理
            GameMain.instance.LoadGame(slot);
        }

        // UIを更新して表示する
        UpdateSlotUI();
    }
}
