using UnityEngine;

public class SlowTower : Tower
{
    [SerializeField] private float slowAmount = 0.3f;
    [SerializeField] private float slowDuration = 2f;

    protected override void Start()
    {
        damage = 5f;
        fireRate = 1.5f;
        range = 5f;
        cost = 75;
        upgradeCost = 90;
    }

    protected override void Shoot()
    {
        if (projectilePrefab == null ||
            firePoint == null ||
            target == null) return;
        AudioManager.Instance?.PlayShoot();
        GameObject projectileObj = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation);

        SlowProjectile slowProjectile =
            projectileObj.GetComponent<SlowProjectile>();

        if (slowProjectile != null)
            slowProjectile.SetTarget(
                target, damage, slowAmount, slowDuration);
    }
}