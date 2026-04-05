using UnityEngine;

public class SlowProjectile : Projectile
{
    private float slowAmount;
    private float slowDuration;

    public void SetTarget(Transform target, float damage,
        float slowAmount, float slowDuration)
    {
        base.SetTarget(target, damage);
        this.slowAmount = slowAmount;
        this.slowDuration = slowDuration;
    }

    protected override void HitTarget()
    {
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            enemy.ApplySlow(slowAmount, slowDuration);
        }
        Destroy(gameObject);
    }
}