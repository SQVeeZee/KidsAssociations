using System;
using Tools.Resources;
using UnityEngine;

public class AnimalsContainer : Singleton<AnimalsContainer>
{
    private const string _iconFolder = "Icons";
    private const string _tailFolder = "Tails";
    private const string _tailsPrefix = "tail";

    [SerializeField] private AnimalsDataConfigs _animalsDataConfigs = null;
    
    private ResourceController _resourceController = null;

    protected override void Awake()
    {
        base.Awake();

        Initialize();
    }
    
    public void GetAnimalsIcon(EAnimalType animalType, Action<Sprite> callback)
    {
        string iconPath = _iconFolder + "/" + animalType;
            
        _resourceController.LoadAsync(iconPath, callback);
    }
    
    public void GetAnimalsTail(EAnimalType animalType, Action<Sprite> callback)
    {
        string tailPath = _tailFolder + "/" + animalType + " " + _tailsPrefix;
            
        _resourceController.LoadAsync(tailPath, callback);
    }
    
    public string GetSlotTailName(EAnimalType animalType)
    {
        string tailsPath = GetAnimalsAssetData(animalType).TailsPath;

        return tailsPath;
    }

    private AnimalsConfigs GetAnimalsAssetData(EAnimalType animalType)
    {
        AnimalsConfigs animalsConfigs = null;
        
        _animalsDataConfigs.AnimalsConfigsDictionary.TryGetValue(animalType, out animalsConfigs);
        
        return animalsConfigs;
    }
    
    private void Initialize()
    {
        _resourceController = ResourceController.Instance;
    }
}