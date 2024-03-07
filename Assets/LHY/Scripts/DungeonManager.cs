using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    [Header("Dungeons")]
    public List<SODungeon> dungeon = new List<SODungeon>();
    public SODungeon currentDungeon;

    [Header("Areas")]
    public Transform enemyArea;
    public Transform playerArea;

    [Header("PlayerState")]
    public PlayerState player;

    int selectedDungeon;//static

    private void Start()
    {
        player = SingletonManager.instance.GetComponentInChildren<PlayerState>();
        selectedDungeon = player.selectDungeonID;

        //maxStage = dungeon[currentDungeon].stage.Count;
        //Debug.Log(maxStage);

        SetDungeon(selectedDungeon);
        SetStage();
    }
    
    
    void MonsterSpawn()
    {
        //����ó�� �ʿ�
        //dungeon[currentDungeon].stage[currentStage].MonsterSpawn(enemyArea);
    }

    void SetDungeon(int selectedDungeon)
    {
        currentDungeon = dungeon[selectedDungeon - 1];

        //Debug.Log("���� �������� : " + dungeon[currentDungeon].stage[currentStage].name);
    }
    void SetStage()
    {
        MonsterSpawn();
    }
    public void NextStage()
    {
        //Clear���� ������
        //if (currentStage == maxStage - 1)
        //{
        //    DungeonReward();
        //    Debug.Log("���� ���������� ���� ���̾�");
        //    BattleEnd();
        //}
        //else
        //{
        //    Debug.Log("���� ���������� �̵��Ѵ�.");
        //    currentStage++;
        //    SetStage();
        //}
    }

    void DungeonReward()
    {
        //Debug.Log("EXP : " + player.character.currentExp + "->" + (player.character.currentExp + dungeon[currentDungeon].stage[currentStage].stageClearExp));
        //player.character.currentExp += dungeon[currentDungeon].stage[currentStage].stageClearExp;
        //Debug.Log("��� ������ Ŭ��������.");
        //������ �ش�.
    }
    void BattleEnd()
    {
        SceneManager.LoadScene("TownScene");
    }
}
