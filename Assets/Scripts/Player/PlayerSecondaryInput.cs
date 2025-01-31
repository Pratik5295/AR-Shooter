using ARGame.Managers;
using StarterAssets;
using UnityEngine;

public class PlayerSecondaryInput : MonoBehaviour
{
    private StarterAssetsInputs _input;

    [SerializeField]
    private Projectile bulletPrefab;

    [SerializeField]
    private Transform bulletSpawnPoint;

    ///TO DO: Move in Stats afterwards <summary>
    [SerializeField]
    private float characterDamage;  //Adds character specific damage to the projectiles fired

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
                projectile.LaunchProjectile(transform.forward,characterDamage);

                _input.shoot = false;

                //Audio effect
                AudioManager.Instance.PlayForegroundSound(0);
            }
        }
    }
}
