using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppointmentCommandUI : MonoBehaviour
{
    private Color originalColor; // ���̔w�i�F��ێ�����ϐ�
    [SerializeField] TextMeshProUGUI appointmentText;
    public GameMain gameManager;

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // ���̔w�i�F��ێ�
            originalColor = image.color;
        }
    }

    public void ShowAppointmentCommandUI()
    {
        ChangeBackgroundColor(originalColor);
        this.gameObject.SetActive(true);
    }

    //�w�i�F��ύX
    private void ChangeBackgroundColor(Color color)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }
}
