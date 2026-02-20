using System;
using System.Collections;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class FPSHurtBox : NetworkBehaviour
{
    [SerializeField] private int _damage = 10;
    [SerializeField] private Collider _collider;
    [SerializeField] private MeshRenderer _meshRenderer;
    public readonly SyncVar<bool> HurtboxEnabler = new SyncVar<bool>();

    private void Awake()
    {
        HurtboxEnabler.OnChange += OnHurtboxEnablerChanger;
    }

    private void OnHurtboxEnablerChanger(bool prev, bool next, bool asServer)
    {
        _collider.enabled = next;
        _meshRenderer.enabled = next;
    }

    public void EnableCollider(bool value)
    {
        _collider.enabled = value;
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
}

