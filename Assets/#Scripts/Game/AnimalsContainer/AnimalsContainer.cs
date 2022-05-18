using System;
using RotaryHeart.Lib.SerializableDictionary;
using Spine.Unity;
using UnityEngine;

public class AnimalsContainer : Singleton<AnimalsContainer>
{
    private const string _iconFolder = "Icons";

    [SerializeField] private SpineAtlasDictionary _spineAtlasDictionary = new SpineAtlasDictionary();
    
    private string _iconPath => _iconFolder + "/";
    
    public void GetAnimalsIcon(EAnimalType animalType, Action<Sprite> callback)
    {
        string iconPath = _iconPath + animalType;
            
        LoadAnimalsSprite(iconPath, callback);
    }

    public SpineAtlasAsset GetAtlasByAnimalType(EAnimalType animalType)
    {
        SpineAtlasAsset atlasAsset = GetAnimalsAssetData(animalType).SpineAtlasAsset;

        return atlasAsset;
    }
    
    public string GetTailByAnimalType(EAnimalType animalType)
    {
        string tailsPath = GetAnimalsAssetData(animalType).TailsPath;

        return tailsPath;
    }


    private AnimalsAssetData GetAnimalsAssetData(EAnimalType animalType)
    {
        AnimalsAssetData animalsAssetData = null;
        _spineAtlasDictionary.TryGetValue(animalType, out animalsAssetData);
        
        return animalsAssetData;
    }
    
    private void LoadAnimalsSprite(string iconPath, Action<Sprite> callback)
    {
        var request = Resources.LoadAsync<Sprite>(iconPath);

        ResourceRequestExtensions.GetAwaiter(request).OnCompleted(
            delegate { callback?.Invoke((Sprite) request.asset); });
    }

    [Serializable]
    public class AnimalsAssetData
    {
        [SerializeField] private SpineAtlasAsset _spineAtlasAsset = null;
        [SerializeField] private string _tailsPath = null;
        public SpineAtlasAsset SpineAtlasAsset => _spineAtlasAsset;
        public string TailsPath => _tailsPath;
    }
    
    
    [Serializable]
    public class SpineAtlasDictionary : SerializableDictionaryBase<EAnimalType, AnimalsAssetData>
    {
            
    }
}
