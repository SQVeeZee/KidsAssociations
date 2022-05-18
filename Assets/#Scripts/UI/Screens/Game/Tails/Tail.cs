using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Tail : MonoBehaviour
{
    public event Action<Tail, EAnimalType> onTailPressed = null;

    [SerializeField] private EAnimalType _animalType = EAnimalType.NONE;
    [SerializeField] private RectTransform _rectTransform = null;
    [SerializeField] private Button _button = null;
    [SerializeField] private Image _lightImage = null;
    public EAnimalType AnimalType => _animalType;

    public Vector3 RectPosition()
    {
        return _rectTransform.anchoredPosition;
    }

    private Tweener _pulseTween = null;
    private Vector3 _startLocalTailScale = default;
    
    private void Awake()
    {
        AddListener();
        
        SetLightState(false);
        
        _startLocalTailScale = transform.localScale;
    }

    public void PulseTail()
    {
        _pulseTween = transform.DOPunchScale(transform.localScale * 0.1f, 0.6f, 1, 0.1f).SetEase(Ease.Linear).SetLoops(-1);
    }

    public void ResetTween()
    {
        _pulseTween?.Kill();

        transform.localScale = _startLocalTailScale;
    }

    private void AddListener()
    {
        _button.onClick.AddListener(OnTailPress);
    }

    private void RemoveListener()
    {
        _button.onClick.RemoveListener(OnTailPress);
    }

    private void OnTailPress()
    {
        SetLightState(true);
        
        onTailPressed?.Invoke(this, _animalType);
    }

    public void SetLightState(bool state)
    {
        _lightImage.enabled = state;
    }
}
