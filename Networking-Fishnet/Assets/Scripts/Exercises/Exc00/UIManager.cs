using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using FishNet.Transporting;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : NetworkBehaviour
{
    [SerializeField] private HealthBarUI[] _healthBarUIs;

    private readonly SyncDictionary<int, HealthBarUI> clientAssignments = new();

    public override void OnStartServer()
    {
        base.OnStartServer();

        ServerManager.OnRemoteConnectionState += OnRemoteConnectionState;
    }
    private void OnRemoteConnectionState(NetworkConnection conn, RemoteConnectionStateArgs args)
    {
        if (args.ConnectionState == RemoteConnectionState.Started)
        {
            AssignHealthBar(conn);
        }
    }

    private void AssignHealthBar(NetworkConnection conn)
    {
        if (clientAssignments.ContainsKey(conn.ClientId)) return;

        int assignedIndex = clientAssignments.Count;
        if (assignedIndex >= _healthBarUIs.Length) return;

        var selectedBar = _healthBarUIs[assignedIndex];
        clientAssignments.TryAdd(conn.ClientId, selectedBar);

        selectedBar.SubscribeHealthBar(conn);
    }
}