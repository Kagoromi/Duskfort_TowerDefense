using UnityEngine;

// Base class for all towers
public class Tower : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float damage = 10f;
    [SerializeField] protected float fireRate = 1f;
    [SerializeField] protected float range = 5f;
    [SerializeField] protected int cost = 50;
    [SerializeField] protected int upgradeCost = 75;
    [SerializeField] protected int level = 1;

    [Header("Projectile")]
    [SerializeField] protected GameObject projectilePrefab;
    [SerializeField] protected Transform firePoint;

    protected float fireCountdown = 0f;
    protected Transform target;

    protected virtual void Update()
    {
        if (GameManager.Instance.IsGameOver) return;
        FindTarget();

        if (target == null) return;


        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
    }

    // Find nearest enemy in range
    protected virtual void FindTarget()
    {
        GameObject[] enemies =
            GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(
                transform.position, enemy.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
            target = nearestEnemy.transform;
        else
            target = null;
    }

    protected virtual void Shoot()
    {
        if (projectilePrefab == null || firePoint == null) return;
        AudioManager.Instance?.PlayShoot();
        GameObject projectileObj = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation);

        Projectile projectile =
            projectileObj.GetComponent<Projectile>();
        if (projectile != null)
            projectile.SetTarget(target, damage);
    }

    public virtual void Upgrade()
    {
        if (level >= 3)
        {
            Debug.Log("Max level reached!");
            return;
        }

        if (!GameManager.Instance.SpendGold(upgradeCost)) return;

        level++;
        damage *= 1.2f;
        range *= 1.1f;
        fireRate *= 1.2f;
        upgradeCost = Mathf.RoundToInt(upgradeCost * 3f);
    }

    public int GetCost() => cost;
    public int GetUpgradeCost() => upgradeCost;
    public int GetLevel() => level;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    protected virtual void Start() { }
}