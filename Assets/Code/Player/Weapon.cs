using UnityEngine;

public class Weapon : MonoBehaviour 
{
    [SerializeField] private float timeBetweenShots = 0.1f;
    [SerializeField] private float bulletSpeed = 100f;

    [SerializeField] private PlayerContainer playerContainer;
    [SerializeField] private Transform gunPointTransform;
    [SerializeField] private ObjectPoolSO bulletObjectPool;

    private bool shooting = false;
    private CooldownTimer cooldownTimer = new CooldownTimer();

    private int bulletsShot = 0;
    public int BulletsShot => bulletsShot;

    private void Update()
    {
        cooldownTimer.UpdateCooldown(Time.deltaTime);
    }

    public void HandleGunInput()
    {
        shooting = Input.GetKey(KeyCode.Mouse0);

        if (cooldownTimer.IsReady && shooting)
        {
            Shoot();
        }
    }

    private void Shoot() 
    {
        Bullet spawnedBullet = bulletObjectPool.GetObject().GetComponent<Bullet>();
        bulletsShot++;
        spawnedBullet.transform.position = gunPointTransform.position;
        spawnedBullet.transform.rotation = playerContainer.MainCamera.transform.rotation;
        spawnedBullet.SetVelocity(playerContainer.MainCamera.transform.forward * bulletSpeed);
        cooldownTimer.StartCooldown(timeBetweenShots);
    }

}