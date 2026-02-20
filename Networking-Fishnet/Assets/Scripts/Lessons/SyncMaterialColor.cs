using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

public class SyncMaterialColor : NetworkBehaviour
{
    // SyncVar has to be readonly
    public readonly SyncVar<Color> color = new SyncVar<Color>();

    private void Awake()
    {
        // color changes locally through CubeSpawner or other scripts, to sync with other Clients we subscribe
        // a delegate to the OnChange event.
        color.OnChange += OnColorChanged; 
    }

    private void OnColorChanged(Color previous, Color next, bool asServer)
    {
        GetComponent<MeshRenderer>().material.color = color.Value;
    }
}
