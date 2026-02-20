using FishNet.Component.Transforming;
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
    [SerializeField] private Image _background;
    [SerializeField] private Image _fillArea;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _fillAmount.Value = _slider.value;
        _fillAmount.OnChange += OnHealthChanged;
        Transform twodegreesabove = transform.parent.parent;
        twodegreesabove.GetComponent<HealthComponent>().Health.OnChange += UpdateFillAmount;
    }

    [ServerRpc(RequireOwnership = true)] //i give up on this lol
    private void Start()
    {
        HideHealthBar();
    }

    private void HideHealthBar() 
    {
        var tempColorBG = _background.color;
        var tempColorFill = _fillArea.color;
        tempColorBG.a = 0;
        tempColorFill.a = 0;
        _background.color = tempColorBG;
        _fillArea.color = tempColorFill;
    }

    private void OnHealthChanged(float prev, float next, bool asServer)
    {
        _fillAmount.Value = next;
        _slider.value = _fillAmount.Value;
    }
    private void UpdateFillAmount(int prev, int next, bool asServer)
    {
        Debug.Log("UPDATING FILL AMOUNT");
        _fillAmount.Value = (float)next / 100;
        _slider.value = _fillAmount.Value;
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