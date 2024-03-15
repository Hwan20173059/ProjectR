using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldManager : MonoBehaviour
{
    public GameObject fieldPlayer;
    public PlayerInput Input { get; private set; }
    public Field field;

    public Tile selectedTile;


    public void PlayerMoveTile()
    {
        for (int i =0; i < field.tileRaw.Length; i++)
        {
            for(int j = 0; j < field.tileRaw[i].fieldTiles.Length; j++)
            {
                if (field.tileRaw[i].fieldTiles[j].tileState == TileState.player)
                {
                    field.tileRaw[i].fieldTiles[j].ClearTile();
                    field.tileRaw[i].fieldTiles[j].RefreshTile();
                }
            }
        }
    }
}
