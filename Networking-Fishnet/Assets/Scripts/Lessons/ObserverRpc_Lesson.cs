using FishNet.Connection;
using FishNet.Object;
using UnityEngine;

public class ObserverRpc_Lesson : NetworkBehaviour
{
    private void FixedUpdate()
    {
        if(IsServerStarted) 
            RpcSetNumber(Time.frameCount);
    }

    [ObserversRpc(ExcludeOwner = true)]
    private void RpcSetNumber(int next)
    {
        Debug.Log($"Received number {next} from the server.");
    }
}