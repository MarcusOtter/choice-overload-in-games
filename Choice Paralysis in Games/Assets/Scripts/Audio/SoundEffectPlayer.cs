using UnityEngine;

namespace Scripts.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffectPlayer : MonoBehaviour
    {
        internal const float MainVolume = 0.5f;
        internal const float PlayerHearingDistance = 20f;

        [Tooltip("Should the volume be dependant on how far away the player is?")]
        [SerializeField] private bool _scaleVolumeWithPlayerDistance = true;

        [Tooltip("Should the volume scale it's pitch depending on the Time.timeScale?")]
        [SerializeField] private bool _scalePitchWithTimeScale = true;

        private float _activeSoundEffectVolume;

        private AudioSource _source;
        private Transform _playerTransform;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();

            if (_scaleVolumeWithPlayerDistance)
            {
                _playerTransform = GameObject.FindGameObjectWithTag("Player").transform.root;
            }
        }

        private void Update()
        {
            if (_scaleVolumeWithPlayerDistance)
            {             
                _source.volume = CalculateVolume();
            }

            if (_scalePitchWithTimeScale)
            {
                _source.pitch = Time.timeScale;
            }
        }

        internal void PlaySoundEffect(SoundEffect soundEffect)
        {
            float volume = soundEffect.RandomizeVolume
                ? Random.Range(soundEffect.MinVolume, soundEffect.MaxVolume)
                : soundEffect.MinVolume;

            float pitch = soundEffect.RandomizePitch
                ? Random.Range(soundEffect.MinPitch, soundEffect.MaxPitch)
                : 1;

            _activeSoundEffectVolume = volume; // Must be assigned before CalculateVolume() is called.

            _source.pitch = pitch;
            _source.volume = CalculateVolume();
            _source.clip = soundEffect.Clip;
            _source.loop = soundEffect.ShouldLoop;

            _source.Play();
        }

        private float CalculateVolume()
        {
            var volume = _activeSoundEffectVolume * MainVolume;

            if (_scaleVolumeWithPlayerDistance)
            {
                volume *= PlayerDistanceMultiplier();
            }

            return volume;
        }

        /// <summary>
        /// Returns a value between 0 and 1 that represents the Vector2.Distance between 
        /// the player and the position of this object divided by the player hearing distance.
        /// </summary>
        private float PlayerDistanceMultiplier()
        {
            var distance = Vector2.Distance(_playerTransform.position, transform.position);

            if (distance > PlayerHearingDistance) { return 0; }

            return (1 - distance / PlayerHearingDistance);
        }
    }
}
