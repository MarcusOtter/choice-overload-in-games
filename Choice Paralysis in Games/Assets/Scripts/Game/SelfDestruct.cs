using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField] private float _selfDestructDelay = 5;

    private void Awake ()
    {
        Destroy(gameObject, _selfDestructDelay);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
