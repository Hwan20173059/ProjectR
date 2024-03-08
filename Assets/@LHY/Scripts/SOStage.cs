using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu]
public class SOStage : ScriptableObject
{
    public List<GameObject> Slime = new List<GameObject>();
    public int stageClearExp;
    
    public void MonsterSpawn(Transform transform)
    {
        if (Slime.Count <= 0)
        {
            //여기말고 몬스터 호출할때 걸리긴함.
            Debug.Log("몬스터가 설정되어 있지 않습니다.");
            return;
        }
        for (int i = 0; i < Slime.Count; i++)
        {
            //Instantiate(Slime[i], transform);

            // 오브젝트를 아래에 위치의 맞는 위치를 가져와서.
            // [2]  [1]  [3]  순으로 생성되게 함.

            Debug.Log("슬라임을 생성합니다.");
            GameObject item = Instantiate(Slime[i], transform);
            item.transform.position = new Vector3(i, 0, 0);
            //todo : [2]  [1]  [3]  순으로 생성되게 함.
        }
    }
}
