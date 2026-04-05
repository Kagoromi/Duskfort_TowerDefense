using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Sound Effects")]
    [SerializeField] private AudioClip buttonClick;
    [SerializeField] private AudioClip shoot;
    [SerializeField] private AudioClip placeTower;
    [SerializeField] private AudioClip enemyDeath;
    [SerializeField] private AudioClip gameOver;

    private AudioSource audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayButtonClick() => PlaySound(buttonClick);
    public void PlayShoot() => PlaySound(shoot);
    public void PlayPlaceTower() => PlaySound(placeTower);
    public void PlayEnemyDeath() => PlaySound(enemyDeath);
    public void PlayGameOver() => PlaySound(gameOver);

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else if (clip == null)
        {
            Debug.LogWarning($"AudioClip not assigned in AudioManager!");
        }
    }
}