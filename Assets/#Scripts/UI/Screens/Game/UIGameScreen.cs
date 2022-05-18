using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIGameScreen : BaseScreen
{
    [Header("Screen")]
    [SerializeField] private Button _homeButton = null;
    [SerializeField] private Transform _tailsRootTransform = null;
    [SerializeField] private CanvasGroup _tailsCanvasGroup = null;
    
    private LevelsController _levelsController = null;
    private TimeController _timeController = null;
    private TutorialController _tutorialController = null;
    private TailsPanel _tailsPanel = null;

    private BaseLevelItem _levelItem = null;
    private LevelItemConfigs _levelItemConfigs = null;
    
    private void Awake()
    {
        AddListener();
        
        SetData();
    }

    private void OnEnable()
    {
        _levelsController.onLevelItemConfigsSet += OnLevelItemConfigsSet;
        _levelsController.onLevelItemSet += OnLevelItemSet;
        _levelsController.onLevelComplete += ResetTails;
        
        _timeController.onReachPoint += OnTimeReachPoint;
    }

    private void OnDisable()
    {
        _levelsController.onLevelItemConfigsSet -= OnLevelItemConfigsSet;
        _levelsController.onLevelItemSet -= OnLevelItemSet;
        _levelsController.onLevelComplete -= ResetTails;

        _timeController.onReachPoint -= OnTimeReachPoint;
    }
    
    private void AddListener()
    {
        _homeButton.onClick.AddListener(OpenMainMenu);
    }
    
    private void OnLevelItemConfigsSet(LevelItemConfigs levelItemConfigs)
    {
        _levelItemConfigs = levelItemConfigs;
        
        _tailsPanel = InstantiateTailPanel(_levelItemConfigs.TailsPanel);
        _tailsPanel.onTailPressed += OnTailPressed;
    }

    private void OnLevelItemSet(BaseLevelItem levelItem)
    {
        _levelItem = levelItem;

        _tailsCanvasGroup.alpha = 1;
        
        _levelItem.onStartFinalLevelAnimation += HideTails;
    }


    private void OnTimeReachPoint(ETimePointType timePointType)
    {
        switch (timePointType)
        {
            case ETimePointType.KEYPOINT_1:
                DoPulseTails();
                break;
            
            case ETimePointType.KEYPOINT_2:
                _tutorialController.PlayTutorial(GetCorrectTailScreenPosition());
                break;
        }
    }

    private void DoPulseTails()
    {
        _tailsPanel.PulseTails();
    }

    private Vector2 GetCorrectTailScreenPosition()
    {
        Vector2 tailsScreenPosition = default;

        tailsScreenPosition = _tailsPanel.GetTargetTailScreenPosition(_levelItemConfigs.AnimalType);
        
        return tailsScreenPosition;
    }


    private void OpenMainMenu()
    {
        _levelsController.OnLevelSkipped();
    }
    
    private void HideTails()
    {
        _levelItem.onStartFinalLevelAnimation -= HideTails;
        
        _tailsCanvasGroup.DOFade(0, 0.3f);
    }

    private TailsPanel InstantiateTailPanel(TailsPanel tailPanel)
    {
        return Instantiate(tailPanel, _tailsRootTransform);
    }

    private void OnTailPressed(EAnimalType animalType)
    {
        if (animalType == _levelItemConfigs.AnimalType)
        {
            _tutorialController.ResetTutorial();
        }
        
        _levelItem.DefineClickedButton(animalType);
    }

    private void ResetTails(ELevelCompleteReason levelCompleteReason)
    {
        _levelItem = null;
        
        _tailsPanel.onTailPressed -= OnTailPressed;

        Destroy(_tailsPanel.gameObject);
    }

    protected override void SetData()
    {
        _levelsController = LevelsController.Instance;
        _timeController = TimeController.Instance;
        _tutorialController = TutorialController.Instance;
    }
}
