using UnityEngine;

public class GunComponent : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;
    public float bulletMaxImpulse = 10.0f;
    public float maxChargeTime = 3.0f;
    private float chargeTime = 0.0f;
    private bool isCharging = false;

    void Update()
    {
        // if the player starts pressing the button
        if (Input.GetButtonDown("Fire1"))
        {
            chargeTime = 0.0f;
            isCharging = true;
        }

        // if the player holds the button
        if (Input.GetButton("Fire1") && isCharging)
        {
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0, maxChargeTime);
        }

        // if the player releases the button
        if (Input.GetButtonUp("Fire1") && isCharging)
        {
            ShootBullet();
            isCharging = false;
        }
    }

    void ShootBullet()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            bulletSpawnPoint.position,
            bulletSpawnPoint.rotation
        );

        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // scale the bullet force based on the charge time
        float bulletImpulse = (chargeTime / maxChargeTime) * bulletMaxImpulse;
        rb.AddForce(bulletSpawnPoint.forward * bulletImpulse, ForceMode.Impulse);
    }
   
}
