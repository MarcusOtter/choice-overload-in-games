using System;
using TMPro;
using UnityEngine;

namespace Scripts.Game
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Timer : MonoBehaviour
    {
        internal double FinalTime { get; private set; }

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
                SetUIText();
            }
        }

        private void SetUIText()
        {
            _timerGUI.text = GetCurrentTime().ToString("00.00");
        }

        // ReSharper disable once CompareOfFloatsByEqualityOperator
        private bool FinalTimeHasChanged => FinalTime != default(double);

        private double GetCurrentTime()
        {
            return FinalTimeHasChanged
                ? FinalTime
                : Math.Round((Time.time - _levelStartTime), 2);
        }

        internal void Stop()
        {
            FinalTime = GetCurrentTime();
            SetUIText();
        }
    }
}
