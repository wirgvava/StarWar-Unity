using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static float cameraSpeed = 5f;
    public float speedIncrement = 2f;
    public float interval = 10f;
    private bool isCoroutineRunning = false;


    void Update()
    {
        if (Player.isPlaying)
        {
            // Start the coroutine only if it's not already running
            if (!isCoroutineRunning)
            {
                StartCoroutine(IncreaseSpeedOverTime());
                isCoroutineRunning = true;
            }
        }
        else
        {
            // Reset the camera speed to the default value when the player stops playing
            cameraSpeed = 5f;
            isCoroutineRunning = false;
            StopCoroutine(IncreaseSpeedOverTime()); // Stop the coroutine
        }

        // Move the camera
        transform.position += new Vector3(0, cameraSpeed * Time.deltaTime, 0);
    }

    IEnumerator IncreaseSpeedOverTime()
    {
        while (Player.isPlaying)
        {
            yield return new WaitForSeconds(interval);
            cameraSpeed += speedIncrement;
        }
    }
}
