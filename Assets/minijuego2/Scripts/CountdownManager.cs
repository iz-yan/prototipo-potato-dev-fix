using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class CountdownManager : MonoBehaviour
{
              
    public TextMeshProUGUI countdownText;
    public GameObject foodSpawnerObj;    // El objeto vacío que tiene el script FoodSpawner

    private void Start()
    {
        foodSpawnerObj.SetActive(false); // Asegúrate que esté desactivado al inicio
        StartCoroutine(CountdownCoroutine());
    }

    IEnumerator CountdownCoroutine()
    {
        countdownText.text = "¿Estás listo?";
        yield return new WaitForSeconds(1f);

        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "¡Gooo!";
        foodSpawnerObj.SetActive(true); // Activar el objeto con tu FoodSpawner
        yield return new WaitForSeconds(1f);

        countdownText.text = ""; // Limpiar el texto si quieres
    }
}
