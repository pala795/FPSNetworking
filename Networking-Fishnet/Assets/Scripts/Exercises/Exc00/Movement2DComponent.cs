using FishNet.Component.Animating;
using FishNet.Managing.Logging;
using FishNet.Object;
using System;
using UnityEngine;

public class Movement2DComponent : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    // Synchronize animator
    private NetworkAnimator _networkAnimator;

    [NonSerialized] public bool CanMove;

    private void Awake()
    {
        _networkAnimator = GetComponentInChildren<NetworkAnimator>();
        CanMove = true;
    }

    private void Update()
    {
        if (!CanMove) return;
        
        Move();
    }

    // Using Client attribute to force execution only for the owner of this NetworkObject.
    // LoggingType = if and what message should be broadcast in the console upon not meeting requirements.
    // RequireOwnership = if ownership is required to execute this method.
    [Client(Logging = LoggingType.Off, RequireOwnership = true)]
    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");

        var moveDirection = new Vector3(horizontal, 0f, 0f);
        if (moveDirection.magnitude > 1f)
            moveDirection.Normalize();
        
        transform.position += _moveSpeed * Time.deltaTime * moveDirection;;

        _networkAnimator.Animator.SetBool("IsWalking", horizontal != 0f);
    }
}