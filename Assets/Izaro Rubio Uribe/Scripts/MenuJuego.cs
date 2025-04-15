using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using InputDevice = UnityEngine.XR.InputDevice;

public class MenuJuego : MonoBehaviour
{
    public GameObject menuUI;               // Panel del men�
    public Transform cabezaJugador;         // (la c�mara)
    public float distanciaMenu = 2f;        // Distancia del men� con respecto a la cabeza
    private bool menuActivo = false;        // Bool para controlar si el men� est� activo o no

    void Start()
    {
        menuUI.SetActive(false);             // Men� desactivado al principio
    }

    void Update()
    {
        // Simulaci�n del bot�n de men� con la tecla 'M' (para probarlo en el port�til)
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            ToggleMenu();
        }

        // Detectar el bot�n de men� del mando izquierdo (si estamos en VR)
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        bool botonMenuPresionado = false;

        if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out botonMenuPresionado) && botonMenuPresionado)
        {
            if (!menuActivo)
                ToggleMenu();
        }

        // Si el men� est� activo, actualizar su posici�n y rotaci�n
        if (menuActivo)
        {
            // Calcular la nueva posici�n del men�, asegur�ndose que el men� est� a una distancia en frente del jugador
            Vector3 pos = cabezaJugador.position + cabezaJugador.forward * distanciaMenu;
            pos.y = cabezaJugador.position.y;  // Mantener la altura igual a la del jugador

            // Establecer la posici�n del men�
            menuUI.transform.position = pos;

            // Hacer que el men� mire hacia la c�mara (jugador)
            menuUI.transform.LookAt(cabezaJugador.position);
            // Asegura que el men� no est� mirando al rev�s (voltearlo)
            menuUI.transform.forward = -menuUI.transform.forward;
        }
    }

    // Toggle para mostrar/ocultar el men�
    public void ToggleMenu()
    {
        menuActivo = !menuActivo;
        menuUI.SetActive(menuActivo);

        if (menuActivo)
        {
            Time.timeScale = 0f;  // Pausar el juego
        }
        else
        {
            Time.timeScale = 1f;  // Reanudar el juego
        }
    }

    // Funci�n para reanudar el juego
    public void Reanudar()
    {
        menuActivo = false;
        menuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    // Funci�n para salir al men� principal
    public void Salir()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
