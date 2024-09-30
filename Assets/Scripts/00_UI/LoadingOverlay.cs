using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using DG.Tweening;

public class LoadingOverlay : MonoBehaviour
{
    [SerializeField]
    private Image _overlayImage;

    [SerializeField]
    private Color _dispColor;
    [SerializeField]
    private Color _hideColor;

    public async UniTask Display()
    {
        if (_overlayImage.gameObject.activeSelf == true)
        {
            return;
        }

        _overlayImage.gameObject.SetActive(true);
        await _overlayImage.DOColor(_dispColor, 0.5f).AsyncWaitForCompletion();
    }

    public async UniTask Hide()
    {
        if (_overlayImage.gameObject.activeSelf == false)
        {
            return;
        }

        await _overlayImage.DOColor(_hideColor, 0.5f).AsyncWaitForCompletion();
        _overlayImage.gameObject.SetActive(false);
    }
}