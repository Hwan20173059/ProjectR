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
            Debug.Log("���Ͱ� �����Ǿ� ���� �ʽ��ϴ�.");
            return;
        }
        for (int i = 0; i < Slime.Count; i++)
        {
            Debug.Log("�� �����մϴ�.");
            Instantiate(Slime[i], enemyArea);
        }
    }
}
