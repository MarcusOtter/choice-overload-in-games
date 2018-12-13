using UnityEngine;

namespace Scripts.Menu_and_UI
{
    /// <summary>
    /// Listens for player input and progresses to the next scene if the correct input is given.
    /// </summary>
    public class SceneProgressInputListener : MonoBehaviour
    {
        [SerializeField] private KeyCode _progressButton = KeyCode.Space;
    
        private bool _listeningForInput;

        private void Update()
        {
            if (!_listeningForInput) { return; }

            if (Input.GetKeyDown(_progressButton))
            {
                LoadNextScene();
            }
        }

        // Can be used by buttons directly
        public void LoadNextScene()
        {
            SceneTransitioner.Instance.LoadNextScene();
        }

        public void ListenForInput(bool listen)
        {
            _listeningForInput = listen;
        }
    }
}
