using UnityEngine;

public class DroneEnemy : Enemy
{
    protected override void Start()
    {
        maxHealth = 120f;
        speed = 2.5f;
        minReward = 5;
        maxReward = 10;
        damage = 1;
        base.Start();
    }
}