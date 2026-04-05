using UnityEngine;

public class HeavyTower : Tower
{
    protected override void Start()
    {
        damage = 50f;
        fireRate = 0.5f;
        range = 6f;
        cost = 100;
        upgradeCost = 120;
    }

    protected override void Shoot()
    {
        base.Shoot();
    }
}