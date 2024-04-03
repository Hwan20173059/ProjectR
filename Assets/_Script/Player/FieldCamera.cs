using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldCamera : MonoBehaviour
{
    public TileMapManager tileMapManager;

    public Tile playerTile;
    public float cameraSpeed;

    public GameObject fieldSize;

    float width;
    float height;

    private void Start()
    {
        height = Camera.main.orthographicSize;
        width = height * Screen.width / Screen.height;
    }

    private void LateUpdate()
    {
        playerTile = tileMapManager.currentTile;

        transform.position = Vector3.Lerp(transform.position, playerTile.transform.position, Time.deltaTime);

        float lx = fieldSize.GetComponent<BoxCollider2D>().size.x * 0.5f - width;
        float clampX = Mathf.Clamp(transform.position.x, -lx + fieldSize.GetComponent<Transform>().position.x, lx + fieldSize.GetComponent<Transform>().position.x);

        float ly = fieldSize.GetComponent<BoxCollider2D>().size.y * 0.5f - height;
        float clampY = Mathf.Clamp(transform.position.y, -ly + fieldSize.GetComponent<Transform>().position.y, ly + fieldSize.GetComponent<Transform>().position.y);

        transform.position = new Vector3(clampX, clampY, -10);
    }
}
