using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuOpciones : MonoBehaviour
{
    public Dropdown m_Dropdown; // Dropdown para elegir puntos objetivo
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

        PlayerPrefs.SetInt("puntObj", 10);
    }

    // Para guardar la preferencia del toggle en PlayerPrefs
    public void OnToggleChanged(bool isOn)
    {
        // (1 = activado, 0 = desactivado)
        PlayerPrefs.SetInt("SableDoble", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    //Cambiar los puntos objetivo a los que el jugador seleccione
    public void changePuntuacionMax()
    {
        Debug.Log(m_Dropdown.value);
        if (m_Dropdown.value == 1)
            PlayerPrefs.SetInt("puntObj", 20);
        else if (m_Dropdown.value == 2)
            PlayerPrefs.SetInt("puntObj", 30);
        else
            PlayerPrefs.SetInt("puntObj", 10);
    }
}
