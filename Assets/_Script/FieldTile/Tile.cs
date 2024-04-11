using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.TextCore.Text;

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

                onObject = Instantiate(tileMapManager.monsterPrefab, this.transform);
                SpriteRenderer objectSprite = onObject.GetComponent<SpriteRenderer>();
                objectSprite.sprite = Resources.Load<Sprite>(DataManager.Instance.monsterDatabase.monsterDatas[battleID].spritePath);

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

                onObject = Instantiate(tileMapManager.monsterPrefab, this.transform);
                objectSprite = onObject.GetComponent<SpriteRenderer>();
                objectSprite.sprite = Resources.Load<Sprite>(DataManager.Instance.monsterDatabase.monsterDatas[battleID].spritePath);

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
        Sprite sprite;

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

                        tileMapManager.fieldCamera.playerTile = this;

                        tileMapManager.MoveTileOn(tileMapManager.playerTurnIndex);
                        break;
                    }

                case TileState.empty:
                    //tileMapManager.selectImage.sprite = null;
                    tileMapManager.selectName.text = null;
                    tileMapManager.selectText.text = null;
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
                        tileMapManager.AEnemyTurn();
                    }
                    break;


                case TileState.cantGo:
                    //tileMapManager.selectImage.sprite = null;
                    tileMapManager.selectName.text = null;
                    tileMapManager.selectText.text = null;
                    break;


                case TileState.monster:

                    tileMapManager.selectName.text = "����";
                    sprite = tileMapManager.monsterPrefab.GetComponent<SpriteRenderer>().sprite;

                    switch (battleID)
                    {
                        case 0:
                            tileMapManager.selectText.text = "������";
                            break;
                        case 1:
                            tileMapManager.selectText.text = "Ǫ�� ����";
                            break;
                        case 2:
                            tileMapManager.selectText.text = "���� ����";
                            break;
                        case 3:
                            tileMapManager.selectText.text = "����� ��ȣ��";
                            break;
                        case 4:
                            tileMapManager.selectText.text = "�����";
                            break;
                        case 5:
                            tileMapManager.selectText.text = "�߻��� �����";
                            break;
                        case 6:
                            tileMapManager.selectText.text = "���� ����";
                            break;
                        case 7:
                            tileMapManager.selectText.text = "��� ����";
                            break;
                        case 8:
                            tileMapManager.selectText.text = "����";
                            break;
                        case 9:
                            tileMapManager.selectText.text = "��ȣ ��";
                            break;
                        case 10:
                            tileMapManager.selectText.text = "�� ��";
                            break;
                        case 11:
                            tileMapManager.selectText.text = "���� ��";
                            break;
                        case 12:
                            tileMapManager.selectText.text = "û������";
                            break;
                        case 13:
                            tileMapManager.selectText.text = "�͵� ������";
                            break;
                        case 14:
                            tileMapManager.selectText.text = "�ٴ� ��";
                            break;
                        case 15:
                            tileMapManager.selectText.text = "���� ��";
                            break;
                        case 16:
                            tileMapManager.selectText.text = "��Ȧ��";
                            break;
                        case 17:
                            tileMapManager.selectText.text = "�ö��̾���";
                            break;
                        case 18:
                            tileMapManager.selectText.text = "������";
                            break;
                        case 19:
                            tileMapManager.selectText.text = "�ɿ��� ������";
                            break;
                    }
                    break;


                case TileState.chest:

                    tileMapManager.selectName.text = "����";

                    if (chestID == 0)
                    {
                        sprite = tileMapManager.chest0Prefab.GetComponent<SpriteRenderer>().sprite;
                        //tileMapManager.selectImage.sprite = sprite;
                        //tileMapManager.selectImage.SetNativeSize();

                        tileMapManager.selectText.text = "�ʶ��� ����";
                        break;
                    }
                    else if (chestID == 1)
                    {
                        sprite = tileMapManager.chest1Prefab.GetComponent<SpriteRenderer>().sprite;
                        //tileMapManager.selectImage.sprite = sprite;
                        //tileMapManager.selectImage.SetNativeSize();

                        tileMapManager.selectText.text = "ȭ���� ����";
                        break;
                    }
                    else
                        break;


                case TileState.town:

                    tileMapManager.selectName.text = "����";

                    if (townID == 0)
                    {
                        //tileMapManager.selectImage.sprite = null;

                        tileMapManager.selectText.text = "�ʽ����� ����";
                        break;
                    }
                    else if(townID == 1)
                    {
                        //tileMapManager.selectImage.sprite = null;

                        tileMapManager.selectText.text = "���� ����";
                        break;
                    }
                    else
                        break;


                case TileState.dungeon:

                    tileMapManager.selectName.text = "����";

                    if (dungeonID == 0)
                    {
                        //tileMapManager.selectImage.sprite = null;

                        tileMapManager.selectText.text = "�߻��� ��";
                        break;
                    }
                    else if (dungeonID == 1)
                    {
                        //tileMapManager.selectImage.sprite = null;

                        tileMapManager.selectText.text = "ȣ���� ����";
                        break;
                    }
                    else if (dungeonID == 2)
                    {
                        //tileMapManager.selectImage.sprite = null;

                        tileMapManager.selectText.text = "�̱��� �ٴ�";
                        break;
                    }
                    else if (dungeonID == 3)
                    {
                        //tileMapManager.selectImage.sprite = null;

                        tileMapManager.selectText.text = "������ �Ա�";
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

                    tileMapManager.playerManager.selectBattleID = battleID;
                    SceneManager.LoadScene("BattleScene");
                    break;


                case TileState.canTownEnter:
                    tileMapManager.playerManager.selectTownID = townID;

                    if (PlayerManager.Instance.selectTownID == 0)
                        PlayerManager.Instance.currentState = CurrentState.town1;
                    else if (PlayerManager.Instance.selectTownID == 1)
                        PlayerManager.Instance.currentState = CurrentState.town2;

                    SceneManager.LoadScene("TownScene");
                    break;


                case TileState.canDungeonEnter:
                    tileMapManager.playerManager.selectDungeonID = dungeonID;

                    if (PlayerManager.Instance.selectDungeonID == 0)
                        PlayerManager.Instance.currentState = CurrentState.dungeon1;
                    else if (PlayerManager.Instance.selectDungeonID == 1)
                        PlayerManager.Instance.currentState = CurrentState.dungeon2;
                    else if (PlayerManager.Instance.selectDungeonID == 2)
                        PlayerManager.Instance.currentState = CurrentState.dungeon3;
                    else if (PlayerManager.Instance.selectDungeonID == 3)
                        PlayerManager.Instance.currentState = CurrentState.dungeon4;

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
