using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelIcon : MonoBehaviour
{
    public event Action<int> onLevelsButtonClick = null;
    
    [SerializeField] private Button _levelButton = null;
    [SerializeField] private Image _iconImage = null;

    private int _levelId = default;

    public void Initialize(int levelId, Sprite iconSprite)
    {
        _levelId = levelId;
        _iconImage.sprite = iconSprite;
        
        AddListener();
    }
    
    private void AddListener()
    {
        _levelButton.onClick.AddListener(StartLevel);
    }
    
    private void RemoveListener()
    {
        _levelButton.onClick.RemoveListener(StartLevel);
    }

    private void StartLevel()
    {
        onLevelsButtonClick?.Invoke(_levelId);
    }
}
