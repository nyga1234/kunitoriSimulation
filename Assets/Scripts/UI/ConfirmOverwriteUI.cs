using UnityEngine;
using UnityEngine.UI;

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
}
