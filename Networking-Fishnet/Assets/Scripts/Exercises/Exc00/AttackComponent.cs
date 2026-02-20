using FishNet.Component.Animating;
using FishNet.Object;
using System;
using UnityEngine;

public class AttackComponent : NetworkBehaviour
{
    private HurtBox _hurtBox;
    private NetworkAnimator _networkAnimator;
    
    [NonSerialized] public bool CanAttack;

    private void Awake()
    {
        _networkAnimator = GetComponentInChildren<NetworkAnimator>();
        _hurtBox = GetComponentInChildren<HurtBox>();
        CanAttack = true;
    }

    private void Update()
    {
        if(!CanAttack) return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
        }
    }

    // Add logic and make this method synchronized
    private void Attack()
    {
        _networkAnimator.SetTrigger("HasAttacked");
        // add your code
    }

    public void EnableHurtBox(bool value)
    {
        _hurtBox.EnableCollider(value);
    }
}
