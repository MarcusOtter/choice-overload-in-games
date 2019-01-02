using UnityEngine;

namespace Scripts.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        /*
        internal static AudioPlayer Instance { get; private set; }

        [Header("Audio Settings")]
        [SerializeField] [Range(0, 1)] private float _mainVolume = 0.5f; // I would like this to be a setting somewhere.
        [SerializeField] private float _playerMaxHearingDistance;

        [Header("Audio sources")]
        [SerializeField] private AudioSource _soundEffectSource;
        [SerializeField] private AudioSource[] _musicSources;

        [Header("Audio clips")]
        [SerializeField] private AudioClip _skipClip;
        [SerializeField] private AudioClip _playerWeaponShotClip;
        [SerializeField] private AudioClip _bulletImpactClip;

        private Transform _playerTransform;

        private void Awake()
        {
            SingletonCheck();
        }

        private void Start()
        {
            _playerTransform = FindObjectOfType<Game.Player.PlayerMovement>()?.transform;

            foreach (var musicSource in _musicSources)
            {
                musicSource.volume *= _mainVolume;
            }
        }

        /// <summary>Plays an <see cref="AudioClip"/> with a volume (scaled with <see cref="_mainVolume"/>.</summary>
        internal void PlaySoundEffect(AudioClip audioClip, float volume)
        {
            _soundEffectSource.PlayOneShot(audioClip, volume * _mainVolume);
        }

        /// <summary>Plays a <see cref="SoundEffect"/> with it's volume.</summary>
        internal void PlaySoundEffect(SoundEffect soundEffect)
        {
            _soundEffectSource.PlayOneShot(GetClipFromSoundEffect(soundEffect), GetVolumeFromSoundEffect(soundEffect) * _mainVolume);
        }

        /// <summary>
        /// Plays a <see cref="SoundEffect"/> and scales it's volume depending on the distance between the <see cref="_playerTransform"/> and the <paramref name="sourcePosition"/>.
        /// </summary>
        internal void PlaySoundEffect3D(SoundEffect soundEffect, Vector3 sourcePosition)
        {
            var volume = ScaleByDistanceToPlayer(GetVolumeFromSoundEffect(soundEffect), sourcePosition) * _mainVolume;
            _soundEffectSource.PlayOneShot(GetClipFromSoundEffect(soundEffect), volume);
        }

        /// <summary>
        /// Scales a <see langword="float"/> depending on the <see cref="Vector2.Distance"/> between the <see cref="_playerTransform"/> and the <paramref name="sourcePosition"/>. 
        /// (if distance is 50% of the max hearing distance, <see langword="returns"/> <paramref name="valueToScale"/> * 0.5f
        /// </summary>
        private float ScaleByDistanceToPlayer(float valueToScale, Vector2 sourcePosition)
        {
            var distance = Vector2.Distance(_playerTransform.position, sourcePosition);

            if (distance > _playerMaxHearingDistance) { return 0; }

            return (1 - distance / _playerMaxHearingDistance) * valueToScale;
        }

        /// <summary>
        /// Divides the int value of a <see cref="SoundEffect"/> by 100 and returns the volume.
        /// (an int value of 50 = 0.5f)
        /// </summary>
        private float GetVolumeFromSoundEffect(SoundEffect soundEffect)
        {
            return ((int) soundEffect / 100f);
        }

        private AudioClip GetClipFromSoundEffect(SoundEffect soundEffect)
        {
            switch (soundEffect)
            {
                case SoundEffect.PlayerSkip:        return _skipClip;
                case SoundEffect.PlayerWeaponShot:  return _playerWeaponShotClip;
                case SoundEffect.BulletImpact:      return _bulletImpactClip;

                default:                            Logger.Instance.LogWarning($"Could not find clip for {soundEffect}!");
                                                    return null;
            }
        }

        private void SingletonCheck()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        */
    }
}
