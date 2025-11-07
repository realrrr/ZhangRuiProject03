using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandUI : MonoBehaviour
{
    [SerializeField] private Canvas handCanvas;
    [SerializeField] private InputActionReference toggleAction;

    private bool isVisible;


    void OnEnable()
    {
        if (toggleAction != null && toggleAction.action != null)
        {
            toggleAction.action.performed += OnTogglePerformed;
            toggleAction.action.Enable();
        }
    }

    void OnDisable()
    {
        if (toggleAction != null && toggleAction.action != null)
        {
            toggleAction.action.performed -= OnTogglePerformed;
        }
    }

    private void OnTogglePerformed(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton())
        {
            Toggle();
        }
    }

    public void Toggle()
    {
        SetVisibility(!isVisible);
    }

    public void SetVisibility(bool visible)
    {
        if (handCanvas == null) return;
        handCanvas.enabled = visible;
        isVisible = visible;
    }
}
