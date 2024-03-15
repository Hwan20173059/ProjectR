using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TileState
{
    empty,
    player,
    monster,
    chest,
    town,
    dungeon
}

public class Tile : MonoBehaviour
{
    public FieldManager fieldManager;
    public TileState tileState;
    public GameObject onTile;
    public GameObject highlight;
    public GameObject player;

    private void Start()
    {
        RefreshTile();
    }


    public void RefreshTile()
    {
        if (onTile)
        {
            tileState = TileState.player;
            player = Instantiate(onTile, this.gameObject.transform);
        }
        else
            return;
    }

    private void OnMouseEnter()
    {
        highlight.SetActive(true);
    }

    private void OnMouseDown()
    {
        fieldManager.selectedTile = this;
        fieldManager.PlayerMoveTile();
        tileState = TileState.player;
        onTile = fieldManager.fieldPlayer;
        RefreshTile();
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    public void ClearTile()
    {
        tileState = TileState.empty;
        onTile = null;
        Destroy(player);
    }
}
