using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class PersonalMenuUI : MonoBehaviour
{
    [Header("Command Settings")]
    [SerializeField] private CommandOnClick commandClick;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] TextMeshProUGUI characterNameText;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] TextMeshProUGUI forceText;
    [SerializeField] TextMeshProUGUI inteliText;
    [SerializeField] TextMeshProUGUI tactText;
    [SerializeField] TextMeshProUGUI fameText;
    [SerializeField] TextMeshProUGUI ambitionText;
    [SerializeField] TextMeshProUGUI loyalityText;

    [SerializeField] TextMeshProUGUI recruitText;
    [SerializeField] TextMeshProUGUI trainingText;
    [SerializeField] TextMeshProUGUI enterText;
    [SerializeField] TextMeshProUGUI vagabondText;
    [SerializeField] TextMeshProUGUI rebellionText;

    [SerializeField] Image characterImage;    

    [SerializeField] Transform SoliderListField;
    [SerializeField] GameObject personalSolidefPrefab;

    [System.Serializable]
    private class ButtonGroup
    {
        public SelectButtonView informationButton;
        public SelectButtonView recruitButton;
        public SelectButtonView trainingButton;
        public SelectButtonView enterButton;
        public SelectButtonView vagabond;
        public SelectButtonView function;
        public SelectButtonView end;
    }
    [SerializeField] private ButtonGroup buttons;

    private void Start()
    {
        buttons.informationButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickPersonalInformation());
        buttons.recruitButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickSoliderRecruit());
        buttons.trainingButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickSoliderTraining());
        buttons.enterButton.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickEnter());
        buttons.vagabond.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickVagabond());
        buttons.function.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickFunction());
        buttons.end.Button.onClick.AsObservable().Subscribe(_ => commandClick.OnPointerClickPersonalEnd());
    }

    public void ShowPersonalMenuUI(CharacterController character)
    {
        gameObject.SetActive(true);

        recruitText.color = Color.white; // ���F�ɕύX
        trainingText.color = Color.white; // ���F�ɕύX
        enterText.color = Color.white; // ���F�ɕύX
        vagabondText.color = Color.white; // ���F�ɕύX
        rebellionText.color = Color.white; // ���F�ɕύX
        if (character.soliderList.Count >= 10 || character.gold <= 1)
        {
            recruitText.color = new Color32(122, 122, 122, 255);
        }
        if (character.gold <= 1)
        {
            trainingText.color = new Color32(122, 122, 122, 255);
        }
        if (character.influence != GameMain.instance.noneInfluence)
        {
            enterText.color = new Color32(255, 255, 255, 0);
        }
        if (character.isLord == true || character.influence == GameMain.instance.noneInfluence)
        {
            vagabondText.color = new Color32(255, 255, 255, 0);
        }
        if (character.isLord == true || character.influence == GameMain.instance.noneInfluence)
        {
            rebellionText.color = new Color32(255, 255, 255, 0);
        }

        moneyText.text = "���� " + character.gold.ToString();

        characterImage.sprite = character.icon;

        rankText.text = character.rank.ToString();
        characterNameText.text = character.name;

        goldText.text = "[������] " + character.gold.ToString();
        forceText.text = "[�퓬] " + character.force.ToString();
        inteliText.text = "[�q�d] " + character.inteli.ToString();
        tactText.text = "[��r] " + character.tact.ToString();
        fameText.text = "[����] " + character.fame.ToString();
        ambitionText.text = "[��S] " + character.ambition.ToString();
        if (character.isLord == true || character.influence.influenceName == "NoneInfluence" || character == GameMain.instance.playerCharacter)
        {
            loyalityText.text = "[����] --";
        }
        else
        {
            loyalityText.text = "[����] " + character.loyalty.ToString();
        }
        ShowSoliderList(character.soliderList, SoliderListField);
    }

    void ShowSoliderList(List<SoldierController> soliderList, Transform field)
    {
        // ���ݕ\������Ă��镺�m���폜
        foreach (Transform child in field)
        {
            Destroy(child.gameObject);
        }

        // �V�������m���X�g���쐬
        foreach (SoldierController solider in soliderList)
        {
            ShowSolider(solider, field);
        }
    }

    void ShowSolider(SoldierController solider, Transform field)
    {
        var soldierObject = Instantiate(personalSolidefPrefab, field);
        soldierObject.GetComponent<SoldierImageView>().
            ShowSoldierStatus(
            solider.icon, 
            solider.hp.ToString(),
            solider.lv.ToString());
    }

    public void HidePersonalMenuUI()
    {
        gameObject.SetActive(false);
    }
}
