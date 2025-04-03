using UnityEngine;

public class ToggleGameObject : MonoBehaviour
{
    public GameObject toggleGameObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Toggle()
    {

        if (toggleGameObject)
        {
            toggleGameObject.SetActive(!toggleGameObject.activeSelf);
        }
    }
}
