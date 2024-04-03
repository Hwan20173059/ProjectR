using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;

public enum TileState
{
    player, empty, canGo, cantGo,
    monster, chest, town, dungeon,
    canFight, canTownEnter, canDungeonEnter, canOpenChest
}

public class Tile : MonoBehaviour
{
    [Header("Manager")]
    public TileMapManager tileMapManager;

    [Header("State")]
    public TileState tileState;
    public int battleID;
    public int dungeonID;
    public int townID;
    public int chestID;

    [Header("Object")]
    public GameObject onObject;
    public GameObject highlight;

    [Header("index")]
    public int indexX;
    public int indexY;

    [Header("Path Finding")]
    public int cost;

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        this.name = indexX + " / " + indexY;

        RefreshTile();
    }


    public void RefreshTile()
    {
        switch (tileState)
        {
            case TileState.empty:
                SetTile(TileState.empty);
                spriteRenderer.color = new Color(1, 1, 1, 0.5f);
                break;

            case TileState.player:
                SetTile(TileState.player);
                onObject = Instantiate(tileMapManager.playerPrefab, this.transform);
                spriteRenderer.color = Color.blue;
                break;

            case TileState.canGo:
                SetTile(TileState.canGo);
                spriteRenderer.color = Color.cyan;
                break;

            case TileState.cantGo:
                SetTile(TileState.cantGo);
                spriteRenderer.color = new Color(0, 0, 0, 0.5f);
                break;

            case TileState.monster:
                SetTile(TileState.monster);
                if (battleID == 0)
                    onObject = Instantiate(tileMapManager.slimePrefab, this.transform);
                else if (battleID == 1)
                    onObject = Instantiate(tileMapManager.monsterPrefab, this.transform); 
                spriteRenderer.color = Color.yellow;
                break;

            case TileState.chest:
                SetTile(TileState.chest);
                if (chestID == 0)
                    onObject = Instantiate(tileMapManager.chest0Prefab, this.transform);
                else if (chestID == 1)
                    onObject = Instantiate(tileMapManager.chest1Prefab, this.transform);
                spriteRenderer.color = Color.gray;
                break;

            case TileState.town:
                SetTile(TileState.town);
                spriteRenderer.color = Color.green;
                break;

            case TileState.dungeon:
                SetTile(TileState.dungeon);
                spriteRenderer.color = Color.red;
                break;

            case TileState.canFight:
                SetTile(TileState.canFight);
                if (battleID == 0)
                    onObject = Instantiate(tileMapManager.slimePrefab, this.transform);
                else if (battleID == 1)
                    onObject = Instantiate(tileMapManager.monsterPrefab, this.transform);
                spriteRenderer.color = Color.magenta;
                break;

            case TileState.canTownEnter:
                SetTile(TileState.canTownEnter);
                spriteRenderer.color = Color.magenta;
                break;

            case TileState.canDungeonEnter:
                SetTile(TileState.canDungeonEnter);
                spriteRenderer.color = Color.magenta;
                break;

            case TileState.canOpenChest:
                SetTile(TileState.canOpenChest);
                if (chestID == 0)
                    onObject = Instantiate(tileMapManager.chest0Prefab, this.transform);
                else if (chestID == 1)
                    onObject = Instantiate(tileMapManager.chest1Prefab, this.transform);
                spriteRenderer.color = Color.magenta;
                break;
        }
    }

    private void OnMouseEnter()
    {
        if (tileMapManager.isPlayerturn == true)
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
        if (tileMapManager.isPlayerturn == true)
        {
            switch (tileState)
            {
                case TileState.player:
                    if (tileMapManager.isSelect == true)
                    {
                        tileMapManager.isSelect = false;
                        tileMapManager.MoveTileOff(tileMapManager.playerTurnIndex);
                        break;
                    }
                    else
                    {
                        tileMapManager.currentTile = this;
                        tileMapManager.isSelect = true;

                        tileMapManager.MoveTileOn(tileMapManager.playerTurnIndex);
                        break;
                    }

                case TileState.empty:
                    break;

                case TileState.canGo:
                    tileMapManager.playerTurnIndex -= cost;
                    PlayerManager.Instance.currentTurnIndex = tileMapManager.playerTurnIndex;

                    tileMapManager.currentTile = this;

                    tileMapManager.FieldMoveAfter();
                    tileState = TileState.player;

                    tileMapManager.playerPosition[0] = indexX;
                    tileMapManager.playerPosition[1] = indexY;

                    RefreshTile();

                    tileMapManager.isSelect = false;

                    if (tileMapManager.playerTurnIndex > 0)
                        tileMapManager.StillPlayerTurn();
                    else
                    {
                        tileMapManager.playerTurnIndex = PlayerManager.Instance.playerTurnIndex;
                        tileMapManager.AEnemyTurn();
                    }
                    break;


                case TileState.cantGo:
                    break;


                case TileState.monster:
                    if (battleID == 0)
                    {
                        tileMapManager.selectName.text = "슬라임 무리";
                        break;
                    }
                    else if (battleID == 1)
                    {
                        tileMapManager.selectName.text = "몬스터 무리";
                        break;
                    }
                    else
                        break;


                case TileState.chest:
                    if (chestID == 0)
                    {
                        tileMapManager.selectName.text = "초라한 상자";
                        break;
                    }
                    else if (chestID == 1)
                    {
                        tileMapManager.selectName.text = "화려한 상자";
                        break;
                    }
                    else
                        break;


                case TileState.town:
                    if (townID == 0)
                    {
                        tileMapManager.selectName.text = "초심자의 마을";
                        break;
                    }
                    else if(townID == 1)
                    {
                        tileMapManager.selectName.text = "수도 엘더";
                        break;
                    }
                    else
                        break;


                case TileState.dungeon:
                    if (dungeonID == 0)
                    {
                        tileMapManager.selectName.text = "고블린의 거처";
                        break;
                    }
                    else if (dungeonID == 1)
                    {
                        tileMapManager.selectName.text = "슬라임의 둥지";
                        break;
                    }
                    else if (dungeonID == 2)
                    {
                        tileMapManager.selectName.text = "미궁의 바다";
                        break;
                    }
                    else if (dungeonID == 3)
                    {
                        tileMapManager.selectName.text = "지옥의 숲";
                        break;
                    }
                    else
                        break;


                case TileState.canFight:
                    tileMapManager.playerTurnIndex -= cost;
                    PlayerManager.Instance.currentTurnIndex = tileMapManager.playerTurnIndex;

                    tileMapManager.playerManager.fieldX = indexX;
                    tileMapManager.playerManager.fieldY = indexY;

                    tileMapManager.fieldMonster.Remove(this);

                    tileMapManager.SaveMonster();

                    if (tileMapManager.chestPosition != null)
                        tileMapManager.SaveChest();
                    else
                        tileMapManager.chestPosition = null;

                    tileMapManager.playerManager.selectBattleID = 0;
                    SceneManager.LoadScene("BattleScene");
                    break;


                case TileState.canTownEnter:
                    tileMapManager.playerManager.selectTownID = townID;
                    SceneManager.LoadScene("TownScene");
                    break;


                case TileState.canDungeonEnter:
                    tileMapManager.playerManager.selectDungeonID = dungeonID;
                    SceneManager.LoadScene("DungeonScene");
                    break;

                case TileState.canOpenChest:
                    tileMapManager.playerTurnIndex -= cost;
                    PlayerManager.Instance.currentTurnIndex = tileMapManager.playerTurnIndex;

                    tileMapManager.currentTile = this;

                    tileMapManager.FieldMoveAfter();
                    tileState = TileState.player;

                    tileMapManager.chestPosition = null;

                    tileMapManager.playerPosition[0] = indexX;
                    tileMapManager.playerPosition[1] = indexY;

                    RefreshTile();

                    tileMapManager.isSelect = false;

                    tileMapManager.ChestUI.SetActive(true);
                    break;
            }
        }
    }

    public void SetTile(TileState tileState)
    {
        this.tileState = tileState;

        if (onObject != null)
        {
            Destroy(onObject);
            onObject = null;
        }
    }
}
