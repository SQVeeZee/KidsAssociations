using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundConfig", menuName = MenuPath, order = MenuOrder)]
public class SoundConfig : ScriptableObject
{
    private const string MenuPath = "Configs/SoundConfig";
    private const int MenuOrder = -498;


    [SerializeField] private ESoundId _soundId = ESoundId.NONE;

    [Space]
    [SerializeField] private AudioClipConfig[] _audioClips = new AudioClipConfig[0];


    public ESoundId SoundId => _soundId;


    public AudioClipConfig GetAudioClipConfig()
    {
        if (_audioClips.Length > 0)
        {
            var random = new System.Random();

            int id = random.Next(0, _audioClips.Length);
            return _audioClips[id];
        }

        return null;
    }
    
    public AudioClipConfig GetAudioClipConfig(int audioId, ref bool isReverse)
    {
        var id = audioId % (_audioClips.Length - 1);

        if (id == 0 && audioId != 0)
        {
            isReverse = !isReverse;
        }

        if (isReverse)
        {
            return _audioClips[(_audioClips.Length - 1) - id];
        }
        else
        {
            return _audioClips[id];
        }
    }


    [Serializable]
    public class AudioClipConfig
    {
        [SerializeField] private AudioClip _audioClip = null;
        [SerializeField] private float _volume = 1f;


        public AudioClip AudioClip => _audioClip;
        public float Volume => _volume;
    }
}
