using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    //public Vector2 position;

    public void SetPosition(RectTransform target)
    {
        //transform.position = target.position;

        // アンカーポジションを取得
        Vector2 anchoredPosition = target.anchoredPosition;
        // カーソルの位置をアンカーポジションに設定
        (transform as RectTransform).anchoredPosition = anchoredPosition;
    }
}
