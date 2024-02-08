using UnityEngine;
using UnityEngine.UI;

public class ImageSwapper : MonoBehaviour
{
    public static ImageSwapper Instance;

    public Image reactionImage;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void DisplayImage(string imageName)
    {
        // Load the image resource based on imageName
        Sprite imageSprite = Resources.Load<Sprite>("Images/" + imageName);

        // Display the image
        if (imageSprite != null)
            reactionImage.sprite = imageSprite;
        else
            Debug.LogError("Image not found: " + imageName);
    }
}
