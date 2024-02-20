using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class GazeOverButton : MonoBehaviour
{
    public Transform raycastOrigin; // Assign this to your Camera or any GameObject
    public float maxRaycastDistance = 100f; // Max distance for the raycast
    public float activationTime = 5f; // Time required for the button to be activated
    public AudioClip hoverSound; // Sound to play when hovering over the button
    public AudioClip activationSound; // Sound to play when the button is activated
    private AudioSource audioSource; // AudioSource component for playing the sound
    private float currentActivationTime = 0f; // Time the ray has been on the button
    private bool buttonActivated = false; // Flag to track if the button is activated
    private Button lastHighlightedButton; // Reference to the last highlighted button
    public LayerMask LayerMaskVariable;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        CastRay();
    }

    private void CastRay()
    {
        // Ensure we have a valid raycast origin reference
        if (raycastOrigin == null)
        {
            Debug.LogError("Raycast origin is not set!");
            return;
        }

        // Create a ray from the raycast origin position along its forward direction
        Ray ray = new Ray(raycastOrigin.position, raycastOrigin.forward);

        // Debug ray visualization
        Debug.DrawRay(ray.origin, ray.direction * maxRaycastDistance, Color.red);

        // Perform the raycast
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxRaycastDistance, LayerMaskVariable))
        {
            Debug.Log("Hit"+ hit.transform.gameObject.name);
            // Check if the hit object has a Button component
            Button button = hit.collider.gameObject.GetComponent<Button>();
            if (button != null)
            {
                // Play hover sound
                if (audioSource != null && hoverSound != null && !audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(hoverSound);
                }

                // Highlight the button
                if (lastHighlightedButton != button)
                {
                    if (lastHighlightedButton != null)
                    {
                        lastHighlightedButton.colors = ColorBlock.defaultColorBlock; // Reset last highlighted button
                    }
                    lastHighlightedButton = button;
                    ColorBlock colors = button.colors;
                    colors.normalColor = Color.yellow; // Set highlight color
                    button.colors = colors;
                }

                // If the button is not yet activated, start counting time
                if (!buttonActivated)
                {
                    currentActivationTime += Time.deltaTime;
                    // If the activation time is reached, trigger button click
                    if (currentActivationTime >= activationTime)
                    {
                        button.onClick.Invoke();
                        buttonActivated = true;
                        // Play activation sound
                        if (audioSource != null && activationSound != null)
                        {
                            audioSource.PlayOneShot(activationSound);
                        }
                    }
                }
            }
            else
            {
                // Reset timer and highlighting if the ray hits something other than the button
                currentActivationTime = 0f;
                buttonActivated = false;
                if (lastHighlightedButton != null)
                {
                    lastHighlightedButton.colors = ColorBlock.defaultColorBlock; // Reset last highlighted button
                    lastHighlightedButton = null;
                }
                // Stop the audio if raycast doesn't hit the button
                if (audioSource != null && audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }
        }
        else
        {
            // Reset timer and highlighting if the ray doesn't hit anything
            currentActivationTime = 0f;
            buttonActivated = false;
            if (lastHighlightedButton != null)
            {
                lastHighlightedButton.colors = ColorBlock.defaultColorBlock; // Reset last highlighted button
                lastHighlightedButton = null;
            }
            // Stop the audio if raycast doesn't hit anything
            if (audioSource != null && audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
