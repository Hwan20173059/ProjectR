using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TileState
{
    player, empty, canGO, cantGo,
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

    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        RefreshTile();
    }


    public void RefreshTile()
    {
        switch (tileState)
        {
            case TileState.empty:
                SetTile(TileState.empty);
                spriteRenderer.color = Color.white;
                break;

            case TileState.player:
                SetTile(TileState.player);
                onObject = Instantiate(tileMapManager.playerPrefab, this.transform);
                spriteRenderer.color = Color.blue;
                break;

            case TileState.canGO:
                SetTile(TileState.canGO);
                spriteRenderer.color = Color.cyan;
                break;

            case TileState.cantGo:
                SetTile(TileState.cantGo);
                spriteRenderer.color = Color.black;
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
                        tileMapManager.MoveTileOff();
                        tileMapManager.selectUI.SetActive(false);
                        break;
                    }
                    else
                    {
                        tileMapManager.selectedTile = this;
                        tileMapManager.isSelect = true;
                        tileMapManager.infoUI.text = "플레이어";

                        tileMapManager.MoveTileOn();
                        tileMapManager.selectUI.SetActive(true);
                        break;
                    }

                case TileState.empty:
                    tileMapManager.selectUI.SetActive(false);
                    break;

                case TileState.canGO:
                    tileMapManager.playerTurnIndex--;

                    tileMapManager.selectedTile = this;

                    tileMapManager.FieldMoveAfter();
                    tileState = TileState.player;

                    tileMapManager.playerPosition[0] = indexX;
                    tileMapManager.playerPosition[1] = indexY;

                    RefreshTile();

                    tileMapManager.isSelect = false;
                    tileMapManager.selectUI.SetActive(false);

                    if (tileMapManager.playerTurnIndex > 0)
                        tileMapManager.PlayerTurn();
                    else
                    {
                        tileMapManager.playerTurnIndex = 3;
                        tileMapManager.AEnemyTurn();
                    }
                    break;


                case TileState.cantGo:
                    tileMapManager.selectUI.SetActive(false);
                    break;


                case TileState.monster:
                    if (battleID == 0)
                    {
                        tileMapManager.infoUI.text = "슬라임 무리";
                        tileMapManager.selectUI.SetActive(true);
                        break;
                    }
                    else if (battleID == 1)
                    {
                        tileMapManager.infoUI.text = "몬스터 무리";
                        tileMapManager.selectUI.SetActive(true);
                        break;
                    }
                    else
                        break;


                case TileState.chest:
                    if (chestID == 0)
                    {
                        tileMapManager.infoUI.text = "초라한 상자";
                        tileMapManager.selectUI.SetActive(true);
                        break;
                    }
                    else if (chestID == 1)
                    {
                        tileMapManager.infoUI.text = "화려한 상자";
                        tileMapManager.selectUI.SetActive(true);
                        break;
                    }
                    else
                        break;


                case TileState.town:
                    if (townID == 0)
                    {
                        tileMapManager.infoUI.text = "첫번째 마을";
                        tileMapManager.selectUI.SetActive(true);
                        break;
                    }
                    else if(townID == 1)
                    {
                        tileMapManager.infoUI.text = "두번째 마을";
                        tileMapManager.selectUI.SetActive(true);
                        break;
                    }
                    else
                        break;


                case TileState.dungeon:
                    if (dungeonID == 0)
                    {
                        tileMapManager.infoUI.text = "슬라임 동굴";
                        tileMapManager.selectUI.SetActive(true);
                        break;
                    }
                    else if (dungeonID == 1)
                    {
                        tileMapManager.infoUI.text = "고블린 동굴";
                        tileMapManager.selectUI.SetActive(true);
                        break;
                    }
                    else if (dungeonID == 2)
                    {
                        tileMapManager.infoUI.text = "마지막 던전";
                        tileMapManager.selectUI.SetActive(true);
                        break;
                    }
                    else
                        break;


                case TileState.canFight:
                    tileMapManager.playerManager.fieldX = indexX;
                    tileMapManager.playerManager.fieldY = indexY;

                    tileMapManager.fieldMonster.Remove(this);
                    tileMapManager.playerManager.isEnterTown = false;
                    tileMapManager.SaveMonster();

                    tileMapManager.playerManager.selectBattleID = 0;
                    SceneManager.LoadScene("BattleScene");
                    break;


                case TileState.canTownEnter:
                    tileMapManager.playerManager.isEnterTown = true;
                    tileMapManager.playerManager.selectTownID = townID;

                    SceneManager.LoadScene("TownScene");
                    break;


                case TileState.canDungeonEnter:
                    tileMapManager.playerManager.selectDungeonID = dungeonID;

                    SceneManager.LoadScene("DungeonScene");
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
