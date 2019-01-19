using UnityEngine;

namespace Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioVolumeAutoScaler : MonoBehaviour
    {
        private AudioSource _source;

        private float _startingVolume;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _startingVolume = _source.volume;
        }

        private void Update()
        {
            _source.volume = _startingVolume * SoundEffectPlayer.MainVolume;
        }
    }
}
