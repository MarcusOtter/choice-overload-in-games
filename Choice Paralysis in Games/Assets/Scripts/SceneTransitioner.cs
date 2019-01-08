using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
    [RequireComponent(typeof(Animator))]
    public class SceneTransitioner : MonoBehaviour
    {
        internal static SceneTransitioner Instance { get; private set; }

        private Animator _animator;
        private CanvasGroup _canvasGroup;

        private int _leavingSceneAnimation;
        private int _enteringSceneAnimation;

        private IEnumerator _activeLoadingSequence;

        private void Awake()
        {
            SingletonCheck();

            _animator = GetComponent<Animator>();

            _leavingSceneAnimation = Animator.StringToHash("LeavingScene");
            _enteringSceneAnimation = Animator.StringToHash("EnteringScene");
        }

        internal void LoadNextScene()
        {
            ChangeSceneTo(SceneManager.GetActiveScene().buildIndex + 1);
        }

        internal void ReloadScene()
        {
            ChangeSceneTo(SceneManager.GetActiveScene().buildIndex);
        }

        private void ChangeSceneTo(int buildIndex)
        {
            if (_activeLoadingSequence != null) { return; }

            // Start new coroutine
            _activeLoadingSequence = LoadScene(buildIndex);
            StartCoroutine(_activeLoadingSequence);
        }

        private IEnumerator LoadScene(int buildIndex)
        {
            _animator.SetTrigger(_leavingSceneAnimation);
            StartCoroutine(FadeOutAllAudioSources());
            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(buildIndex);
            Logger.Instance.Log($"Changed scene from '{SceneManager.GetSceneByBuildIndex(buildIndex - 1).name}' to '{SceneManager.GetSceneByBuildIndex(buildIndex).name}'", gameObject);
            yield return new WaitForSeconds(1f);

            _animator.SetTrigger(_enteringSceneAnimation);
            _activeLoadingSequence = null;
        }

        // Potentially super expensive? idk.
        // It really only matters on the cave sound audio. So TODO!
        // Should also fade in all the audio sources when you enter a scene
        private IEnumerator FadeOutAllAudioSources()
        {
            var audioSources = FindObjectsOfType<AudioSource>();
            
            var startingVolumes = new float[audioSources.Length];

            for (var i = 0; i < audioSources.Length; i++)
            {
                if (audioSources[i] == null) { continue; }

                startingVolumes[i] = audioSources[i].volume;
            }

            var timer = 1f;

            while (timer > 0)
            {
                for (var i = 0; i < audioSources.Length; i++)
                {
                    if (audioSources[i] == null) { continue; }

                    audioSources[i].volume = startingVolumes[i] * timer;
                }

                timer -= 0.02f;
                yield return new WaitForSeconds(0.02f);
            }
        }

        private void SingletonCheck()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(transform.root.gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
