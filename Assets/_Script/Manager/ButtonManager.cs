using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public PlayerState playerState;
    public GameObject characterUI;
    public GameObject DungeonUI;

    private void Start()
    {
        playerState = SingletonManager.instance.GetComponentInChildren<PlayerState>();
    }

    public void GoDungeon() 
    {
        playerState.selectDungeonID = 1;
        SceneManager.LoadScene("DungeonScene");
    }

    public void DungeonUIOn()
    {
        DungeonUI.SetActive(true);
    }

    public void DungeonUIOff()
    {
        DungeonUI.SetActive(false);
    }

    public void CharacterUIOn()
    {
        characterUI.SetActive(true);
    }

    public void CharacterUIOff()
    {
        characterUI.SetActive(false);
    }

    public void BattleTestScene()
    {
        playerState.selectDungeonID = 1;
        SceneManager.LoadScene("BattleTestScene");
    }
}
