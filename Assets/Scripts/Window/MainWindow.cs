using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWindow : MonoBehaviour
{
    //シングルトン化（どこからでもアクセスできるようにする）
    public static MainWindow instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
