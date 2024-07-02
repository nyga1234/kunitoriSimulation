using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    [SerializeField] private GameObject loadingUI;
    [SerializeField] private Slider slider;
    public float loadTime = 1000.0f;

    public IEnumerator MoveSlider()
    {
        loadingUI.gameObject.SetActive(true);

        float currentTime = 0f;
        float tempTime = 0f;

        while (currentTime < loadTime)
        {
            float progress = currentTime / loadTime;
            slider.value = progress;
            currentTime += Time.deltaTime;
            float waitTime = currentTime - tempTime;
            tempTime = currentTime;

            yield return new WaitForSeconds(waitTime);
            //yield return null;
        }

        slider.value = 1.0f; // スライダーの値を1に設定

        loadingUI.gameObject.SetActive(false);
    }
}
