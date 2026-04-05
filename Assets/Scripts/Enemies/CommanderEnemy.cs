using UnityEngine;

public class CommanderEnemy : Enemy
{
    protected override void Start()
    {
        maxHealth = 300f;
        speed = 2f;
        minReward = 40;
        maxReward = 60;
        damage = 1;
        base.Start();
    }

    // Commander gives extra gold on death
    protected override void Die()
    {
        GameManager.Instance.AddGold(50);
        base.Die();
    }
}