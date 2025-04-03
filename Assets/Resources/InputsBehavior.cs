using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputsBehavior : MonoBehaviour
{
    public Animator rightAnimator;
    public Animator rightHandAnimator;
    public GameObject rightThumbstick;
    public Animator leftAnimator;
    public Animator leftHandAnimator;
    public GameObject leftThumbstick;
    private Quaternion leftBaseRotation;
    public void Start()
    {
        leftBaseRotation = leftThumbstick.GetComponent<Transform>().localRotation; 
    }

    public void OnAPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("A Pressed");
            if (rightAnimator)
            {
                Debug.Log("A Anim");
                rightAnimator.SetBool("APressed", true);
            }
        }
        if (context.canceled)
        {
            if (rightAnimator)
            {
                rightAnimator.SetBool("APressed", false);
            }
            Debug.Log("A Released");
        }
    }
    public void OnBPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("B Pressed");
            if (rightAnimator)
            {
                Debug.Log("B Anim");
                rightAnimator.SetBool("BPressed", true);
            }
        }
        if (context.canceled)
        {
            Debug.Log("B Released");
            if (rightAnimator)
            {
                rightAnimator.SetBool("BPressed", false);
            }
        }
    }
    
    public void OnYPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("Y Pressed");
            if (leftAnimator)
            {
                leftAnimator.SetBool("YPressed", true);
            }
        }
        if (context.canceled)
        {
            if (leftAnimator)
            {
                leftAnimator.SetBool("YPressed", false);
            }
            Debug.Log("Y Released");
        }
    }
    public void OnXPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("X Pressed");
            if (leftAnimator)
            {
                Debug.Log("X Anim");
                leftAnimator.SetBool("XPressed", true);
            }
        }
        if (context.canceled)
        {
            Debug.Log("X Released");
            if (leftAnimator)
            {
                leftAnimator.SetBool("XPressed", false);
            }
        }
    }
    
    public void OnRightTriggerAxis(InputAction.CallbackContext context)
    {
        if (rightAnimator)
        {
            rightAnimator.SetFloat("RightTrigger", context.ReadValue<float>());
            Debug.Log("Trigger Value: " + context.ReadValue<float>());
        } 
    }
    
    public void OnLeftTriggerAxis(InputAction.CallbackContext context)
    {
        if (leftAnimator) 
        {
            leftAnimator.SetFloat("LeftTrigger", context.ReadValue<float>());
            Debug.Log("Trigger Value: " + context.ReadValue<float>());
        } 
    }
    
    public void OnRightThumbstickAxis(InputAction.CallbackContext context)
    {
        if (rightThumbstick)
        {
            Vector2 thumbstickValue = context.ReadValue<Vector2>();
            rightThumbstick.transform.localEulerAngles = new Vector3(thumbstickValue.y, 0, - thumbstickValue.x) * 15f;
        }
    }
    
    public void OnLeftThumbstickAxis(InputAction.CallbackContext context)
    {
        if (leftThumbstick)
        {
            Vector2 thumbstickValue = context.ReadValue<Vector2>();
            Vector3 eulerAngleRotation = new Vector3(thumbstickValue.y, 0, -thumbstickValue.x) * 15f;
            // Convert in quaternion
            Quaternion rotation = Quaternion.Euler(eulerAngleRotation);
            rotation = rotation * leftBaseRotation;
            leftThumbstick.transform.localRotation = rotation;
        }
    }
    
    public void OnRightGripAxis(InputAction.CallbackContext context)
    {
        if (rightHandAnimator)
        {
            rightHandAnimator.SetFloat("Close", context.ReadValue<float>());
        }
    }
    
    public void OnLeftGripAxis(InputAction.CallbackContext context)
    {
        if (leftHandAnimator)
        {
            leftHandAnimator.SetFloat("Close", context.ReadValue<float>());
        }
    }
    
    public void OnRightTriggerTouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (rightHandAnimator)
            {
                rightHandAnimator.SetBool("Point", false);
            }
        }
        if (context.canceled)
        {
            if (rightHandAnimator)
            {
                rightHandAnimator.SetBool("Point", true);
            }
        }
    }
    
    public void OnLeftTriggerTouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (leftHandAnimator)
            {
                leftHandAnimator.SetBool("Point", false);
            }
        }
        if (context.canceled)
        {
            if (leftHandAnimator)
            {
                leftHandAnimator.SetBool("Point", true);
            }
        }
    }
}
