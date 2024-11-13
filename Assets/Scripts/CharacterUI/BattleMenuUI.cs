using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class BattleMenuUI : MonoBehaviour
{
    [Header("Command Settings")]
    [SerializeField] private CommandOnClick commandClick;

    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;

    [SerializeField] BattleMenuCommandUI battleMenuCommandUI;

    [SerializeField] TextMeshProUGUI moneyText;

    [SerializeField] Image lordImage;

    [SerializeField] Sprite charaUI;

    [SerializeField] Image character1Image;
    [SerializeField] Image character2Image;
    [SerializeField] Image character3Image;
    [SerializeField] Image character4Image;
    [SerializeField] Image character5Image;
    [SerializeField] Image character6Image;

    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] TextMeshProUGUI charaNameText;
    [SerializeField] TextMeshProUGUI territorySumText;
    [SerializeField] TextMeshProUGUI goldSumText;
    [SerializeField] TextMeshProUGUI forceSumText;
    [SerializeField] TextMeshProUGUI soliderSumText;

    [System.Serializable]
    private class ButtonGroup
    {
        public SelectButtonView informationButton;
        public SelectButtonView attackButton;
        public SelectButtonView functionButton;
        public SelectButtonView endButton;
    }
    [SerializeField] private ButtonGroup buttons;

    private void Start()
    {
        buttons.informationButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickBattleInformation());
        buttons.attackButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickAttack());
        buttons.functionButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickFunction());
        buttons.endButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickBattleEnd());
    }

    public void ShowBattleMenuUI(CharacterController character, Influence influence)
    {
        gameObject.SetActive(true);
        battleMenuCommandUI.ShowBattleMenuCommandUI();

        moneyText.text = "資金 " + character.gold.ToString();

        if (influence != GameMain.instance.noneInfluence)
        {
            //領主画像の設定
            CharacterController LordCharacter = character.influence.characterList.Find(chara => chara.rank == Rank.領主);
            lordImage.sprite = LordCharacter.icon;

            //領主配下画像の設定
            List<CharacterController> noLordCharacterList = character.influence.characterList.FindAll(x => !x.isLord);
            for (int i = 0; i < noLordCharacterList.Count; i++)
            {
                if (noLordCharacterList[i] != null)
                {
                    // 各キャラクターに対応するImage変数を取得
                    Image characterImage = GetCharacterImage(i + 1);

                    // 対応するImageにスプライトを代入
                    characterImage.sprite = noLordCharacterList[i].icon;
                }
            }
        }
        else
        {
            lordImage.sprite = character.icon;

            character1Image.gameObject.SetActive(false);
            character2Image.gameObject.SetActive(false);
            character3Image.gameObject.SetActive(false);
            character4Image.gameObject.SetActive(false);
            character5Image.gameObject.SetActive(false);
            character6Image.gameObject.SetActive(false);
        }
        
        charaNameText.text = character.name;
        rankText.text = character.rank.ToString();

        territoryUIOnMouse.InfluenceCalcSum(influence);
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

    public void HideBattleMenuUI()
    {
        gameObject.SetActive(false);
    }
}
