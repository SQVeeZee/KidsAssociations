using System;
using System.Collections;
using UnityEngine;

public class TimeController : Singleton<TimeController>
{
    [SerializeField] private TimeConfigs _timeConfigs = null;
    
    public event Action<ETimePointType> onReachPoint = null;

    private int _currentTime = 0;
    private Coroutine _timerCoroutine = null;

    private int _highestTimePoint = 0;
    private BaseLevelItem _levelItem = null;
    
    protected override void Awake()
    {
        base.Awake();

        _highestTimePoint = GetHighestTimePoint();
    }

    private void OnEnable()
    {
        LevelsController.Instance.onLevelItemSet += OnLevelItemSet;
        LevelsController.Instance.onLevelComplete += OnLevelCompleted;
    }

    private void OnDisable()
    {
        LevelsController.Instance.onLevelItemSet -= OnLevelItemSet;
        LevelsController.Instance.onLevelComplete -= OnLevelCompleted;
    }
    
    private void StartTimer()
    {
        _currentTime = 0;
        
        _timerCoroutine = StartCoroutine(DoCounter());
    }
    
    private void ResetTimer()
    {
        if (_timerCoroutine != null)
        {
            StopCoroutine(_timerCoroutine);
        }
    }
    
    private void RestartTimer()
    {
        ResetTimer();
        
        StartTimer();
    }

    private void OnLevelItemSet(BaseLevelItem levelItem)
    {
        _levelItem = levelItem;
        _levelItem.onClickTailButton += RestartTimer;

        StartTimer();
    }

    private void OnLevelCompleted(ELevelCompleteReason levelCompleteReason)
    {
        _levelItem.onClickTailButton -= RestartTimer;
        _levelItem = null;
        
        ResetTimer();        
    }

    private int GetHighestTimePoint()
    {
        int highestTimePoint = 0;
        
        foreach (var config in _timeConfigs.Configs)
        {
            if (config.Value > highestTimePoint)
            {
                highestTimePoint = config.Value;
            }
        }

        return highestTimePoint;
    }

    private IEnumerator DoCounter()
    {
        for (int i = 0; i <= _highestTimePoint; i++)
        {
            yield return new WaitForSeconds(1);

            _currentTime++;

            var pointType = IsKeyPoint(_currentTime);

            if (pointType != ETimePointType.NONE)
            {
                onReachPoint?.Invoke(pointType);
            }
        }
    }

    private ETimePointType IsKeyPoint(int time)
    {
        foreach (var config in _timeConfigs.Configs)
        {
            if (config.Value == time)
            {
                return config.Key;
            }
        }

        return ETimePointType.NONE;
    }
}

public enum ETimePointType
{
    NONE = 0,
    
    KEYPOINT_1,
    KEYPOINT_2,
}
