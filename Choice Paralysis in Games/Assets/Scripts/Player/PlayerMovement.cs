using UnityEngine;

namespace Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;

        private UserInputController _inputController;
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _inputController = UserInputController.Instance;
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = new Vector2(_inputController.HorizontalAxis * _movementSpeed,
                _inputController.VerticalAxis * _movementSpeed);
        }
    }
}

