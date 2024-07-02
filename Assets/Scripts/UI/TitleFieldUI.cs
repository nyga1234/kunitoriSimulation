using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleFieldUI : MonoBehaviour
{
    //�V���O���g�����i�ǂ�����ł��A�N�Z�X�ł���悤�ɂ���j
    public static TitleFieldUI instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public Text titleFieldText;
    public Text titleFieldSubText;

    public void ShowCharacterChoiceText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "      �v���C����L������I�����Ă�������";
    }

    public void ShowInformationText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "      ���̎Q��";
    }

    public void ShowAppointmentText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �z���̐g�� / ������ύX";
    }
    
    public void ShowSearchText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �������̐l�ނ�T��";
    }

    public void ShowBanishmentText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �z���𐨗͂���Ǖ�";
    }

    public void ShowAllianceText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �����͂Ɠ��������";
    }

    public void ShowLaureateText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �S�Ă��̂ĕ��Q";
    }

    public void ShowRecruitText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     ���m���ٗp";
    }

    public void ShowTrainingText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     ���m���P��";
    }

    public void ShowEnterText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �C�ӂ̐��͂ɏ���";
    }

    public void ShowVagabondText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �����̐��͂�����";
    }
    
    public void ShowRebellionText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �N�[�f�^�[���N����";
    }

    public void ShowFunctionText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �Z�[�u / �ݒ� / �Q�[���̏I��";
    }

    public void ShowAttackText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �אڂ̑����֐N�U";
    }

    public void ShowProvokeText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �G������֗U���܂�";
    }

    public void ShowSubdueText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �s���̋t���𓢔�";
    }

    public void ShowEndText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �^�[���̏I��";
    }

    public void ShowPlayerLordPhase()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �v���C���[�̎�t�F�[�Y";
    }

    public void ShowPlayerPersonalPhase()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �v���C���[�l�t�F�[�Y";
    }

    public void ShowPlayerBattlePhase()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     �v���C���[�퓬�t�F�[�Y";
    }
    
    public void ShowChangeLordTurnText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     " + GameManager.instance.turnCount + "��";
        titleFieldSubText.text = "���ƃt�F�[�Y";
    }

    public void ShowChangePersonalTurnText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     " + GameManager.instance.turnCount + "��";
        titleFieldSubText.text = "�l�t�F�[�Y";
    }

    public void ShowChangeBattleTurnText()
    {
        this.gameObject.SetActive(true);
        titleFieldText.text = "     " + GameManager.instance.turnCount + "��";
        titleFieldSubText.text = "�퓬�t�F�[�Y";
    }

    public void HideTitleSubText()
    {
        titleFieldSubText.gameObject.SetActive(false);
    }
}
