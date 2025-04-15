using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using InputDevice = UnityEngine.XR.InputDevice;

public class MenuJuego : MonoBehaviour
{
    public GameObject menuUI;               // Panel del menú
    public Transform cabezaJugador;         // (la cámara)
    public float distanciaMenu = 2f;        // Distancia del menú con respecto a la cabeza
    private bool menuActivo = false;        // Bool para controlar si el menú está activo o no

    void Start()
    {
        menuUI.SetActive(false);             // Menú desactivado al principio
    }

    void Update()
    {
        // Simulación del botón de menú con la tecla 'M' (para probarlo en el portátil)
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            ToggleMenu();
        }

        // Detectar el botón de menú del mando izquierdo (si estamos en VR)
        InputDevice leftHand = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
        bool botonMenuPresionado = false;

        if (leftHand.TryGetFeatureValue(UnityEngine.XR.CommonUsages.menuButton, out botonMenuPresionado) && botonMenuPresionado)
        {
            if (!menuActivo)
                ToggleMenu();
        }

        // Si el menú está activo, actualizar su posición y rotación
        if (menuActivo)
        {
            // Calcular la nueva posición del menú, asegurándose que el menú esté a una distancia en frente del jugador
            Vector3 pos = cabezaJugador.position + cabezaJugador.forward * distanciaMenu;
            pos.y = cabezaJugador.position.y;  // Mantener la altura igual a la del jugador

            // Establecer la posición del menú
            menuUI.transform.position = pos;

            // Hacer que el menú mire hacia la cámara (jugador)
            menuUI.transform.LookAt(cabezaJugador.position);
            // Asegura que el menú no esté mirando al revés (voltearlo)
            menuUI.transform.forward = -menuUI.transform.forward;
        }
    }

    // Toggle para mostrar/ocultar el menú
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

    // Función para reanudar el juego
    public void Reanudar()
    {
        menuActivo = false;
        menuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    // Función para salir al menú principal
    public void Salir()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
