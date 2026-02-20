using System;
using FishNet.Managing.Logging;
using FishNet.Object;
using UnityEngine;

public class FireComponent : NetworkBehaviour
{
    private LookComponent _lookComponent;

    public override void OnStartClient()
    {
        base.OnStartClient();
        _lookComponent = GetComponentInParent<LookComponent>();
        
        GiveOwnership(_lookComponent.Owner);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    [Client(Logging = LoggingType.Off, RequireOwnership = true)]
    private void Fire()
    {
        var centerScreen = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = _lookComponent.Camera.ScreenPointToRay(centerScreen);

        if (Physics.Raycast(ray, out var hit))
        {
            Debug.Log(hit.transform.name);
            
            if (hit.transform.TryGetComponent(out HealthComponent health))
            {
                health.TakeDamage(5);
            }
        }
    }
}
