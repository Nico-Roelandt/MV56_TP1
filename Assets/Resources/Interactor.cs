using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Interactor : MonoBehaviour
{
    Dictionary<string, GameObject> overlappingGrabbables = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> overlappingTriggers = new Dictionary<string, GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        GrabbableBehavior gb = other.GetComponentInParent<GrabbableBehavior>();
        if (gb)
        {
            overlappingGrabbables.Add(gb.gameObject.name, gb.gameObject);
        }
        Trigger tb = other.GetComponentInParent<Trigger>();
        if (tb)
        {
            overlappingTriggers.Add(tb.gameObject.name, tb.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        GrabbableBehavior gb = other.GetComponentInParent<GrabbableBehavior>();
        if (gb)
        {
            overlappingGrabbables.Remove(gb.gameObject.name);
        }
        Trigger tb = other.GetComponentInParent<Trigger>();
        if (tb)
        {
            overlappingTriggers.Remove(tb.gameObject.name);
        }
    }
    
    private GameObject GetNearestGrabbable()
    {
        GameObject nearestGrabbable = null;
        float minDistance = Mathf.Infinity;
        foreach (KeyValuePair<string, GameObject> kvp in overlappingGrabbables)
        {
            if (kvp.Value.GetComponent<GrabbableBehavior>())
            {
                float distance = Vector3.Distance(transform.position, kvp.Value.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestGrabbable = kvp.Value;
                }
            }
        }

        return nearestGrabbable;
    }
    
    public void OnGrabAction(InputAction.CallbackContext context)
    {
        GameObject nearestGrabbable = GetNearestGrabbable();
        if (nearestGrabbable)
        {
            if (context.started)
            {
                nearestGrabbable.GetComponent<GrabbableBehavior>().TryGrab(gameObject);
            }
            if (context.canceled)
            {
                nearestGrabbable.GetComponent<GrabbableBehavior>().TryRelease(gameObject);
            }
        }
    }

    public void OnTriggerAction(InputAction.CallbackContext context)
    {
        GameObject nearestTrigger = GetNearestTrigger();
        if (nearestTrigger)
        {
            if (context.started)
            {
                nearestTrigger.GetComponent<Trigger>().TriggerFunction();
            }
        }
    }
    private GameObject GetNearestTrigger()
    {
        GameObject nearestTrigger = null;
        float minDistance = Mathf.Infinity;
        foreach (KeyValuePair<string, GameObject> kvp in overlappingTriggers)
        {
            float distance = Vector3.Distance(transform.position, kvp.Value.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                nearestTrigger = kvp.Value;
            }
        }
        return nearestTrigger;
    }
}
