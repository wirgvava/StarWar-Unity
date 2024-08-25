using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingBackground : MonoBehaviour
{
    public float backgroundSpeed = 0.5f;
    public float speedIncrement = 0.1f;
    public float interval = 10f;
    private bool isCoroutineRunning = false;

    public Renderer backgroundRenderer;

    // Update is called once per frame
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
            backgroundSpeed = 0.5f;
            isCoroutineRunning = false;
            StopCoroutine(IncreaseSpeedOverTime()); // Stop the coroutine
        }

        backgroundRenderer.material.mainTextureOffset += new Vector2(0f, backgroundSpeed * Time.deltaTime);             
    }

    IEnumerator IncreaseSpeedOverTime()
    {
        while (Player.isPlaying)
        {
            yield return new WaitForSeconds(interval);
            backgroundSpeed += speedIncrement;
        }
    }
}
