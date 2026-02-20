using System;
using UnityEngine;
using UnityEngine.UI;

public class HudManager : MonoBehaviour
{
    [SerializeField] private GameObject _hurtScreen;

    private void Awake()
    {
        GetComponentInParent<HealthComponent>().Health.OnChange += EnableHurtScreen;
    }

    private void EnableHurtScreen(int prev, int next, bool asServer)
    {
        Debug.Log("HurtScreen");
        if(prev <= next) return;

        var color = _hurtScreen.GetComponent<Image>().color;
        _hurtScreen.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1);
        _hurtScreen.SetActive(true);
        _hurtScreen.GetComponent<Animator>().Play("Disappear");
    }
    
}
