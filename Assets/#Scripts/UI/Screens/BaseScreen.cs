using System;
using DG.Tweening;
using UnityEngine;

public abstract class BaseScreen : MonoBehaviour, IScreen
{
    [SerializeField] protected EScreenType _screenType = EScreenType.NONE;
    
    [Header("Canvas")]
    [SerializeField] protected Canvas _canvas = null;
    [SerializeField] protected CanvasGroup _canvasGroup = null;
    
    [Header("Timings")]
    [SerializeField] protected float _showTime = 0.5f;
    [SerializeField] protected float _hideTime = 0.5f;

    
    public EScreenType ScreenType => _screenType;

    protected abstract void SetData();
    
    public void DoShow(bool force = false, Action callback = null)
    {
        _canvasGroup.interactable = true;
        
        _canvas.gameObject.SetActive(true);

        if (force)
        {
            _canvasGroup.alpha = 1;
            OnScreenShow();
        }
        else
        {
            _canvasGroup.DOFade(1, _showTime).OnComplete(OnScreenShow);
        }
        
        void OnScreenShow()
        {
            callback?.Invoke();
        }
    }

    public void DoHide(bool force = false, Action callback = null)
    {
        _canvasGroup.interactable = false;

        if (force)
        {
            _canvasGroup.alpha = 0;
            OnScreenHide();
        }
        else
        {
            _canvasGroup.DOFade(0, _hideTime).OnComplete(OnScreenHide);
        }
        
        void OnScreenHide()
        {
            _canvas.gameObject.SetActive(false);

            callback?.Invoke();
        }
    }
}
