using UnityEngine;

namespace Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        internal Vector2 LookDirection { get; private set; }

        [Header("General Enemy Settings")]
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _maxHealth;

        private float _health;

        private Rigidbody2D _rigidbody;
        private Transform _playerTransform;
        private EnemyGraphics _enemyGraphics;

        protected virtual void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
            _enemyGraphics = GetComponentInChildren<EnemyGraphics>();

            _health = _maxHealth;
        }

        protected virtual void FixedUpdate()
        {
            Move();
        }

        protected virtual void Move()
        {
            if (_playerTransform == null) { return; }
            LookDirection = (_playerTransform.position - transform.position).normalized;
            _rigidbody.velocity = LookDirection * _movementSpeed;
        }

        public virtual void TakeDamage(int incomingDamage)
        {
            _health -= incomingDamage;
            _enemyGraphics?.PlayDamagedAnimation();

            if (_health <= 0) { Die(); }
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}

