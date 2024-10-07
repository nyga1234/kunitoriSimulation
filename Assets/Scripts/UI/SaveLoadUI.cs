using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
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
    [SerializeField] Sprite charaUI;
    [SerializeField] Image playerImage1;
    [SerializeField] Image playerImage2;
    [SerializeField] Image playerImage3;
    [SerializeField] SoliderController imageSoliderPrefab;
    [SerializeField] Transform SoliderListField1;
    [SerializeField] Transform SoliderListField2;
    [SerializeField] Transform SoliderListField3;

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
                    GameMain.instance.SaveGame(slot); //Show()�̈����ɏ㏑���Z�[�u������n��
                });
            }
            else
            {
                // �Z�[�u����
                GameMain.instance.SaveGame(slot);
            }
        }
        else
        {
            // ���[�h����
            GameMain.instance.LoadGame(slot);
        }

        // UI���X�V���ĕ\������
        UpdateSlotUI();
    }

    private void UpdateSlotUI()
    {
        playerImage1.sprite = SaveLoadManager.HasSaveData(1) ?
            GameMain.instance.playerCharacter.characterModel.icon : charaUI;

        playerImage2.sprite = SaveLoadManager.HasSaveData(2) ?
            GameMain.instance.playerCharacter.characterModel.icon : charaUI;

        playerImage3.sprite = SaveLoadManager.HasSaveData(3) ?
            GameMain.instance.playerCharacter.characterModel.icon : charaUI;

        if (SaveLoadManager.HasSaveData(1))
        {
            ShowSoliderList(GameMain.instance.playerCharacter.soliderList, SoliderListField1);
        }
        if (SaveLoadManager.HasSaveData(2))
        {
            ShowSoliderList(GameMain.instance.playerCharacter.soliderList, SoliderListField2);
        }
        if (SaveLoadManager.HasSaveData(3))
        {
            ShowSoliderList(GameMain.instance.playerCharacter.soliderList, SoliderListField3);
        }
        // �e�X���b�g�ɃZ�[�u�f�[�^�����邩�m�F���ĕ\�����X�V
        slotText1.text = SaveLoadManager.HasSaveData(1) ? GetSaveText() : "�󂫃X���b�g";
        slotText2.text = SaveLoadManager.HasSaveData(2) ? GetSaveText() : "�󂫃X���b�g";
        slotText3.text = SaveLoadManager.HasSaveData(3) ? GetSaveText() : "�󂫃X���b�g";
    }

    private string GetSaveText()
    {
        string turnCount = GameMain.instance.turnCount.ToString() + "��";
        string rank = GameMain.instance.playerCharacter.characterModel.rank.ToString();
        string name = GameMain.instance.playerCharacter.characterModel.name;
        return turnCount + " : " + rank + " " + name; 
    }

    // �Z�[�u�����[�h���̃��[�h���Z�b�g
    public void SetMode(bool saving)
    {
        isSaving = saving;
        //UpdateSlotUI();
    }

    void ShowSoliderList(List<SoliderController> soliderList, Transform field)
    {
        // ���ݕ\������Ă��镺�m���폜
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }

        // �V�������m���X�g���쐬
        foreach (SoliderController solider in soliderList)
        {
            SoliderController battleSolider = Instantiate(imageSoliderPrefab, field);
            battleSolider.ShowBattleDetailSoliderUI(solider, true);
        }
    }
}
