using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWindow : MonoBehaviour
{
    //�V���O���g�����i�ǂ�����ł��A�N�Z�X�ł���悤�ɂ���j
    public static MainWindow instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
}
