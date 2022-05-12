using System;
using UnityEngine;

public class LevelsGalleryController : MonoBehaviour
{
    public event Action<int> onButtonClick = null;
    
    [SerializeField] private Transform _iconsRootTransform = null;
    [SerializeField] private LevelIcon _levelIconPrefab = null;

    private LevelIcon[] _levelIcons = default;
    
    public void Initialize(LevelsConfigs levelsConfigs)
    {
        int levelsCount = levelsConfigs.GetLevelConfigsCount;

        _levelIcons = new LevelIcon[levelsCount];

        for (int i = 0; i < levelsConfigs.GetLevelConfigsCount; i++)
        {
            LevelItemConfigs levelItemConfigs = levelsConfigs.LevelItemConfigs[i];
            LevelIcon levelIcon = InstantiateIconPrefab();
            int configId = i;

            AnimalsContainer.Instance.GetAnimalsIcon(levelItemConfigs.AnimalType,
                delegate(Sprite iconSprite) { levelIcon.Initialize(configId, iconSprite); });

            levelIcon.onLevelsButtonClick += OnButtonClick;
        }
    }

    private LevelIcon InstantiateIconPrefab()
    {
        return Instantiate(_levelIconPrefab, _iconsRootTransform);
    }

    private void OnButtonClick(int id)
    {
        onButtonClick?.Invoke(id);
    }
}