using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public List<DungeonSO> dungeonList;
    public int selectDungeon;
    public int curStage;
    public GameObject characterPrefab;
    public List<EquipItem> rouletteResult;

    public Character character;
    public bool IsSelectingAction = false;
    public bool IsRouletteUsed = true;
    public Monster selectMonster;
    public List<Monster> monsters;
    public IState characterPrevState;
    public IState[] monstersPrevState;
    public List<int> performList;

    public bool isStageClear { get { return StageClearCheck(); } }

    Vector3 monsterSpawnPosition = new Vector3 (-1, 3, 0);
    Vector3 characterSpawnPosition = new Vector3 (-6.5f, 1.5f, 0);

    public GameObject targetCirclePrefab;
    public TargetCircle targetCircle;

    public GameObject monsterPool;
    public PlayerInput Input {  get; private set; }

    public BattleCanvas battleCanvas;

    public BattleStateMachine stateMachine;

    private WaitForSeconds waitFor1Sec = new WaitForSeconds(1f);

    private void Awake()
    {
        performList = new List<int>();

        Input = GetComponent<PlayerInput>();

        battleCanvas = GetComponentInChildren<BattleCanvas>();

        stateMachine = new BattleStateMachine(this);

        characterPrefab = PlayerManager.Instance.selectedCharacter;
    }
    private void Start()
    {
        Input.ClickActions.MouseClick.started += OnClickStart;

        stateMachine.ChangeState(stateMachine.startState);
    }

    private void OnClickStart(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (targetCircle == null)
        {
            targetCircle = Instantiate(targetCirclePrefab).GetComponent<TargetCircle>();
            targetCircle.gameObject.SetActive(false);
        }

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.ClickActions.MousePos.ReadValue<Vector2>());
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("Monster"))
        {
            selectMonster = hit.collider.GetComponent<Monster>();
            
            targetCircle.targetTransform = selectMonster.transform;
            targetCircle.gameObject.SetActive(true);
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

        int randomSpawnAmount = Random.Range(dungeonList[selectDungeon].stages[curStage].randomSpawnMinAmount, dungeonList[selectDungeon].stages[curStage].randomSpawnMaxAmount + 1);
        if(randomSpawnAmount > 0)
        {
            for (int i = 0; i < randomSpawnAmount; i++)
            {
                int monsterIndex = Random.Range(0, dungeonList[selectDungeon].stages[curStage].randomSpawnMonsters.Count);
                GameObject monster = Instantiate(dungeonList[selectDungeon].stages[curStage].randomSpawnMonsters[monsterIndex], monsterPool.transform);
                monsters.Add(monster.GetComponent<Monster>());
                monster.transform.position = monsterSpawnPosition;
                ChangeSpawnPosition();
            }
        }

        if(dungeonList[selectDungeon].stages[curStage].spawnMonsters.Count > 0)
        {
            foreach (GameObject monsterPrefab in dungeonList[selectDungeon].stages[curStage].spawnMonsters)
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
            if(!(monsters[i].IsDead))
            {
                return false;
            }
        }
        return true;
    }

    public IEnumerator Roulette()
    {
        for (int i = 0; i < 3; i++)
        {
            rouletteResult.Add(PlayerManager.Instance.equip[Random.Range(0, 3)]);
            battleCanvas.SetRoulette(i);
        }
        yield return null;
    }

    public void RouletteClear()
    {
        rouletteResult.Clear();
        battleCanvas.ClearRoulette();
        battleCanvas.rouletteButton.gameObject.SetActive(true);
    }
}
