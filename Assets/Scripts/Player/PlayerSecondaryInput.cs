using StarterAssets;
using UnityEngine;

public class PlayerSecondaryInput : MonoBehaviour
{
    private StarterAssetsInputs _input;

    [SerializeField]
    private Projectile bulletPrefab;

    [SerializeField]
    private Transform bulletSpawnPoint;

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
                Projectile projectile = Instantiate(bulletPrefab,bulletSpawnPoint.position, Quaternion.identity);
                projectile.SetDirection(transform.forward);

                _input.shoot = false;
            }
        }
    }
}
