using System;
using UnityEngine;

public class LevelsController : Singleton<LevelsController>
{
    public event Action<LevelsConfigs> onLevelsConfigsSet = null;
    
    [SerializeField] private LevelItem _levelItemPrefab = null;
    
    [Header("Configs")]
    [SerializeField] private LevelsConfigs _levelsConfigs = null;

    private LevelItem _currentLevelItem = null;
    private int _currentLevelNumber = 1;
    
    private void Start()
    {
        onLevelsConfigsSet?.Invoke(_levelsConfigs);
    }

    public void CreateLevelById(int levelId)
    {
        _currentLevelItem = InstantiateLevel();
        
        _currentLevelItem.onLevelCompleted += OnLevelComplete;
        
        UIManager.Instance.ShowScreen(EScreenType.GAME);
    }

    private LevelItem InstantiateLevel()
    {
        return Instantiate(_levelItemPrefab, transform);
    }

    private void OnLevelComplete(ELevelCompleteReason levelCompleteReason)
    {
        _currentLevelNumber++;
        
        UIManager.Instance.ShowScreen(EScreenType.WIN);
    }

    private void OnDisable()
    {
        if (_currentLevelItem != null)
        {
            _currentLevelItem.onLevelCompleted -= OnLevelComplete;
        }
    }
}
