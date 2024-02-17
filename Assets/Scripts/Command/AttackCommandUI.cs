using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackCommandUI : MonoBehaviour
{
    private Color originalUIColor; // ���̔w�i�F��ێ�����ϐ�

    [SerializeField] Text attackText;
    public GameManager gameManager;

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // ���̔w�i�F��ێ�
            originalUIColor = image.color;
        }
    }

    public void ShowAttackCommandUI()
    {
        ChangeBackgroundColor(originalUIColor);
        attackText.color = Color.white; // ���F�ɕύX
        this.gameObject.SetActive(true);

        if (GameManager.instance.playerCharacter.characterModel.gold < 3)
        {
            attackText.color = new Color32(122, 122, 122, 255);
        }

        if (GameManager.instance.playerCharacter.characterModel.isLord == false && GameManager.instance.playerCharacter.characterModel.isAttackable == false)
        {
            attackText.color = new Color32(122, 122, 122, 255);
        }
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
