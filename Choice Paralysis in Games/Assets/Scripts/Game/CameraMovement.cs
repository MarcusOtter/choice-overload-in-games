using System;
using System.Collections;
using Scripts.Game.Weapons;
using UnityEngine;

namespace Scripts.Game
{
    public class CameraMovement : MonoBehaviour
    {
        [Header("Follow mouse & player settings")]
        [SerializeField] [Range(0, 1)] private float _smoothTime = 0.1f;
        [SerializeField] private float _maxDistance = 4;

        [Header("Screen shake settings")]
        [SerializeField] [Range(0.01f, 1f)] private float _smallShakeStrength = 0.12f;
        [SerializeField] [Range(0, 1)] private float _shakeSpeed = 0.5f;

        private UserInputController _userInput;
        private Transform _playerTransform;
        private Weapon _playerWeapon;

        private Vector2 _velocity;

        private Coroutine _runningCameraShake;

        private void OnEnable()
        {
            _userInput = UserInputController.Instance;
            if (_userInput == null) { return; }
            _userInput.OnAttackKeyDown += CameraShakeSmall;
        }

        private void Start()
        {
            _userInput = UserInputController.Instance;
            _playerTransform = GameObject.FindGameObjectWithTag(EnvironmentVariables.PlayerTag)?.transform.root;
            _playerWeapon = _playerTransform?.root.GetComponentInChildren<Weapon>();
        }

        private void FixedUpdate()
        {
            if (_playerTransform == null) { return; }

            FollowPlayer();
        }


        private void CameraShakeSmall(object sender, EventArgs args)
        {
            if (_runningCameraShake != null) { return; }

            _runningCameraShake = StartCoroutine(CameraShakeSmall());
        }

        private IEnumerator CameraShakeSmall()
        {
            var camTransform = Camera.main.transform;
         
            // The point to put the camera at
            Vector3 shakePoint = (_playerWeapon.AimDirection * -1) * _smallShakeStrength;
            shakePoint.z = camTransform.localPosition.z;

            // Move towards the shakepoint while far away from it
            while (Vector3.Distance(camTransform.localPosition, shakePoint) > 0.1f)
            {
                camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, shakePoint, _shakeSpeed);
                yield return new WaitForEndOfFrame();
            }

            camTransform.localPosition = shakePoint;

            yield return new WaitForSeconds(0.1f);

            Vector3 middlePoint = new Vector3(0, 0, camTransform.localPosition.z);

            // Move towards the middle of the screen again while far away from the middle
            while(Vector3.Distance(camTransform.localPosition, middlePoint) > 0.1f)
            {
                camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, middlePoint, _shakeSpeed);
                yield return new WaitForEndOfFrame();
            }

            camTransform.localPosition = middlePoint;
            _runningCameraShake = null;
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

        private void OnDisable()
        {
            if (_userInput == null) { return; }
            _userInput.OnAttackKeyDown -= CameraShakeSmall;
        }
    }
}
