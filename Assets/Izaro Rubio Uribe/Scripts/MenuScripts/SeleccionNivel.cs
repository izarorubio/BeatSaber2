using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;



public class SeleccionadorNiveles : MonoBehaviour
{
    // Nombre de la escena que se carga al pulsar
    [SerializeField] private string escenaNivelElegido;

    private void Start()
    {
        // Obtener componente XRBaseInteractable del objeto
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();

        // Si el objeto es interactuable, se suscribe al evento
        if (interactable != null)
        {
            // Cuando se pulse, llamar a CambiarEscenaNivel
            interactable.selectEntered.AddListener(CambiarEscenaNivel);
        }
    }

    private void CambiarEscenaNivel(SelectEnterEventArgs args)
    {
        // Guardamos las opciones seleccionadas en el menú
        FindObjectOfType<MenuOpciones>()?.GuardarOpciones();

        // Cargar la escena del nivel elegido
        SceneManager.LoadScene(escenaNivelElegido);
    }
}
