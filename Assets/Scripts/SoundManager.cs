using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSourceBGM; //BGMのスピーカー
    public AudioSource audioSourceSE; //SEのスピーカー

    public AudioClip mainBGM; //鳴らす素材
    public AudioClip battleBGM; //鳴らす素材

    public AudioClip cursorSE; //鳴らす素材
    public AudioClip mapOnCursorSE; //鳴らす素材
    public AudioClip canselSE; //鳴らす素材
    public AudioClip dialogueSE; //鳴らす素材
    public AudioClip yesnoSE; //鳴らす素材
    public AudioClip yesSE; //鳴らす素材
    public AudioClip trainingSE; //鳴らす素材
    public AudioClip clidkSE; //鳴らす素材
    public AudioClip recruitSE; //鳴らす素材
    public AudioClip battleSE; //鳴らす素材

    //シングルトン化（どこからでもアクセスできるようにする）
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
        // 最初のBGMを再生
        PlayBGM(mainBGM);
    }

    private void PlayBGM(AudioClip bgm)
    {
        // AudioSourceでBGMを再生
        audioSourceBGM.clip = bgm;
        audioSourceBGM.Play();
    }

    public void SwitchBattleBGM()
    {
        // 切り替えるBGMを設定
        PlayBGM(battleBGM);
    }

    public void SwitchMainBGM()
    {
        // 切り替えるBGMを設定
        PlayBGM(mainBGM);
    }

    public void PlayCursorSE()
    {
        audioSourceSE.PlayOneShot(cursorSE); //SEを一度だけ鳴らす
    }

    public void PlayMapOnCursorSE()
    {
        audioSourceSE.PlayOneShot(mapOnCursorSE); //SEを一度だけ鳴らす
    }

    public void PlayCancelSE()
    {
        audioSourceSE.PlayOneShot(canselSE); //SEを一度だけ鳴らす
    }

    public void PlayDialogueSE()
    {
        audioSourceSE.PlayOneShot(dialogueSE); //SEを一度だけ鳴らす
    }

    public void PlalyYesNoUISE()
    {
        audioSourceSE.PlayOneShot(yesnoSE); //SEを一度だけ鳴らす
    }

    public void PlayYesSE()
    {
        audioSourceSE.PlayOneShot(yesSE); //SEを一度だけ鳴らす
    }

    public void PlayTrainingSE()
    {
        audioSourceSE.PlayOneShot(trainingSE); //SEを一度だけ鳴らす
    }

    public void PlayClickSE()
    {
        audioSourceSE.PlayOneShot(clidkSE); //SEを一度だけ鳴らす
    }

    public void PlayRecruitSE()
    {
        audioSourceSE.PlayOneShot(recruitSE); //SEを一度だけ鳴らす
    }

    public void PlayBattleSE()
    {
        audioSourceSE.PlayOneShot(battleSE); //SEを一度だけ鳴らす
    }
}
