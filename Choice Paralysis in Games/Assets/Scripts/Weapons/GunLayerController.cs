using UnityEngine;

namespace Scripts.Weapons
{
    public class GunLayerController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _gunSpriteRenderer;

        private void Update()
        {
            _gunSpriteRenderer.sortingOrder = 
                transform.localEulerAngles.z > 90 && transform.localEulerAngles.z < 270 ? 2 : 1;
        }
    }
}
