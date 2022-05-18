using System;
using UnityEngine;

public abstract class BaseLevelItem : MonoBehaviour, ILevelItem
{
    public event Action onClickTailButton = null;
    public event Action<ELevelCompleteReason> onLevelCompleted;
    public event Action onStartFinalLevelAnimation = null;

    [SerializeField] private EAnimalType _animalType = default;
    [SerializeField] protected Transform _animalRoot = null;

    private LevelItemConfigs _levelItemConfigs = null;

    private AnimalController _animalController = null;
    private AnimalsContainer _animalsContainer = null;

    private EAnimalType _lastTimeClickedTailType = EAnimalType.NONE;
    
    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        UnSubscribe();
    }

    public void StartLevel(LevelItemConfigs levelItemConfigs)
    {
        _levelItemConfigs = levelItemConfigs;

        _animalsContainer = AnimalsContainer.Instance;
        
        _animalController = InstantiateAnimal();
        _animalController.Initialize(_animalType);
    }

    public void DefineClickedButton(EAnimalType animalType)
    {
        onClickTailButton?.Invoke();

        if (IsClickedTailRepeat(animalType))
        {
            _animalController.DoAnimalSad();
        }
        else
        {
            if (animalType == _levelItemConfigs.AnimalType)
            {
                onStartFinalLevelAnimation?.Invoke();

                SoundsController.Instance.Play(ESoundId.MOTHER_CA);
                
                _animalController.SetRightTail(delegate { OnLevelCompleted(ELevelCompleteReason.WIN); });
            }
            else
            {
                SoundsController.Instance.Play(ESoundId.MOTHER_IA);

                _animalController.SetNewTail(animalType);
            }
        }

        _lastTimeClickedTailType = animalType;
    }

    private bool IsClickedTailRepeat(EAnimalType clickedTailType)
    {
        return clickedTailType == _lastTimeClickedTailType;
    }

    private AnimalController InstantiateAnimal()
    {
        var animalController = Instantiate(_levelItemConfigs.AnimalController, _animalRoot);

        var worldPoint = CameraManager.Instance.GetCameraItem(ECameraType.GAME).Camera
            .ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 4f, 0));
        
        worldPoint.z = 1;
        
        animalController.transform.position = worldPoint;
        
        return animalController;
    }

    private void OnLevelCompleted(ELevelCompleteReason levelCompleteReason)
    {
        onLevelCompleted?.Invoke(levelCompleteReason);
    }

    private void Subscribe()
    {
        TimeController.Instance.onReachPoint += OnTimerReachKeyPoint;
    }

    private void UnSubscribe()
    {
        TimeController.Instance.onReachPoint -= OnTimerReachKeyPoint;
    }

    private void OnTimerReachKeyPoint(ETimePointType keyPointType)
    {
        switch (keyPointType)
        {
            case ETimePointType.KEYPOINT_1: 
                break;
            
            case ETimePointType.KEYPOINT_2: 
                break;
        }
    }
}
