using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI attackerMilitaryText;
    [SerializeField] TextMeshProUGUI defenderMilitaryText;

    [SerializeField] Image attackerIcon;
    [SerializeField] Image defenderIcon;

    [SerializeField] Image AttackerSoliderField, DefenderSoliderField;

    [SerializeField] GameObject AttackButton;
    [SerializeField] GameObject RetreatButton;

    [SerializeField] Transform attackButtonTransform;
    [SerializeField] Transform defenceButtonTransform;

    [SerializeField] TextMeshProUGUI attackerRankText;
    [SerializeField] TextMeshProUGUI attackerNameText;
    [SerializeField] TextMeshProUGUI attackerForceText;
    [SerializeField] TextMeshProUGUI attackerInteliText;

    [SerializeField] TextMeshProUGUI defenderRankText;
    [SerializeField] TextMeshProUGUI defenderNameText;
    [SerializeField] TextMeshProUGUI defenderForceText;
    [SerializeField] TextMeshProUGUI defenderInteliText;

    Vector3 attackButtonPosition;
    Vector3 retreatButtonPosition;

    //private void Start()
    //{
    //    // 親要素の座標も考慮して設定
    //    attackButtonPosition = transform.TransformPoint(AttackButton.transform.localPosition);
    //    retreatButtonPosition = transform.TransformPoint(RetreatButton.transform.localPosition);
    //    Debug.Log(attackButtonPosition);
    //    Debug.Log(retreatButtonPosition);
    //}

    public void ShowBattleUI(CharacterController attackerCharacter, CharacterController defenderCharacter, Territory territory)
    {
        SoundManager.instance.SwitchBattleBGM();
        TitleFieldUI.instance.titleFieldText.text = "      攻撃ボタンクリックで戦闘";
        TitleFieldUI.instance.HideTitleSubText();

        AttackButton.transform.position = attackButtonTransform.position;
        RetreatButton.transform.position = defenceButtonTransform.position;

        if (defenderCharacter == GameMain.instance.playerCharacter)
        {
            AttackButton.transform.position = defenceButtonTransform.position;
            RetreatButton.transform.position = attackButtonTransform.position;
        }
        this.gameObject.SetActive(true);

        attackerMilitaryText.text = attackerCharacter.name;
        defenderMilitaryText.text = defenderCharacter.name;

        SetBattleFieldUI(territory);

        attackerIcon.sprite = attackerCharacter.icon;
        defenderIcon.sprite = defenderCharacter.icon;

        attackerRankText.text = attackerCharacter.rank.ToString();
        attackerNameText.text = attackerCharacter.name.ToString();
        attackerForceText.text = "[戦闘]" + attackerCharacter.force.ToString();
        attackerInteliText.text = "[智謀]" + attackerCharacter.inteli.ToString();

        defenderRankText.text = defenderCharacter.rank.ToString();
        defenderNameText.text = defenderCharacter.name.ToString();
        defenderForceText.text = "[戦闘]" + defenderCharacter.force.ToString();
        defenderInteliText.text = "[智謀]" + defenderCharacter.inteli.ToString();
    }

    public void HideBattleUI()
    {
        SoundManager.instance.SwitchMainBGM();
        gameObject.SetActive(false);   
    }

    void SetBattleFieldUI(Territory territory)
    {
        //地形に対応する画像ファイル名
        string desert = "Landforms/desert";
        string wilderness = "Landforms/wilderness";
        string plain = "Landforms/plain";
        string forest = "Landforms/forest";
        string fort = "Landforms/fort";

        // Resources.Loadを使用してスプライトを読み込む
        Sprite desertSprite = Resources.Load<Sprite>(desert);
        Sprite wildernessSprite = Resources.Load<Sprite>(wilderness);
        Sprite plainSprite = Resources.Load<Sprite>(plain);
        Sprite forestSprite = Resources.Load<Sprite>(forest);
        Sprite fortSprite = Resources.Load<Sprite>(fort);

        switch (territory.attackTerritoryType)
        {
            case Territory.AttackTerritoryType.desert:
                AttackerSoliderField.sprite = desertSprite;
                break;
            case Territory.AttackTerritoryType.wilderness:
                AttackerSoliderField.sprite = wildernessSprite;
                break;
            case Territory.AttackTerritoryType.plain:
                AttackerSoliderField.sprite = plainSprite;
                break;
            case Territory.AttackTerritoryType.forest:
                AttackerSoliderField.sprite = forestSprite;
                break;
            case Territory.AttackTerritoryType.fort:
                AttackerSoliderField.sprite = fortSprite;
                break;
        }

        switch (territory.defenceTerritoryType)
        {
            case Territory.DefenceTerritoryType.desert:
                DefenderSoliderField.sprite = desertSprite;
                break;
            case Territory.DefenceTerritoryType.wilderness:
                DefenderSoliderField.sprite = wildernessSprite;
                break;
            case Territory.DefenceTerritoryType.plain:
                DefenderSoliderField.sprite = plainSprite;
                break;
            case Territory.DefenceTerritoryType.forest:
                DefenderSoliderField.sprite = forestSprite;
                break;
            case Territory.DefenceTerritoryType.fort:
                DefenderSoliderField.sprite = fortSprite;
                break;
        }

    }
}
