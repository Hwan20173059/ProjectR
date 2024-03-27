using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum RouletteResult
{
    Triple,
    FrontPair,
    SidePair,
    BackPair,
    Different,
}
public class BattleManager : MonoBehaviour
{
    public List<DungeonSO> dungeonList;
    public int selectDungeon;
    public int curStage;
    public GameObject characterPrefab;
    public List<EquipItem> rouletteEquip;
    public RouletteResult rouletteResult;

    public Character character;
    public List<Monster> monsters;
    public Monster selectMonster;
    public List<int> performList;
    public IState characterPrevState;
    public IState[] monstersPrevState;
    public bool IsSelectingAction = false;
    public bool IsRouletteUsed = true;

    Vector3 characterSpawnPosition = new Vector3 (-6.5f, 1.5f, 0);
    Vector3 monsterSpawnPosition = new Vector3 (-1, 3, 0);

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

    private void Update()
    {
        stateMachine.HandleInput();

        stateMachine.Update();
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
            battleCanvas.UpdateMonsterState();
            battleCanvas.MonsterStatePanelOn();
        }
    }
    
    public void SpawnCharacter()
    {
        GameObject character = Instantiate(characterPrefab);
        character.transform.position = characterSpawnPosition;
        this.character = character.GetComponent<Character>();
        this.character.Init(1);
        this.character.startPosition = character.transform.position;
        this.character.battleManager = this;

        battleCanvas.CreateCharacterHpBar(this.character);
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

            battleCanvas.CreateMonsterHpBar(monsters[i]);
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

    public void NextStageStart()
    {
        battleCanvas.NextStagePanelOff();
        character.curCoolTime = 0;
        Destroy(monsterPool);
        monsterPool = null;
        monsters.Clear();
        stateMachine.ChangeState(stateMachine.startState);
    }

    public void SaveUnitState()
    {
        // 캐릭터 현재 상태 저장
        characterPrevState = character.stateMachine.currentState;
        // 몬스터들의 현재 상태 저장
        for (int i = 0; i < monsters.Count; i++)
        {
            monstersPrevState[i] = monsters[i].stateMachine.currentState;
        }
    }

    public void LoadUnitState()
    {
        if (!(character.IsDead))
        {
            // 캐릭터 상태를 이전 상태로 변경
            character.stateMachine.ChangeState(characterPrevState);
        }

        for (int i = 0; i < monsters.Count; i++)
        {
            if (!(monsters[i].IsDead))
            {
                // 몬스터들의 상태를 이전 상태로 변경
                monsters[i].stateMachine.ChangeState(monstersPrevState[i]);
            }
        }
    }

    public void ChangeUnitStateToWait()
    {
        // 캐릭터 현재 상태 Wait으로 변경
        if (!(character.IsDead))
            character.stateMachine.ChangeState(character.stateMachine.waitState);
        // 몬스터들의 현재 상태 Wait으로 변경
        for (int i = 0; i < monsters.Count; i++)
        {
            if (!(monsters[i].IsDead))
                monsters[i].stateMachine.ChangeState(monsters[i].stateMachine.waitState);
        }
    }

    public void StartActionByFirstUnit()
    {
        // 수행 리스트에 가장 먼저 입력한 유닛의 상태를 Aciton으로 변경
        if (performList[0] == 100)
        {
            character.stateMachine.ChangeState(character.stateMachine.actionState);
        }
        else
        {
            monsters[performList[0]].stateMachine.ChangeState(monsters[performList[0]].stateMachine.actionState);
        }
    }

    public bool StageClearCheck()
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

    public int AliveMonsterCount()
    {
        int count = 0;
        for (int i = 0; i < monsters.Count; i++)
        {
            if (!monsters[i].IsDead)
                count++;
        }
        return count;
    }

    public void OnClickAttackButton()
    {
        if (IsSelectingAction && selectMonster != null && !(selectMonster.IsDead))
        {
            character.curCoolTime = 0f;
            performList.Add(100);
            character.stateMachine.ChangeState(character.stateMachine.readyState);
        }
    }

    public void OnClickRouletteButton()
    {
        if (!IsRouletteUsed)
        {
            StartCoroutine(Roulette());
            IsRouletteUsed = true;

            battleCanvas.RouletteButtonOff();
        }
    }

    public IEnumerator Roulette()
    {
        for (int i = 0; i < 3; i++)
        {
            rouletteEquip.Add(PlayerManager.Instance.equip[Random.Range(0, 3)]);
            battleCanvas.SetRoulette(i);
        }

        if (rouletteEquip[0] == rouletteEquip[1])
        {
            if (rouletteEquip[1] == rouletteEquip[2])
            {
                rouletteResult = RouletteResult.Triple;
            }
            else
            {
                rouletteResult = RouletteResult.FrontPair;
            }
        }
        else if (rouletteEquip[0] == rouletteEquip[2])
        {
            rouletteResult = RouletteResult.SidePair;
        }
        else if (rouletteEquip[1] == rouletteEquip[2])
        {
            rouletteResult = RouletteResult.BackPair;
        }
        else
        {
            rouletteResult = RouletteResult.Different;
        }

        yield return null;
    }

    public void RouletteClear()
    {
        rouletteEquip.Clear();
        battleCanvas.ClearRoulette();
        battleCanvas.RouletteButtonOn();
    }

    public int GetChangeValue(RouletteResult rouletteResult, int baseValue)
    {
        switch(rouletteResult)
        {
            case RouletteResult.Triple:
                {
                    if (rouletteEquip[2].tripleChangeType == ValueChangeType.ADD)
                        baseValue += rouletteEquip[2].data.tripleValue;
                    else
                        baseValue *= rouletteEquip[2].data.tripleValue;
                    return baseValue;
                }
            case RouletteResult.FrontPair:
                {
                    if (rouletteEquip[0].doubleChangeType == ValueChangeType.ADD)
                        baseValue += rouletteEquip[0].data.doubleValue;
                    else
                        baseValue *= rouletteEquip[0].data.doubleValue;

                    if (rouletteEquip[2].singleChangeType == ValueChangeType.ADD)
                        baseValue += rouletteEquip[2].data.singleValue;
                    else
                        baseValue *= rouletteEquip[2].data.singleValue;
                    return baseValue;
                }
            case RouletteResult.SidePair:
                {
                    if (rouletteEquip[0].doubleChangeType == ValueChangeType.ADD)
                        baseValue += rouletteEquip[0].data.doubleValue;
                    else
                        baseValue *= rouletteEquip[0].data.doubleValue;

                    if (rouletteEquip[1].singleChangeType == ValueChangeType.ADD)
                        baseValue += rouletteEquip[1].data.singleValue;
                    else
                        baseValue *= rouletteEquip[1].data.singleValue;
                    return baseValue;
                }
            case RouletteResult.BackPair:
                {
                    if (rouletteEquip[0].singleChangeType == ValueChangeType.ADD)
                        baseValue += rouletteEquip[0].data.singleValue;
                    else
                        baseValue *= rouletteEquip[0].data.singleValue;

                    if (rouletteEquip[1].doubleChangeType == ValueChangeType.ADD)
                        baseValue += rouletteEquip[1].data.doubleValue;
                    else
                        baseValue *= rouletteEquip[1].data.doubleValue;
                    return baseValue;
                }
            case RouletteResult.Different:
                {
                    if (rouletteEquip[0].singleChangeType == ValueChangeType.ADD)
                        baseValue += rouletteEquip[0].data.singleValue;
                    else
                        baseValue *= rouletteEquip[0].data.singleValue;

                    if (rouletteEquip[1].singleChangeType == ValueChangeType.ADD)
                        baseValue += rouletteEquip[1].data.singleValue;
                    else
                        baseValue *= rouletteEquip[1].data.singleValue;

                    if (rouletteEquip[2].singleChangeType == ValueChangeType.ADD)
                        baseValue += rouletteEquip[2].data.singleValue;
                    else
                        baseValue *= rouletteEquip[2].data.singleValue;
                    return baseValue;
                }
            default: return baseValue;
        }
    }
}
