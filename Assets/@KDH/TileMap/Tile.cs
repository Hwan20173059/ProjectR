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
    public int dungeonID;
    public int townID;

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
                ClearTile(TileState.empty);
                spriteRenderer.color = Color.white;
                break;

            case TileState.player:
                ClearTile(TileState.player);
                onObject = Instantiate(fieldManager.playerPrefab, this.transform);
                spriteRenderer.color = Color.blue;
                break;

            case TileState.cango:
                ClearTile(TileState.cango);
                spriteRenderer.color = Color.cyan;
                break;

            case TileState.cantgo:
                ClearTile(TileState.cantgo);
                spriteRenderer.color = Color.black;
                break;

            case TileState.monster:
                ClearTile(TileState.monster);
                onObject = Instantiate(fieldManager.monsterPrefab, this.transform);
                spriteRenderer.color = Color.yellow;
                break;

            case TileState.chest:
                ClearTile(TileState.chest);
                spriteRenderer.color = Color.gray;
                break;

            case TileState.town:
                ClearTile(TileState.town);
                spriteRenderer.color = Color.green;
                break;

            case TileState.dungeon:
                ClearTile(TileState.dungeon);
                spriteRenderer.color = Color.red;
                break;

            case TileState.canfight:
                ClearTile(TileState.canfight);
                onObject = Instantiate(fieldManager.monsterPrefab, this.transform);
                spriteRenderer.color = Color.magenta;
                break;
        }
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
                        fieldManager.infoUI.text = "�÷��̾�";

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

                        fieldManager.playerPosition[0] = indexX;
                        fieldManager.playerPosition[1] = indexY;

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
                        // ���� ���� ����
                        break;
                    }
                    else
                    {
                        fieldManager.infoUI.text = "���� ����";
                        fieldManager.selectUI.SetActive(true);
                        // ���� ���� ����
                        break;
                    }

                case TileState.chest:
                    if (fieldManager.isSelect == true)
                    {
                        // �ش� ���� ���� + �ش� ��ġ�� �̵�
                        break;
                    }
                    else
                    {
                        fieldManager.infoUI.text = "���� ����";
                        fieldManager.selectUI.SetActive(true);
                        // ���� ���� ����
                        break;
                    }

                case TileState.town:
                    if (fieldManager.isSelect == true)
                    {
                        fieldManager.playerManager.isEnterTown = true;
                        fieldManager.playerManager.selectTownID = townID;

                        SceneManager.LoadScene("TownScene");
                        break;
                    }
                    else
                    {
                        fieldManager.infoUI.text = "���� ����";
                        fieldManager.selectUI.SetActive(true);
                        // ���� ���� ����
                        break;
                    }

                case TileState.dungeon:
                    if (fieldManager.isSelect == true)
                    {
                        fieldManager.playerManager.isEnterTown = true;
                        fieldManager.playerManager.selectDungeonID = dungeonID;

                        // �ε� �� ����
                        break;
                    }
                    else
                    {
                        fieldManager.infoUI.text = "���� ����";
                        fieldManager.selectUI.SetActive(true);
                        // ���� ���� ����
                        break;
                    }

                case TileState.canfight:
                    if (fieldManager.isSelect == true)
                    {
                        fieldManager.playerManager.fieldX = indexX;
                        fieldManager.playerManager.fieldY = indexY;

                        fieldManager.fieldMonster.Remove(this);
                        fieldManager.playerManager.isEnterTown = false;
                        fieldManager.SaveMonster();

                        fieldManager.playerManager.selectDungeonID = 0;
                        SceneManager.LoadScene("DungeonScene");
                        break;
                    }
                    else
                    {
                        fieldManager.infoUI.text = "���� ����";
                        fieldManager.selectUI.SetActive(true);
                        // ���� ���� ����
                        break;
                    }
            }
        }
    }

    public void ClearTile(TileState tileState)
    {
        this.tileState = tileState;

        if (onObject != null)
        {
            Destroy(onObject);
            onObject = null;
        }
    }
}
