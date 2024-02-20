using UnityEngine;
using UnityEngine.UI;

public class GazeSelection : MonoBehaviour
{
    public float gazeDuration = 2f;
    public Image progressBar; // UI element to represent the progress of gaze selection
    public GameObject selectionIndicator; // Visual indicator for selection

    private bool isGazing;
    private float gazeTimer;

    private void Update()
    {
        if (isGazing)
        {
            gazeTimer += Time.deltaTime;
            progressBar.fillAmount = gazeTimer / gazeDuration; // Update progress bar

            if (gazeTimer >= gazeDuration)
            {
                // Execute action for selected item
                ExecuteSelection();
                ResetGaze();
            }
        }
    }

    private void ExecuteSelection()
    {
        // Determine which UI element is being gazed at and perform corresponding action
        // For example, you can use switch-case statements or check the name of the currently gazed object
        switch (selectionIndicator.name)
        {
            case "LaunchButton":
                // Load game scene
                break;
            case "QuitButton":
                // Quit application
                break;
            case "SettingsButton":
                // Open settings menu
                break;
            case "OptionsButton":
                // Open options menu
                break;
            default:
                break;
        }
    }

    private void ResetGaze()
    {
        isGazing = false;
        gazeTimer = 0f;
        progressBar.fillAmount = 0f;
        selectionIndicator.SetActive(false);
    }

    public void OnGazeEnter(GameObject target)
    {
        isGazing = true;
        selectionIndicator.SetActive(true);
        selectionIndicator.transform.position = target.transform.position;
    }

    public void OnGazeExit()
    {
        ResetGaze();
    }
}
