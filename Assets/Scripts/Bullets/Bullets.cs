using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    public float cameraSpeed = 10f;
    public float speedIncrement = 2f;
    public float interval = 10f;

    // Update is called once per frame
    void Update()
    {
        this.cameraSpeed = CameraMovement.cameraSpeed + 5f;
        transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }
}
