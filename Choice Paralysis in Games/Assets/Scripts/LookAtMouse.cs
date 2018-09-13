using UnityEngine;

namespace Scripts
{
    public class LookAtMouse : MonoBehaviour
    {
        private UserInputController _userInputController;

        private void Start()
        {
            _userInputController = UserInputController.Instance;
        }

        private void FixedUpdate()
        {
            transform.up = (Vector2) (_userInputController.MouseWorldPosition - transform.position).normalized;
        }
    }
}
