using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ProyectilSpawner : MonoBehaviour
{
    public GameObject proyectilPrefab;
    public GameObject bombaPrefab;
    public GameObject[] proyectilesFlecha;

    public Transform jugadorTransform; // Transform del jugador (c�mara)
    public float distancia = 10f; // Distancia fija desde el jugador
    // Tiempos entre instancias
    public float tiempoSpawnMin = 2f; 
    public float tiempoSpawnMax = 3f;
    // Rango para variar la direcci�n del cubo
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

        // En "BeatSaberFlechas", solo se instancian cubos con flechas
        if (nombreEscena == "BeatSaberFlechas")
        {           
            bombProbability = 0f;  // No hay probabilidad de que aparezcan bombas
        }
        // En "BeatSaberAvanzado"hay cubos normales y bombas
        else if (nombreEscena == "BeatSaberAvanzado")
        {
            bombProbability = 0.35f; // Probabilidad de bombas
        }
        // En "MiniBeatSaber", solo se instancian cubos normales
        else if (nombreEscena == "MiniBeatSaber")
        {
            bombProbability = 0f; // No hay probabilidad de que aparezcan bombas
        }

    }

    IEnumerator SpawnCubes()
    {
        while (!juegoTerminado) // Bucle para instanciar cubos (solo sigue si el juego no ha terminado)
        {
            // Calcular offset aleatorio para variar la direcci�n
            float offset = Random.Range(-destinationOffsetRange, destinationOffsetRange);

            // Calcular  posici�n de instanciaci�n
            Vector3 spawnPos = jugadorTransform.position + (jugadorTransform.forward * distancia);
            spawnPos += jugadorTransform.right * offset;

            // Fijar altura en la generaci�n del proyectil
            spawnPos.y = 1f;

            // L�gica para elegir qu� tipo de proyectil instanciar
            GameObject prefabAInstanciar;

            // Dependiendi de la escena se lanzan proyectiles diferentes:
            //Si la probabilidad de que aparezcan bombas es mayor que cero...
            if (bombProbability > 0f && Random.value < bombProbability)
            {
                prefabAInstanciar = bombaPrefab; // Instanciar bomba
            }
            // Si estamos en la escena "BeatSaberFlechas"...
            else if (SceneManager.GetActiveScene().name == "BeatSaberFlechas" && proyectilesFlecha.Length > 0)
            {
                int index = Random.Range(0, proyectilesFlecha.Length); // Seleccionar cubo con flecha (direcci�n random)
                Debug.Log("Instanciando: " + proyectilesFlecha[index].name); // Debug para saber cu�l es la que est� instanciando
                prefabAInstanciar = proyectilesFlecha[index];
            }
            else
            {
                prefabAInstanciar = proyectilPrefab; // Instanciar cubo normal (para nivel Inicial)
            }

            // Instanciar el proyectil
            GameObject cube = Instantiate(prefabAInstanciar, spawnPos, Quaternion.identity);

            // Dar direcci�n hacia el jugador con un ligero offset
            cube.AddComponent<ProyectilMov>().Initialize(jugadorTransform.position, offset);

            // Esperar un tiempo aleatorio (2-3'') antes de instanciar el siguiente cubo
            yield return new WaitForSeconds(Random.Range(tiempoSpawnMin, tiempoSpawnMax));
        }
    }

    // Detener el instanciar cubos (FIN)
    public void DetenerDisparos()
    {
        juegoTerminado = true;
        Debug.Log("Fin del juego, no se lanzan m�s cubos");
    }

}
