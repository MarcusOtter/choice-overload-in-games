using System.Collections;
using UnityEngine;

public class DamageableObject : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHp;

    private int _hp;

    private Coroutine _hitAnimation;

    private void Start()
    {
        _hp = _maxHp;
    }

    public void TakeDamage(int incomingDamage)
    {
        _hp -= incomingDamage;

        if (_hitAnimation == null)
        {
            _hitAnimation = StartCoroutine(DamagedAnimation());
        }

        CheckDeath();
    }

    // This could probably be replaced with actual animation clips
    // which would allow for more custom animation & reuseability
    private IEnumerator DamagedAnimation()
    {
        var startPos = transform.localPosition;
        var elapsed = 0f;
        var flick = true;

        while (elapsed < 0.2f)
        {
            var offset = flick ? Vector3.left / 20 : Vector3.right / 20;
            transform.localPosition = startPos + offset;

            flick = !flick;

            elapsed += Time.fixedDeltaTime * 3;
            yield return new WaitForSeconds(Time.fixedDeltaTime * 3);
        }

        transform.position = startPos;
        _hitAnimation = null;
    }

    private void CheckDeath()
    {
        if (_hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
