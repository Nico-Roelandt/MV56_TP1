using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody))]
public class Trigger : MonoBehaviour
{
    public UnityEvent onTriggerEvent;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TriggerFunction()
    {
        onTriggerEvent.Invoke();
    }
}
