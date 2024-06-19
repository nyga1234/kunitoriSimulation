//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System.Linq;
//using UnityEngine.TextCore.Text;
////using static UnityEditor.PlayerSettings;


//public class InflueneceManager : MonoBehaviour
//{
//    public Button yesButton; // Button���Q�Ƃ��邽�߂̕ϐ�

//    [SerializeField] Cursor cursor;
//    [SerializeField] TitleFieldUI titleFieldUI;
//    [SerializeField] YesNoUI yesNoUI;
//    [SerializeField] DialogueUI dialogueUI;
//    [SerializeField] InfluenceUI influenceUI;
//    [SerializeField] InfluenceOnMapUI influenceOnMapUI;
//    [SerializeField] CharacterIndexUI characterIndexUI;
//    [SerializeField] CharacterDetailUI characterDetailUI;
//    [SerializeField] TerritoryGenerator territoryGenerator;
//    [SerializeField] GameObject characterIndexMenu;
//    [SerializeField] GameObject mapField;
//    [SerializeField] BattleManager battleManager;
//    [SerializeField] BattleUI battleUI;
//    [SerializeField] GameObject backToCharaMenuButton;
//    [SerializeField] GameObject backToMapFieldButton;

//    public Influence influence;
//    public Territory territory;

//    List<Influence> influenceList = new List<Influence>();
//    List<Influence> blueInfluenceList = new List<Influence>();
//    List<Influence> redInfluenceList = new List<Influence>();
//    List<Influence> noneInfluenceList = new List<Influence>();

//    private bool isSoundPlayed = false; // �����Đ����ꂽ���ǂ����������t���O
//    Territory beforeTerritory = null;

//    private void Start()
//    {
//        Button btn = yesButton.GetComponent<Button>(); // Button�R���|�[�l���g���擾
//        btn.onClick.AddListener(TaskOnClick); // �N���b�N����TaskOnClick���\�b�h���Ăяo��
//    }

//    void TaskOnClick()
//    {
//        if (yesNoUI.IsYes()) // ����yesNoUI��"Yes"�ɐݒ肳��Ă���ꍇ
//        {
//            if (GameManager.instance.step == GameManager.Step.Enter)
//            {
//                GameManager.instance.noneInfluence.RemoveCharacter(GameManager.instance.playerCharacter);
//                territory.influence.AddCharacter(GameManager.instance.playerCharacter);

//                mapField.gameObject.SetActive(false);
//                influenceOnMapUI.HideInfluenceOnMapUI();

//                GameManager.instance.ShowPersonalUI(GameManager.instance.playerCharacter);
//                dialogueUI.ShowEnterInfluenceUI();
//            }

//        }
//    }

//    IEnumerator WaitForAttackBattle()
//    {
//        yesNoUI.ShowAttackYesNoUI();
//        //yesNoUI����\���ɂȂ�܂őҋ@
//        yield return new WaitUntil(() => !yesNoUI.IsYesNoVisible());

//        if (yesNoUI.IsYes())
//        {
//            // ���͏���\��
//            influenceOnMapUI.HideInfluenceOnMapUI();
//            mapField.gameObject.SetActive(false);

//            int attackSoliderHPSum = 0;
//            foreach (SoliderController solider in GameManager.instance.playerCharacter.soliderList)
//            {
//                attackSoliderHPSum += solider.soliderModel.hp;
//            }

//            Debug.Log(territory.influence.influenceName);
//            Debug.Log(this.influence.influenceName);
//            Debug.Log(this.territory.influence.influenceName);
//            CharacterController defenceCharacter = GameManager.instance.SelectDefenceCharacter(attackSoliderHPSum);

//            battleUI.ShowBattleUI(GameManager.instance.playerCharacter, defenceCharacter, territory);
//            battleManager.StartBattle(GameManager.instance.playerCharacter, defenceCharacter);
//        }
//    }

//    public void OnPointerEnterTerritory()
//    {
//        if (!yesNoUI.IsYesNoVisible())
//        {
//            if (GameManager.instance.step == GameManager.Step.Information || GameManager.instance.step == GameManager.Step.Attack || GameManager.instance.step == GameManager.Step.Choice || GameManager.instance.step == GameManager.Step.Enter)
//            {
//                if (!yesNoUI.gameObject.activeSelf)
//                {
//                    cursor.gameObject.SetActive(true);
//                }

