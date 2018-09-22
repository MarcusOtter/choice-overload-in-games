using UnityEngine;

namespace Scripts
{
    public class LookAtMouse : MonoBehaviour
    {
        internal Vector2 LookDirection { get; private set; }
        private UserInputController _userInputController;

        private void Start()
        {
            _userInputController = UserInputController.Instance;
        }

        private void FixedUpdate()
        {
            LookDirection = (_userInputController.MouseWorldPosition - transform.position).normalized;
            transform.up = LookDirection;
        }
    }
}
