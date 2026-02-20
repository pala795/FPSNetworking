using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class Rpc_Lesson : NetworkBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            RpcSendChat("Hello world!");

        if (Input.GetKeyDown(KeyCode.Alpha2))
            RpcSendChatNoOwner("Hey world!");

        if (Input.GetKeyDown(KeyCode.Alpha3))
            UpdateOwner();
    }

    [ServerRpc]
    private void RpcSendChat(string msg)
    {
        Debug.Log($"Client {Owner.ClientId} sent '{msg}' on the server.");
    }

    [ServerRpc(RequireOwnership = false)]
    private void RpcSendChatNoOwner(string msg, NetworkConnection conn = null)
    {
        Debug.Log($"Received {msg} on the server from connection {conn.ClientId}.");
    }
    
    private void UpdateOwner()
    {
        //Even though this example passes in owner, you can send to
        //any connection that is an observer.
        RpcSetColor(Owner, Random.ColorHSV());
    }

    // Only from the server (or host)
    [TargetRpc]
    private void RpcSetColor(NetworkConnection conn, Color newColor)
    {
        //This might be something you only want the owner to be aware of.
        GetComponentInChildren<MeshRenderer>().material.color = newColor;
    }
}