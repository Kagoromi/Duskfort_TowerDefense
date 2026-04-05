using UnityEngine;

public class ArmoredEnemy : Enemy
{
    [SerializeField] private float damageReduction = 0.5f;

    protected override void Start()
    {
        maxHealth = 200f;
        speed = 2f;
        minReward = 15;
        maxReward = 25;
        damage = 1;
        base.Start();
    }

    // Armored enemy takes reduced damage
    public override void TakeDamage(float amount)
    {
        base.TakeDamage(amount * damageReduction);
    }
}