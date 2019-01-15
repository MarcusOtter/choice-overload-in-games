using UnityEngine;

namespace Scripts.Examination
{
    public class DataMailer : MonoBehaviour
    {
        [SerializeField] private ExaminationEntry _entry;

        private void Start()
        {
            if (DataCollector.Instance != null)
            {
                _entry = DataCollector.Instance.Entry;
            }
        }
    }
}
