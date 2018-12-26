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

        [Header("Movement animation settings")]
        [SerializeField] private string _speedAnimationParameter = "Speed";

        private int _speedParameterHash;

        private Animator _bodyAnimator;
        private Rigidbody2D _rigidbody;

        private float _rotationZ;

        private float _bulletSpawnPointXOffset;

        private void Start()
        {
            _bodyAnimator = GetComponent<Animator>();
            _rigidbody = transform.root.GetComponent<Rigidbody2D>();

            _bulletSpawnPointXOffset = _bulletSpawnPoint.position.x;
            _speedParameterHash = Animator.StringToHash(_speedAnimationParameter);
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

            // Flip the weapon spawner offset (for weapons that don't spawn from the middle of the sprite)
            _bulletSpawnPoint.localPosition =
                new Vector3(_rotationZ < 180 ? -_bulletSpawnPointXOffset : _bulletSpawnPointXOffset,
                    _bulletSpawnPoint.localPosition.y, 0);

            // Update the float in the animator
            _bodyAnimator.SetFloat(_speedParameterHash, Mathf.Abs(_rigidbody.velocity.x));
        }
    }
}
