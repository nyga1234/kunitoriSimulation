using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class BattleDetailUI : MonoBehaviour
{
    //[SerializeField] InflueneceManager influenceManager;
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;

    [SerializeField] Image attackerCharaIcon;
    [SerializeField] Image defenderCharaIcon;
    [SerializeField] Text attackerCharaInfoText;
    [SerializeField] Text defenderCharaInfoText;
    [SerializeField] Text attackerForceText;
    [SerializeField] Text defenderForceText;

    [SerializeField] Image attackTerritoryTypeImage;
    [SerializeField] Image defenceTerritoryTypeImage;

    [SerializeField] Transform AttackerSoliderListField;
    [SerializeField] Transform DefenderSoliderListField;
    [SerializeField] SoliderController personalSolidefPrefab;

    int attackerSoliderHPSum;
    int defenderSoliderHPSum;

    public void ShowBattleDetailUI(CharacterController attackCharacter, CharacterController defenceCharacter)
    {
        gameObject.SetActive(true);

        attackerCharaIcon.sprite = attackCharacter.characterModel.icon;
        defenderCharaIcon.sprite = defenceCharacter.characterModel.icon;

        CharacterController attackerLordCharacter = attackCharacter.influence.characterList.Find(chara => chara.characterModel.isLord);
        CharacterController defenderLordCharacter = defenceCharacter.influence.characterList.Find(chara => chara.characterModel.isLord);

        attackerCharaInfoText.text = attackerLordCharacter.characterModel.name + "�R" + " " + attackCharacter.characterModel.name + "��";
        defenderCharaInfoText.text = defenderLordCharacter.characterModel.name + "�R" + " " + defenceCharacter.characterModel.name + "��";

        // ������
        attackerSoliderHPSum = 0;
        defenderSoliderHPSum = 0;

        foreach (SoliderController solider in attackCharacter.soliderList)
        {
            attackerSoliderHPSum += solider.soliderModel.hp;
        }
        foreach (SoliderController solider in defenceCharacter.soliderList)
        {
            defenderSoliderHPSum += solider.soliderModel.hp;
        }

        attackerForceText.text = attackerSoliderHPSum.ToString();
        defenderForceText.text = defenderSoliderHPSum.ToString();

        ShowLandformInformationUI();

        ShowSoliderList(attackCharacter.soliderList, AttackerSoliderListField);
        ShowSoliderList(defenceCharacter.soliderList, DefenderSoliderListField);
    }

    void ShowSoliderList(List<SoliderController> soliderList, Transform field)
    {
        // ���ݕ\������Ă��镺�m���폜
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }

        // �V�������m���X�g���쐬
        foreach (SoliderController solider in soliderList)
        {
            ShowSolider(solider, field);
        }
    }

    void ShowSolider(SoliderController solider, Transform field)
    {
        SoliderController battleSolider = Instantiate(personalSolidefPrefab, field, false);
        battleSolider.ShowBattleSoliderUI(solider);
    }

    public void ShowLandformInformationUI()
    {
        this.gameObject.SetActive(true);

        SetBattleFieldUI(territoryUIOnMouse.territory);
    }

    void SetBattleFieldUI(Territory territory)
    {
        //�n�`�ɑΉ�����摜�t�@�C����
        string desert = "Landforms/desert";
        string wilderness = "Landforms/wilderness";
        string plain = "Landforms/plain";
        string forest = "Landforms/forest";
        string fort = "Landforms/fort";

        // Resources.Load���g�p���ăX�v���C�g��ǂݍ���
        Sprite desertSprite = Resources.Load<Sprite>(desert);
        Sprite wildernessSprite = Resources.Load<Sprite>(wilderness);
        Sprite plainSprite = Resources.Load<Sprite>(plain);
        Sprite forestSprite = Resources.Load<Sprite>(forest);
        Sprite fortSprite = Resources.Load<Sprite>(fort);

        switch (territory.attackTerritoryType)
        {
            case Territory.AttackTerritoryType.desert:
                attackTerritoryTypeImage.sprite = desertSprite;
                break;
            case Territory.AttackTerritoryType.wilderness:
                attackTerritoryTypeImage.sprite = wildernessSprite;
                break;
            case Territory.AttackTerritoryType.plain:
                attackTerritoryTypeImage.sprite = plainSprite;
                break;
            case Territory.AttackTerritoryType.forest:
                attackTerritoryTypeImage.sprite = forestSprite;
                break;
            case Territory.AttackTerritoryType.fort:
                attackTerritoryTypeImage.sprite = fortSprite;
                break;
        }

        switch (territory.defenceTerritoryType)
        {
            case Territory.DefenceTerritoryType.desert:
                defenceTerritoryTypeImage.sprite = desertSprite;
                break;
            case Territory.DefenceTerritoryType.wilderness:
                defenceTerritoryTypeImage.sprite = wildernessSprite;
                break;
            case Territory.DefenceTerritoryType.plain:
                defenceTerritoryTypeImage.sprite = plainSprite;
                break;
            case Territory.DefenceTerritoryType.forest:
                defenceTerritoryTypeImage.sprite = forestSprite;
                break;
            case Territory.DefenceTerritoryType.fort:
                defenceTerritoryTypeImage.sprite = fortSprite;
                break;
        }
    }

    public void HideBattleDetailUI()
    {
        gameObject.SetActive(false);
    }
}
