using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class SaveLoadUI : MonoBehaviour
{
    [SerializeField] private Button maskButton;
    [SerializeField] private Button closeButton;

    [SerializeField] private Transform slotParent;

    [SerializeField] private GameObject slotPrefab;
    [SerializeField] GameObject imageSoliderPrefab;
    
    [SerializeField] private ConfirmOverwriteUI confirmOverwriteUI;

    //private bool isSaving = true; // セーブかロードかを判断するフラグ
    private List<GameObject> slotInstances = new List<GameObject>(); // 動的に生成されたスロットのインスタンス

    private int maxSlots = 3; // スロットの数（変更可能）

    private void Start()
    {
        GenerateSlots();// スロットの生成

        maskButton.onClick.AddListener(() => this.gameObject.SetActive(false));
        closeButton.onClick.AsObservable().Subscribe(_ => { UniTask uniTask = OnPressClose(); });

        UpdateSlotUI();

        SceneController.instance.Stack.Add("UISaveLoad");
    }

    private void OnDestroy()
    {
        SceneController.instance.Stack.Remove("UISaveLoad");
    }

    private void GenerateSlots()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            GameObject slotInstance = Instantiate(slotPrefab, slotParent); // スロットPrefabを生成
            slotInstances.Add(slotInstance); // インスタンスをリストに追加
            SaveSlotView saveSlotView = slotInstance.GetComponent<SaveSlotView>();
            int capturedIndex = i;// ローカル変数を使用してスロット番号をキャプチャ
            
            saveSlotView.GetComponent<Button>().onClick.AddListener(() => OnSlotButtonClick(capturedIndex));
        }

        closeButton.transform.SetAsLastSibling();
    }

    private void OnSlotButtonClick(int slot)
    {
        if (SaveLoadManager.IsSaving)
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
                GameMain.instance.SaveGame(slot);// セーブ処理
            }
        }
        else
        {
            GameManager.instance.ChangeScene("Title", "GameMain"); // ロード処理
        }

        // UIを更新して表示する
        UpdateSlotUI();
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
                var playerCharData = gameState.characters.FirstOrDefault(c => c.isPlayerCharacter);

                saveSlotView.SlotText.text = GetSaveText(gameState); // セーブテキストを更新
                saveSlotView.PlayerImage.sprite = playerCharData.icon; //プレイヤーアイコンの更新
                ShowSoldierList(playerCharData.soliders, saveSlotView.SoldierListField);
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
        // プレイヤーキャラクターの情報を取得
        var playerCharData = gameState.characters.FirstOrDefault(c => c.isPlayerCharacter);
        
        string turnCount = gameState.turnCount.ToString() + "期";
        string rank = playerCharData != null ? playerCharData.rank.ToString() :  null;
        string name = playerCharData != null ? playerCharData.name : null;
        return $"{turnCount} : {rank} {name}";
    }

    void ShowSoldierList(List<SoliderData> soldierList, Transform field)
    {
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }

        foreach (var soldierData in soldierList)
        {
            var soldierObject = Instantiate(imageSoliderPrefab, field);
            soldierObject.GetComponent<SoldierImageView>().ShowSoldierImage(soldierData.icon, true);
        }
    }

    public async UniTask OnPressClose() => await SceneController.UnloadAsync("UISaveLoad");
}
