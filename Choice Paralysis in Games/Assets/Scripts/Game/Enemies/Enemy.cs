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
        [SerializeField] private AudioClip _damagedAudio;

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
        }

        protected virtual void FixedUpdate()
        {
            if (IsDead) { return; }

            Move();
        }

        protected virtual void Move()
        {
            // Overwritten

            //ThisDoesntExist.Something();
            //ExaminationHandler.ExaminationModeNumber = 1;
        }
        
        public virtual void TakeDamage(int incomingDamage)
        {
            Health -= incomingDamage;
            OnDamageTaken?.Invoke();
            PlayDamagedSound();

            if (Health <= 0) { Die(); }
        }

        private void PlayDamagedSound()
        {
            // TODO: Remake audio player
            Audio.AudioPlayer.Instance.PlaySoundEffect(_damagedAudio, 1f);
        }

        protected void Die()
        {
            IsDead = true;
            OnDeath?.Invoke();
        }
    }
}

