using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(Camera))]
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _smoothTime = 0.3f;
        [SerializeField] private float _maxDistance = 4;

        private UserInputController _userInput;
        private Transform _playerTransform;

        private Vector2 _velocity;

        private void Start()
        {
            _userInput = UserInputController.Instance;
            _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
        }

        private void FixedUpdate()
        {
            if (_playerTransform == null) { return; }

            FollowPlayer();
        }

        /// <summary>
        /// Smoothly follows the player and offsets the position depending on where the cursor is relative to the player
        /// </summary>
        private void FollowPlayer()
        {
            // The point between the cursor and the player
            Vector2 midPoint = ((Vector2)_playerTransform.position + (Vector2)_userInput.MouseWorldPosition) / 2;

            // Offset between mid point and player, with clamped magnitude
            Vector2 offset = midPoint - (Vector2)_playerTransform.position;
            offset = Vector2.ClampMagnitude(offset, _maxDistance);

            Vector3 targetPos = new Vector3(_playerTransform.position.x + offset.x, _playerTransform.position.y + offset.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, _smoothTime);
        }
    }
}
