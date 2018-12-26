using UnityEngine;

namespace Scripts.Game.Player
{
    [RequireComponent(typeof(Animator))]
    public class PlayerGraphics : MonoBehaviour
    {
        [Header("Necessary references")]
        [SerializeField] private Transform _gunRotation;

        [Header("Weapon sprite settings")]
        [SerializeField] private SpriteRenderer _weaponSpriteRenderer;
        [SerializeField] private int _sortInFrontNumber = 11;
        [SerializeField] private int _sortBehindNumber = 9;
        [SerializeField] private Transform _bulletSpawnPoint;

        [Header("Body sprite settings")]
        [SerializeField] private string _rotationAnimationName = "Rotation";

        private Animator _bodyAnimator;

        private float _rotationZ;

        private float _bulletSpawnPointXOffset;

        private void Start()
        {
            _bodyAnimator = GetComponent<Animator>();

            _bulletSpawnPointXOffset = _bulletSpawnPoint.position.x;
        }

        private void Update()
        {
            _rotationZ = _gunRotation.localEulerAngles.z;

            // Set sorting order of gun
            _weaponSpriteRenderer.sortingOrder = _rotationZ > 90 && _rotationZ < 270
                ? _sortInFrontNumber
                : _sortBehindNumber;

            // Set the flipY of gun sprite
            _weaponSpriteRenderer.flipY = _rotationZ < 180;
            _bulletSpawnPoint.localPosition =
                new Vector3(_rotationZ < 180 ? -_bulletSpawnPointXOffset : _bulletSpawnPointXOffset,
                    _bulletSpawnPoint.localPosition.y, 0);

            // Update the float in the animator
            _bodyAnimator.SetFloat(_rotationAnimationName, _rotationZ);
        }
    }
}
