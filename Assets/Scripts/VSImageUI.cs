using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VSImageUI : MonoBehaviour
{
    public void SetPosition(Transform target)
    {
        transform.position = target.position;
    }
}
