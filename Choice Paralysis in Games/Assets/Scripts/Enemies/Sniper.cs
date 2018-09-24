using System.Collections;
using Scripts.Weapons; // bad
using UnityEngine;

namespace Scripts.Enemies
{
    // TODO: Make weapon script that handles all the logic for the sniper
    public class Sniper : Enemy
    {
        [Header("References")]
        [SerializeField] private Material _lineRendererMaterial;
        [SerializeField] private Color _targetFoundColor;
        [SerializeField] private Bullet _bulletPrefab;

        [Header("Weapon settings")]
        [SerializeField] private float _rotateSpeed = 0.03f;

        private Transform _weaponTransform;
        private LineRenderer _lineRenderer;

        private Vector2? _shootTarget;

        private bool _shooting;

        private void Awake()
        {
            _lineRenderer = Instantiate(new GameObject("Weapon", typeof(LineRenderer)), transform).GetComponent<LineRenderer>();

            _weaponTransform = _lineRenderer.transform;

            _lineRenderer.widthCurve = AnimationCurve.Constant(0, 1, 0.1f);
            _lineRenderer.material = _lineRendererMaterial;
            _lineRenderer.positionCount = 2;

            StartCoroutine(RayStartupAnimation());
        }

        private IEnumerator RayStartupAnimation()
        {
            _lineRenderer.widthMultiplier = 0;
            while (_lineRenderer.widthMultiplier < 1)
            {
                _lineRenderer.widthMultiplier += 0.05f;
                yield return new WaitForSeconds(0.05f);
            }
        }

        private void Update()
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, _lineRenderer.transform.up * 100 + transform.position);

            if (_shooting) { return; }

            if (_shootTarget == null)
            {
                RotateTowardsPlayer(_weaponTransform, _rotateSpeed);
                _lineRenderer.startColor = Color.white;
                _lineRenderer.endColor = Color.white;

                var raycastHit = Physics2D.Raycast(transform.position, _weaponTransform.up, 100f);
                Debug.DrawLine(transform.position, _weaponTransform.up * 100f, Color.blue);
                _shootTarget = (raycastHit && raycastHit.transform.CompareTag("Player"))
                    ? raycastHit.point
                    : (Vector2?)null;
            }
            else
            {
                _lineRenderer.startColor = _targetFoundColor;
                _lineRenderer.endColor = _targetFoundColor;
                _shooting = true;
                StartCoroutine(ChargeUpAndShoot());
            }
        }

        private IEnumerator ChargeUpAndShoot()
        {
            var startWidth = _lineRenderer.widthMultiplier;

            while (_lineRenderer.widthMultiplier < startWidth * 3)
            {
                _lineRenderer.widthMultiplier += 0.2f;
                yield return new WaitForSeconds(0.02f);
            }

            while (_lineRenderer.widthMultiplier > startWidth)
            {
                _lineRenderer.widthMultiplier -= 0.2f;
                yield return new WaitForSeconds(0.02f);
            }

            Shoot();
            _shooting = false;
            _shootTarget = null;
        }

        private void Shoot()
        {
            Instantiate(_bulletPrefab, transform.position + _weaponTransform.up * 2, _weaponTransform.rotation).Shoot(10, 50f);
        }

        private void RotateTowardsPlayer(Transform transformToRotate, float speed)
        {
            transformToRotate.up = Vector3.Lerp(transformToRotate.up, (PlayerTransform.position - transformToRotate.position).normalized, speed);
        }
    }
}

