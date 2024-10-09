using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class ConfirmOverwriteUI : MonoBehaviour
{
    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;
    private System.Action onConfirm; // OKを押したときのコールバック

    private void Start()
    {
        // OKボタンの処理
        okButton.onClick.AddListener(() =>
        {
            onConfirm?.Invoke(); // OK時に渡されたコールバックを実行
            this.gameObject.SetActive(false); // ダイアログを閉じる
        });

        // キャンセルボタンの処理
        cancelButton.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false); // ダイアログを閉じる
        });
    }

    // 上書きを確認するダイアログを表示
    public void Show(System.Action confirmAction)
    {
        onConfirm = confirmAction; // OK時に実行するアクションを設定
        this.gameObject.SetActive(true); // ダイアログを表示
    }

    //// 上書きを確認するダイアログを表示して、結果を返す非同期メソッド
    //public async UniTask<bool> ShowAsync()
    //{
    //    this.gameObject.SetActive(true); // ダイアログを表示

    //    bool isConfirmed = false;

    //    // OKボタンのクリックを待つ
    //    okButton.onClick.AddListener(() =>
    //    {
    //        isConfirmed = true;
    //        this.gameObject.SetActive(false); // ダイアログを閉じる
    //    });

    //    // キャンセルボタンのクリックを待つ
    //    cancelButton.onClick.AddListener(() =>
    //    {
    //        isConfirmed = false;
    //        this.gameObject.SetActive(false); // ダイアログを閉じる
    //    });

    //    // ダイアログが閉じるまで待機
    //    await UniTask.WaitUntil(() => !this.gameObject.activeSelf);

    //    return isConfirmed; // OKが押されたかどうかを返す
    //}
}
