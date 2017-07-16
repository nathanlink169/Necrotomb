using GameFramework;
using UnityEngine;

public class PlayerTriggerBoxActivator : BaseBehaviour
{
    public TriggerBoxDelegate DelegateToCall = null;
    public delegate void TriggerBoxDelegate(GameObject in_triggerBoxOwner);
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && DelegateToCall != null)
        {
            DelegateToCall.Invoke(gameObject);
        }
    }
}