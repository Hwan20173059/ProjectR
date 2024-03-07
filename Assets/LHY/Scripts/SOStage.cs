using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SOStage : ScriptableObject
{
    public List<Slime> Slime = new List<Slime>();
    public int stageClearExp;
    
    public void MonsterSpawn(Transform enemyArea)
    {
        if (Slime.Count <= 0)
        {
            Debug.Log("몬스터가 설정되어 있지 않습니다.");
            return;
        }
        for (int i = 0; i < Slime.Count; i++)
        {
            Debug.Log("를 생성합니다.");
            Instantiate(Slime[i], enemyArea);
        }
    }
}
