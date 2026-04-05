using UnityEngine;
using System.IO;
using System;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance { get; private set; }

    private int totalEnemiesKilled = 0;
    private int totalGoldEarned = 0;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddEnemyKilled()
    {
        totalEnemiesKilled++;
    }

    public void AddGoldEarned(int amount)
    {
        totalGoldEarned += amount;
    }

    public void SaveStats(int wavessurvived)
    {
        string path = Application.persistentDataPath +
            "/stats.txt";

        string content =
            "=== DUSKFORT STATISTICS ===\n" +
            "Date: " + DateTime.Now.ToString() + "\n" +
            "Waves survived: " + wavessurvived + "\n" +
            "Enemies killed: " + totalEnemiesKilled + "\n" +
            "Gold earned: " + totalGoldEarned + "\n" +
            "===========================\n\n";

        File.AppendAllText(path, content);

        Debug.Log("Stats saved to: " + path);
    }
}