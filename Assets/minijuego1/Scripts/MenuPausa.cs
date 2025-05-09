using UnityEngine;

public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausaUI;
    private bool juegoPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (juegoPausado)
                Reanudar();
            else
                Pausar();
        }
    }

    public void Reanudar()
    {
        menuPausaUI.SetActive(false);
        juegoPausado = false;
        Time.timeScale = 1f;
    }

    void Pausar()
    {
        menuPausaUI.SetActive(true);
        juegoPausado = true;
        Time.timeScale = 0f;
    }
}
