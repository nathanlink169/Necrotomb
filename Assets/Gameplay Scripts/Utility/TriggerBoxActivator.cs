using GameFramework;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBoxActivator : BaseBehaviour
{
    public Collider Other;
    public UnityEvent Delegate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Delegate != null)
        {
            this.Other = other;
            Delegate.Invoke();
        }
    }
}