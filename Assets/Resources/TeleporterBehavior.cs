using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class TeleporterBehavior : MonoBehaviour
{
    private bool canTeleport = false;

    private bool pointerVisible = false;
    private Vector3 destinationPosition;
    private Quaternion destinationRotation;
    private LineRenderer lineRenderer;
    public GameObject player;
    public String FloorTag;
    public String RideableTag;
    
    public float maxDistance = 5f;

    public Material allowedMaterial;
    public Material unallowedMaterial;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

    void Teleport()
    {
        if (player)
        {
            player.transform.position = destinationPosition;
            destinationRotation.x = player.transform.rotation.x;
            destinationRotation.z = player.transform.rotation.z;
            player.transform.rotation = destinationRotation;
        }
    }

    public void OnTeleportAction(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            ShowPointer();
        }

        if (context.canceled)
        {
            if (canTeleport)
            {
                Teleport();
            }
            HidePointer();
        }
    }

    // Update is called once per frame
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
                if (hit.collider.gameObject.CompareTag(FloorTag))
                {
                    Debug.Log(FloorTag);
                    canTeleport = true;
                    destinationPosition = hit.point;
                    lineRenderer.material = allowedMaterial;
                }
                else if (hit.collider.gameObject.CompareTag(RideableTag))
                {
                    Debug.Log("Rideable");
                    canTeleport = true;
                    destinationPosition.x = hit.collider.gameObject.transform.position.x;
                    destinationPosition.z = hit.collider.gameObject.transform.position.z;
                    destinationRotation.y = hit.collider.gameObject.transform.rotation.y;
                    lineRenderer.material = allowedMaterial;
                }
                else
                { 
                    canTeleport = false;
                    lineRenderer.material = unallowedMaterial;
                }
            }
            else
            {
                lineRenderer.SetPosition(1, transform.position + transform.forward * maxDistance);
                canTeleport = false;
                lineRenderer.material = unallowedMaterial;
            }
        }
    }
}
