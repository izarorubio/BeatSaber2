using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DireccionCorte
{
    Arriba,
    Abajo,
    Izquierda,
    Derecha
}
public class ProyectilFlecha : MonoBehaviour
{
    public DireccionCorte direccionCorrecta;

    // Llamado desde el sable con el vector de corte
    public bool ValidarCorte(Vector3 direccionCorte)
    {
        direccionCorte.Normalize();

        // Comparamos con la dirección esperada
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
