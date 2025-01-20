using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [System.Serializable]
    public class CanvasControl
    {
        [SerializeField] public string name;
        [SerializeField] public Canvas canvas;
        [SerializeField] public KeyCode activationKey;
        [SerializeField] public Button activationButton;
        [SerializeField] public Button deactivationButton; // Nuevo botón de desactivación
    }

    public CanvasControl[] canvasControls;

    void Start()
    {
        for (int i = 0; i < canvasControls.Length; i++)
        {
            if (canvasControls[i].activationButton != null)
            {
                int index = i;
                canvasControls[i].activationButton.onClick.AddListener(() => ToggleCanvas(canvasControls[index].canvas, true));
            }

            if (canvasControls[i].deactivationButton != null)
            {
                int index = i;
                canvasControls[i].deactivationButton.onClick.AddListener(() => ToggleCanvas(canvasControls[index].canvas, false));
            }
        }
    }

    void Update()
    {
        for (int i = 0; i < canvasControls.Length; i++)
        {
            if (Input.GetKeyDown(canvasControls[i].activationKey))
            {
                ToggleCanvas(canvasControls[i].canvas, !canvasControls[i].canvas.gameObject.activeSelf);
            }
        }
    }

    private void ToggleCanvas(Canvas canvas, bool state)
    {
        if (canvas != null)
        {
            canvas.gameObject.SetActive(state);
        }
        else
        {
            Debug.Log("No has asignado un canvas");
        }
    }

    public bool IsCanvasActive(string canvasName)
    {
        for (int i = 0; i < canvasControls.Length; i++)
        {
            if (canvasControls[i].name == canvasName && canvasControls[i].canvas.gameObject.activeSelf)
            {
                return true;
            }
        }
        return false;
    }
}
