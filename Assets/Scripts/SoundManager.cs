using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSourceBGM; //BGM�̃X�s�[�J�[
    public AudioSource audioSourceSE; //SE�̃X�s�[�J�[

    public AudioClip mainBGM; //�炷�f��
    public AudioClip battleBGM; //�炷�f��

    public AudioClip cursorSE; //�炷�f��
    public AudioClip mapOnCursorSE; //�炷�f��
    public AudioClip canselSE; //�炷�f��
    public AudioClip dialogueSE; //�炷�f��
    public AudioClip yesnoSE; //�炷�f��
    public AudioClip yesSE; //�炷�f��
    public AudioClip trainingSE; //�炷�f��
    public AudioClip clidkSE; //�炷�f��
    public AudioClip recruitSE; //�炷�f��
    public AudioClip battleSE; //�炷�f��

    //�V���O���g�����i�ǂ�����ł��A�N�Z�X�ł���悤�ɂ���j
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        audioSourceBGM = GetComponent<AudioSource>();
        // �ŏ���BGM���Đ�
        PlayBGM(mainBGM);
    }

    private void PlayBGM(AudioClip bgm)
    {
        // AudioSource��BGM���Đ�
        audioSourceBGM.clip = bgm;
        audioSourceBGM.Play();
    }

    public void SwitchBattleBGM()
    {
        // �؂�ւ���BGM��ݒ�
        PlayBGM(battleBGM);
    }

    public void SwitchMainBGM()
    {
        // �؂�ւ���BGM��ݒ�
        PlayBGM(mainBGM);
    }

    public void PlayCursorSE()
    {
        audioSourceSE.PlayOneShot(cursorSE); //SE����x�����炷
    }

    public void PlayMapOnCursorSE()
    {
        audioSourceSE.PlayOneShot(mapOnCursorSE); //SE����x�����炷
    }

    public void PlayCancelSE()
    {
        audioSourceSE.PlayOneShot(canselSE); //SE����x�����炷
    }

    public void PlayDialogueSE()
    {
        audioSourceSE.PlayOneShot(dialogueSE); //SE����x�����炷
    }

    public void PlalyYesNoUISE()
    {
        audioSourceSE.PlayOneShot(yesnoSE); //SE����x�����炷
    }

    public void PlayYesSE()
    {
        audioSourceSE.PlayOneShot(yesSE); //SE����x�����炷
    }

    public void PlayTrainingSE()
    {
        audioSourceSE.PlayOneShot(trainingSE); //SE����x�����炷
    }

    public void PlayClickSE()
    {
        audioSourceSE.PlayOneShot(clidkSE); //SE����x�����炷
    }

    public void PlayRecruitSE()
    {
        audioSourceSE.PlayOneShot(recruitSE); //SE����x�����炷
    }

    public void PlayBattleSE()
    {
        audioSourceSE.PlayOneShot(battleSE); //SE����x�����炷
    }
}
