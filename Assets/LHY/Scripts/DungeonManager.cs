using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public List<SODungeon> dungeon = new List<SODungeon>();

    public SOStage stage;
    public Transform EnemyArea;
    int currentDungeon;
    int currentStage;
    private void Start()
    {
        SetDungeon();
        SetStage();
    }
    

    void MonsterSpawn()
    {
        dungeon[currentDungeon].stage[currentStage].MonsterSpawn(EnemyArea);
    }

    void SetDungeon()
    {
        currentDungeon = 0;

        Debug.Log("���� �������� : " + dungeon[currentDungeon].stage[currentStage].name);

    }
    void SetStage()
    {
        MonsterSpawn();
    }
    public void NextStage()
    {
        //Clear���� ������
        if (dungeon[currentDungeon].stage == null)
        {
            Debug.Log("���� ���������� ���� ���̾�");
        }
        else
        {
            Debug.Log("���� ���������� �̵��Ѵ�.");
            currentStage++;
            SetStage();
        }
    }
}