//                if (isSoundPlayed == false)
//                {
//                    if (beforeTerritory != null)
//                    {
//                        SoundManager.instance.PlayMapOnCursorSE();
//                        isSoundPlayed = true;
//                    }
//                }
//                cursor.SetPosition(transform);//�J�[�\�����}�E�X�ʒu�ֈړ�
//                Debug.Log("���B");

//                //Territory influenceTerritory = this.GetComponent<Territory>();

//                //if (beforeTerritory != influenceTerritory)
//                //{
//                //    isSoundPlayed = false;
//                //    beforeTerritory = influenceTerritory;
//                //}
//                //else
//                //{
//                //    isSoundPlayed = true;
//                //}

//                //influenceOnMapUI.ShowInfluenceOnMapUI(influenceTerritory.influence, influenceTerritory);
//            }

//            //Territory territory = this.GetComponent<Territory>();

//            //if (territory != null)
//            //{
//            //    //���X�e�b�v
//            //    if (Input.GetMouseButtonDown(0) && GameManager.instance.step == GameManager.Step.Information || Input.GetMouseButtonDown(0) && GameManager.instance.step == GameManager.Step.Choice)//���N���b�N������
//            //    {
//            //        SoundManager.instance.PlayClickSE();
//            //        //�N���b�N�����̓y��ݒ�
//            //        this.territory = territory;

//            //        // ���͏���\��
//            //        influenceOnMapUI.HideInfluenceOnMapUI();
//            //        mapField.gameObject.SetActive(false);

//            //        //�L�����N�^�[���\��
//            //        characterIndexMenu.SetActive(true);
//            //        characterIndexUI.ShowCharacterIndexUI(territory.influence.characterList);
//            //    }
//            //    //�d���X�e�b�v
//            //    else if (GameManager.instance.step == GameManager.Step.Enter)
//            //    {
//            //        if (Input.GetMouseButtonDown(0) && !yesNoUI.gameObject.activeSelf)
//            //        {
//            //            if (territory.influence == GameManager.instance.noneInfluence)
//            //            {
//            //                TitleFieldUI.instance.titleFieldText.text = "      �󂫗̓y�ł�";
//            //            }
//            //            else
//            //            {
//            //                //�N���b�N�����̓y��ݒ�
//            //                this.territory = territory;
//            //                cursor.gameObject.SetActive(false);
//            //                yesNoUI.ShowEnterUI();
//            //            }
//            //        }
//            //        if (yesNoUI.gameObject.activeSelf)
//            //        {
//            //            TaskOnClick();
//            //        }
//            //    }
//            //    //�N�U�X�e�b�v
//            //    else if (Input.GetMouseButtonDown(0) && GameManager.instance.step == GameManager.Step.Attack)
//            //    {
//            //        //�N���b�N�����̓y��ݒ�
//            //        this.territory = territory;
//            //        //�N���b�N�������͂�ݒ�iCharacterUIOnClick�Ŗh�q���̃L�������擾���邽�߂ɐݒ�j
//            //        this.influence = territory.influence;

//            //        if (this.influence == GameManager.instance.playerCharacter.influence)
//            //        {
//            //            titleFieldUI.titleFieldText.text = "     �����̓y�ł�";
//            //            return;
//            //        }
//            //        else if (this.influence == GameManager.instance.noneInfluence)
//            //        {
//            //            titleFieldUI.titleFieldText.text = "     �󂫗̓y�ł�";
//            //            return;
//            //        }
//            //        else if (GameManager.instance.playerCharacter.influence.IsAttackableTerritory(this.territory) == false)
//            //        {
//            //            titleFieldUI.titleFieldText.text = "     �אڂ��Ă��܂���";
//            //            return;
//            //        }
//            //        else
//            //        {
//            //            if (GameManager.instance.playerCharacter.characterModel.isLord == true)
//            //            {
//            //                SoundManager.instance.PlayClickSE();
//            //                titleFieldUI.titleFieldText.text = "     �N�U�����镔����I�����Ă�������";

//            //                // ���͏����\���ɂ���
//            //                influenceOnMapUI.HideInfluenceOnMapUI();
//            //                mapField.gameObject.SetActive(false);

