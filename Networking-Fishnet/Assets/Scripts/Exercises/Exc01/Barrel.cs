using UnityEngine;
using System.Collections;
using FishNet.Object;

public class Barrel : NetworkBehaviour
{

    private FPSHurtBox _hurtBox;
    private void Awake()
    {
        _hurtBox = GetComponentInChildren<FPSHurtBox>(includeInactive: true);
    }
    [ServerRpc(RequireOwnership = false)]
    public void Explode()
    {
        Debug.Log("Exploding barrel...");
        if (_hurtBox == null) return;
        _hurtBox.HurtBoxEnabler.Value = true;
        Debug.Log("Barrel exploded!");
        StartCoroutine(ResetAfterCooldown(1f));
    }
    private IEnumerator ResetAfterCooldown(float cooldown)
    {
        Debug.Log("Resetting barrel after cooldown...");
        yield return new WaitForSeconds(cooldown);
        Debug.Log("Reset barrel...");
        Despawn(transform.gameObject);
    }
}

