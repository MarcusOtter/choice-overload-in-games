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
            StartCoroutine(FadeOutAudio());
            yield return new WaitForSeconds(1f);

            SceneManager.LoadScene(buildIndex);
            Logger.Instance.Log($"Changed scene from '{SceneManager.GetSceneByBuildIndex(buildIndex - 1).name}' to '{SceneManager.GetSceneByBuildIndex(buildIndex).name}'", gameObject);
            yield return new WaitForSeconds(1f);

            _animator.SetTrigger(_enteringSceneAnimation);
            StartCoroutine(FadeInAudio());
            _activeLoadingSequence = null;
        }

        private IEnumerator FadeOutAudio()
        {
            while (Audio.SoundEffectPlayer.MainVolume > 0)
            {
                Audio.SoundEffectPlayer.MainVolume -= 0.04f;
                yield return new WaitForSeconds(0.02f);
            }

            Audio.SoundEffectPlayer.MainVolume = 0;
        }

        private IEnumerator FadeInAudio()
        {
            while (Audio.SoundEffectPlayer.MainVolume < 1)
            {
                Audio.SoundEffectPlayer.MainVolume += 0.04f;
                yield return new WaitForSeconds(0.02f);
            }

            Audio.SoundEffectPlayer.MainVolume = 1;
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
