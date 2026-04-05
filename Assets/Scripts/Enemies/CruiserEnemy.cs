using UnityEngine;

public class CruiserEnemy : Enemy
{
    protected override void Start()
    {
        maxHealth = 150f;
        speed = 1.8f;
        minReward = 8;
        maxReward = 15;
        damage = 1;
        base.Start();
    }
}