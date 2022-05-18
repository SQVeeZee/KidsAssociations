using UnityEngine;

public class SoundsController : Singleton<SoundsController>
{
    [SerializeField] private SoundsConfigs _soundsConfigs = null;
    [SerializeField] private SoundsControllerSceneObject _soundsControllerSceneObject = null;

    public void Play(ESoundId soundId)
    {
        if (soundId == ESoundId.NONE)
        {
            return;
        }

        var soundConfig = _soundsConfigs.GetSoundConfig(soundId);
        if (soundConfig != null)
        {
            Play(soundConfig);
        }
    }

    private void Play(SoundConfig soundConfig)
    {
        var audioClipConfig = soundConfig.GetAudioClipConfig();

        Play(audioClipConfig);
    }
    
    private void Play(SoundConfig.AudioClipConfig audioClipConfig)
    {
        var audioClip = audioClipConfig.AudioClip;
        float volume = audioClipConfig.Volume;

        Play(audioClip, volume);
    }

    private void Play(AudioClip audioClip, float volume)
    {
        var soundsControllerSceneObject = Instantiate(_soundsControllerSceneObject, transform);

        soundsControllerSceneObject.PlayOneShot(audioClip, volume);
        
        DestroySoundsObject(soundsControllerSceneObject, audioClip.length);
    }

    private void DestroySoundsObject(SoundsControllerSceneObject soundsControllerSceneObject, float time)
    {
        Destroy(soundsControllerSceneObject.gameObject, time);
    }
}
