using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UniRx;
using UnityEngine.UI;

public class SaveLoadUI : MonoBehaviour
{
    [SerializeField] private Button maskButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private Transform slotParent;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] GameObject imageSoliderPrefab;
    [SerializeField] private UtilityParamObject varParam;

    private List<GameObject> slotInstances = new List<GameObject>(); // ���I�ɐ������ꂽ�X���b�g�̃C���X�^���X

    private int maxSlots = 3; // �X���b�g�̐��i�ύX�\�j

    private void Start()
    {
        //confirmUI���擾

        GenerateSlots();// �X���b�g�̐���

        maskButton.onClick.AddListener(() => this.gameObject.SetActive(false));
        closeButton.onClick.AsObservable().Subscribe(_ => { UniTask uniTask = OnPressClose(); });

        UpdateSlotUI();

        SceneController.instance.Stack.Add("UISaveLoad");
    }

    private void OnDestroy()
    {
        SceneController.instance.Stack.Remove("UISaveLoad");
    }

    private void GenerateSlots()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            GameObject slotInstance = Instantiate(slotPrefab, slotParent); // �X���b�gPrefab�𐶐�
            slotInstances.Add(slotInstance); // �C���X�^���X�����X�g�ɒǉ�
            SaveSlotView saveSlotView = slotInstance.GetComponent<SaveSlotView>();
            int capturedIndex = i;// ���[�J���ϐ����g�p���ăX���b�g�ԍ����L���v�`��
            saveSlotView.GetComponent<Button>().OnClickAsObservable().Subscribe(_ =>
            {
                OnSlotButtonClick(capturedIndex);
            });
        }

        closeButton.transform.SetAsLastSibling();
    }

    private async void  OnSlotButtonClick(int slot)
    {
        // �Z�[�u����
        if (SaveLoadManager.IsSaving)
        {
            if (!SaveLoadManager.HasSaveData(slot))
            {
                GameManager.instance.SaveGame(slot);
                UpdateSlotUI();
            }
            else
            {
                await SceneController.LoadAsync("UIConfirm");
                varParam.ConfirmText = "�Z�[�u�f�[�^���㏑�����܂����H";
                // OK�܂���Cancel�{�^�����N���b�N�����̂�ҋ@
                await UniTask.WaitUntil(() => varParam.IsConfirm.HasValue);

                if (varParam.IsConfirm == true)
                {
                    GameManager.instance.SaveGame(slot);
                    UpdateSlotUI();
                }
            }
        }
        else // ���[�h����
        {
            if (SaveLoadManager.HasSaveData(slot))
            {
                await GameManager.instance.ChangeScene("Title", "GameMain");
                SaveLoadManager.SelectSlot = slot;
            }
            else
            {
                await SceneController.LoadAsync("UIDialogue");
                varParam.DialogueText = "�I�������X���b�g�ɂ̓Z�[�u�f�[�^�����݂��܂���";
                return;
            }
        }
    }

    public void UpdateSlotUI()
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
                var playerCharData = gameState.characters.FirstOrDefault(c => c.isPlayerCharacter);

                saveSlotView.SlotText.text = GetSaveText(gameState); // �Z�[�u�e�L�X�g���X�V
                saveSlotView.PlayerImage.sprite = playerCharData.icon; //�v���C���[�A�C�R���̍X�V
                ShowSoldierList(playerCharData.soliders, saveSlotView.SoldierListField);
            }
            else
            {
                saveSlotView.SlotText.text = "�󂫃X���b�g"; // �Z�[�u�f�[�^�������ꍇ
                saveSlotView.PlayerImage.sprite = null;
            }
        }
    }

    // GameState�Ɋ�Â��ĕ\���e�L�X�g���擾����
    private string GetSaveText(GameState gameState)
    {
        // �v���C���[�L�����N�^�[�̏����擾
        var playerCharData = gameState.characters.FirstOrDefault(c => c.isPlayerCharacter);
        
        string turnCount = gameState.turnCount.ToString() + "��";
        string rank = playerCharData != null ? playerCharData.rank.ToString() :  null;
        string name = playerCharData != null ? playerCharData.name : null;
        return $"{turnCount} : {rank} {name}";
    }

    void ShowSoldierList(List<SoliderData> soldierList, Transform field)
    {
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }

        foreach (var soldierData in soldierList)
        {
            var soldierObject = Instantiate(imageSoliderPrefab, field);
            soldierObject.GetComponent<SoldierImageView>().ShowSoldierImage(soldierData.icon, true);
        }
    }

    public async UniTask OnPressClose() => await SceneController.UnloadAsync("UISaveLoad");
}