using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Game.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerDeathBehaviour : MonoBehaviour, IDamageable
    {
        internal static int DeathCount { get; private set; }

        [SerializeField] internal UnityEvent OnDeath;
        [SerializeField] private PlayerGraphics _playerGraphics;
        
        public void TakeDamage(int incomingDamage)
        {
            Logger.Instance.Log($"Player took {incomingDamage} damage.", gameObject);
            StartCoroutine(DeathBehaviour());
        }

        private IEnumerator DeathBehaviour()
        {
            _playerGraphics?.PlayDeathAnimation();
            FindObjectOfType<Timer>()?.Stop();

            OnDeath?.Invoke();

            yield return new WaitForSeconds(3f); // Wait for death animation and text

            switch (DeathCount)
            {
                case 0:
                    SceneTransitioner.Instance.ReloadScene();
                    break;
                case 1:
                    SceneTransitioner.Instance.LoadNextScene();
                    break;
            }

            DeathCount++;
        }
    } 
}
