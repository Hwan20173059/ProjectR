using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    //������ ���� ��� ����
    public List<SODungeon> dungeon = new List<SODungeon>();

    public Transform EnemyArea;
    public PlayerState player;
    int currentDungeon;//static
    int currentStage;//private
    int maxStage;//private
    private void Start()
    {
        currentDungeon = player.selectDungeonID;
        maxStage = dungeon[currentDungeon].stage.Count;
        Debug.Log(maxStage);
        SetDungeon();
        SetStage();
    }
    
    
    void MonsterSpawn()
    {
        //����ó�� �ʿ�
        dungeon[currentDungeon].stage[currentStage].MonsterSpawn(EnemyArea);
    }

    void SetDungeon()
    {
        Debug.Log("���� �������� : " + dungeon[currentDungeon].stage[currentStage].name);

    }
    void SetStage()
    {
        MonsterSpawn();
    }
    public void NextStage()
    {
        //Clear���� ������
        if (currentStage == maxStage - 1)
        {
            DungeonReward();
            Debug.Log("���� ���������� ���� ���̾�");
            BattleEnd();
        }
        else
        {
            Debug.Log("���� ���������� �̵��Ѵ�.");
            currentStage++;
            SetStage();
        }
    }

    void DungeonReward()
    {
        Debug.Log("EXP : " + player.character.currentExp + "->" + (player.character.currentExp + dungeon[currentDungeon].stage[currentStage].stageClearExp));
        player.character.currentExp += dungeon[currentDungeon].stage[currentStage].stageClearExp;
        Debug.Log("��� ������ Ŭ��������.");
        //������ �ش�.
    }
    void BattleEnd()
    {
        SceneManager.LoadScene("TownScene");
    }
}
