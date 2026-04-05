using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    [SerializeField] private int startingGold = 150;
    [SerializeField] private int startingLives = 3;

    public int Gold { get; private set; }
    public int Lives { get; private set; }
    public int Wave { get; private set; }
    public int EnemiesKilled { get; private set; }
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Gold = startingGold;
        Lives = startingLives;
        Wave = 1;
        EnemiesKilled = 0;
        IsGameOver = false;
    }

    public void AddGold(int amount)
    {
        Gold += amount;
        UIManager.Instance?.UpdateUI();
    }

    public bool SpendGold(int amount)
    {
        if (Gold >= amount)
        {
            Gold -= amount;
            UIManager.Instance?.UpdateUI();
            return true;
        }
        return false;
    }

    public void TakeDamage(int amount)
    {
        Lives -= amount;
        UIManager.Instance?.UpdateUI();
        if (Lives <= 0)
            GameOver();
    }

    public void EnemyKilled()
    {
        EnemiesKilled++;
    }

    public void NextWave()
    {
        Wave++;
        UIManager.Instance?.UpdateUI();
    }

    private void GameOver()
    {
        IsGameOver = true;
        AudioManager.Instance?.PlayGameOver();
        StatsManager.Instance?.SaveStats(Wave);
        LeaderboardManager.Instance?.SaveScore(Wave, EnemiesKilled);
        UIManager.Instance?.ShowGameOver();
    }

    public void RestartGame()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("Level");
    }

    public void GoToMenu()
    {
        Destroy(gameObject);
        SceneManager.LoadScene("MainMenu");
    }
}