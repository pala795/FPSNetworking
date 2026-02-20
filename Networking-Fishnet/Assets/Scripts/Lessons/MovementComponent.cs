using FishNet.Managing.Logging;
using UnityEngine;
using FishNet.Object;

// NetworkBehaviour gives access to all network functionalities and forces gameObject to be NetworkObject.
public class MovementComponent : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private void Update()
    {
        Move();
    }

    // Using Client attribute to force execution only for the owner of this NetworkObject.
    // LoggingType = if and what message should be broadcast in console upon not meeting requirements.
    // RequireOwnership = if ownership is required to execute this method.
    [Client(Logging = LoggingType.Off, RequireOwnership = true)]
    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        var inputDirection = new Vector3(horizontal, 0f, vertical);
        if (inputDirection.magnitude > 1f)
            inputDirection.Normalize();

        Vector3 moveDirection = transform.TransformDirection(inputDirection);

        transform.position += _moveSpeed * Time.deltaTime * moveDirection;
    }

}