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

        Debug.Log("현재 스테이지 : " + dungeon[currentDungeon].stage[currentStage].name);

    }
    void SetStage()
    {
        MonsterSpawn();
    }
    public void NextStage()
    {
        //Clear조건 충족시
        if (dungeon[currentDungeon].stage == null)
        {
            Debug.Log("다음 스테이지는 없어 끝이야");
        }
        else
        {
            Debug.Log("다음 스테이지로 이동한다.");
            currentStage++;
            SetStage();
        }
    }
}
