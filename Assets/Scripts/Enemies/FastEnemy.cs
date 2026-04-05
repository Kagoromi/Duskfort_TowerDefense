using UnityEngine;

public class FastEnemy : Enemy
{
    protected override void Start()
    {
        maxHealth = 90f;
        speed = 4f;
        minReward = 3;
        maxReward = 8;
        damage = 1;
        base.Start();
    }

    protected override void Move()
    {
        base.Move();
    }
}