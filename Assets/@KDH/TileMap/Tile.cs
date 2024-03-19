using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum TileState
{
    player,
    empty, cantgo,    
    monster, chest, town, dungeon
}

public class Tile : MonoBehaviour
{
    public FieldManager fieldManager;
    public TileState tileState;

    public GameObject onObject;
    public GameObject highlight;

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
        }

        if (onObject)
        {
            if (tileState == TileState.player || tileState == TileState.monster)
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
                        fieldManager.selectUI.SetActive(false);
                        break;
                    }
                    else
                    {
                        fieldManager.isSelect = true;
                        fieldManager.infoUI.text = "�÷��̾�";
                        fieldManager.selectUI.SetActive(true);
                        break;
                    }

                case TileState.empty:
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
                        // �ش� ������ �̵� + �������� ���ö� ��ġ ���
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

    public void ClearTile()
    {
        tileState = TileState.empty;
        Destroy(onObject);
        onObject = null;
    }
}
