using UnityEngine;

namespace Scripts
{
    [RequireComponent(typeof(Camera))]
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private float _smoothTime = 0.1f;
        [SerializeField] private float _maxDistance = 4;

        private UserInputController _userInput;
        private Transform _playerTransform;

        private Vector2 _velocity;

        private void Start()
        {
            _userInput = UserInputController.Instance;
            _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void FixedUpdate()
        {
            Vector2 midPoint = (_playerTransform.position + _userInput.MouseWorldPosition) / 2;

            Vector2 damp = Vector2.SmoothDamp(transform.position, midPoint, ref _velocity, _smoothTime);
            Vector3 targetPos = new Vector3(damp.x, damp.y, transform.position.z);
            transform.position = targetPos;

            /*
            if (Vector2.Distance(_playerTransform.position, targetPos) < _maxDistance)
            {
                transform.position = targetPos;
                Debug.Log("Fast");
            }
            else
            {
                damp = Vector2.SmoothDamp(transform.position, midPoint, ref _velocity, _smoothTime);
                _velocity = Vector2.ClampMagnitude(_velocity, 0.01f);
                targetPos = new Vector3(damp.x, damp.y, transform.position.z);
                transform.position = targetPos;
                Debug.Log("Slowed");
            }
            */
        }
    }
}
