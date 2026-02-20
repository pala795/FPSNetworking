using UnityEngine;
using System.Collections;

public class FPSHurtBox : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private Collider _collider;



    public void EnableCollider(bool value)
    {
        _collider.enabled = value;
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject == gameObject) return;
        if (other.TryGetComponent(out HealthComponent health))
        {
            health.TakeDamage(_damage);
        }
    }
}

