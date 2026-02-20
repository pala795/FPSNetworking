using UnityEngine;
using System.Collections;

public class FPSHurtBox : MonoBehaviour
{
    [SerializeField] private int _damage = 10;
    private Collider _collider;

    private IEnumerator ResetAfterCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        EnableCollider(false);
    }

    public void EnableCollider(bool value)
    {
        _collider.enabled = value;
        StartCoroutine(ResetAfterCooldown(0.5f));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == gameObject) return;
        if (other.TryGetComponent(out HealthComponent health))
        {
            health.TakeDamage(_damage);
        }
    }
}

