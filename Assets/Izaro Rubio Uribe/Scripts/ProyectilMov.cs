using UnityEngine;
using System.Collections;


public class ProyectilMov : MonoBehaviour
{
    public float velocidad = 5f;
    public float alturaFija = 1.25f; // Altura fija sobre el suelo
    private Vector3 direccion;

    public void Initialize(Vector3 target, float offset)
    {
        // Aplicar random el offset en la posición final
        Vector3 destination = new Vector3(target.x + offset, alturaFija, target.z + offset);

        // Calcular la dirección hacia el destino con offset
        direccion = (destination - transform.position).normalized;

        // Fijar altura al proyectil/cubo
        transform.position = new Vector3(transform.position.x, alturaFija, transform.position.z);

        // Iniciar corrutina para destruir proyectil si no se golpea(para que no se llene la escena)
        StartCoroutine(DestroyAfterTime(7f));
    }

    void Update()
    {
        // Mover proyectil en la dirección calculada (con altura fija)
        transform.position += new Vector3(direccion.x, 0, direccion.z) * velocidad * Time.deltaTime;
        transform.position = new Vector3(transform.position.x, alturaFija, transform.position.z);
    }

    //Currutina para destruir proyectil en 7''
    private IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
