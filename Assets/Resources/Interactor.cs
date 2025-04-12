using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class Interactor : MonoBehaviour
{
    Dictionary<string, GameObject> overlappingGrabbables = new Dictionary<string, GameObject>();
    Dictionary<string, GameObject> overlappingTriggers = new Dictionary<string, GameObject>();
    
    private bool canTrigger = false;
    private bool pointerVisible = false;
    private GameObject gameobjectPointed;
    private LineRenderer lineRenderer;
    public float maxDistance = 5f;
    public Material allowedMaterial;
    public Material unallowedMaterial;
    
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        HidePointer();
    }
    
    void HidePointer()
    {
        if (lineRenderer)
        {
            lineRenderer.enabled = false;
        }
        pointerVisible = false;
    }
    
    void ShowPointer()
    {
        if (lineRenderer)
        {
            lineRenderer.enabled = true;
        }
        pointerVisible = true;
    }
    
    
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
        Debug.Log("OnGrabAction");
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
    
    void FixedUpdate()
    {
        if (pointerVisible)
        {
            lineRenderer.SetPosition(0, transform.position);
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
                    maxDistance))
            {
                lineRenderer.SetPosition(1, transform.position + transform.forward * hit.distance);
                if (hit.collider.gameObject.GetComponent<Trigger>())
                {
                    canTrigger = true;
                    gameobjectPointed = hit.collider.gameObject;
                    lineRenderer.material = allowedMaterial;
                }
                else
                {
                    canTrigger = false;
                    gameobjectPointed = null;
                    lineRenderer.material = unallowedMaterial;
                }
            }
            else
            {
                lineRenderer.SetPosition(1, transform.position + transform.forward * maxDistance);
                canTrigger = false;
                gameobjectPointed = null;
                lineRenderer.material = unallowedMaterial;
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
        else
        {
            ShowPointer();
            if (context.canceled)
            {
                if (gameobjectPointed)
                {
                    gameobjectPointed.GetComponent<Trigger>().TriggerFunction();
                }
                gameobjectPointed = null;
                HidePointer();
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
