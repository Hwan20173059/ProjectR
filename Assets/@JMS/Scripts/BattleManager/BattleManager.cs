using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public List<DungeonSO> Dungeons;
    public CharacterJMS Character;
    public List<GameObject> MonsterList;
    public Queue<StateMachine> PerformList;

    public BattleStateMachine stateMachine;

    private void Awake()
    {
        PerformList = new Queue<StateMachine>();

        stateMachine = new BattleStateMachine(this);
    }
    private void Start()
    {
        stateMachine.ChangeState(stateMachine.WaitState);
    }
    private void Update()
    {
        stateMachine.HandleInput();

        stateMachine.Update();
    }
    void BattleInit()
    {

    }
}
