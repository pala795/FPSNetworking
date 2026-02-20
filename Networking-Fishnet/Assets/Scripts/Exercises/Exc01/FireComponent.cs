using System;
using FishNet.Managing.Logging;
using FishNet.Object;
using UnityEngine;

public class FireComponent : NetworkBehaviour
{
    private LookComponent _lookComponent;
    //private LineRenderer _lineRenderer;

    //private void Awake()
    //{
    //    _lineRenderer = GetComponent<LineRenderer>();
    //}

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
        //_lineRenderer.SetPosition(0, ray.origin);

        if (Physics.Raycast(ray, out var hit))
        {
            Debug.Log(hit.transform.name);
            //_lineRenderer.SetPosition(1, hit.point);
            
            if (hit.transform.TryGetComponent(out HealthComponent health))
            {
                health.TakeDamage(5);
            }
            if(hit.transform.TryGetComponent(out Barrel target))
            {
                Debug.Log("Barrel hit!");
                target.Explode();
            }
        }
    }
}
