using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public PlayerInput Input {  get; private set; }
    public Monster selectMonster;

    public BattleCanvas BattleCanvas;

    public List<DungeonSO> Dungeons;
    public int selectDungeon;
    public int curStage;
    public bool isStageClear { get { return StageClearCheck(); } }

    public GameObject monsterPool;

    Vector3 MonsterSpawnPosition = new Vector3 (-1, 3, 0);
    public GameObject CharacterPrefab;
    Vector3 CharacterSpawnPosition = new Vector3 (-6.5f, 1.5f, 0);

    public List<int> PerformList;

    public Character Character;
    public IState characterPrevState;
    public List<Monster> Monsters;
    public IState[] monstersPrevState;

    private WaitForSeconds WaitFor1Sec = new WaitForSeconds(1f);

    public BattleStateMachine stateMachine;

    private void Awake()
    {
        Input = GetComponent<PlayerInput>();

        BattleCanvas = GetComponentInChildren<BattleCanvas>();

        PerformList = new List<int>();

        stateMachine = new BattleStateMachine(this);
    }
    private void Start()
    {
        Input.ClickActions.MouseClick.started += OnClickStart;

        stateMachine.ChangeState(stateMachine.StartState);
        //SpawnCharacter();
        //SpawnMonster();
        //stateMachine.ChangeState(stateMachine.WaitState);
        //StartCoroutine(BattleStart());
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
        GameObject character = Instantiate(CharacterPrefab);
        character.transform.position = CharacterSpawnPosition;
        Character = character.GetComponent<Character>();
        Character.startPosition = character.transform.position;
        Character.battleManager = this;
    }

    public void SpawnMonster()
    {
        if(monsterPool == null)
        {
            GameObject monsterPool = new GameObject("MonsterPool");
            this.monsterPool = monsterPool;
        }

        int randomSpawnAmount = Random.Range(Dungeons[selectDungeon].Stages[curStage].randomSpawnMinAmount, Dungeons[selectDungeon].Stages[curStage].randomSpawnMaxAmount + 1);
        if(randomSpawnAmount > 0)
        {
            for (int i = 0; i < randomSpawnAmount; i++)
            {
                int monsterIndex = Random.Range(0, Dungeons[selectDungeon].Stages[curStage].RandomSpawnMonsters.Count);
                GameObject monster = Instantiate(Dungeons[selectDungeon].Stages[curStage].RandomSpawnMonsters[monsterIndex], monsterPool.transform);
                Monsters.Add(monster.GetComponent<Monster>());
                monster.transform.position = MonsterSpawnPosition;
                ChangeSpawnPosition();
            }
        }

        if(Dungeons[selectDungeon].Stages[curStage].SpawnMonsters.Count > 0)
        {
            foreach (var monster in Dungeons[selectDungeon].Stages[curStage].SpawnMonsters)
            {
                GameObject _monster = Instantiate(monster, monsterPool.transform);
                Monsters.Add(_monster.GetComponent<Monster>());
                _monster.transform.position = MonsterSpawnPosition;
                ChangeSpawnPosition();
            }
        }

        for (int i = 0; i < Monsters.Count; i++)
        {
            Monsters[i].monsterNumber = i;
            Monsters[i].startPosition = Monsters[i].transform.position;
            Monsters[i].battleManager = this;
        }

        MonsterSpawnPosition = new Vector3(-1, 3, 0);
        monstersPrevState = new IState[Monsters.Count];
    }

    public void ChangeSpawnPosition()
    {
        if (MonsterSpawnPosition.y == 3)
            MonsterSpawnPosition = new Vector3(MonsterSpawnPosition.x, MonsterSpawnPosition.y - 2.5f, MonsterSpawnPosition.z);
        else
            MonsterSpawnPosition = new Vector3(MonsterSpawnPosition.x + 2.5f, MonsterSpawnPosition.y + 2.5f, MonsterSpawnPosition.z);
    }

    public IEnumerator BattleStart()
    {
        yield return WaitFor1Sec;
        Character.stateMachine.ChangeState(Character.stateMachine.ReadyState);
        foreach (var monster in Monsters)
        {
            monster.stateMachine.ChangeState(monster.stateMachine.ReadyState);
        }
    }
    private bool StageClearCheck()
    {
        for(int i = 0; i < Monsters.Count; i++)
        {
            if(!(Monsters[i].stateMachine.currentState is MonsterDeadState))
            {
                return false;
            }
        }
        return true;
    }
}