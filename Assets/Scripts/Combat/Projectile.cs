using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetDirection(Vector3 direction)
    {
        if (rb != null)
        {
            // Apply force to the projectile in the forward direction of the fire point
            rb.velocity = direction * projectileSpeed;
        }
    }

    

}
