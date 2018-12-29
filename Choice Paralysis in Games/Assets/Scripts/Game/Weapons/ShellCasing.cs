using UnityEngine;

namespace Scripts.Game.Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ShellCasing : MonoBehaviour
    {
        [SerializeField] private float _spawnRotationMin = -45f;
        [SerializeField] private float _spawnRotationMax = 45f;

        [SerializeField] private float _velocityMin = 3f;
        [SerializeField] private float _velocityMax = 5f;

        [SerializeField] private float _rotationSpeedMin = -50f;
        [SerializeField] private float _rotationSpeedMax = 50f;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start ()
        {
            transform.rotation *= Quaternion.Euler(0, 0, Random.Range(_spawnRotationMin, _spawnRotationMax));

            _rigidbody.AddForce(transform.up * Random.Range(_velocityMin, _velocityMax), ForceMode2D.Impulse);

            _rigidbody.AddTorque(Random.Range(_rotationSpeedMin, _rotationSpeedMax), ForceMode2D.Impulse);
        }
    }
}
