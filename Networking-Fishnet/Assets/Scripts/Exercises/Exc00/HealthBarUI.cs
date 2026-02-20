using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBarUI : NetworkBehaviour
{
    private Slider _slider;
    private readonly SyncVar<float> _fillAmount = new ();

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _fillAmount.Value = _slider.value;
        _fillAmount.OnChange += OnHealthChanged;
    }
    
    private void OnHealthChanged(float prev, float next, bool asServer)
    {
        _fillAmount.Value = next;
        _slider.value = _fillAmount.Value;
    }

    private void UpdateFillAmount(int prev, int next, bool asServer)
    {
        _fillAmount.Value = (float)next / 100;
    }

    #region Subscribe To HealthComponent
    public void SubscribeHealthBar(NetworkConnection conn)
    {
        StartCoroutine(WaitForSpawn(conn));
    }
    
    private IEnumerator WaitForSpawn(NetworkConnection conn)
    {
        while (!conn.FirstObject) //usually the player, very basic implementation
        {
            yield return null;
        }
        
        GiveOwnership(conn);
        conn.FirstObject.GetComponent<HealthComponent>().Health.OnChange += UpdateFillAmount;
    }
    #endregion
}