using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputTracker : MonoBehaviour, IPointerDownHandler
{
    public event Action onPointerDown = null;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        onPointerDown?.Invoke();
    }
}
