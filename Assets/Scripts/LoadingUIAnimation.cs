using DG.Tweening;
using UnityEngine;

public class LoadingUIAnimation : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private GameObject _logo;
    
    private void Awake()
    {
        LevelLoaded();
    }
    
    public void LevelLoaded()
    {
        _logo.transform.DOScale(Vector3.one, .25f).SetEase(Ease.InBounce);
        _rectTransform.DOScale(Vector3.zero, .5f).SetDelay(.25f).SetEase(Ease.OutQuad);
    }

    public void OnLoadStart()
    {
        _rectTransform.DOScale(Vector3.one, .5f).SetEase(Ease.InQuad);
        _logo.transform.DOScale(Vector3.one, .25f).SetDelay(.5f).SetEase(Ease.OutBounce);
    }
}
