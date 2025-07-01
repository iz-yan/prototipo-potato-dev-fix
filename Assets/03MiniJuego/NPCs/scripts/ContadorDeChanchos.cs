using UnityEngine;

public class ContadorDeChanchos : MonoBehaviour
{
    private int contadorChanchos = 0;
    [SerializeField] private GameObject npcOculto; 
    [SerializeField] private string tagChanchitos = "Chanchito";
    private BehavioyrChanchito scriptChanchito;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagChanchitos))
        {
            scriptChanchito = collision.GetComponent<BehavioyrChanchito>();
            if (scriptChanchito != null && scriptChanchito.IsCaught)
            {
                contadorChanchos++;
                Debug.Log($"hay:{contadorChanchos}");
                PlayerScore.Instance.GanarPuntos(scriptChanchito.PuntajeChancho);
                VerificarContador();
            }
            //collision.gameObject.SetActive(false);
        }
    }

    private void VerificarContador()
    {
        if (contadorChanchos >= 3)
        {
            GameObject[] chanchitosRestantes = GameObject.FindGameObjectsWithTag(tagChanchitos);
            foreach (GameObject chanchito in chanchitosRestantes)
            {
                chanchito.SetActive(false);
            }

            if (npcOculto != null)
            {
                npcOculto.SetActive(true);
            }
        }
    }
}
