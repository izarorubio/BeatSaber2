using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ProyectilSpawner : MonoBehaviour
{
    public GameObject proyectilPrefab;
    public GameObject bombaPrefab;
    public GameObject[] proyectilesFlecha;

    public Transform jugadorTransform; // Transform del jugador (cámara)
    public float distancia = 10f; // Distancia fija desde el jugador
    // Tiempos entre instancias
    public float tiempoSpawnMin = 2f; 
    public float tiempoSpawnMax = 3f;
    // Rango para variar la dirección del cubo
    public float destinationOffsetRange = 2f;
    // Para detener los proyectiles cuando finalice
    private bool juegoTerminado = false;

    //Probabilidad de que aparezca una bomba
    [Range(0f, 1f)]
    public float bombProbability; 


    void Start()
    {
        // Iniciar bucle
        StartCoroutine(SpawnCubes());

        // Que dependiendo de la escena haya bombas o no
        string nombreEscena = SceneManager.GetActiveScene().name;

        // Si estamos en "BeatSaberFlechas", solo se instancian cubos con flechas
        if (nombreEscena == "BeatSaberFlechas")
        {
            // Hacer que solo se instancien proyectiles con flechas (sin cubos normales ni bombas)
            bombProbability = 0f;
        }
        // Si estamos en "BeatSaberAvanzado", podemos instanciar cubos normales y bombas
        else if (nombreEscena == "BeatSaberAvanzado")
        {
            bombProbability = 0.35f; // Ajusta la probabilidad de bomba según tu preferencia
        }
        // Si estamos en "MiniBeatSaber", solo se instancian cubos normales
        else if (nombreEscena == "MiniBeatSaber")
        {
            bombProbability = 0f;  // No queremos bombas ni cubos con flechas
        }

    }

    IEnumerator SpawnCubes()
    {
        while (!juegoTerminado) // Bucle para instanciar cubos (solo sigue si el juego no ha terminado)
        {
            // Calcular offset aleatorio para variar la dirección
            float offset = Random.Range(-destinationOffsetRange, destinationOffsetRange);

            // Calcular  posición de instanciación
            Vector3 spawnPos = jugadorTransform.position + (jugadorTransform.forward * distancia);
            spawnPos += jugadorTransform.right * offset;

            // Fijar altura en la generación del proyectil
            spawnPos.y = 1f;

            // Lógica para elegir qué tipo de proyectil instanciar
            GameObject prefabAInstanciar;

            // Determinamos qué proyectil lanzar dependiendo de la escena
            if (bombProbability > 0f && Random.value < bombProbability)
            {
                prefabAInstanciar = bombaPrefab; // Instanciar bomba
            }
            else if (SceneManager.GetActiveScene().name == "BeatSaberFlechas" && proyectilesFlecha.Length > 0) // Solo si estamos en "BeatSaberFlechas"
            {
                int index = Random.Range(0, proyectilesFlecha.Length); // Seleccionar cubo con flecha
                Debug.Log("Instanciando: " + proyectilesFlecha[index].name);
                prefabAInstanciar = proyectilesFlecha[index];
            }
            else
            {
                prefabAInstanciar = proyectilPrefab; // Instanciar cubo normal
            }

            // Instanciar el proyectil
            GameObject cube = Instantiate(prefabAInstanciar, spawnPos, Quaternion.identity);

            // Dar dirección hacia el jugador con un ligero offset
            cube.AddComponent<ProyectilMov>().Initialize(jugadorTransform.position, offset);

            // Esperar un tiempo aleatorio (2-3'') antes de instanciar el siguiente cubo
            yield return new WaitForSeconds(Random.Range(tiempoSpawnMin, tiempoSpawnMax));
        }
    }

    // Detener el instanciar cubos (FIN)
    public void DetenerDisparos()
    {
        juegoTerminado = true;
        Debug.Log("Fin del juego, no se lanzan más cubos");
    }

}
