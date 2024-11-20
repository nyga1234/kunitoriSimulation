using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackedCharacterUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI CharacterNameText;//ñºëO
    [SerializeField] TextMeshProUGUI CharacterForceText;//êÌì¨
    [SerializeField] TextMeshProUGUI CharacterInteliText;//íqñd
    [SerializeField] TextMeshProUGUI CharacterTactText;//éËòr
    [SerializeField] TextMeshProUGUI CharacterRankText;//êgï™
    [SerializeField] TextMeshProUGUI CharacterFameText;//ñºê∫
    [SerializeField] TextMeshProUGUI CharacterAmbitionText;//ñÏêS
    [SerializeField] TextMeshProUGUI CharacterLoyaltyText;//íâêΩ
    [SerializeField] TextMeshProUGUI CharacterSalaryText;//ããóø%

    private Color originalColor; // å≥ÇÃîwåiêFÇï€éùÇ∑ÇÈïœêî

    void Start()
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            // å≥ÇÃîwåiêFÇï€éù
            originalColor = image.color;
        }
    }

    public void ShowAttackedCharacterUI(CharacterController character)
    {
        this.gameObject.SetActive(true);

        ChangeTextColorWhite();

        CharacterNameText.text = character.name;
        CharacterForceText.text = character.force.ToString();
        CharacterInteliText.text = character.inteli.ToString();
        CharacterTactText.text = character.tact.ToString();
        //CharacterRankText.text = character.rank.ToString();
        CharacterFameText.text = character.fame.ToString();
        CharacterAmbitionText.text = character.ambition.ToString();
        if (character.rank == Rank.óÃéÂ || character.influence.influenceName == "NoneInfluence")
        {
            CharacterLoyaltyText.text = "--";
        }
        else
        {
            CharacterLoyaltyText.text = character.loyalty.ToString();
        }
        if (character.influence.influenceName == "NoneInfluence")
        {
            CharacterSalaryText.text = "--";
        }
        else
        {
            CharacterSalaryText.text = character.salary.ToString();
        }

        //CharacterUIOnMouse onMouseComponent = this.gameObject.GetComponent<CharacterUIOnMouse>();
        SelectCharacterUI onMouseComponent = this.gameObject.GetComponent<SelectCharacterUI>();
        if (onMouseComponent != null)
        {
            onMouseComponent.SetCharacterController(character);
        }
    }

    public void HideCharacterUI()
    {
        ChangeBackgroundColor(originalColor);
        gameObject.SetActive(false);
    }

    //îwåiêFÇïœçX
    private void ChangeBackgroundColor(Color color)
    {
        Image image = GetComponent<Image>();
        if (image != null)
        {
            image.color = color;
        }
    }

    private void ChangeTextColorWhite()
    {
        CharacterNameText.color = Color.white;
        CharacterForceText.color = Color.white;
        CharacterInteliText.color = Color.white;
        CharacterTactText.color = Color.white;
        CharacterRankText.color = Color.white;
        CharacterFameText.color = Color.white;
        CharacterAmbitionText.color = Color.white;
        CharacterLoyaltyText.color = Color.white;
        CharacterSalaryText.color = Color.white;
    }
}
