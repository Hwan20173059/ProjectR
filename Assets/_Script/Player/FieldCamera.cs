using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCamera : MonoBehaviour
{
    public TileMapManager tileMapManager;

    public Tile playerTile;
    public float cameraSpeed;

    public GameObject fieldSize;

    public bool isTurn;

    float width;
    float height;

    private Vector2 clickPoint;
    private Vector3 touchStart;

    private void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void LateUpdate()
    {
        if (!isTurn)
        {
            playerTile = tileMapManager.currentTile;

            transform.position = Vector3.Lerp(transform.position, playerTile.transform.position, Time.deltaTime);

            float lx = fieldSize.GetComponent<BoxCollider2D>().size.x * 0.5f - width;
            float clampX = Mathf.Clamp(transform.position.x, -lx + fieldSize.GetComponent<Transform>().position.x, lx + fieldSize.GetComponent<Transform>().position.x);

            float ly = fieldSize.GetComponent<BoxCollider2D>().size.y * 0.5f - height;
            float clampY = Mathf.Clamp(transform.position.y, -ly + fieldSize.GetComponent<Transform>().position.y, ly + fieldSize.GetComponent<Transform>().position.y);

            transform.position = new Vector3(clampX, clampY, -10);
        }
        else
        {
            // 마우스 최초 클릭 시의 위치 기억
            if (Input.GetMouseButtonDown(0))
            {
                touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 position = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Camera.main.transform.position = new Vector3(
                    Camera.main.transform.position.x + position.x, 
                    Camera.main.transform.position.y + position.y, 
                    Camera.main.transform.position.z
                    );
            }

            float lx = fieldSize.GetComponent<BoxCollider2D>().size.x * 0.5f - width;
            float clampX = Mathf.Clamp(transform.position.x, -lx + fieldSize.GetComponent<Transform>().position.x, lx + fieldSize.GetComponent<Transform>().position.x);

            float ly = fieldSize.GetComponent<BoxCollider2D>().size.y * 0.5f - height;
            float clampY = Mathf.Clamp(transform.position.y, -ly + fieldSize.GetComponent<Transform>().position.y, ly + fieldSize.GetComponent<Transform>().position.y);

            transform.position = new Vector3(clampX, clampY, -10);
        }
    }
}
