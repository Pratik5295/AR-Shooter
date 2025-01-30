using UnityEngine;
using UnityEngine.EventSystems;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float projectileSpeed;

    [SerializeField]
    private float projectileDamage;   //Damage associated with each projectile

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float destroyAfter;

    public Vector3 moveDirection = Vector3.zero;

    public void LaunchProjectile(Vector3 direction)
    {
        rb.velocity = direction.normalized * projectileSpeed;
        //moveDirection = direction;


        DisableProjectile();
    }

    private void DisableProjectile()
    {
        Destroy(gameObject,destroyAfter);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }

    void Update()
    {
        // Move the projectile in the stored direction
        //transform.position += moveDirection * projectileSpeed * Time.deltaTime;
    }



}
