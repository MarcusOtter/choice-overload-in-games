using UnityEngine;

namespace Scripts.Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        // Can't be internal because that's more restrective than protected
        public Vector2 LookDirection { get; protected set; }

        [Header("General Enemy Settings")]
        [SerializeField] protected float MovementSpeed;
        [SerializeField] protected float MaxHealth;

        protected float Health;

        protected Rigidbody2D Rigidbody;
        protected Transform PlayerTransform;
        protected EnemyGraphics EnemyGraphics;

        protected virtual void Start()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            PlayerTransform = GameObject.FindGameObjectWithTag("Player")?.transform;
            EnemyGraphics = GetComponentInChildren<EnemyGraphics>();

            Health = MaxHealth;
        }

        protected virtual void FixedUpdate()
        {
            Move();
        }

        protected virtual void Move()
        {
            // Overwritten
        }

        public virtual void TakeDamage(int incomingDamage)
        {
            Health -= incomingDamage;
            EnemyGraphics?.PlayDamagedAnimation();

            if (Health <= 0) { Die(); }
        }

        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}

