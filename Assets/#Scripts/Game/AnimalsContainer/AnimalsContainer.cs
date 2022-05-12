using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

public class AnimalsContainer : Singleton<AnimalsContainer>
{
    private const string _iconFolder = "Icons";
    private const string _tailsFolder = "TAILS";

    [SerializeField] private TailsDictionary _tailsDictionary = null;
    
    private string _iconPath => _iconFolder + "/";
    private string _tailsPath => _tailsFolder + "/";

    private string _tail = " tail";
    
    public void GetAnimalsIcon(EAnimalType animalType, Action<Sprite> callback)
    {
        string iconPath = _iconPath + animalType;
            
        LoadAnimalsSprite(iconPath, callback);
    } 
    
    public void GetAnimalsTail(EAnimalType animalType, Action<Sprite> callback)
    {
        string tailPath = _tailsPath + animalType + _tail;
            
        LoadAnimalsSprite(tailPath, callback);
    }
    
    private void LoadAnimalsSprite(string iconPath, Action<Sprite> callback)
    {
        var request = Resources.LoadAsync<Sprite>(iconPath);

        ResourceRequestExtensions.GetAwaiter(request).OnCompleted(
            delegate { callback?.Invoke((Sprite) request.asset); });
    }
    
    [Serializable]
    public class TailsDictionary: SerializableDictionaryBase<EAnimalType, string> { }
}
