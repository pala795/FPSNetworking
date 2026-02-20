using FishNet.Component.Animating;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class HealthComponent : NetworkBehaviour
{
    [SerializeField] private int _maxHealth = 100;

    public readonly SyncVar<int> Health = new();
    // Synchronize animator
    private NetworkAnimator _networkAnimator;

    #region Unity Callbacks
    private void Awake()
    {
        Health.Value = _maxHealth;

        _networkAnimator = GetComponentInChildren<NetworkAnimator>();
        Health.OnChange += OnHealthChanged;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(10);
        }
    }
    #endregion
    
    [ServerRpc(RequireOwnership = false)]
    public void TakeDamage(int amount)
    {
        Health.Value -= amount;
    }

    private void ReceiveHit()
    {
        if(!_networkAnimator) return;
        
        _networkAnimator.SetTrigger("HasBeenHit");
    }

    private void OnHealthChanged(int prev, int next, bool asServer)
    {
        Health.Value = next;
        ReceiveHit();
    }
}