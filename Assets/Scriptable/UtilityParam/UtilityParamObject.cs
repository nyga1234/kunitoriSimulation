using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Utility Param", fileName = "UtilityParam")]
public class UtilityParamObject : ScriptableObject
{
    [Header("")]
    public List<Influence> influenceList;
    public List<CharacterController> characterList;
    public List<SoldierController> soldierList;
    public List<SoldierController> soldierList1;
    public List<SoldierController> soldierList2;
    public List<SoldierController> soldierList3;
    public List<SoldierController> soldierList4;

    [SerializeField] private bool? isConfirm;
    [SerializeField] private bool? isDialogue;
    [SerializeField] private string confirmText;
    [SerializeField] private string dialogueText;

    public bool? IsConfirm { get => isConfirm; set => isConfirm = value; }
    public bool? IsDialogue { get => isDialogue; set => isDialogue = value; }
    public string ConfirmText { get => confirmText; set => confirmText = value; }
    public string DialogueText { get => dialogueText; set => dialogueText = value; }
}
