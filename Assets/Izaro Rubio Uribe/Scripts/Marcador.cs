
using TMPro;
using UnityEngine;

public class Marcador : MonoBehaviour
{
    public TMP_Text puntosText;
    private int puntos = 0;
    public int totalPuntos;

    private ProyectilSpawner spawner;

    void Start()
    {
        // Buscar spawner en la escena
        spawner = FindObjectOfType<ProyectilSpawner>();

        // Si hay opciones guardadas, usarlas
        if (OpcionesJugador.instancia != null)
        {
            totalPuntos = OpcionesJugador.instancia.puntosObjetivo;
        }

        puntosText.text = puntos.ToString();
    }

    // Sumar puntos en el marcador
    public void SumarPuntos()
    {
        puntos++;
        puntosText.text = puntos.ToString(); // Actualizar el texto de los puntos

        // Si llega al total de puntos (nº de paredes)...
        if (puntos == totalPuntos)
        {
            puntosText.text = "FIN";

            FinDelJuego();
        }
    }

    // Restar puntos en el marcador
    public void RestarPuntos()
    {
        puntos = Mathf.Max(0, puntos - 1); // Evitar que baje de 0
        puntosText.text = puntos.ToString();
    }

    //Llamar a detener lanzamiento de cubos si se finaliza el juego
    private void FinDelJuego()
    {
        Debug.Log("Juego Terminado!");
        if (spawner != null)
        {
            spawner.DetenerDisparos();
        }
    }
}
