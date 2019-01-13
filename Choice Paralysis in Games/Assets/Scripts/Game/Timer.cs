using TMPro;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Timer : MonoBehaviour
    {
        internal float FinalTime { get; private set; }

        private TextMeshProUGUI _timerGUI;

        private float _levelStartTime;

        private void Awake()
        {
            _timerGUI = GetComponent<TextMeshProUGUI>();
            _levelStartTime = Time.time;
        }

        private void Update()
        {
            if (!FinalTimeHasChanged)
            {
                _timerGUI.text = GetCurrentTime().ToString("00.00");
            }
        }

        private bool FinalTimeHasChanged => FinalTime != default(float);

        private float GetCurrentTime()
        {
            return FinalTimeHasChanged
                ? FinalTime
                : (Time.time - _levelStartTime);
        }

        internal void Stop()
        {
            FinalTime = GetCurrentTime();
        }
    }
}
