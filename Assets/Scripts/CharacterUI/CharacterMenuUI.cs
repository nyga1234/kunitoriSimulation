using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class CharacterMenuUI : MonoBehaviour
{
    //[SerializeField] InflueneceManager influeneceManager;
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;

    [SerializeField] CharacterMenuCommandUI characterMenuCommandUI;

    [SerializeField] TextMeshProUGUI moneyText;

    [SerializeField] TextMeshProUGUI rankText;

    [SerializeField] Image lordImage;

    [SerializeField] Sprite charaUI;

    [SerializeField] Image character1Image;
    [SerializeField] Image character2Image;
    [SerializeField] Image character3Image;
    [SerializeField] Image character4Image;
    [SerializeField] Image character5Image;
    [SerializeField] Image character6Image;

    [SerializeField] TextMeshProUGUI charaNameText;
    [SerializeField] TextMeshProUGUI territorySumText;
    [SerializeField] TextMeshProUGUI goldSumText;
    [SerializeField] TextMeshProUGUI forceSumText;
    [SerializeField] TextMeshProUGUI soliderSumText;

    [SerializeField]
    private CommandOnClick commandClick;

    [SerializeField]
    private SelectButtonView informationButton;

    private void Start()
    {
        informationButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickInformation());
    }

    public void ShowCharacterMenuUI(CharacterController character, Influence influence)
    {
        gameObject.SetActive(true);
        characterMenuCommandUI.ShowCharacterMenuCommandUI();
        moneyText.text = "資金 " + character.gold.ToString();

        rankText.text = character.rank.ToString();

        //領主画像の設定
        lordImage.sprite = character.icon;
        //領主配下画像の設定
        for (int i = 1; i <= 6; i++)
        {
            Image characterImage = GetCharacterImage(i);
            characterImage.sprite = charaUI;
        }
        List<CharacterController> noPlayerCharacterList = character.influence.characterList.FindAll(x => !x.isPlayerCharacter);
        
        for (int i = 0; i < noPlayerCharacterList.Count; i++)
        {
            if (noPlayerCharacterList[i] != null)
            {
                // 各キャラクターに対応するImage変数を取得
                Image characterImage = GetCharacterImage(i + 1);

                // 対応するImageにスプライトを代入
                characterImage.sprite = noPlayerCharacterList[i].icon;
            }
        }

        territoryUIOnMouse.InfluenceCalcSum(influence);
        charaNameText.text = character.name;
        territorySumText.text = "[領地数] " + influence.territorySum.ToString();
        goldSumText.text = "[資金計]" + influence.goldSum.ToString();
        forceSumText.text = "[総戦力] " + influence.forceSum.ToString();
        soliderSumText.text = "[兵士数] " + influence.soliderSum.ToString();
    }

    // インデックスに応じたImage変数を取得するメソッド
    private Image GetCharacterImage(int index)
    {
        switch (index)
        {
            case 1: return character1Image;
            case 2: return character2Image;
            case 3: return character3Image;
            case 4: return character4Image;
            case 5: return character5Image;
            case 6: return character6Image;
            default: return null;
        }
    }

    public void HideCharacterMenuUI()
    {
        gameObject.SetActive(false);
    }
}
