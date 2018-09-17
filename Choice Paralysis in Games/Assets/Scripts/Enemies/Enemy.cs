using UnityEngine;

namespace Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        internal Vector2 Direction { get; private set; }

        [SerializeField] private float _movementSpeed;

        private float _health;

        private Rigidbody2D _rigidbody;
        private Transform _playerTransform;
        private EnemyGraphics _enemyGraphics;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
            _enemyGraphics = GetComponentInChildren<EnemyGraphics>();
        }

        private void FixedUpdate()
        {
            if (_playerTransform == null) { return; }
            Direction = (_playerTransform.position - transform.position).normalized;
            _rigidbody.velocity = Direction * _movementSpeed;
        }

        public void TakeDamage(int incomingDamage)
        {
            
            _health -= incomingDamage;
            _enemyGraphics?.PlayDamagedAnimation();
        }
    }
}

