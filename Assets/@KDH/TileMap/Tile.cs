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


    private void Start()
    {
        RefreshTile();
    }

    public void RefreshTile()
    {
        if (onTile)
            Instantiate(onTile, this.gameObject.transform);
        else
            return;
    }

    public void MoveButton()
    {
        tileState = TileState.player;
        onTile = fieldManager.fieldPlayer;
    }

    public void ClearTile()
    {
        tileState = TileState.empty;
        onTile = null;
    }
}
