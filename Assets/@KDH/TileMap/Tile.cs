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
    public FieldManager fieldManager;

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
                SetTile(TileState.empty);
                spriteRenderer.color = Color.white;
                break;

            case TileState.player:
                SetTile(TileState.player);
                onObject = Instantiate(fieldManager.playerPrefab, this.transform);
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
                onObject = Instantiate(fieldManager.monsterPrefab, this.transform);
                spriteRenderer.color = Color.yellow;
                break;

            case TileState.chest:
                SetTile(TileState.chest);
                onObject = Instantiate(fieldManager.chestPrefab, this.transform);
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
                onObject = Instantiate(fieldManager.monsterPrefab, this.transform);
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
                onObject = Instantiate(fieldManager.chestPrefab, this.transform);
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
                        fieldManager.infoUI.text = "플레이어";

                        fieldManager.MoveTileOn();
                        fieldManager.selectUI.SetActive(true);
                        break;
                    }

                case TileState.empty:
                        break;

                case TileState.canGO:
                    if (fieldManager.isSelect == true)
                    {
                        fieldManager.playerTurnIndex--;

                        fieldManager.selectedTile = this;

                        fieldManager.FieldMoveAfter();
                        tileState = TileState.player;

                        fieldManager.playerPosition[0] = indexX;
                        fieldManager.playerPosition[1] = indexY;

                        RefreshTile();

                        fieldManager.isSelect = false;
                        fieldManager.selectUI.SetActive(false);

                        if (fieldManager.playerTurnIndex > 0)
                        {
                            fieldManager.PlayerTurn();
                        }
                        else
                        {
                            fieldManager.playerTurnIndex = 3;
                            fieldManager.AEnemyTurn();
                        }

                        break;
                    }
                    else
                        break;

                case TileState.cantGo:
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
                        fieldManager.infoUI.text = "마을 정보";
                        fieldManager.selectUI.SetActive(true);
                        // 마을 정보 제공
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
                        fieldManager.playerManager.isEnterTown = true;
                        fieldManager.playerManager.selectDungeonID = dungeonID;

                        // 로드 씬 던전
                        break;
                    }
                    else
                    {
                        fieldManager.infoUI.text = "던전 정보";
                        fieldManager.selectUI.SetActive(true);
                        // 던전 정보 제공
                        break;
                    }

                case TileState.canFight:
                    if (fieldManager.isSelect == true)
                    {
                        fieldManager.playerManager.fieldX = indexX;
                        fieldManager.playerManager.fieldY = indexY;

                        fieldManager.fieldMonster.Remove(this);
                        fieldManager.playerManager.isEnterTown = false;
                        fieldManager.SaveMonster();

                        fieldManager.playerManager.selectDungeonID = 0;
                        SceneManager.LoadScene("BattleScene");
                        break;
                    }
                    else
                    {
                        fieldManager.infoUI.text = "몬스터 정보";
                        fieldManager.selectUI.SetActive(true);
                        // 몬스터 정보 제공
                        break;
                    }

                case TileState.canTownEnter:
                    if (fieldManager.isSelect == true)
                    {
                        fieldManager.playerManager.isEnterTown = true;
                        fieldManager.playerManager.selectTownID = townID;

                        SceneManager.LoadScene("TownScene");
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
