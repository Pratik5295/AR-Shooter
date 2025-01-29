using UnityEngine;

public class RangedEnemy : Enemy
{
    public GameObject projectilePrefab;
    public Transform firePoint;

    protected override void AttackBehavior()
    {
        attackTimer += Time.deltaTime;

        if (attackTimer >= attackCooldown)
        {
            // Fire projectile
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * 10f;

            attackTimer = 0f;
        }
    }
}
