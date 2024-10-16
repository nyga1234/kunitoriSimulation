using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AppointmentCommandUI : MonoBehaviour
{
    private Color originalColor; // 元の背景色を保持する変数
    [SerializeField] TextMeshProUGUI appointmentText;
    public GameMain gameManager;

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // 元の背景色を保持
            originalColor = image.color;
        }
    }

    public void ShowAppointmentCommandUI()
    {
        ChangeBackgroundColor(originalColor);
        this.gameObject.SetActive(true);
    }

    //背景色を変更
    private void ChangeBackgroundColor(Color color)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }
}
