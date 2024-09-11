using UnityEngine;
using UnityEngine.UI;

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
}
