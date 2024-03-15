using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public GameObject fieldPlayer;
    public PlayerInput Input { get; private set; }
    public Field field;

    public Tile selectedTile;

    private void Awake()
    {
        Input = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        Input.ClickActions.MouseClick.started += OnClickStart;
    }

    private void OnClickStart(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.ClickActions.MousePos.ReadValue<Vector2>());
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null && CompareTag("Tile"))
        {
            selectedTile = hit.collider.GetComponent<Tile>();
            selectedTile.MoveButton();
        }

    }
}
