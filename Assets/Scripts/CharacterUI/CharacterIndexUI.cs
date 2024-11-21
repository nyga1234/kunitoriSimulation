using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CharacterIndexUI : MonoBehaviour
{
    [SerializeField] AttackedCharacterUI attackedCharacterUI;
    [SerializeField] AbandonUI abandonUI;
    [SerializeField] LandformInformationUI landformInformationUI;

    public List<CharacterController> indexCharacterList;

    [SerializeField] private GameObject characterUI;
    [SerializeField] Transform CharacterUIField;

    [SerializeField] CharacterUIOnClick charaOnClick;

    private List<GameObject> slotInstances = new List<GameObject>(); // ���I�ɐ������ꂽ�X���b�g�̃C���X�^���X

    public void ShowCharacterIndexUI(List<CharacterController> characterList)
    {
        indexCharacterList = characterList;
        this.gameObject.SetActive(true);

        for (int i = 0; i < characterList.Count; i++)
        {
            // �X���b�g�̃C���X�^���X�𐶐�
            GameObject slotInstance = Instantiate(characterUI, CharacterUIField);
            slotInstances.Add(slotInstance);
            slotInstance.gameObject.SetActive(true);

            // �{�^���C�x���g��ݒ�
            var character = characterList[i];
            slotInstance.GetComponent<Button>().OnClickAsObservable().Subscribe(_ =>
            {
                OnSlotButtonClick(character);
            });

            // �L�����N�^�[���̕\��
            CharacterUI characterSlot = slotInstance.GetComponent<CharacterUI>();
            characterSlot.ShowCharacterUI(characterList[i]);

            SelectCharacterUI onMouseComponent = characterSlot.GetComponent<SelectCharacterUI>();
            onMouseComponent.SetCharacterController(characterList[i]);
        }
    }

    public void HideCharacterIndexUI()
    {
        this.gameObject.SetActive(false);
        for (int i = 0; i < slotInstances.Count; i++)
        {
            Destroy(slotInstances[i]); // �C���X�^���X���폜
        }
        attackedCharacterUI.HideCharacterUI();
        abandonUI.HideAbandonUI();
        landformInformationUI.HideLandformInformationUI();
    }

    private void OnSlotButtonClick(CharacterController character)
    {
        charaOnClick.OnPointerClickCharacter(character);
    }
}
