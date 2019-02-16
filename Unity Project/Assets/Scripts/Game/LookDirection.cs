using Scripts.Game.Weapons;
using UnityEngine;

namespace Scripts.Game
{
    public class LookDirection : MonoBehaviour
    {
        [SerializeField] private LookDirectionSource _lookDirectionSource;

        private Weapon _weapon;
        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            switch (_lookDirectionSource)
            {
                case LookDirectionSource.WeaponRotation:
                    _weapon = transform.root.GetComponentInChildren<Weapon>();
                    break;

                default:
                case LookDirectionSource.Velocity:
                    _rigidbody = transform.root.GetComponent<Rigidbody2D>();
                    break;
            }
        }

        private void Update()
        {
            Vector2 lookDirection;

            switch (_lookDirectionSource)
            {
                case LookDirectionSource.WeaponRotation:
                    lookDirection = _weapon.AimDirection;
                    break;

                default:
                case LookDirectionSource.Velocity:
                    lookDirection = _rigidbody.velocity.normalized;
                    break;
            }

            transform.up = lookDirection;
        }
    }

    [System.Serializable]
    internal enum LookDirectionSource
    {
        WeaponRotation,
        Velocity
    }
}
