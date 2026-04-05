using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("HUD")]
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI livesText;
    [SerializeField] private TextMeshProUGUI waveText;

    [Header("Tower Buttons")]
    [SerializeField] private Button rapidTowerButton;
    [SerializeField] private Button heavyTowerButton;
    [SerializeField] private Button slowTowerButton;

    [Header("Game Over")]
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverText;

    [Header("Tower Info Panel")]
    [SerializeField] private GameObject towerInfoPanel;
    [SerializeField] private TextMeshProUGUI towerLevelText;
    [SerializeField] private TextMeshProUGUI upgradeCostText;

    private TowerPlacement towerPlacement;
    private Tower selectedTower;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        towerPlacement = FindAnyObjectByType<TowerPlacement>();
        gameOverPanel.SetActive(false);
        towerInfoPanel.SetActive(false);
        UpdateUI();
    }

    public void UpdateUI()
    {
        if (goldText != null)
            goldText.text = "Gold: " +
                GameManager.Instance.Gold;
        if (livesText != null)
            livesText.text = "Lives: " +
                GameManager.Instance.Lives;
        if (waveText != null)
            waveText.text = "Wave: " +
                GameManager.Instance.Wave;
    }

    public void ShowTowerInfo(Tower tower)
    {
        selectedTower = tower;
        towerInfoPanel.SetActive(true);

        towerLevelText.text = "Level: " + tower.GetLevel();

        if (tower.GetLevel() >= 3)
            upgradeCostText.text = "MAX";
        else
            upgradeCostText.text = "Cost: " +
                tower.GetUpgradeCost() + "g";
    }

    public void OnUpgradeButton()
    {
        AudioManager.Instance?.PlayButtonClick();
        if (selectedTower == null) return;
        selectedTower.Upgrade();
        ShowTowerInfo(selectedTower);
    }

    public void OnCloseInfoButton()
    {
        AudioManager.Instance?.PlayButtonClick();
        towerInfoPanel.SetActive(false);
        selectedTower = null;
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = "You survived " +
            GameManager.Instance.Wave + " waves!";
    }

    public void OnRapidTowerButton()
    {
        AudioManager.Instance?.PlayButtonClick();
        towerPlacement.SelectRapidTower();

    }

    public void OnHeavyTowerButton()
    {
        AudioManager.Instance?.PlayButtonClick();
        towerPlacement.SelectHeavyTower();
    }

    public void OnSlowTowerButton()
    {
        AudioManager.Instance?.PlayButtonClick();
        towerPlacement.SelectSlowTower();
    }

    public void OnRestartButton()
    {
        AudioManager.Instance?.PlayButtonClick();
        GameManager.Instance.RestartGame();
    }

    public void OnMenuButton()
    {
        AudioManager.Instance?.PlayButtonClick();
        GameManager.Instance.GoToMenu();
    }
}