using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject menuButton;

    public static int score { get; private set; } = 0;
    public static int clicks { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
        }
    }

    private void Start()
    {
        Play();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void Play()
    {
        score = 0;
        scoreText.text = score.ToString();

        playButton.SetActive(false);
        //menuButton.SetActive(false);

        Time.timeScale = 1f;
        player.enabled = true;

        Pipes[] pipes = FindObjectsOfType<Pipes>();

        for (int i = 0; i < pipes.Length; i++) {
            Destroy(pipes[i].gameObject);
        }
    }

    public void GameOver()
    {
        playButton.SetActive(true);
        //menuButton.SetActive(true);
        
        if (score > 0)
        {
            DataHolder.coins += (clicks * score);
            PlayerPrefs.SetInt("manys", DataHolder.coins);
        }

        Pause();
    }

    public void IncreaseScore()
    {
        score++;
    }
    public void IncreaseClicks()
    {
        clicks++;
        scoreText.text = clicks.ToString();
    }

    public static void AddCoins()
    {
        DataHolder.coins++;
    }
    GameManager gameManager;
    public static void LikeGameOver()
    {
        if (score > 0)
        {
            DataHolder.coins += (clicks * score);
            PlayerPrefs.SetInt("manys", DataHolder.coins);
        }
        clicks = 0;
    }
}
