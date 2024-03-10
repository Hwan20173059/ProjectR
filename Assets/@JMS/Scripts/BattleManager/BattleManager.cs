using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public List<DungeonSO> Dungeons;
    public GameObject CharacterPrefab;
    public Queue<StateMachine> PerformList;
    public Image ActionBar;

    Vector3 CharacterSpawnPosition = new Vector3 (-6.5f, 1.5f, 0);
    Vector3 MonsterSpawnPosition = new Vector3 (-1, 3, 0);
    public int selectDungeon;
    public int curStage;

    public BattleStateMachine stateMachine;
    public CharacterJMS Character;
    public List<Monster> Monsters;

    private void Awake()
    {
        PerformList = new Queue<StateMachine>();

        stateMachine = new BattleStateMachine(this);
    }
    private void Start()
    {
        SpawnCharacter();
        SpawnMonster();
        stateMachine.ChangeState(stateMachine.WaitState);
        StartCoroutine(BattleStart());
    }
    private void Update()
    {
        stateMachine.HandleInput();

        stateMachine.Update();
    }
    
    void SpawnCharacter()
    {
        GameObject character = Instantiate(CharacterPrefab);
        character.transform.position = CharacterSpawnPosition;
        Character = character.GetComponent<CharacterJMS>();
    }

    void SpawnMonster()
    {
        int randomSpawnAmount = Random.Range(Dungeons[selectDungeon].Stages[curStage].randomSpawnMinAmount, Dungeons[selectDungeon].Stages[curStage].randomSpawnMaxAmount + 1);
        if(randomSpawnAmount > 0)
        {
            for (int i = 0; i < randomSpawnAmount; i++)
            {
                int monsterIndex = Random.Range(0, Dungeons[selectDungeon].Stages[curStage].RandomSpawnMonsters.Count);
                GameObject monster = Instantiate(Dungeons[selectDungeon].Stages[curStage].RandomSpawnMonsters[monsterIndex]);
                Monsters.Add(monster.GetComponent<Monster>());
                monster.transform.position = MonsterSpawnPosition;
                ChangeSpawnPosition();
            }
        }

        if(Dungeons[selectDungeon].Stages[curStage].SpawnMonsters.Count > 0)
        {
            foreach (var monster in Dungeons[selectDungeon].Stages[curStage].SpawnMonsters)
            {
                GameObject _monster = Instantiate(monster);
                Monsters.Add(_monster.GetComponent<Monster>());
                _monster.transform.position = MonsterSpawnPosition;
                ChangeSpawnPosition();
            }
        }
    }

    void ChangeSpawnPosition()
    {
        if (MonsterSpawnPosition.y == 3)
            MonsterSpawnPosition = new Vector3(MonsterSpawnPosition.x, MonsterSpawnPosition.y - 2.5f, MonsterSpawnPosition.z);
        else
            MonsterSpawnPosition = new Vector3(MonsterSpawnPosition.x + 2.5f, MonsterSpawnPosition.y + 2.5f, MonsterSpawnPosition.z);
    }

    IEnumerator BattleStart()
    {
        yield return new WaitForSeconds(1f);
        Character.stateMachine.ChangeState(Character.stateMachine.ReadyState);
        foreach (var monster in Monsters)
        {
            monster.stateMachine.ChangeState(monster.stateMachine.ReadyState);
        }
    }
}
