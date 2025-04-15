using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enumeraci�n para las direcciones correctas de los cortes
public enum DireccionCorte
{
    Arriba,
    Abajo,
    Izquierda,
    Derecha
}
public class ProyectilFlecha : MonoBehaviour
{
    // Direcci�n que debe tener el corte para que sea v�lido (asignada en el prefab)
    public DireccionCorte direccionCorrecta;

    public bool ValidarCorte(Vector3 direccionCorte) // (Llamadado desde el sable: true si el corte es v�lido, false si no)
    {
        direccionCorte.Normalize(); // Normalizar el vector para asegurar que tenga longitud 1


        // Comparamos con la direcci�n esperada (si el valor es cercano a 1 (0.7) se da por bueno)
        switch (direccionCorrecta)
        {
            case DireccionCorte.Arriba:
                return Vector3.Dot(direccionCorte, Vector3.up) > 0.7f;
            case DireccionCorte.Abajo:
                return Vector3.Dot(direccionCorte, Vector3.down) > 0.7f;
            case DireccionCorte.Izquierda:
                return Vector3.Dot(direccionCorte, Vector3.left) > 0.7f;
            case DireccionCorte.Derecha:
                return Vector3.Dot(direccionCorte, Vector3.right) > 0.7f;
            default:
                return false;
        }
    }
}
