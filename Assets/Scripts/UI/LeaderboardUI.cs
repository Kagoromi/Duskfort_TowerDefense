using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoresText;

    private void Start()
    {
        scoresText.text =
            LeaderboardManager.Instance.GetScoresText();
    }

    public void OnBackButton()
    {
        AudioManager.Instance?.PlayButtonClick();
        SceneManager.LoadScene("MainMenu");
    }
}