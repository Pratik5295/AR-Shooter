using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{
    private Transform mainCamera;

    void Start()
    {
        mainCamera = Camera.main.transform; // Cache the main camera
    }

    void Update()
    {
        if (mainCamera != null)
        {
            transform.LookAt(transform.position + mainCamera.forward); // Make UI face the camera
        }
    }
}
