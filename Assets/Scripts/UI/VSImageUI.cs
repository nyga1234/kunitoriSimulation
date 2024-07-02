using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSImageUI : MonoBehaviour
{
    public void SetPosition(RectTransform target)
    {
        //SoundManager.instance.PlayBattleSE();
        
        //transform.position = target.position;

        // アンカーポジションを取得
        Vector2 anchoredPosition = target.anchoredPosition;
        // vsImageの位置をアンカーポジションに設定
        (transform as RectTransform).anchoredPosition = anchoredPosition;
    }
}
