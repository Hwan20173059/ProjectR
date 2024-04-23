using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : TileMapManager
{
    public DungeonTutorialManager dungeonTutorialManager;
    public GameObject dungeonClearUI;

    public SpriteRenderer background;
    public Sprite dungeon1;
    public Sprite dungeon2;
    public Sprite dungeon3;
    public Sprite dungeon4;

    void Start()
    {
        playerManager = PlayerManager.Instance;

        RefreshBackground();
        playerPrefab.GetComponent<SpriteRenderer>().sprite = playerManager.characterList[playerManager.selectedCharacterIndex].sprite;

        loadingUI.OpenScreen();

        if (playerManager.firstDungeon == true)
        {
            dungeonTutorialManager.gameObject.SetActive(true);
            dungeonTutorialManager.ActiveTutorial();
            playerManager.firstDungeon = false;
        }

        if (playerManager.isDungeon == false)
        {
            playerTurnIndex = playerManager.playerTurnIndex;

            CreateRandomMap();
            AllRefreshTile();

            PlayerFieldSetting(0, 0);

            playerManager.monsterPosition = new List<int>();

            switch (playerManager.currentState) 
            {
                case CurrentState.dungeon1:
                    SpawnRandomMonster(3);
                    break;
                case CurrentState.dungeon2:
                    SpawnRandomMonster(4);
                    break;
                case CurrentState.dungeon3:
                    SpawnRandomMonster(4);
                    break;
                case CurrentState.dungeon4:
                    SpawnRandomMonster(5);
                    break;
            }

            RandomSpawnChest();

            PlayerTurn();
        }
        else
        {
            if (playerManager.monsterPosition.Count > 0)
            {
                playerTurnIndex = playerManager.currentTurnIndex;

                LoadRandomMap(playerManager.dungeonMap);
                AllRefreshTile();

                LoadMonster();

                if (playerManager.chestPosition.Count != 0)
                    LoadChest();

                PlayerFieldSetting(playerManager.fieldX, playerManager.fieldY);

                currentTile = field.tileRaw[playerManager.fieldY].fieldTiles[playerManager.fieldX];

                if (playerTurnIndex > 0)
                    StillPlayerTurn();
                else
                    AEnemyTurn();
                
            }
            else
            {
                ClearDungeon();
                PlayerTurn();
            }
        }

        CharacterUIRefresh();
        currentTile = field.tileRaw[0].fieldTiles[0];

        playerManager.isField = false;
        playerManager.isTown = false;
        playerManager.isDungeon = true;
    }

    public void ExitButton()
    {
        if (playerManager.selectDungeonID == 0)
        {
            playerManager.fieldX = 4;
            playerManager.fieldY = 4;
        }
        else if (playerManager.selectDungeonID == 1)
        {
            playerManager.fieldX = 14;
            playerManager.fieldY = 2;
        }
        else if (playerManager.selectDungeonID == 2)
        {
            playerManager.fieldX = 33;
            playerManager.fieldY = 5;
        }
        else if (playerManager.selectDungeonID == 3)
        {
            playerManager.fieldX = 33;
            playerManager.fieldY = 0;
        }

        playerManager.currentState = CurrentState.field;
        AudioManager.Instance.SetState();

        SceneManager.LoadScene("FieldScene");
    }

    protected void ClearDungeon()
    {
        switch (PlayerManager.Instance.selectDungeonID)
        {
            case 0:
                RewardManager.instance.RewardPopup(1500, 500, 26);
                break;
            case 1:
                RewardManager.instance.RewardPopup(3000, 1000, 27);
                break;
            case 2:
                RewardManager.instance.RewardPopup(5000, 2000, 27);
                break;
            case 3:
                RewardManager.instance.RewardPopup(10000, 4000, 28);
                break;
        }

        dungeonClearUI.SetActive(true);
    }

    protected void RandomSpawnChest()
    {
        int randomY = UnityEngine.Random.Range(0, field.tileRaw.Length);
        int randomX = UnityEngine.Random.Range(0, field.tileRaw[randomY].fieldTiles.Length);
        int randomID = UnityEngine.Random.Range(0, 2);

        if (field.tileRaw[randomY].fieldTiles[randomX].tileState == TileState.empty)
        {
            chestPosition = field.tileRaw[randomY].fieldTiles[randomX];
            ChestSetting(randomID, randomX, randomY);
        }
        else
            RandomSpawnChest();
    }

    protected void LoadRandomMap(int[] index)
    {
        RandomTile(index[0], 0, 0);
        RandomTile(index[1], 1, 0);
        RandomTile(index[2], 0, 1);
        RandomTile(index[3], 1, 1);
    }

    protected void SaveRandomMap(int a,int b, int c, int d) 
    {
        playerManager.dungeonMap[0] = a;
        playerManager.dungeonMap[1] = b;
        playerManager.dungeonMap[2] = c;
        playerManager.dungeonMap[3] = d;
    }

    protected void CreateRandomMap()
    {
        int[] random = new int[4];

        for(int i = 0; i < random.Length; i++)
            random[i] = UnityEngine.Random.Range(0, 6);

        SaveRandomMap(random[0], random[1], random[2], random[3]);
        RandomTile(random[0], 0, 0);
        RandomTile(random[1], 1, 0);
        RandomTile(random[2], 0, 1);
        RandomTile(random[3], 1, 1);
    }

    protected void RandomTile(int index, int X, int Y)
    {
        switch (index) 
        {
            case 0:
                {
                    field.tileRaw[Y * 4].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;

                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 2].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 3].tileState = TileState.empty;
                    break;
                }
            case 1:
                {
                    field.tileRaw[Y * 4].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;

                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;
                    break;
                }
            case 2:
                {
                    field.tileRaw[Y * 4].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 2].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;

                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 3].tileState = TileState.empty;
                    break;
                }
            case 3:
                {
                    field.tileRaw[Y * 4].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 2].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;

                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;
                    break;
                }
            case 4:
                {
                    field.tileRaw[Y * 4].fieldTiles[X * 4].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;

                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 3].tileState = TileState.cantGo;

                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 3].tileState = TileState.empty;
                    break;
                }
            case 5:
                {
                    field.tileRaw[Y * 4].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 2].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 1].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 1].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 2].tileState = TileState.cantGo;
                    field.tileRaw[Y * 4 + 2].fieldTiles[X * 4 + 3].tileState = TileState.empty;

                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 1].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 2].tileState = TileState.empty;
                    field.tileRaw[Y * 4 + 3].fieldTiles[X * 4 + 3].tileState = TileState.empty;
                    break;
                }
        }
    }

    public void RefreshBackground()
    {
        switch (playerManager.selectDungeonID)
        {
            case 0:
                background.sprite = dungeon1;
                break;
            case 1:
                background.sprite = dungeon2;
                break;
            case 2:
                background.sprite = dungeon3;
                break;
            case 3:
                background.sprite = dungeon4;
                break;
        }
    }
}
