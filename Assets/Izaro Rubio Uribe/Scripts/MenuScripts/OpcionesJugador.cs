using UnityEngine;

public class OpcionesJugador : MonoBehaviour
{
    // Instancia estática para acceder globalmente (patrón Singleton)
    public static OpcionesJugador instancia;

    public bool usarSableDoble = false;

    public int puntosObjetivo;

    // Para inicializar el singleton para que estén las opciones antes de lo demás (Awake se ejecuta antes que Start)
    private void Awake()
    {
        // Si ya existe una instancia y no es esta, destruirla (por si acaso)
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
        }
        else
        {
            // Si no hay instancia, esta se convierte en la única
            instancia = this;

            // Para que no se destruya al cambiar de escena
            DontDestroyOnLoad(gameObject);
        }
    }
}
