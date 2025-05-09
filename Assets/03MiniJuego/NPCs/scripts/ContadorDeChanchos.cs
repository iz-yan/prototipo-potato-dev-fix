using UnityEngine;

public class ContadorDeChanchos : MonoBehaviour
{
    private int contadorChanchos = 0;
    [SerializeField] private GameObject npcOculto; 
    [SerializeField] private string tagChanchitos = "Chanchito"; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tagChanchitos))
        {
            contadorChanchos++;
            Debug.Log($"hay:{contadorChanchos}");
            BehavioyrChanchito scriptChanchito= collision.GetComponent<BehavioyrChanchito>();
            if (scriptChanchito != null)
            {
                PlayerScore.Instance.GanarPuntos(scriptChanchito.PuntajeChancho);
                VerificarContador();
            }
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
