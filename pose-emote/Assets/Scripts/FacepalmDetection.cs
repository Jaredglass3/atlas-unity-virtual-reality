using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacePalmDetection : MonoBehaviour
{
    // Reference to the image object
    public Image popUpImage;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the hand
        if (other.CompareTag("Hand"))
        {
            // Trigger the image pop-up
            popUpImage.gameObject.SetActive(true);
        }

        Debug.Log("Detected; "+ other.gameObject.name);
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the collider belongs to the hand
        if (other.CompareTag("Hand"))
        {
            // Deactivate the image object
            popUpImage.gameObject.SetActive(false);
        }
    }
}