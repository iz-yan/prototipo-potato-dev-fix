using UnityEngine;

[System.Serializable]
public class Achievement
{
    public string id;
    public string nombre;
    public string descripcion;
    public bool desbloqueado;

    public Achievement(string id, string nombre, string descripcion)
    {
        this.id = id;
        this.nombre = nombre;
        this.descripcion = descripcion;
        this.desbloqueado = false;
    }

    public void Desbloquear()
    {
        desbloqueado = true;
        Debug.Log($"¡Logro desbloqueado: {nombre}!");
        PlayerPrefs.SetInt(id, 1);
        PlayerPrefs.Save();
    }
}
