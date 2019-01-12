using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Scripts.Game.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerDeathBehaviour : MonoBehaviour, IDamageable
    {
        // Grab deathcount from DataCollector
        // Make the death behaviour depend on the deathcount
        // 0 deaths: set time to 0, see if that works, then enable the death canvas & reload scene
        // 1 death: change to next scene

        private static int _deathCount;

        [SerializeField] internal UnityEvent OnDeath;
        [SerializeField] private PlayerGraphics _playerGraphics;
        [SerializeField] private float _gameOverTextDelay;
        [SerializeField] private Menu_and_UI.ObjectFader _gameOverTextFader;
        
        public void TakeDamage(int incomingDamage)
        {
            Logger.Instance.Log($"Player took {incomingDamage} damage.", gameObject);
            StartCoroutine(DeathBehaviour());
        }

        private IEnumerator DeathBehaviour()
        {
            OnDeath?.Invoke();

            if (_playerGraphics != null)
            {
                _playerGraphics.PlayDeathAnimation();
                yield return new WaitForSeconds(_gameOverTextDelay);
            }
            
            _gameOverTextFader.FadeComponentsOnFader();
            yield return new WaitForSeconds(2f);

            switch (_deathCount)
            {
                case 0:
                    SceneTransitioner.Instance.ReloadScene();
                    break;
                case 1:
                    SceneTransitioner.Instance.LoadNextScene();
                    break;
            }

            _deathCount++;
        }
    } 
}
