using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class SaveLoadUI : MonoBehaviour
{
    [SerializeField] private Button maskButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private GameObject slotPrefab;  // �X���b�g��Prefab
    [SerializeField] private Transform slotParent;   // �X���b�g��z�u����e�I�u�W�F�N�g
    [SerializeField] private ConfirmOverwriteUI confirmOverwriteUI;
    [SerializeField] GameObject imageSoliderPrefab;

    private bool isSaving = true; // �Z�[�u�����[�h���𔻒f����t���O
    private List<GameObject> slotInstances = new List<GameObject>(); // ���I�ɐ������ꂽ�X���b�g�̃C���X�^���X

    private int maxSlots = 3; // �X���b�g�̐��i�ύX�\�j

    private void Start()
    {
        // �X���b�g�̐���
        GenerateSlots();

        //����i�}�X�N�{�^���j
        maskButton.onClick.AddListener(() => this.gameObject.SetActive(false));

        //����i����{�^���j
        closeButton.onClick.AddListener(() => this.gameObject.SetActive(false));

        // UI�̏�����
        UpdateSlotUI();
    }

    // �X���b�g���ɉ�����Prefab�𐶐�
    private void GenerateSlots()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            GameObject slotInstance = Instantiate(slotPrefab, slotParent); // �X���b�gPrefab�𐶐�
            slotInstances.Add(slotInstance); // �C���X�^���X�����X�g�ɒǉ�

            SaveSlotView saveSlotView = slotInstance.GetComponent<SaveSlotView>();

            // ���[�J���ϐ����g�p���ăX���b�g�ԍ����L���v�`��
            int capturedIndex = i;

            // �{�^���ɃN���b�N���X�i�[��ǉ�
            saveSlotView.GetComponent<Button>().onClick.AddListener(() => OnSlotButtonClick(capturedIndex));
        }
    }


    private void UpdateSlotUI()
    {
        Debug.Log("UpdateSlotUI");
        for (int i = 0; i < maxSlots; i++)
        {
            SaveSlotView saveSlotView = slotInstances[i].GetComponent<SaveSlotView>();

            // �Z�[�u�f�[�^�����݂���ꍇ�A���̃f�[�^��ǂݍ���
            if (SaveLoadManager.HasSaveData(i))
            {
                // �Z�[�u�f�[�^�����[�h
                GameState gameState = SaveLoadManager.LoadGame(i);

                // �X���b�g�̏����X�V
                saveSlotView.SlotText.text = GetSaveText(gameState); // �ۑ����ꂽ����\��

                var playerCharData = gameState.characters.FirstOrDefault(c => c.isPlayerCharacter);
                saveSlotView.PlayerImage.sprite = playerCharData.icon;

                //saveSlotView.PlayerImage.sprite = gameState.characters
                //    .FirstOrDefault(c => c.isPlayerCharacter)?.icon; // �v���C���[�L�����N�^�[�̃A�C�R��

                // �v���C���[�L�����N�^�[�̕��m���X�g��\��

                if (playerCharData != null)
                {
                    ShowSoldierList(playerCharData.soliders, saveSlotView.SoldierListField);
                }
                //ShowSoliderList(GameMain.instance.playerCharacter.soliderList, saveSlotView.SoldierListField);
            }
            else
            {
                saveSlotView.SlotText.text = "�󂫃X���b�g"; // �Z�[�u�f�[�^�������ꍇ
                saveSlotView.PlayerImage.sprite = null;
                //ShowSoldierList(new List<SoliderController>(), saveSlotView.SoldierListField);
            }
        }
    }

    // GameState�Ɋ�Â��ĕ\���e�L�X�g���擾����
    private string GetSaveText(GameState gameState)
    {
        string turnCount = gameState.turnCount.ToString() + "��";
        // �v���C���[�L�����N�^�[�̏����擾
        var playerCharData = gameState.characters.FirstOrDefault(c => c.isPlayerCharacter);
        string rank = playerCharData != null ? playerCharData.rank.ToString() :  null;
        string name = playerCharData != null ? playerCharData.name : null;

        return $"{turnCount} : {rank} {name}";
    }

    // �Z�[�u�����[�h���̃��[�h���Z�b�g
    public void SetMode(bool saving)
    {
        isSaving = saving;
        //UpdateSlotUI();
    }

    //void ShowSoliderList(List<SoliderController> soliderList, Transform field)
    //{
    //    // ���ݕ\������Ă��镺�m���폜
    //    foreach (Transform child in field)
    //    {
    //        Destroy(child.gameObject);
    //    }

    //    // �V�������m���X�g���쐬
    //    foreach (SoliderController solider in soliderList)
    //    {
    //        SoliderController battleSolider = Instantiate(imageSoliderPrefab, field);
    //        battleSolider.ShowBattleDetailSoliderUI(solider, true);
    //    }
    //}

    void ShowSoldierList(List<SoliderData> soldierList, Transform field)
    {
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }

        foreach (var soldierData in soldierList)
        {
            var soldierObject = Instantiate(imageSoliderPrefab, field);
            soldierObject.GetComponent<ShowSaveViewSoldier>().ShowSoldierUI(soldierData.icon);
        }
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
}
