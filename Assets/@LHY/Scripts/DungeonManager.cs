using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    //던전에 관한 목록 저장
    public List<SODungeon> dungeon = new List<SODungeon>();

    public Transform EnemyArea;
    public Transform PlayerArea;
    private PlayerState playerState;
    public GameObject player;
    int currentDungeon;//static
    int currentStage;//private
    int maxStage;//private

    private void Awake()
    {
        playerState = PlayerState.Instance.GetComponent<PlayerState>();
    }
    private void Start()
    {
        currentDungeon = playerState.selectDungeonID;
        maxStage = dungeon[currentDungeon].stage.Count;
        Debug.Log(maxStage);
        PlayerSpawn();
        SetDungeon();
        SetStage();
    }
    
    void PlayerSpawn()
    {
        Instantiate(player, PlayerArea);
    }

    void MonsterSpawn()
    {
        //예외처리 필요
        dungeon[currentDungeon].stage[currentStage].MonsterSpawn(EnemyArea);
    }

    void SetDungeon()
    {
        Debug.Log("현재 스테이지 : " + dungeon[currentDungeon].stage[currentStage].name);

    }
    void SetStage()
    {
        MonsterSpawn();
    }
    public void NextStage()
    {
        //Clear조건 충족시
        if (currentStage == maxStage - 1)
        {
            DungeonReward();
            Debug.Log("다음 스테이지는 없어 끝이야");
            //BattleEnd();
        }
        else
        {
            Debug.Log("다음 스테이지로 이동한다.");
            currentStage++;
            SetStage();
        }
    }

    void DungeonReward()
    {
        Debug.Log("EXP : " + playerState.character.currentExp + "->" + (playerState.character.currentExp + dungeon[currentDungeon].stage[currentStage].stageClearExp));
        playerState.character.currentExp += dungeon[currentDungeon].stage[currentStage].stageClearExp;
        Debug.Log("모든 던전을 클리어했음.");
        //보상을 준다.
    }
    void BattleEnd()
    {
        SceneManager.LoadScene("TownScene");
    }
}
