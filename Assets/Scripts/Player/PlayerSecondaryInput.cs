using StarterAssets;
using UnityEngine;

public class PlayerSecondaryInput : MonoBehaviour
{
    private StarterAssetsInputs _input;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private Transform bulletSpawnPoint;

    [SerializeField]
    private float projectileSpeed;

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    private void Update()
    {
        //Check for shooting

        Shooting();
    }

    private void Shooting()
    {
        if (_input != null)
        {
            if (_input.shoot)
            {
                //Instantiate the shooting projectile
                GameObject projectile = Instantiate(bulletPrefab,bulletSpawnPoint.position, Quaternion.identity);

                Rigidbody rb = projectile.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    // Apply force to the projectile in the forward direction of the fire point
                    rb.velocity = Vector3.forward * projectileSpeed;
                }

                _input.shoot = false;
            }
        }
    }
}
