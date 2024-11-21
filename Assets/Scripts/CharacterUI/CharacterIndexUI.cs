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

    private List<GameObject> slotInstances = new List<GameObject>(); // 動的に生成されたスロットのインスタンス

    public void ShowCharacterIndexUI(List<CharacterController> characterList)
    {
        indexCharacterList = characterList;
        this.gameObject.SetActive(true);

        for (int i = 0; i < characterList.Count; i++)
        {
            GameObject slotInstance = Instantiate(characterUI, CharacterUIField); // スロットPrefabを生成
            slotInstances.Add(slotInstance); // インスタンスをリストに追加
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
            Destroy(slotInstances[i]); // インスタンスを削除
        }
        attackedCharacterUI.HideCharacterUI();
        abandonUI.HideAbandonUI();
        landformInformationUI.HideLandformInformationUI();
    }
}
