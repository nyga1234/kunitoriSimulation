using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    //public Vector2 position;

    public void SetPosition(RectTransform target)
    {
        //transform.position = target.position;

        // �A���J�[�|�W�V�������擾
        Vector2 anchoredPosition = target.anchoredPosition;
        // �J�[�\���̈ʒu���A���J�[�|�W�V�����ɐݒ�
        (transform as RectTransform).anchoredPosition = anchoredPosition;
    }
}
