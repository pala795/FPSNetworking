using System;
using FishNet.Connection;
using FishNet.Managing.Logging;
using FishNet.Object;
using NUnit.Framework;
using UnityEngine;

public class LookComponent : NetworkBehaviour
{
    [SerializeField] private Canvas _hudCanvas;
    [SerializeField] private Transform _cameraSocket;
    [SerializeField] private GameObject _cameraPrefab;
    [SerializeField] private float mouseSensitivity = 100f;

    public Camera Camera { get; private set; }
    private Transform _root;
    private float _xRotation;

    public override void OnOwnershipClient(NetworkConnection prevOwner)
    {
        base.OnOwnershipClient(prevOwner);
        if (!IsOwner)
        {
            _hudCanvas.enabled = false;
        }
        CreateCamera();
    }

    private void Awake()
    {
        _root = transform.root;
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        
        if(IsOwner)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    
    private void Update()
    {
        Look();
    }

    [Client(Logging = LoggingType.Off, RequireOwnership = true)]
    private void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        Camera.transform.parent.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
        _root.Rotate(Vector3.up * mouseX);
    }

    [Client(Logging = LoggingType.Off, RequireOwnership = true)]
    private void CreateCamera()
    {
        Camera = Instantiate(_cameraPrefab, _cameraSocket).GetComponent<Camera>();
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}