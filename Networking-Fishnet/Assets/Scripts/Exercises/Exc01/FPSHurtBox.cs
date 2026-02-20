using System;
using System.Collections;
using FishNet.Object;
using FishNet.Object.Synchronizing;

public class FPSHurtBox : NetworkBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private Collider _collider;
    [SerializeField] private MeshRenderer _meshRenderer;
    public readonly SyncVar<bool> HurtBoxEnabler = new SyncVar<bool>();

    private void Awake()
    {
        HurtBoxEnabler.OnChange += OnHurtBoxEnablerChanged;
        _collider.enabled = false;
        _meshRenderer.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject == gameObject) return;
        if (other.TryGetComponent(out HealthComponent health))
        {
            Debug.Log("Explode");
            health.TakeDamage(_damage);
        }
    }
    private void OnHurtBoxEnablerChanged(bool oldValue, bool newValue, bool asServer)
    {
        _collider.enabled = newValue;
        _meshRenderer.enabled = newValue;
    }
}

