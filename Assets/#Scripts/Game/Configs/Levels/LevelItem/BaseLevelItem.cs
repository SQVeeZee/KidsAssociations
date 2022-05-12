using System;
using UnityEngine;

public abstract class BaseLevelItem : MonoBehaviour, ILevelItem
{
    public event Action<ELevelCompleteReason> onLevelCompleted;

    private bool _isLevelCompleted = false;

    public void SetLevelItemConfig(LevelItemConfigs levelItemConfigs)
    {
        
    }
    
    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        UnSubscribe();
    }

    private void Awake()
    {
        InitializeLevelData();
    }

    public void StartLevel()
    {
        
    }

    private void InitializeLevelData()
    {
        _isLevelCompleted = false;
    }


    private void OnLevelCompleted(ELevelCompleteReason levelCompleteReason)
    {
        if (!_isLevelCompleted)
        {
            onLevelCompleted?.Invoke(levelCompleteReason);
            _isLevelCompleted = true;
        }
    }


    private void Subscribe()
    {
    }

    private void UnSubscribe()
    {
    }
}
