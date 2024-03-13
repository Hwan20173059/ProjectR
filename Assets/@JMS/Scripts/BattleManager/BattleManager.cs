using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public PlayerInput Input {  get; private set; }
    public Monster selectMonster;

    public BattleCanvas battleCanvas;

    public List<DungeonSO> dungeons;
    public int selectDungeon;
    public int curStage;
    public bool isStageClear { get { return StageClearCheck(); } }

    public GameObject monsterPool;

    Vector3 monsterSpawnPosition = new Vector3 (-1, 3, 0);
    public GameObject characterPrefab;
    Vector3 characterSpawnPosition = new Vector3 (-6.5f, 1.5f, 0);

    public List<int> performList;

    public Character character;
    public IState characterPrevState;
    public List<Monster> monsters;
    public IState[] monstersPrevState;

    private WaitForSeconds waitFor1Sec = new WaitForSeconds(1f);

    public BattleStateMachine stateMachine;

    private void Awake()
    {
        Input = GetComponent<PlayerInput>();

        battleCanvas = GetComponentInChildren<BattleCanvas>();

        performList = new List<int>();

        stateMachine = new BattleStateMachine(this);
    }
    private void Start()
    {
        Input.ClickActions.MouseClick.started += OnClickStart;

        stateMachine.ChangeState(stateMachine.startState);
    }

    private void OnClickStart(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.ClickActions.MousePos.ReadValue<Vector2>());
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Monster"))
        {
            selectMonster = hit.collider.GetComponent<Monster>();
        }
    }

    private void Update()
    {
        stateMachine.HandleInput();

        stateMachine.Update();
    }
    
    public void SpawnCharacter()
    {
        GameObject character = Instantiate(characterPrefab);
        character.transform.position = characterSpawnPosition;
        this.character = character.GetComponent<Character>();
        this.character.Init(1);
        this.character.startPosition = character.transform.position;
        this.character.battleManager = this;
    }

    public void SpawnMonster()
    {
        if(monsterPool == null)
        {
            GameObject monsterPool = new GameObject("MonsterPool");
            this.monsterPool = monsterPool;
        }

        int randomSpawnAmount = Random.Range(dungeons[selectDungeon].stages[curStage].randomSpawnMinAmount, dungeons[selectDungeon].stages[curStage].randomSpawnMaxAmount + 1);
        if(randomSpawnAmount > 0)
        {
            for (int i = 0; i < randomSpawnAmount; i++)
            {
                int monsterIndex = Random.Range(0, dungeons[selectDungeon].stages[curStage].randomSpawnMonsters.Count);
                GameObject monster = Instantiate(dungeons[selectDungeon].stages[curStage].randomSpawnMonsters[monsterIndex], monsterPool.transform);
                monsters.Add(monster.GetComponent<Monster>());
                monster.transform.position = monsterSpawnPosition;
                ChangeSpawnPosition();
            }
        }

        if(dungeons[selectDungeon].stages[curStage].spawnMonsters.Count > 0)
        {
            foreach (GameObject monsterPrefab in dungeons[selectDungeon].stages[curStage].spawnMonsters)
            {
                GameObject monster = Instantiate(monsterPrefab, monsterPool.transform);
                monsters.Add(monster.GetComponent<Monster>());
                monster.transform.position = monsterSpawnPosition;
                ChangeSpawnPosition();
            }
        }

        for (int i = 0; i < monsters.Count; i++)
        {
            monsters[i].monsterNumber = i;
            monsters[i].startPosition = monsters[i].transform.position;
            monsters[i].Init(1);
            monsters[i].battleManager = this;
        }

        monsterSpawnPosition = new Vector3(-1, 3, 0);
        monstersPrevState = new IState[monsters.Count];
    }

    public void ChangeSpawnPosition()
    {
        if (monsterSpawnPosition.y == 3)
            monsterSpawnPosition = new Vector3(monsterSpawnPosition.x, monsterSpawnPosition.y - 2.5f, monsterSpawnPosition.z);
        else
            monsterSpawnPosition = new Vector3(monsterSpawnPosition.x + 2.5f, monsterSpawnPosition.y + 2.5f, monsterSpawnPosition.z);
    }

    public IEnumerator BattleStart()
    {
        yield return waitFor1Sec;
        character.stateMachine.ChangeState(character.stateMachine.readyState);
        foreach (Monster monster in monsters)
        {
            monster.stateMachine.ChangeState(monster.stateMachine.readyState);
        }
    }
    private bool StageClearCheck()
    {
        for(int i = 0; i < monsters.Count; i++)
        {
            if(!(monsters[i].stateMachine.currentState is MonsterDeadState))
            {
                return false;
            }
        }
        return true;
    }
}
