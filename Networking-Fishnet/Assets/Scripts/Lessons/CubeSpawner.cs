using FishNet.Managing.Logging;
using FishNet.Object;
using UnityEngine;

public class CubeSpawner : NetworkBehaviour
{
    [SerializeField] private NetworkObject _cubePrefab;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnCube();
    }

    // We are using a ServerRpc here because the Server needs to do all network object spawning.
    // RPC = Remote Procedure Calls. ServerRpc runs logic on the server.
    // RequireOwnership = true is redundant since only the Owner Client can call a ServerRpc.
    // To get rid of warning in console, try checking for IsOwner before even calling the method.
    [ServerRpc(RequireOwnership = true)]
    private void SpawnCube()
    {
        NetworkObject obj = Instantiate(_cubePrefab, new Vector3(transform.position.x, 2, transform.position.z), Quaternion.identity);
        
        if(obj.TryGetComponent<SyncMaterialColor>(out var syncMaterialColor)) // lesson 02
        {
            syncMaterialColor.color.Value = Random.ColorHSV();
        }
        
        Spawn(obj, Owner); // NetworkBehaviour shortcut for ServerManager.Spawn(obj);
    }
}
