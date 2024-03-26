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
                if (battleID == 0)
                    onObject = Instantiate(fieldManager.slimePrefab, this.transform);
                else if (battleID == 1)
                    onObject = Instantiate(fieldManager.monsterPrefab, this.transform); 
                spriteRenderer.color = Color.yellow;
                break;

            case TileState.chest:
                SetTile(TileState.chest);
                if (chestID == 0)
                    onObject = Instantiate(fieldManager.chest0Prefab, this.transform);
                else if (chestID == 1)
                    onObject = Instantiate(fieldManager.chest1Prefab, this.transform);
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
                    onObject = Instantiate(fieldManager.slimePrefab, this.transform);
                else if (battleID == 1)
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
                if (chestID == 0)
                    onObject = Instantiate(fieldManager.chest0Prefab, this.transform);
                else if (chestID == 1)
                    onObject = Instantiate(fieldManager.chest1Prefab, this.transform);
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
                    fieldManager.selectUI.SetActive(false);
                    break;

                case TileState.canGO:
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
                        fieldManager.PlayerTurn();
                    else
                    {
                        fieldManager.playerTurnIndex = 3;
                        fieldManager.AEnemyTurn();
                    }
                    break;


                case TileState.cantGo:
                    fieldManager.selectUI.SetActive(false);
                    break;


                case TileState.monster:
                    if (battleID == 0)
                    {
                        fieldManager.infoUI.text = "슬라임 무리";
                        fieldManager.selectUI.SetActive(true);
                        break;
                    }
                    else if (battleID == 1)
                    {
                        fieldManager.infoUI.text = "몬스터 무리";
                        fieldManager.selectUI.SetActive(true);
                        break;
                    }
                    else
                        break;


                case TileState.chest:
                    if (chestID == 0)
                    {
                        fieldManager.infoUI.text = "초라한 상자";
                        fieldManager.selectUI.SetActive(true);
                        break;
                    }
                    else if (chestID == 1)
                    {
                        fieldManager.infoUI.text = "화려한 상자";
                        fieldManager.selectUI.SetActive(true);
                        break;
                    }
                    else
                        break;


                case TileState.town:
                    if (townID == 0)
                    {
                        fieldManager.infoUI.text = "첫번째 마을";
                        fieldManager.selectUI.SetActive(true);
                        break;
                    }
                    else if(townID == 1)
                    {
                        fieldManager.infoUI.text = "두번째 마을";
                        fieldManager.selectUI.SetActive(true);
                        break;
                    }
                    else
                        break;


                case TileState.dungeon:
                    if (dungeonID == 0)
                    {
                        fieldManager.infoUI.text = "슬라임 동굴";
                        fieldManager.selectUI.SetActive(true);
                        break;
                    }
                    else if (dungeonID == 1)
                    {
                        fieldManager.infoUI.text = "고블린 동굴";
                        fieldManager.selectUI.SetActive(true);
                        break;
                    }
                    else if (dungeonID == 2)
                    {
                        fieldManager.infoUI.text = "마지막 던전";
                        fieldManager.selectUI.SetActive(true);
                        break;
                    }
                    else
                        break;


                case TileState.canFight:
                    fieldManager.playerManager.fieldX = indexX;
                    fieldManager.playerManager.fieldY = indexY;

                    fieldManager.fieldMonster.Remove(this);
                    fieldManager.playerManager.isEnterTown = false;
                    fieldManager.SaveMonster();

                    fieldManager.playerManager.selectBattleID = 0;
                    SceneManager.LoadScene("BattleScene");
                    break;


                case TileState.canTownEnter:
                    fieldManager.playerManager.isEnterTown = true;
                    fieldManager.playerManager.selectTownID = townID;

                    SceneManager.LoadScene("TownScene");
                    break;


                case TileState.canDungeonEnter:
                    fieldManager.playerManager.selectBattleID = dungeonID;

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
