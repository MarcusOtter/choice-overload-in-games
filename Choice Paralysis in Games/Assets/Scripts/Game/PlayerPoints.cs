using TMPro;
using UnityEngine;

namespace Scripts.Game
{
    public class PlayerPoints : MonoBehaviour
    {
        internal int CurrentPoints { get; private set; }

        [SerializeField] private TextMeshProUGUI _scoreText;

        internal void ModifyPoints(int delta)
        {
            CurrentPoints += delta;

            UpdateScoreText();
        }

        private void UpdateScoreText()
        {
            if (_scoreText == null) { return; }

            _scoreText.text = CurrentPoints.ToString("0");
        }
    }
}