//            //                //�N�U�L�����N�^�[�I����ʂ�
//            //                characterIndexMenu.SetActive(true);
//            //                characterIndexUI.ShowCharacterIndexUI(GameManager.instance.playerCharacter.influence.characterList);
//            //            }
//            //            else
//            //            {
//            //                Debug.Log(territory.influence.influenceName);
//            //                Debug.Log(this.influence.influenceName);
//            //                Debug.Log(this.territory.influence.influenceName);
//            //                StartCoroutine(WaitForAttackBattle()); ;
//            //            }
//            //        }
//            //    }
//            //}
//        }

//        if (GameManager.instance.playerCharacter == null)
//        {
//            return;
//        }
//        else if (GameManager.instance.playerCharacter.influence != GameManager.instance.noneInfluence)
//        {
//            foreach (Territory territory in GameManager.instance.allTerritoryList)
//            {
//                if (territory.influence == GameManager.instance.playerCharacter.influence)
//                {
//                    territory.ShowHomeTerritory(true);
//                }
//                else
//                {
//                    territory.ShowHomeTerritory(false);
//                }
//            }
//        }
//        else
//        {
//            foreach (Territory territory in GameManager.instance.allTerritoryList)
//            {
//                territory.ShowHomeTerritory(false);
//            }
//        }
//    }

//    //private void Update()
//    //{
//    //    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);// �}�E�X�̈ʒu���擾
//    //    RaycastHit2D hit2D = Physics2D.Raycast(mousePosition, Vector2.down);//���C���΂�

//    //    //���C�ƏՓ˂����I�u�W�F�N�g��\���i�}�E�X�J�[�\�����̓y�ɏd�Ȃ�����j
//    //    if (hit2D && hit2D.collider && !yesNoUI.IsYesNoVisible())
//    //    {
//    //        if (GameManager.instance.step == GameManager.Step.Information || GameManager.instance.step == GameManager.Step.Attack || GameManager.instance.step == GameManager.Step.Choice || GameManager.instance.step == GameManager.Step.Enter)
//    //        {
//    //            if (!yesNoUI.gameObject.activeSelf)
//    //            {
//    //                cursor.gameObject.SetActive(true);
//    //            }

//    //            if (isSoundPlayed == false)
//    //            {
//    //                if (beforeTerritory != null)
//    //                {
//    //                    SoundManager.instance.PlayMapOnCursorSE();
//    //                    isSoundPlayed = true;
//    //                }
//    //            }
//    //            cursor.SetPosition(hit2D.transform);//�J�[�\�����}�E�X�ʒu�ֈړ�

//    //            Territory influenceTerritory = hit2D.collider.GetComponent<Territory>();

//    //            if (beforeTerritory != influenceTerritory)
//    //            {
//    //                isSoundPlayed = false;
//    //                beforeTerritory = influenceTerritory;
//    //            }
//    //            else
//    //            {
//    //                isSoundPlayed = true;
//    //            }

//    //            influenceOnMapUI.ShowInfluenceOnMapUI(influenceTerritory.influence, influenceTerritory);
//    //        }

//    //        Territory territory = hit2D.collider.GetComponent<Territory>();

//    //        if (territory != null)
//    //        {
//    //            //���X�e�b�v
//    //            if (Input.GetMouseButtonDown(0) && GameManager.instance.step == GameManager.Step.Information || Input.GetMouseButtonDown(0) && GameManager.instance.step == GameManager.Step.Choice)//���N���b�N������
//    //            {
//    //                SoundManager.instance.PlayClickSE();
//    //                //�N���b�N�����̓y��ݒ�
//    //                this.territory = territory;

//    //                // ���͏���\��
//    //                influenceOnMapUI.HideInfluenceOnMapUI();
//    //                mapField.gameObject.SetActive(false);

//    //                //�L�����N�^�[���\��
//    //                characterIndexMenu.SetActive(true);
//    //                characterIndexUI.ShowCharacterIndexUI(territory.influence.characterList);
//    //            }
//    //            //�d���X�e�b�v
//    //            else if (GameManager.instance.step == GameManager.Step.Enter)
//    //            {
//    //                if (Input.GetMouseButtonDown(0) && !yesNoUI.gameObject.activeSelf)
//    //                {
//    //                    if (territory.influence == GameManager.instance.noneInfluence)
//    //                    {
//    //                        TitleFieldUI.instance.titleFieldText.text = "      �󂫗̓y�ł�";
//    //                    }
//    //                    else
//    //                    {
//    //                        //�N���b�N�����̓y��ݒ�
//    //                        this.territory = territory;
//    //                        cursor.gameObject.SetActive(false);
//    //                        yesNoUI.ShowEnterUI();
//    //                    }
//    //                }
//    //                if (yesNoUI.gameObject.activeSelf)
//    //                {
//    //                    TaskOnClick();
//    //                }
//    //            }
//    //            //�N�U�X�e�b�v
//    //            else if (Input.GetMouseButtonDown(0) && GameManager.instance.step == GameManager.Step.Attack)
//    //            {
//    //                //�N���b�N�����̓y��ݒ�
//    //                this.territory = territory;
//    //                //�N���b�N�������͂�ݒ�iCharacterUIOnClick�Ŗh�q���̃L�������擾���邽�߂ɐݒ�j
//    //                this.influence = territory.influence;

