using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuOpciones : MonoBehaviour
{
    public TMP_Dropdown puntosDropdown; // Dropdown para elegir puntos objetivo
    public Toggle dobleSableToggle; // Toggle para activar/desactivar el sable doble


    void Start()
    {
        // Si ya se ha guardado una preferencia, usarla
        if (PlayerPrefs.HasKey("SableDoble"))
        {
            bool activo = PlayerPrefs.GetInt("SableDoble") == 1;
            dobleSableToggle.isOn = activo;
        }

        // Registrar evento cuando el jugador cambie el toggle
        dobleSableToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    // Para guardar la preferencia del toggle en PlayerPrefs
    public void OnToggleChanged(bool isOn)
    {
        // (1 = activado, 0 = desactivado)
        PlayerPrefs.SetInt("SableDoble", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    // Para guardar los puntos objetivo seleccionados en el objeto singleton
    public void GuardarOpciones()
    {
        OpcionesJugador.instancia.puntosObjetivo = int.Parse(puntosDropdown.options[puntosDropdown.value].text);
    }
}
