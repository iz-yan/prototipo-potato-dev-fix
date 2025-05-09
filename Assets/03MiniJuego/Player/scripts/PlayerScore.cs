using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
public class PlayerScore : MonoBehaviour
{
    [SerializeField] TMP_Text textScore, textLife;
    private int life=3, score=0;
    public static PlayerScore Instance; //para que el singleton sea visto de forma global

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("Creando nueva instancia");
        }
        else
        {
            Debug.Log("Destruyendo duplicado: " + gameObject.name);
            Destroy(gameObject);
        }
    }
    void Start()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        if (textScore != null)
            textScore.text = "Puntos: " + score.ToString();

        if (textLife != null)
            textLife.text = "Vidas: " + life.ToString();
    }
    public void perderVida()
    {
        life -= 1;
        Debug.Log($"perdiste Una Vida, vida actual:{life}");
        UpdateUI();
        if (life <= 0)
        {
            Debug.Log("perdiste");
            SceneManager.LoadScene("GameOver");
        }
    }
    public void GanarPuntos(int puntosGanados)
    {
        score += puntosGanados;
        Debug.Log($"ganaste puntos, puntos actuales:{score}");
        UpdateUI() ;
    }
}
