using System.Collections;
using UnityEngine;

namespace Scripts.Weapons
{
    [RequireComponent(typeof(LineRenderer))]
    public class Sniper : Weapon
    {
        internal bool IsShooting { get; private set; }

        [Header("Sniper settings")]
        [SerializeField] private float _rotateSpeed = 0.03f;
        [SerializeField] private Color _targetFoundColor = Color.red;

        //[SerializeField] private Transform _shootPointsParent;

        private LineRenderer _lineRenderer;

        private Vector2? _shootTarget;
        private Transform _playerTransform;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 2;

            _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform.root;
        }

        protected override void WeaponBehaviour()
        {
            UpdateLineRenderer();

            RotateTowardsPlayer(_rotateSpeed);

            if (IsShooting) { return; }

            if (_shootTarget == null)
            {
                FindTarget();

                _lineRenderer.startColor = Color.clear;
                _lineRenderer.endColor = Color.clear;
            }
            else
            {
                IsShooting = true;
                StartCoroutine(WaitAndShoot());

                _lineRenderer.startColor = _targetFoundColor;
                _lineRenderer.endColor = _targetFoundColor;
            }
        }

        private void FindTarget()
        {
            Debug.DrawRay(transform.position, transform.up * 100f, Color.blue);

            var raycastHit = Physics2D.Raycast(transform.position, transform.up, 100f);
            _shootTarget = raycastHit && raycastHit.transform.CompareTag("Player")
                ? raycastHit.point
                : (Vector2?) null;
        }

        private void RotateTowardsPlayer(float speed)
        {
            transform.up = Vector3.RotateTowards(transform.up, (_playerTransform.position - transform.position).normalized, speed * Time.deltaTime, 0);
            AimDirection = transform.up;
        }

        private IEnumerator WaitAndShoot()
        {
            yield return new WaitForSeconds(ShootCooldown);

            Instantiate(BulletPrefabToSpawn, transform.position + transform.up * 2, transform.rotation).Shoot(BulletDamage, BulletSpeed);
            IsShooting = false;
            _shootTarget = null;
        }

        private void UpdateLineRenderer()
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, transform.up * 100f + transform.position);
        }
    }
}
