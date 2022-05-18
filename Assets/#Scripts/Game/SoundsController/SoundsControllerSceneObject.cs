using UnityEngine;

public class SoundsControllerSceneObject : MonoBehaviour
{
    [SerializeField] private AudioSource _soundsAudioSource = null;

    public void PlayOneShot(AudioClip audioClip, float volume)
    {
        _soundsAudioSource.PlayOneShot(audioClip, volume);
    }

    public void StopSounds()
    {
        _soundsAudioSource.Stop();
    }
}
