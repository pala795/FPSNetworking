using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HurtBox : MonoBehaviour
{
    [SerializeField] private int _damageAmount = 10;
    
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    public void EnableCollider(bool value)
    {
        _collider.enabled = value;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == gameObject) return;

        if (other.TryGetComponent(out HealthComponent health))
        {
            health.TakeDamage(_damageAmount);
        }
    }
}