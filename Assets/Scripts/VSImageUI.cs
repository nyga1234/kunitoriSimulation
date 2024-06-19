using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSImageUI : MonoBehaviour
{
    public void SetPosition(Transform target)
    {
        //SoundManager.instance.PlayBattleSE();
        transform.position = target.position;
    }
}
