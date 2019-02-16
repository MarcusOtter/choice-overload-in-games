using UnityEngine;

namespace Scripts.Game
{
    /// <summary>
    /// Allows animations and UnityEvents to modify the timescale.
    /// For example, the player death animation.
    /// </summary>
    public class TimeScaler : MonoBehaviour
    {
        [SerializeField] private float _timeScale = 1f;

        private void Awake()
        {
            Time.timeScale = 1f;
        }
        
        private void Update()
        {
            Time.timeScale = _timeScale;
        }

        public void SetTimeScale(float newTimeScale)
        {
            _timeScale = newTimeScale;
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
        }
    }
}
