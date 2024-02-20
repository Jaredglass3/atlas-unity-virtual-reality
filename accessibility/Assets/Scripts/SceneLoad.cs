using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    // Function to be called when the button is clicked
    public void ChangeToOptionsScene()
    {
        // Load the scene named "Options"
        SceneManager.LoadScene("Options");

        // Debug log to indicate that the button was triggered
        Debug.Log("Button clicked: Changing to Options scene.");
    }
}
