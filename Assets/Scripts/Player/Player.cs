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
    private Vector3 touchOffset;
    private bool isDragging = false;


    public static bool isPlaying = false;
    public static bool isPlayable = true;
    public static bool isGameOver = false;
    public static bool isAddedHighScoreToLeaderboard = false;

    void Start()
    {
        ships = new GameObject[] { ship_1, ship_2, ship_3, ship_4,  ship_5, ship_6 };
        SetActiveCurrentShip();
    }

    void Update()
    {
        if (isPlayable)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                touchPos.z = 0f;

                if (touch.phase == TouchPhase.Began)
                {
                    RaycastHit2D hit = Physics2D.Raycast(touchPos, Vector2.zero);
                    if (hit.collider != null && hit.collider.CompareTag("Player"))
                    {
                        isPlaying = true;
                        isDragging = true;
                        touchOffset = transform.position - touchPos;
                        touchOffset.z = 0f; // Maintain the same z-axis position
                        touchOffset.y += offsetY; // Adjust for offset above the finger
                    }
                }
                else if (touch.phase == TouchPhase.Moved && isDragging)
                {
                    Vector3 newPos = touchPos + touchOffset;
                    transform.position = Vector3.MoveTowards(transform.position, newPos, speed * Time.deltaTime);
                }
                else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    isDragging = false;
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
