using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] Text attackerMilitaryText;
    [SerializeField] Text defenderMilitaryText;

    [SerializeField] Image attackerIcon;
    [SerializeField] Image defenderIcon;

    [SerializeField] Image AttackerSoliderField, DefenderSoliderField;

    [SerializeField] GameObject AttackButton;
    [SerializeField] GameObject RetreatButton;

    [SerializeField] Text attackerRankText;
    [SerializeField] Text attackerNameText;
    [SerializeField] Text attackerForceText;
    [SerializeField] Text attackerInteliText;

    [SerializeField] Text defenderRankText;
    [SerializeField] Text defenderNameText;
    [SerializeField] Text defenderForceText;
    [SerializeField] Text defenderInteliText;

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

        AttackButton.transform.position = new Vector3(1065, 190, 0);
        RetreatButton.transform.position = new Vector3(855, 190, 0);

        if (defenderCharacter == GameManager.instance.playerCharacter)
        {
            AttackButton.transform.position = new Vector3(855, 190, 0);
            RetreatButton.transform.position = new Vector3(1065, 190, 0);
        }
        this.gameObject.SetActive(true);

        attackerMilitaryText.text = attackerCharacter.characterModel.name;
        defenderMilitaryText.text = defenderCharacter.characterModel.name;

        SetBattleFieldUI(territory);

        attackerIcon.sprite = attackerCharacter.characterModel.icon;
        defenderIcon.sprite = defenderCharacter.characterModel.icon;

        attackerRankText.text = attackerCharacter.characterModel.rank.ToString();
        attackerNameText.text = attackerCharacter.characterModel.name.ToString();
        attackerForceText.text = "[戦闘]" + attackerCharacter.characterModel.force.ToString();
        attackerInteliText.text = "[智謀]" + attackerCharacter.characterModel.inteli.ToString();

        defenderRankText.text = defenderCharacter.characterModel.rank.ToString();
        defenderNameText.text = defenderCharacter.characterModel.name.ToString();
        defenderForceText.text = "[戦闘]" + defenderCharacter.characterModel.force.ToString();
        defenderInteliText.text = "[智謀]" + defenderCharacter.characterModel.inteli.ToString();
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
