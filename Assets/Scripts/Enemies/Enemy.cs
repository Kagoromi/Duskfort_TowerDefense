using UnityEngine;
using System.Collections;

// Base class for all enemies
public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] protected float maxHealth = 100f;
    [SerializeField] protected float speed = 2f;
    [SerializeField] protected int minReward = 10;
    [SerializeField] protected int maxReward = 25;
    [SerializeField] protected int damage = 1;
    [Header("Visual Effects")]
    [SerializeField] private GameObject deathEffectPrefab;

    protected float currentHealth;
    protected Transform[] waypoints;
    protected int waypointIndex = 0;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        waypoints = WaveManager.Instance.GetWaypoints();
    }

    protected virtual void Update()
    {
        if (GameManager.Instance.IsGameOver) return;
        Move();
    }

    // Movement along waypoints
    protected virtual void Move()
    {
        if (waypointIndex >= waypoints.Length) return;

        Transform target = waypoints[waypointIndex];
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;

        // Rotate towards movement direction
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir);

        if (Vector3.Distance(transform.position, target.position) < 0.2f)
            waypointIndex++;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    protected virtual void Die()
    {
        AudioManager.Instance?.PlayEnemyDeath();
        StatsManager.Instance?.AddEnemyKilled();
        if (deathEffectPrefab != null)
        {
            GameObject effect = Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
        int reward = Random.Range(minReward, maxReward + 1);
        GameManager.Instance.AddGold(reward);
        StatsManager.Instance?.AddGoldEarned(reward);
        GameManager.Instance.EnemyKilled();
        Destroy(gameObject);
    }

    // When enemy reaches the base
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Base"))
        {
            GameManager.Instance.TakeDamage(damage);
            Destroy(gameObject);
        }
    }

    public void ApplySlow(float amount, float duration)
    {
        StartCoroutine(SlowCoroutine(amount, duration));
    }

    private IEnumerator SlowCoroutine(float amount, float duration)
    {
        speed *= amount;
        yield return new WaitForSeconds(duration);
        speed /= amount;
    }
}