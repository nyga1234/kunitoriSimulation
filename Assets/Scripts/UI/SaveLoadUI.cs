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
    [SerializeField] private TextMeshProUGUI slotText1; // �X���b�g1�̃X�e�[�^�X�\���p
    [SerializeField] private TextMeshProUGUI slotText2; // �X���b�g2�̃X�e�[�^�X�\���p
    [SerializeField] private TextMeshProUGUI slotText3; // �X���b�g3�̃X�e�[�^�X�\���p

    private bool isSaving = true; // �Z�[�u�����[�h���𔻒f����t���O
    [SerializeField] private ConfirmOverwriteUI confirmOverwriteUI;

    private void Start()
    {
        // UI�̏�����
        UpdateSlotUI();

        //����i�}�X�N�{�^���j
        maskButton.onClick.AddListener(() => this.gameObject.SetActive(false));

        // �X���b�g�{�^���̃C�x���g�o�^
        slotButton1.onClick.AddListener(() => OnSlotButtonClick(1));
        slotButton2.onClick.AddListener(() => OnSlotButtonClick(2));
        slotButton3.onClick.AddListener(() => OnSlotButtonClick(3));

        //����i����{�^���j
        closeButton.onClick.AddListener(() => this.gameObject.SetActive(false));
    }

    private void OnSlotButtonClick(int slot)
    {
        if (isSaving)
        {
            if (SaveLoadManager.HasSaveData(slot))
            {
                // �㏑���m�FUI��\��
                confirmOverwriteUI.Show(() =>
                {
                    GameManager.instance.SaveGame(slot); // �㏑���Z�[�u������n��
                });
            }
            else
            {
                // �Z�[�u����
                GameManager.instance.SaveGame(slot);
            }
        }
        else
        {
            // ���[�h����
            GameManager.instance.LoadGame(slot);
        }

        // UI���X�V���ĕ\������
        UpdateSlotUI();
    }

    private void UpdateSlotUI()
    {
        // �e�X���b�g�ɃZ�[�u�f�[�^�����邩�m�F���ĕ\�����X�V
        slotText1.text = SaveLoadManager.HasSaveData(1) ? GetSaveText() : "�󂫃X���b�g";
        slotText2.text = SaveLoadManager.HasSaveData(2) ? GetSaveText() : "�󂫃X���b�g";
        slotText3.text = SaveLoadManager.HasSaveData(3) ? GetSaveText() : "�󂫃X���b�g";
    }

    private string GetSaveText()
    {
        string turnCount = GameManager.instance.turnCount.ToString() + "��";
        string rank = GameManager.instance.playerCharacter.characterModel.rank.ToString();
        string name = GameManager.instance.playerCharacter.characterModel.name;
        return turnCount + " : " + rank + " " + name; 
    }

    // �Z�[�u�����[�h���̃��[�h���Z�b�g
    public void SetMode(bool saving)
    {
        isSaving = saving;
        UpdateSlotUI();
    }
}
