using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
    public class PlayerGraphics : MonoBehaviour
    {
        [Header("Necessary references")]
        [SerializeField] private Transform _gunRotation;

        [Header("Gun sprite settings")]
        [SerializeField] private SpriteRenderer _gunSpriteRenderer;
        [SerializeField] private int _sortInFrontNumber = 11;
        [SerializeField] private int _sortBehindNumber = 9;

        [Header("Body sprite settings")]
        [SerializeField] private string _rotationAnimationName = "Rotation";

        private Animator _bodyAnimator;

        private float _rotationZ;

        private void Start()
        {
            _bodyAnimator = GetComponent<Animator>();
        }

        private void Update()
        {
            _rotationZ = _gunRotation.localEulerAngles.z;

            // Set sorting order of gun
            _gunSpriteRenderer.sortingOrder = _rotationZ > 90 && _rotationZ < 270
                ? _sortInFrontNumber
                : _sortBehindNumber;

            // Set the flipY of gun sprite
            _gunSpriteRenderer.flipY = _rotationZ < 180;

            // Update the float in the animator
            _bodyAnimator.SetFloat(_rotationAnimationName, _rotationZ);
        }
    }
}
