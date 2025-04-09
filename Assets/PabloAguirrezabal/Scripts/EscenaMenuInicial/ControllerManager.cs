using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerManager : MonoBehaviour
{
    public InputActionReference customAction;
    void Start()
    {
        customAction.action.started += menuPulsado;
    }



    void menuPulsado(InputAction.CallbackContext context)
    {
        Debug.Log("menu pulsado");
    }
}
