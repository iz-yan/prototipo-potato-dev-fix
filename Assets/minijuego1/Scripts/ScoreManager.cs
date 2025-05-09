using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    [Header("Configuración UI")]
    [SerializeField] private TMP_Text scoreText;

    private int currentScore = 0;
    private bool alreadyWon = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        UpdateScoreUI();
    }

    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
        Debug.Log($"Puntaje actualizado: {currentScore}");

        if (currentScore >= 4 && !alreadyWon)
        {
            alreadyWon = true;

            //Desbloquear nivel 2
            DesbloqueoNiveles.DesbloquearNivel("nivel_2_desbloqueado");

            //Mostrar el logro de nivel completado
            AchievementManager.instance.LogroCompletado("nivel1");

            StartCoroutine(LoadVictorySceneWithDelay(0.5f));
        }
    }

    private IEnumerator LoadVictorySceneWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Victoria");
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Puntaje: {currentScore}";
        }
        else
        {
            Debug.LogWarning("Score Text no asignado en el Inspector");
            scoreText = GameObject.FindGameObjectWithTag("puntaje")?.GetComponent<TMP_Text>();
        }
    }
}
