using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class PanelController : MonoBehaviour
{
    [SerializeField] private RectTransform panelRectTransform;

    private CanvasGroup _backgroundCanvasGroup;

    private void Awake()
    {
        _backgroundCanvasGroup = GetComponent<CanvasGroup>();
    }

    public void Show()  // Panel Ç¥½Ã
    {
        _backgroundCanvasGroup.alpha = 0;
        panelRectTransform.localScale = Vector3.zero;

        _backgroundCanvasGroup.DOFade(endValue: 1, duration: 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(endValue: 1, duration: 0.3f).SetEase(Ease.OutBack);
    }

    public void Hide()  // Panel ¼û±â±â
    {
        _backgroundCanvasGroup.alpha = 1;
        panelRectTransform.localScale = Vector3.one;

        _backgroundCanvasGroup.DOFade(endValue: 0, duration: 0.3f).SetEase(Ease.Linear);
        panelRectTransform.DOScale(endValue: 0, duration: 0.3f).SetEase(Ease.InBack);
    }
}
