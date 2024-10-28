//using UnityEngine;
//using UnityEngine.UI;
//using Cysharp.Threading.Tasks;
//using UniRx;

//public class ConfirmOverwriteUI : MonoBehaviour
//{
//    [SerializeField] private Button okButton;
//    [SerializeField] private Button cancelButton;
//    [SerializeField] private GameObject confirmDialog; // ConfirmUI自体の参照

//    private bool isConfirmed; // OKが押されたかどうかを保持

//    private void Start()
//    {
//        okButton.onClick.AsObservable().Subscribe(_ =>
//        {
//            isConfirmed = true; // OKが押されたことを記録
//            confirmDialog.SetActive(false);
//        });


//        cancelButton.onClick.AsObservable().Subscribe(_ =>
//        {
//            isConfirmed = false; // OKが押されたことを記録
//            confirmDialog.SetActive(false);
//        });

//        //SceneController.instance.Stack.Add("UIConfirm");
//    }

//    //private void OnDestroy()
//    //{
//    //    SceneController.instance.Stack.Remove("UIConfirm");
//    //}

//    // 上書きを確認するダイアログを表示して、結果を返す非同期メソッド
//    public async UniTask<bool> ShowAsync()
//    {
//        confirmDialog.SetActive(true); // ダイアログを表示
//        isConfirmed = false; // 初期化

//        // ダイアログが閉じるまで待機
//        await UniTask.WaitUntil(() => !this.gameObject.activeSelf);

//        return isConfirmed; // OKが押されたかどうかを返す
//    }
//}