using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }

    private const int MAX_SCORES = 5;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SaveScore(int wave, int killed)
    {
        for (int i = 0; i < MAX_SCORES; i++)
        {
            int existing = PlayerPrefs.GetInt("Score" + i, 0);
            if (wave > existing)
            {
                for (int j = MAX_SCORES - 1; j > i; j--)
                    PlayerPrefs.SetInt("Score" + j,
                        PlayerPrefs.GetInt("Score" + (j - 1), 0));

                PlayerPrefs.SetInt("Score" + i, wave);
                break;
            }
        }
        PlayerPrefs.Save();
    }

    public string GetScoresText()
    {
        string result = "";
        for (int i = 0; i < MAX_SCORES; i++)
        {
            int score = PlayerPrefs.GetInt("Score" + i, 0);
            if (score > 0)
                result += (i + 1) + ". " + score + " waves\n";
            else
                result += (i + 1) + ". ---\n";
        }
        return result;
    }
}