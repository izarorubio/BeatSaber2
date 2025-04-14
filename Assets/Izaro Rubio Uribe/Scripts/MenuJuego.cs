using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.InputSystem;
using InputDevice = UnityEngine.XR.InputDevice;

public class MenuJuego : MonoBehaviour
{
    public GameObject menuUI;               // El panel del menú
    public Transform cabezaJugador;         // La cámara o cabeza del jugador (XR Rig)
    public float distanciaMenu = 2f;        // Distancia del menú con respecto a la cabeza
    private bool menuActivo = false;        // Controla si el menú está activo o no

    void Start()
    {
        menuUI.SetActive(false);             // Desactivar menú al inicio
    }

    void Update()
    {
        // Simulación del botón de menú con la tecla 'M' en el editor
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
            // Asegurarse de que el menú no esté mirando al revés (voltearlo)
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
