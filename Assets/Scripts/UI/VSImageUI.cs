using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSImageUI : MonoBehaviour
{
    public void SetPosition(RectTransform target)
    {
        //SoundManager.instance.PlayBattleSE();
        
        //transform.position = target.position;

        // �A���J�[�|�W�V�������擾
        Vector2 anchoredPosition = target.anchoredPosition;
        // vsImage�̈ʒu���A���J�[�|�W�V�����ɐݒ�
        (transform as RectTransform).anchoredPosition = anchoredPosition;
    }
}
