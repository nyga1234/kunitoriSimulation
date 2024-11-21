using Cysharp.Threading.Tasks.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterIndexUI : MonoBehaviour
{
    [SerializeField] AttackedCharacterUI attackedCharacterUI;
    [SerializeField] AbandonUI abandonUI;
    [SerializeField] LandformInformationUI landformInformationUI;

    public List<CharacterController> indexCharacterList;

    [SerializeField] private GameObject characterUI;
    [SerializeField] Transform CharacterUIField;

    private List<GameObject> slotInstances = new List<GameObject>(); // ���I�ɐ������ꂽ�X���b�g�̃C���X�^���X

    public void ShowCharacterIndexUI(List<CharacterController> characterList)
    {
        indexCharacterList = characterList;
        this.gameObject.SetActive(true);

        for (int i = 0; i < characterList.Count; i++)
        {
            GameObject slotInstance = Instantiate(characterUI, CharacterUIField); // �X���b�gPrefab�𐶐�
            slotInstances.Add(slotInstance); // �C���X�^���X�����X�g�ɒǉ�
            slotInstance.gameObject.SetActive(true);
            CharacterUI characterSlot = slotInstance.GetComponent<CharacterUI>();
            characterSlot.ShowCharacterUI(characterList[i]);
            SelectCharacterUI onMouseComponent = characterSlot.GetComponent<SelectCharacterUI>();
            CharacterUIOnClick onClickComponent = characterSlot.GetComponent<CharacterUIOnClick>();
            if (onMouseComponent != null || onClickComponent != null)
            {
                onMouseComponent.SetCharacterController(characterList[i]);
                onClickComponent.SetCharacterController(characterList[i]);
            }
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
}
