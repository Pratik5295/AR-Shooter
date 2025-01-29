using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed = 2f;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (rb != null)
        {
            // Apply force to the projectile in the forward direction of the fire point
            rb.velocity = Vector3.forward * projectileSpeed;
        }
    }

}
