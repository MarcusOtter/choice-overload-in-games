using UnityEngine;

namespace Scripts
{
    public class GunLayerController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _gunSpriteRenderer;

        private void Start()
        {
            _gunSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        private void Update()
        {
            _gunSpriteRenderer.sortingOrder = 
                transform.localEulerAngles.z > 90 && transform.localEulerAngles.z < 270 ? 2 : 1;

            Debug.Log(transform.localEulerAngles.z);
        }
    }
}
