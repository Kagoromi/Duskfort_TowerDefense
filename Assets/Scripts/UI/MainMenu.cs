using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void OnStartButton()
    {
        AudioManager.Instance?.PlayButtonClick();
        SceneManager.LoadScene("Level");
    }

    public void OnLeaderboardButton()
    {
        AudioManager.Instance?.PlayButtonClick();
        SceneManager.LoadScene("Leaderboard");
    }

    public void OnExitButton()
    {
        AudioManager.Instance?.PlayButtonClick();
        Application.Quit();
    }
}