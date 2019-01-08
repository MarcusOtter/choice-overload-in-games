using System.Collections;
using UnityEngine;

namespace Scripts.Game.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerDeathBehaviour : MonoBehaviour, IDamageable
    {
        // Grab deathcount from DataCollector
        // Make the death behaviour depend on the deathcount
        // 0 deaths: set time to 0, see if that works, then enable the death canvas & reload scene
        // 1 death: change to next scene

        [SerializeField] private MonoBehaviour[] _componentsToDisable;

        private void Start()
        {
            
        }

        public void TakeDamage(int incomingDamage)
        {
            Logger.Instance.Log($"Player took {incomingDamage} damage.");
            StartCoroutine(DeathBehaviour());
        }

        private IEnumerator DeathBehaviour()
        {
            Time.timeScale = 0.5f;

            foreach (var component in _componentsToDisable)
            {
                component.enabled = false;
            }

            // Play player death animation

            // Wait some amount of time

            // Enable the death canvas

            // If 0 deaths
            SceneTransitioner.Instance.ReloadScene();

            // Else if 1 death
            //SceneTransitioner.Instance.LoadNextScene();
            yield break;
        }
    } 
}
