using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

public class ConfirmOverwriteUI : MonoBehaviour
{
    [SerializeField] private Button okButton;
    [SerializeField] private Button cancelButton;
    private System.Action onConfirm; // OK���������Ƃ��̃R�[���o�b�N

    private void Start()
    {
        // OK�{�^���̏���
        okButton.onClick.AddListener(() =>
        {
            onConfirm?.Invoke(); // OK���ɓn���ꂽ�R�[���o�b�N�����s
            this.gameObject.SetActive(false); // �_�C�A���O�����
        });

        // �L�����Z���{�^���̏���
        cancelButton.onClick.AddListener(() =>
        {
            this.gameObject.SetActive(false); // �_�C�A���O�����
        });
    }

    // �㏑�����m�F����_�C�A���O��\��
    public void Show(System.Action confirmAction)
    {
        onConfirm = confirmAction; // OK���Ɏ��s����A�N�V������ݒ�
        this.gameObject.SetActive(true); // �_�C�A���O��\��
    }

    //// �㏑�����m�F����_�C�A���O��\�����āA���ʂ�Ԃ��񓯊����\�b�h
    //public async UniTask<bool> ShowAsync()
    //{
    //    this.gameObject.SetActive(true); // �_�C�A���O��\��

    //    bool isConfirmed = false;

    //    // OK�{�^���̃N���b�N��҂�
    //    okButton.onClick.AddListener(() =>
    //    {
    //        isConfirmed = true;
    //        this.gameObject.SetActive(false); // �_�C�A���O�����
    //    });

    //    // �L�����Z���{�^���̃N���b�N��҂�
    //    cancelButton.onClick.AddListener(() =>
    //    {
    //        isConfirmed = false;
    //        this.gameObject.SetActive(false); // �_�C�A���O�����
    //    });

    //    // �_�C�A���O������܂őҋ@
    //    await UniTask.WaitUntil(() => !this.gameObject.activeSelf);

    //    return isConfirmed; // OK�������ꂽ���ǂ�����Ԃ�
    //}
}
