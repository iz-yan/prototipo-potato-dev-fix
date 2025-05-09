using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance { get; private set; }

    [Header("Configuración")]
    [SerializeField] private int initialLives = 3;
    [SerializeField] private TMP_Text livesText;
    [SerializeField] private string gameOverSceneName = "GameOver";

    private int currentLives;

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
            return;
        }
    }

    private void Start()
    {
        currentLives = initialLives;
        UpdateLivesUI();
    }

    public void LoseLife(int amount)
    {
        if (currentLives <= 0) return;

        currentLives -= amount;
        UpdateLivesUI();

        if (currentLives <= 0)
        {
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        Debug.Log("¡Game Over!");

        //Detener la música
        MusicManager.Instance?.StopMusic();

        //Cargar la escena de Game Over
        if (!string.IsNullOrEmpty(gameOverSceneName))
        {
            SceneManager.LoadScene(gameOverSceneName);
        }
        else
        {
            Debug.LogError("Nombre de escena GameOver no configurado");
        }
    }

    private void UpdateLivesUI()
    {
        if (livesText != null)
        {
            livesText.text = $"Vidas: {currentLives}";
        }
        else
        {
            Debug.LogWarning("Lives Text no asignado en el Inspector");
        }
    }
}