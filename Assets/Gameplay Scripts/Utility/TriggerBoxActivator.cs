using GameFramework;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBoxActivator : BaseBehaviour
{
    public UnityEvent Delegate;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Delegate != null)
        {
            Delegate.Invoke();
        }
    }
}