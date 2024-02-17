using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenuCommandUI : MonoBehaviour
{
    [SerializeField] AppointmentCommandUI appointmentCommandUI;
    [SerializeField] SearchCommandUI searchCommandUI;
    [SerializeField] BanishmentCommandUI banishmentCommandUI;

    public void ShowCharacterMenuCommandUI()
    {
        gameObject.SetActive(true);
        appointmentCommandUI.ShowAppointmentCommandUI();
        searchCommandUI.ShowSearchCommandUI();
        banishmentCommandUI.ShowBanishmentCommandUI();
    }
}
