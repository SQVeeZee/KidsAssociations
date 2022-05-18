using System;
using System.Collections.Generic;
using UnityEngine;

public class TailsPanel : MonoBehaviour
{
    public event Action<EAnimalType> onTailPressed;
    
    [SerializeField] private List<Tail> _tails = new List<Tail>();

    private Tail _lastTimePressedTail = null;

    private void OnEnable()
    {
        foreach (var tail in _tails)
        {
            tail.onTailPressed += OnTailPressed;
        }
    }

    private void OnDisable()
    {
        foreach (var tail in _tails)
        {
            tail.onTailPressed -= OnTailPressed;
        }
    }

    public void PulseTails()
    {
        foreach (var tail in _tails)
        {
            tail.PulseTail();
        }
    }

    public Vector2 GetTargetTailScreenPosition(EAnimalType animalType)
    {
        foreach (var tail in _tails)
        {
            if (tail.AnimalType == animalType)
            {
                return tail.RectPosition();
            }
        }

        return Vector2.zero;
    }

    private void OnTailPressed(Tail pressedTile, EAnimalType animalType)
    {
        ResetPreviousLight(pressedTile);

        ResetPulses();
        
        onTailPressed?.Invoke(animalType);
    }

    private void ResetPreviousLight(Tail pressedTile)
    {
        if (_lastTimePressedTail == pressedTile) return;
        
        if (_lastTimePressedTail != null)
        {
            _lastTimePressedTail.SetLightState(false);
        }

        _lastTimePressedTail = pressedTile;
    }
    
    private void ResetPulses()
    {
        foreach (var tail in _tails)
        {
            tail.ResetTween();
        }
    }
}
