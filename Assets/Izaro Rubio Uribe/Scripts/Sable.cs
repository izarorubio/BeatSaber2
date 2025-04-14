using UnityEngine;

public class Sable : MonoBehaviour
{
    private Vector3 ultimaPosicion;
    private Vector3 direccionCorte;

    private void Update()
    {
        // Calcular dirección de movimiento del sable
        direccionCorte = (transform.position - ultimaPosicion) / Time.deltaTime;
        ultimaPosicion = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        //Si el objeto con el que colisiona tiene tag "Cube"...
        if (other.CompareTag("Cube"))
        {
            // Destruir el cubo al golpear
            Destroy(other.gameObject);
            // Sumar puntos en el marcador
            FindAnyObjectByType<Marcador>().SumarPuntos();

        }
        //Si el objeto con el que colisiona tiene tag "Bomba"...
        else if (other.CompareTag("Bomba")) //
        {
            // Destruir el cubo al golpear
            Destroy(other.gameObject);
            // Restar puntos en el marcador
            FindAnyObjectByType<Marcador>().RestarPuntos();
        }
        // Si colisiona con un cubo flecha
        else if (other.CompareTag("CuboFlecha"))
        {
            ProyectilFlecha cuboFlecha = other.GetComponent<ProyectilFlecha>();
            if (cuboFlecha != null)
            {
                if (cuboFlecha.ValidarCorte(direccionCorte))
                {
                    Destroy(other.gameObject);
                    FindAnyObjectByType<Marcador>().SumarPuntos();
                }
                else
                {
                    Debug.Log("¡Corte incorrecto!");
                }
            }
        }
    }
}
