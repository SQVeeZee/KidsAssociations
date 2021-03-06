using UnityEngine;

public class UIMainMenuScreen : BaseScreen
{
    [Header("Screen")]
    [SerializeField] private LevelsGalleryController _levelsGalleryController = null;

    private LevelsController _levelsController = null;
    
    private void Awake()
    {
        SetData();
    }

    private void OnEnable()
    {
        _levelsController.onLevelsConfigsSet += Initialize;
        _levelsGalleryController.onButtonClick += OnClickLevelIcon;
    }

    private void OnDisable()
    {
        _levelsController.onLevelsConfigsSet -= Initialize;
        _levelsGalleryController.onButtonClick -= OnClickLevelIcon;
    }

    private void Initialize(LevelsConfigs levelsConfigs)
    {
        _levelsGalleryController.Initialize(levelsConfigs);
    }

    private void OnClickLevelIcon(int levelId)
    {
        _levelsController.CreateLevelById(levelId);
    }

    protected override void SetData()
    {
        _levelsController = LevelsController.Instance;
    }
}
