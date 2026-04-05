using UnityEngine;

public class RapidTower : Tower
{
    protected override void Start()
    {
        damage = 12f;
        fireRate = 3f;
        range = 4f;
        cost = 50;
        upgradeCost = 60;
    }
}