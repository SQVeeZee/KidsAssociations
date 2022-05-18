using System;
using UnityEngine;

public class LevelsController : Singleton<LevelsController>
{
    public event Action<LevelsConfigs> onLevelsConfigsSet = null;
    public event Action<LevelItemConfigs> onLevelItemConfigsSet = null;
    public event Action<BaseLevelItem> onLevelItemSet = null;
    public event Action<ELevelCompleteReason> onLevelComplete = null;
        
    [SerializeField] private LevelItem _levelItemPrefab = null;
    
    [Header("Configs")]
    [SerializeField] private LevelsConfigs _levelsConfigs = null;

    private LevelItemConfigs _currentLevelItemCofigs = null;
    private LevelItem _currentLevelItem = null;
    
    private void Start()
    {
        onLevelsConfigsSet?.Invoke(_levelsConfigs);
    }

    public void CreateLevelById(int levelId)
    {
        _currentLevelItemCofigs = _levelsConfigs.LevelItemConfigs[levelId];
        
        _currentLevelItem = InstantiateLevel();
        _currentLevelItem.onLevelCompleted += OnLevelComplete;
        
        onLevelItemConfigsSet?.Invoke(_currentLevelItemCofigs);
        onLevelItemSet?.Invoke(_currentLevelItem);
        
        _currentLevelItem.StartLevel(_currentLevelItemCofigs);
        
        UIManager.Instance.ShowScreen(EScreenType.GAME);
    }

    public void OnLevelSkipped()
    {
        OnLevelComplete(ELevelCompleteReason.SKIP);
    }

    private LevelItem InstantiateLevel()
    {
        return Instantiate(_levelItemPrefab, transform);
    }

    private void OnLevelComplete(ELevelCompleteReason levelCompleteReason)
    {
        _currentLevelItemCofigs = null;
        
        _currentLevelItem.onLevelCompleted -= OnLevelComplete;
        Destroy(_currentLevelItem.gameObject);
        
        onLevelComplete?.Invoke(levelCompleteReason);
        
        UIManager.Instance.ShowScreen(EScreenType.MAIN_MENU);
    }
}
