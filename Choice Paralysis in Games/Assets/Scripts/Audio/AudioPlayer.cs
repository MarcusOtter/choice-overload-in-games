using UnityEngine;

namespace Scripts.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        internal static AudioPlayer Instance { get; private set; }

        [Header("Audio sources")]
        [SerializeField] private AudioSource _soundEffectSource;


        [Header("Audio clips")]
        [SerializeField] private AudioClip _skipClip;
        [SerializeField] private AudioClip _playerWeaponShotClip;

        private void Awake()
        {
            SingletonCheck();
        }

        private void Start()
        {

        }

        private void Update()
        {

        }

        /*
        // Called by animation event
        public void PlaySkipSound()
        {
            PlaySoundEffect(AudioIdentifier.PlayerSkip);
        }
        */

        internal void PlaySoundEffect(AudioIdentifier audioId)
        {
            _soundEffectSource.PlayOneShot(GetClipFromIdentifier(audioId), GetVolumeFromIdentifier(audioId));
        }

        private AudioClip GetClipFromIdentifier(AudioIdentifier audioId)
        {
            switch (audioId)
            {
                case AudioIdentifier.PlayerSkip:        return _skipClip;
                case AudioIdentifier.PlayerWeaponShot:  return _playerWeaponShotClip;
                default:                                return null;
            }
        }

        private float GetVolumeFromIdentifier(AudioIdentifier audioId)
        {
            return ((int) audioId / 100f);
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
    }
}
