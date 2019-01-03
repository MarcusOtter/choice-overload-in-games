using UnityEngine;

namespace Scripts.Menu_and_UI
{
    public class SceneProgresser : MonoBehaviour
    {
        [Header("Triggers")]
        [SerializeField] private bool _progressOnTriggerEnter;
        [SerializeField] private bool _progressOnCollisionEnter;

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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_progressOnTriggerEnter) { return; }
            LoadNextScene();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!_progressOnCollisionEnter) { return; }
            LoadNextScene();
        }
    }
}
