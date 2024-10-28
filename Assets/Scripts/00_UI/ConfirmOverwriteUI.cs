//using UnityEngine;
//using UnityEngine.UI;
//using Cysharp.Threading.Tasks;
//using UniRx;

//public class ConfirmOverwriteUI : MonoBehaviour
//{
//    [SerializeField] private Button okButton;
//    [SerializeField] private Button cancelButton;
//    [SerializeField] private GameObject confirmDialog; // ConfirmUI���̂̎Q��

//    private bool isConfirmed; // OK�������ꂽ���ǂ�����ێ�

//    private void Start()
//    {
//        okButton.onClick.AsObservable().Subscribe(_ =>
//        {
//            isConfirmed = true; // OK�������ꂽ���Ƃ��L�^
//            confirmDialog.SetActive(false);
//        });


//        cancelButton.onClick.AsObservable().Subscribe(_ =>
//        {
//            isConfirmed = false; // OK�������ꂽ���Ƃ��L�^
//            confirmDialog.SetActive(false);
//        });

//        //SceneController.instance.Stack.Add("UIConfirm");
//    }

//    //private void OnDestroy()
//    //{
//    //    SceneController.instance.Stack.Remove("UIConfirm");
//    //}

//    // �㏑�����m�F����_�C�A���O��\�����āA���ʂ�Ԃ��񓯊����\�b�h
//    public async UniTask<bool> ShowAsync()
//    {
//        confirmDialog.SetActive(true); // �_�C�A���O��\��
//        isConfirmed = false; // ������

//        // �_�C�A���O������܂őҋ@
//        await UniTask.WaitUntil(() => !this.gameObject.activeSelf);

//        return isConfirmed; // OK�������ꂽ���ǂ�����Ԃ�
//    }
//}