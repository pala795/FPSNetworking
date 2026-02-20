using UnityEngine;
using FishNet.Object;

public class Barrel : NetworkBehaviour
{
    private FPSHurtBox _hurtBox;
    private void Awake()
    {
        _hurtBox = GetComponentInChildren<FPSHurtBox>();
    }
    public void Explode()
    {
        if (_hurtBox == null) return;
        _hurtBox.EnableCollider(true);
        Debug.Log("Barrel exploded!");
        transform.gameObject.SetActive(false);
    }
}
