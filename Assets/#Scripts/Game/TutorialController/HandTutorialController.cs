using System;
using DG.Tweening;
using UnityEngine;

public class HandTutorialController : MonoBehaviour
{
    [SerializeField] private Transform _fingerTransform = null;
    [SerializeField] private SpriteRenderer _fingerSpriteRenderer = null;

    private Tweener _pulseTween = null;
    
    private Vector3 _startFingerScale = default;

    private void Awake()
    {
        _startFingerScale = _fingerTransform.localScale;
    }

    public void DoFingerAnimation(Vector3 targetWorldPosition)
    {
        _fingerTransform.position = targetWorldPosition;

        _fingerSpriteRenderer.DOFade(1,.3f);

        _pulseTween = _fingerTransform.DOPunchScale(_startFingerScale * 0.1f, 0.5f,1,1).SetLoops(-1).SetEase(Ease.Linear);
    }

    public void ResetPulseTween(Action callback = null)
    {
        _pulseTween?.Kill();

        _fingerSpriteRenderer.DOFade(0,.3f).OnComplete(delegate { callback?.Invoke(); });
    }
}
