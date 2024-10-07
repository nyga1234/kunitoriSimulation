using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppointmentCommandUI : MonoBehaviour
{
    private Color originalColor; // Œ³‚Ì”wŒiF‚ğ•Û‚·‚é•Ï”
    [SerializeField] TextMeshProUGUI appointmentText;
    public GameMain gameManager;

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // Œ³‚Ì”wŒiF‚ğ•Û
            originalColor = image.color;
        }
    }

    public void ShowAppointmentCommandUI()
    {
        ChangeBackgroundColor(originalColor);
        this.gameObject.SetActive(true);
    }

    //”wŒiF‚ğ•ÏX
    private void ChangeBackgroundColor(Color color)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }
}
