using GGJ.Managers;
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

    public void LaunchProjectile(Vector3 direction, float characterDamage = 0f)
    {
        projectileDamage += characterDamage;

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
        Debug.Log($"Hit: {other.gameObject}");

        if(other.gameObject.TryGetComponent<Health>(out var entity))
        {
            entity.TakeDamage(projectileDamage);

            AudioManager.Instance.PlayForegroundSound(1);
        }

        Destroy(gameObject);
    }


}
