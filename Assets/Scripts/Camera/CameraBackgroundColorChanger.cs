using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackgroundColorChanger : MonoBehaviour
{
    public Color startColor = new Color32(38, 32, 53, 255); // #262035
    public Color endColor = Color.black; // #000000
    public float duration = 100f; // 100 seconds

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        mainCamera.backgroundColor = startColor;
    }

    void Update()
    {
        if (Player.isPlaying)
        {
            StartCoroutine(ChangeBackgroundColor());
        }
    }

    IEnumerator ChangeBackgroundColor()
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            mainCamera.backgroundColor = Color.Lerp(startColor, endColor, elapsed / duration);
            yield return null;
        }
        // Ensure the color is set to the exact end color at the end of the duration
        mainCamera.backgroundColor = endColor;
    }
}
