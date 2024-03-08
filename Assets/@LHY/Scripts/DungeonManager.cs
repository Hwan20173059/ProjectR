using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class DungeonManager : MonoBehaviour
{
    //������ ���� ��� ����
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
        playerState = SingletonManager.instance.GetComponentInChildren<PlayerState>();
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
            //BattleEnd();
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
        Debug.Log("EXP : " + playerState.character.currentExp + "->" + (playerState.character.currentExp + dungeon[currentDungeon].stage[currentStage].stageClearExp));
        playerState.character.currentExp += dungeon[currentDungeon].stage[currentStage].stageClearExp;
        Debug.Log("��� ������ Ŭ��������.");
        //������ �ش�.
    }
    public void BattleEnd()
    {
        SceneManager.LoadScene("TownScene");
    }
}
