using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Game.Enemies
{
    /// <summary>Base class for enemies</summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class Enemy : MonoBehaviour, IDamageable
    {
        [Header("General Enemy Settings")]
        [SerializeField] protected float MaxHealth;
        [SerializeField] protected float MovementSpeed;

        [Header("Audio settings")]
        [SerializeField] private Audio.SoundEffect _damagedSound;

        [Header("Enemy events")]
        [SerializeField] protected UnityEvent OnDeath;
        [SerializeField] protected UnityEvent OnDamageTaken;

        protected bool IsDead;
        protected float Health;

        protected Rigidbody2D Rigidbody;
        protected Transform PlayerTransform;

        protected virtual void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Health = MaxHealth;
        }

        protected virtual void Start()
        {
            PlayerTransform = GameObject.FindGameObjectWithTag(EnvironmentVariables.PlayerTag)?.transform.root;

            // Ensure this script is disabled when the player dies.
            FindObjectOfType<Player.PlayerDeathBehaviour>()?.OnDeath.AddListener(() => enabled = false);
        }

        protected virtual void FixedUpdate()
        {
            if (IsDead) { return; }

            Move();
        }

        protected abstract void Move();
        
        public void TakeDamage(int incomingDamage)
        {
            Health -= incomingDamage;
            OnDamageTaken?.Invoke();
            PlayDamagedSound();

            if (Health <= 0) { Die(); }
        }

        private void PlayDamagedSound()
        {
            Audio.SoundEffectPlayer.PlaySoundEffect(_damagedSound, transform);
        }

        protected void Die()
        {
            IsDead = true;
            FindObjectOfType<Examination.DataCollector>()?.IncrementKillCount();
            OnDeath?.Invoke();
        }
    }
}

