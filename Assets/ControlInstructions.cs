using UnityEngine;

public class ControlInstructions : MonoBehaviour
{
    public GameObject controlPanel; // Reference to the control instructions panel

    private bool controlsVisible = false; // Flag to track the visibility of control instructions

    private void Update()
    {
        // Check if the left Shift key is pressed
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!controlsVisible)
            {
                ShowControlInstructions();
            }
            else
            {
                HideControlInstructions();
            }
        }
    }

    private void ShowControlInstructions()
    {
        controlPanel.SetActive(true);
        controlsVisible = true;
    }

    private void HideControlInstructions()
    {
        controlPanel.SetActive(false);
        controlsVisible = false;
    }
}
