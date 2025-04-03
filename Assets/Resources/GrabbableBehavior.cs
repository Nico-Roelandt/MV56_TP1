using System;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class GrabbableBehavior : MonoBehaviour
{
    public enum GrabType { None, Free, Snap };
    public Rigidbody rigidbody;
    public GameObject grabber;
    public bool wasKinematic;
    public GrabType grabType = GrabType.Free;

    public bool isHeld = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        wasKinematic = rigidbody.isKinematic;
    }

    public void TryGrab(GameObject grabber)
    {
        switch(grabType)
        {
            case GrabType.None:
                break;
            case GrabType.Free:
                rigidbody.isKinematic = true;
                transform.parent = grabber.transform;
                this.grabber = grabber;
                isHeld = true;
                break;
            case GrabType.Snap:
                rigidbody.isKinematic = true;
                transform.parent = grabber.transform;
                this.grabber = grabber;
                isHeld = true;
                transform.position = grabber.transform.position;
                transform.rotation = grabber.transform.rotation;
                break;
        }
    }

    public void TryRelease(GameObject grabber)
    {
        if (grabber.Equals(this.grabber) && isHeld)
        {
            transform.parent = null;
            rigidbody.isKinematic = wasKinematic;
            isHeld = false;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
