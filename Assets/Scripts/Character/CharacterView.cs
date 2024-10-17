using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterView : MonoBehaviour
{
    [SerializeField] Text Character1NameText;//ñºëO
    [SerializeField] Text Character1ForceText;//êÌì¨
    [SerializeField] Text Character1InteliText;//íqñd
    [SerializeField] Text Character1TactText;//éËòr
    [SerializeField] Text Character1RankText;//êgï™
    [SerializeField] Text Character1FameText;//ñºê∫
    [SerializeField] Text Character1AmbitionText;//ñÏêS
    [SerializeField] Text Character1LoyaltyText;//íâêΩ
    [SerializeField] Text Character1SalaryText;//ããóø%

    public void ShowCharacterUI(CharacterController character)
    {
        gameObject.SetActive(true);
        Character1NameText.text = character.name;
        Character1ForceText.text = character.force.ToString();
        Character1InteliText.text = character.inteli.ToString();
        Character1TactText.text = character.tact.ToString();
        Character1RankText.text = character.rank.ToString();
        Character1FameText.text = character.fame.ToString();
        Character1AmbitionText.text = character.ambition.ToString();
        Character1LoyaltyText.text = character.ambition.ToString();
        Character1SalaryText.text = character.ambition.ToString();
    }

    public void HideCharacterUI()
    {
        gameObject.SetActive(false);
    }
}
