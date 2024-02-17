using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailUI : MonoBehaviour
{
    public void ShowDetailUI()
    {
        this.gameObject.SetActive(true);
    }

    public void HideDetailUI()
    {
        this.gameObject.SetActive(false);
    }
}
