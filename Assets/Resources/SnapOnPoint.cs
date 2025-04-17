using UnityEngine;

public class SnapOnPoint : MonoBehaviour
{
    public GameObject PointToSnap;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == PointToSnap)
        {
            transform.position = PointToSnap.transform.position;
            transform.rotation = PointToSnap.transform.rotation;
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = true;
            }
        }
        
    }
}
