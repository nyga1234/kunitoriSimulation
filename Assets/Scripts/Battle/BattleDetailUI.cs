using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class BattleDetailUI : MonoBehaviour
{
    //[SerializeField] InflueneceManager influenceManager;
    //[SerializeField] TerritoryManager territoryManager;
    [SerializeField] private UtilityParamObject varParam;
    [SerializeField] TerritoryUIOnMouse territoryUIOnMouse;

    [SerializeField] Image attackerCharaIcon;
    [SerializeField] Image defenderCharaIcon;
    [SerializeField] TextMeshProUGUI attackerCharaInfoText;
    [SerializeField] TextMeshProUGUI defenderCharaInfoText;
    [SerializeField] TextMeshProUGUI attackerForceText;
    [SerializeField] TextMeshProUGUI defenderForceText;

    [SerializeField] Image attackTerritoryTypeImage;
    [SerializeField] Image defenceTerritoryTypeImage;

    [SerializeField] Transform AttackerSoliderListField;
    [SerializeField] Transform DefenderSoliderListField;
    [SerializeField] GameObject battleSolidefPrefab;
    //[SerializeField] SoliderController battleSolidefPrefab;

    int attackerSoliderHPSum;
    int defenderSoliderHPSum;

    public void ShowBattleDetailUI(CharacterController attackCharacter, CharacterController defenceCharacter)
    {
        gameObject.SetActive(true);

        attackerCharaIcon.sprite = attackCharacter.icon;
        defenderCharaIcon.sprite = defenceCharacter.icon;

        CharacterController attackerLordCharacter = attackCharacter.influence.characterList.Find(chara => chara.rank == Rank.�̎�);
        CharacterController defenderLordCharacter = defenceCharacter.influence.characterList.Find(chara => chara.rank == Rank.�̎�);

        attackerCharaInfoText.text = attackerLordCharacter.name + "�R" + " " + attackCharacter.name + "��";
        defenderCharaInfoText.text = defenderLordCharacter.name + "�R" + " " + defenceCharacter.name + "��";

        // ������
        attackerSoliderHPSum = 0;
        defenderSoliderHPSum = 0;

        foreach (SoldierController solider in attackCharacter.soliderList)
        {
            attackerSoliderHPSum += solider.hp;
        }
        foreach (SoldierController solider in defenceCharacter.soliderList)
        {
            defenderSoliderHPSum += solider.hp;
        }

        attackerForceText.text = attackerSoliderHPSum.ToString();
        defenderForceText.text = defenderSoliderHPSum.ToString();

        ShowLandformInformationUI();

        ShowSoliderList(attackCharacter.soliderList, AttackerSoliderListField, true);
        ShowSoliderList(defenceCharacter.soliderList, DefenderSoliderListField, false);
    }

    void ShowSoliderList(List<SoldierController> soliderList, Transform field, bool Attack)
    {
        // ���ݕ\������Ă��镺�m���폜
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }

        // �V�������m���X�g���쐬
        foreach (SoldierController solider in soliderList)
        {
            ShowSolider(solider, field, Attack);
        }
    }

    void ShowSolider(SoldierController solider, Transform field, bool Attack)
    {
        //SoliderController battleSolider = Instantiate(battleSolidefPrefab, field, false);
        //battleSolider.ShowBattleDetailSoliderUI(solider, Attack);
        GameObject soldierObject = Instantiate(battleSolidefPrefab, field, false);
        soldierObject.GetComponent<SoldierImageView>().ShowSoldierImage(solider.icon, Attack);
    }

    public void ShowLandformInformationUI()
    {
        this.gameObject.SetActive(true);

        SetBattleFieldUI(varParam.Territory);
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