//    //                if (this.influence == GameManager.instance.playerCharacter.influence)
//    //                {
//    //                    titleFieldUI.titleFieldText.text = "     �����̓y�ł�";
//    //                    return;
//    //                }
//    //                else if (this.influence == GameManager.instance.noneInfluence)
//    //                {
//    //                    titleFieldUI.titleFieldText.text = "     �󂫗̓y�ł�";
//    //                    return;
//    //                }
//    //                else if (GameManager.instance.playerCharacter.influence.IsAttackableTerritory(this.territory) == false)
//    //                {
//    //                    titleFieldUI.titleFieldText.text = "     �אڂ��Ă��܂���";
//    //                    return;
//    //                }
//    //                else
//    //                {
//    //                    if (GameManager.instance.playerCharacter.characterModel.isLord == true)
//    //                    {
//    //                        SoundManager.instance.PlayClickSE();
//    //                        titleFieldUI.titleFieldText.text = "     �N�U�����镔����I�����Ă�������";

//    //                        // ���͏����\���ɂ���
//    //                        influenceOnMapUI.HideInfluenceOnMapUI();
//    //                        mapField.gameObject.SetActive(false);

//    //                        //�N�U�L�����N�^�[�I����ʂ�
//    //                        characterIndexMenu.SetActive(true);
//    //                        characterIndexUI.ShowCharacterIndexUI(GameManager.instance.playerCharacter.influence.characterList);
//    //                    }
//    //                    else
//    //                    {
//    //                        Debug.Log(territory.influence.influenceName);
//    //                        Debug.Log(this.influence.influenceName);
//    //                        Debug.Log(this.territory.influence.influenceName);
//    //                        StartCoroutine(WaitForAttackBattle()); ;
//    //                    }
//    //                }
//    //            }
//    //        }
//    //    }

//    //    if (GameManager.instance.playerCharacter == null)
//    //    {
//    //        return;
//    //    }
//    //    else if (GameManager.instance.playerCharacter.influence != GameManager.instance.noneInfluence)
//    //    {
//    //        foreach (Territory territory in GameManager.instance.allTerritoryList)
//    //        {
//    //            if (territory.influence == GameManager.instance.playerCharacter.influence)
//    //            {
//    //                territory.ShowHomeTerritory(true);
//    //            }
//    //            else
//    //            {
//    //                territory.ShowHomeTerritory(false);
//    //            }
//    //        }
//    //    }
//    //    else
//    //    {
//    //        foreach (Territory territory in GameManager.instance.allTerritoryList)
//    //        {
//    //            territory.ShowHomeTerritory(false);
//    //        }
//    //    }
//    //}

//    public void InfluenceCalcSum(Influence influence)
//    {
//        influence.CalcTerritorySum(influence);
//        influence.CalcGoldSum(influence.characterList);
//        influence.CalcCharacterSum(influence.characterList);
//        influence.CalcSoliderSum(influence.characterList);
//        influence.CalcForceSum(influence.characterList);
//    }

//    public void ChangeTerritoryByBattle(Influence influence)
//    {
//        //�̓y�ɐ��͂�ݒ�
//        this.territory.influence = influence;
//        //���͂ɗ̓y��ݒ�
//        influence.AddTerritory(this.territory);
//        this.influence.RemoveTerritory(this.territory);
//        //influenceList.Find(x => x.InfluenceType == influence.InfluenceType)?.AddTerritory(this.territory);

//        if (influence.territoryList.Count == GameManager.instance.territoryCouont)
//        {
//            GameManager.instance.uniteCountryFlag = true;
//            GameManager.instance.uniteInfluence = influence;
//        }
//    }
//}
