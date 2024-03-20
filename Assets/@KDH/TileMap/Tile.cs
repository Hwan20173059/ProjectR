using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TileState
{
    player,
    empty, cango, cantgo,    
    monster, chest, town, dungeon,
    canfight
}

public class Tile : MonoBehaviour
{
    [Header("Manager")]
    public FieldManager fieldManager;

    [Header("State")]
    public TileState tileState;

    [Header("Object")]
    public GameObject onObject;
    public GameObject highlight;

    [Header("index")]
    public int indexX;
    public int indexY;

    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        RefreshTile();
    }


    public void RefreshTile()
    {
        switch (tileState)
        {
            case TileState.empty:
                spriteRenderer.color = Color.white;
                break;

            case TileState.player:
                spriteRenderer.color = Color.blue;
                break;

            case TileState.cango:
                spriteRenderer.color = Color.cyan;
                break;

            case TileState.cantgo:
                spriteRenderer.color = Color.black;
                break;

            case TileState.monster:
                spriteRenderer.color = Color.yellow;
                break;

            case TileState.chest:
                spriteRenderer.color = Color.gray;
                break;

            case TileState.town:
                spriteRenderer.color = Color.green;
                break;

            case TileState.dungeon:
                spriteRenderer.color = Color.red;
                break;

            case TileState.canfight:
                spriteRenderer.color = Color.magenta;
                break;
        }

        if (onObject)
        {
            if (tileState == TileState.player || tileState == TileState.cango || tileState == TileState.canfight)
                onObject = Instantiate(onObject, this.gameObject.transform);
        }
        else
            return;
    }

    private void OnMouseEnter()
    {
        if (fieldManager.isPlayerturn == true)
        {
            highlight.SetActive(true);
        }
    }

    private void OnMouseExit()
    {
        highlight.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (fieldManager.isPlayerturn == true)
        {
            switch (tileState)
            {
                case TileState.player:
                    if (fieldManager.isSelect == true)
                    {
                        fieldManager.isSelect = false;
                        fieldManager.MoveTileOff();
                        fieldManager.selectUI.SetActive(false);
                        break;
                    }
                    else
                    {
                        fieldManager.selectedTile = this;
                        fieldManager.isSelect = true;
                        fieldManager.infoUI.text = "플레이어";

                        fieldManager.MoveTileOn();
                        fieldManager.selectUI.SetActive(true);
                        break;
                    }

                case TileState.empty:
                        break;

                case TileState.cango:
                    if (fieldManager.isSelect == true)
                    {
                        fieldManager.selectedTile = this;

                        fieldManager.PlayerMoveTile();
                        tileState = TileState.player;
                        onObject = fieldManager.fieldPlayer;
                        RefreshTile();

                        fieldManager.isSelect = false;
                        fieldManager.selectUI.SetActive(false);

                        fieldManager.AEnemyTurn();
                        break;
                    }
                    else
                        break;

                case TileState.cantgo:
                    break;

                case TileState.monster:
                    if (fieldManager.isSelect == true)
                    {
                        fieldManager.selectUI.SetActive(false);
                        // 몬스터 정보 제공
                        break;
                    }
                    else
                    {
                        fieldManager.infoUI.text = "몬스터 정보";
                        fieldManager.selectUI.SetActive(true);
                        // 몬스터 정보 제공
                        break;
                    }

                case TileState.chest:
                    if (fieldManager.isSelect == true)
                    {
                        // 해당 상자 오픈 + 해당 위치로 이동
                        break;
                    }
                    else
                    {
                        fieldManager.infoUI.text = "상자 정보";
                        fieldManager.selectUI.SetActive(true);
                        // 상자 정보 제공
                        break;
                    }

                case TileState.town:
                    if (fieldManager.isSelect == true)
                    {
                        SceneManager.LoadScene("TownScene");
                        break;
                    }
                    else
                    {
                        fieldManager.infoUI.text = "마을 정보";
                        fieldManager.selectUI.SetActive(true);
                        // 마을 정보 제공
                        break;
                    }

                case TileState.dungeon:
                    if (fieldManager.isSelect == true)
                    {
                        // 해당 던전으 이동 + 던전에서 나올때 위치 기억
                        break;
                    }
                    else
                    {
                        fieldManager.infoUI.text = "던전 정보";
                        fieldManager.selectUI.SetActive(true);
                        // 던전 정보 제공
                        break;
                    }

                case TileState.canfight:
                    if (fieldManager.isSelect == true)
                    {
                        fieldManager.playerManager.selectDungeonID = 0;
                        SceneManager.LoadScene("DungeonScene");
                        break;
                    }
                    else
                    {
                        fieldManager.infoUI.text = "몬스터 정보";
                        fieldManager.selectUI.SetActive(true);
                        // 몬스터 정보 제공
                        break;
                    }
            }
        }
    }

    public void ClearTile()
    {
        tileState = TileState.empty;
        if (onObject != null)
        {
            Destroy(onObject);
            onObject = null;
        }
    }
}
