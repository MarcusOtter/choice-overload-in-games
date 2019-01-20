using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Menu_and_UI
{
    [RequireComponent(typeof(Button))]
    public class OpenLinkOnClick : MonoBehaviour
    {
        [SerializeField] private string _link;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(() => Application.OpenURL(_link));
        }

    }
}
