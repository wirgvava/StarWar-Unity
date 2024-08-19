using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject ship_1;
    public GameObject ship_2;
    public GameObject ship_3;
    public GameObject ship_4;
    public GameObject ship_5;
    public GameObject ship_6;

    public GameObject[] ships;

    public float speed = 50f;
    public float offsetY = 0.5f;

    public static bool isPlaying = false;
    public static bool isPlayable = true;
    public static bool isGameOver = false;

    void Start()
    {
        ships = new GameObject[] { ship_1, ship_2, ship_3, ship_4,  ship_5, ship_6 };
        SetActiveCurrentShip();
    }

    void Update()
    {
        if (isPlayable)
        {
            // Check for touch input
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                // Move the player towards the touch position
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchPos.z = 0f;
                touchPos.y += offsetY;


                // Cast a ray from the touch position
                RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);

                if (hit.collider != null)
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        isPlaying = true;
                        transform.position = Vector3.MoveTowards(transform.position, touchPos, speed * Time.deltaTime);
                    }
                }
            }
        }
    }

    private void SetActiveCurrentShip()
    {
        int index = 1;
        foreach (GameObject ship in ships)
        {
            if (index == GameController.ChoosenShip)
            {
                ship.SetActive(true);
            }
            else 
            {
                ship.SetActive(false);
            }

            index ++;
        }
    }
}
