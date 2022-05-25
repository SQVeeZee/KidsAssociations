using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimalsDataConfigs", menuName = MenuPath, order = MenuOrder)]
public class AnimalsDataConfigs : ScriptableObject
{
    private const string MenuPath = "Configs/AnimalsDataConfigs";
    private const int MenuOrder = int.MinValue + 121;
    
    [SerializeField] private AnimalsDictionary animalsDictionary;
    
    public AnimalsDictionary AnimalsConfigsDictionary => animalsDictionary;
    
    [Serializable]
    public class AnimalsDictionary : SerializableDictionaryBase<EAnimalType, AnimalsConfigs>
    {
            
    }
}