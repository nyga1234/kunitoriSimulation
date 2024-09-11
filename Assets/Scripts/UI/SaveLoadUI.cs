using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SaveLoadUI : MonoBehaviour
{
    [SerializeField] private Button maskButton;
    [SerializeField] private Button slotButton1;
    [SerializeField] private Button slotButton2;
    [SerializeField] private Button slotButton3;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI slotText1; // スロット1のステータス表示用
    [SerializeField] private TextMeshProUGUI slotText2; // スロット2のステータス表示用
    [SerializeField] private TextMeshProUGUI slotText3; // スロット3のステータス表示用

    private bool isSaving = true; // セーブかロードかを判断するフラグ
    [SerializeField] private ConfirmOverwriteUI confirmOverwriteUI;

    private void Start()
    {
        // UIの初期化
        UpdateSlotUI();

        //閉じる（マスクボタン）
        maskButton.onClick.AddListener(() => this.gameObject.SetActive(false));

        // スロットボタンのイベント登録
        slotButton1.onClick.AddListener(() => OnSlotButtonClick(1));
        slotButton2.onClick.AddListener(() => OnSlotButtonClick(2));
        slotButton3.onClick.AddListener(() => OnSlotButtonClick(3));

        //閉じる（閉じるボタン）
        closeButton.onClick.AddListener(() => this.gameObject.SetActive(false));
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
                    GameManager.instance.SaveGame(slot); // 上書きセーブ処理を渡す
                });
            }
            else
            {
                // セーブ処理
                GameManager.instance.SaveGame(slot);
            }
        }
        else
        {
            // ロード処理
            GameManager.instance.LoadGame(slot);
        }

        // UIを更新して表示する
        UpdateSlotUI();
    }

    private void UpdateSlotUI()
    {
        // 各スロットにセーブデータがあるか確認して表示を更新
        slotText1.text = SaveLoadManager.HasSaveData(1) ? GetSaveText() : "空きスロット";
        slotText2.text = SaveLoadManager.HasSaveData(2) ? GetSaveText() : "空きスロット";
        slotText3.text = SaveLoadManager.HasSaveData(3) ? GetSaveText() : "空きスロット";
    }

    private string GetSaveText()
    {
        string turnCount = GameManager.instance.turnCount.ToString() + "期";
        string rank = GameManager.instance.playerCharacter.characterModel.rank.ToString();
        string name = GameManager.instance.playerCharacter.characterModel.name;
        return turnCount + " : " + rank + " " + name; 
    }

    // セーブかロードかのモードをセット
    public void SetMode(bool saving)
    {
        isSaving = saving;
        UpdateSlotUI();
    }
}
