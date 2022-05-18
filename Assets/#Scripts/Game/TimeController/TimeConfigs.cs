using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[CreateAssetMenu(fileName = "TimeConfigs", menuName = MenuPath, order = MenuOrder)]
public class TimeConfigs : ScriptableObject
{
    private const string MenuPath = "Configs/TimeConfigs";
    private const int MenuOrder = -499;

    [SerializeField] private KeyTimePointDictionary _configs = new KeyTimePointDictionary();
    
    public KeyTimePointDictionary Configs => _configs;
        
    [Serializable]
    public class KeyTimePointDictionary : SerializableDictionaryBase<ETimePointType, int>
    {
            
    }
}