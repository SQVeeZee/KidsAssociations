using System;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

    [CreateAssetMenu(fileName = "SoundsConfigs", menuName = MenuPath, order = MenuOrder)]
    public class SoundsConfigs : ScriptableObject
    {
        private const string MenuPath = "Configs/SoundsConfigs";
        private const int MenuOrder = -499;


        [SerializeField] private SoundsDictionary _configs = new SoundsDictionary();
        
        
        public SoundConfig GetSoundConfig(ESoundId soundId)
        {
            if (_configs.TryGetValue(soundId, out var soundConfig) && soundConfig != null)
            {
                return soundConfig;
            }

            return null;
        }
        
        [Serializable]
        public class SoundsDictionary : SerializableDictionaryBase<ESoundId, SoundConfig>
        {
            
        }
    }
