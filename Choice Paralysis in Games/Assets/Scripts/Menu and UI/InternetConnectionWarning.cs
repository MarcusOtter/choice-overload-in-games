using UnityEngine;

namespace Scripts.Menu_and_UI
{
    public class InternetConnectionWarning : MonoBehaviour
    {
        [SerializeField] private GameObject _internetWarning;

        private bool _paused;

        private void Update ()
        {
            if (Application.internetReachability == NetworkReachability.NotReachable)
            {
                SetPause(true);
                return;
            }

            if (_paused) { SetPause(false); }
        }

        private void SetPause(bool shouldPause)
        {
            if (_paused == shouldPause) { return; }

            if (shouldPause)
            {
                _internetWarning.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
                _internetWarning.SetActive(false);
            }

            _paused = shouldPause;
        }
    }
}
