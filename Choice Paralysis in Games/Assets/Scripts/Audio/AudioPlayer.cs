using UnityEngine;

namespace Scripts.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        internal static AudioPlayer Instance { get; private set; }

        [SerializeField] private AudioClip _skipClip;

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

        internal void PlaySoundEffect()
        {

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
