using System.Collections;
using UnityEngine;

namespace Scripts.Game.Weapons
{
    [RequireComponent(typeof(LineRenderer))]
    public class Sniper : Weapon
    {
        internal bool IsShooting { get; private set; }

        [Header("Sniper settings")]
        [SerializeField] private Transform _shootPointsParent;
        [SerializeField] private float _afterShotCooldown = 3;
        [SerializeField] private float _rotateSpeed = 0.5f;
        [SerializeField] private Color _targetFoundColor = Color.red;

        [Header("Sniper audio")]
        [SerializeField] private Audio.SoundEffect _chargeUpSound;

        private LineRenderer _lineRenderer;

        private Vector2? _shootTarget;
        private Transform _playerTransform;

        private Coroutine _targetFoundBehaviour;

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.positionCount = 2;

            _playerTransform = GameObject.FindGameObjectWithTag(EnvironmentVariables.PlayerTag)?.transform.root;
        }

        protected override void WeaponBehaviour()
        {
            SetLineRendererPosition();

            var playerDirection = (_playerTransform.position - transform.position).normalized;

            var hit = Physics2D.Raycast(transform.position, playerDirection, 100f);

            // Assure there is nothing between the sniper and the player
            if (!hit) { return; }
            if (!hit.transform.CompareTag(EnvironmentVariables.PlayerTag)) { return; }

            // If the player isn't already found
            if (_targetFoundBehaviour == null)
            {
                transform.up = playerDirection;
                SetLineRendererPosition();

                _targetFoundBehaviour = StartCoroutine(TargetFoundBehaviour());
            }
        }

        private IEnumerator TargetFoundBehaviour()
        {
            EnableLineRenderer(true);
            Audio.SoundEffectPlayer.PlaySoundEffect(_chargeUpSound, transform);
            IsShooting = true;

            float elapsedTime = 0;
            while (elapsedTime < ShootDelay)
            {
                RotateTowardsPlayer(_rotateSpeed);

                elapsedTime += Time.deltaTime;
                yield return new WaitForSeconds(Time.deltaTime);
            }

            Shoot();
            EnableLineRenderer(false);

            yield return new WaitForSeconds(_afterShotCooldown);
            _targetFoundBehaviour = null;
        }

        private void RotateTowardsPlayer(float speed)
        {
            transform.up = Vector3.RotateTowards(transform.up, (_playerTransform.position - transform.position).normalized, speed * Time.deltaTime, 0);
            AimDirection = transform.up;
        }

        private void Shoot()
        {
            for (int i = 0; i < _shootPointsParent.childCount; i++)
            {
                var shootPoint = _shootPointsParent.GetChild(i);
                Instantiate(BulletPrefabToSpawn, shootPoint.position + shootPoint.up * 2, shootPoint.rotation).Shoot(BulletDamage, BulletSpeed);
            }

            Audio.SoundEffectPlayer.PlaySoundEffect(ShootSound, transform);
            IsShooting = false;
        }

        private void EnableLineRenderer(bool enable)
        {
            if (enable)
            {
                _lineRenderer.startColor = _targetFoundColor;
                _lineRenderer.endColor = _targetFoundColor;
            }
            else
            {
                _lineRenderer.startColor = Color.clear;
                _lineRenderer.endColor = Color.clear;
            }
        }

        private void SetLineRendererPosition()
        {
            _lineRenderer.SetPosition(0, transform.position);
            _lineRenderer.SetPosition(1, transform.up * 100f + transform.position);
        }
    }
}
