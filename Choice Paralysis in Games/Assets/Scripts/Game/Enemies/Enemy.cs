using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Game.Enemies
{
    /// <summary>Base class for enemies</summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour, IDamageable
    {
        [Header("General Enemy Settings")]
        [SerializeField] protected float MaxHealth;
        [SerializeField] protected float MovementSpeed;
        [SerializeField] protected UnityEvent OnDeath;

        protected bool IsDead;
        protected float Health;

        protected Rigidbody2D Rigidbody;
        protected Transform PlayerTransform;
        protected EnemyGraphics EnemyGraphics;

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            EnemyGraphics = GetComponentInChildren<EnemyGraphics>();

            Health = MaxHealth;
        }

        protected virtual void Start()
        {
            PlayerTransform = GameObject.FindGameObjectWithTag(EnvironmentVariables.PlayerTag)?.transform.root;
        }

        protected virtual void FixedUpdate()
        {
            if (IsDead) { return; }

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

        protected void Die()
        {
            IsDead = true;
            OnDeath.Invoke();
        }
    }
}

