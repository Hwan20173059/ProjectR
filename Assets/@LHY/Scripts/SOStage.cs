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
            //���⸻�� ���� ȣ���Ҷ� �ɸ�����.
            Debug.Log("���Ͱ� �����Ǿ� ���� �ʽ��ϴ�.");
            return;
        }
        for (int i = 0; i < Slime.Count; i++)
        {
            //Instantiate(Slime[i], transform);

            // ������Ʈ�� �Ʒ��� ��ġ�� �´� ��ġ�� �����ͼ�.
            // [2]  [1]  [3]  ������ �����ǰ� ��.

            Debug.Log("�������� �����մϴ�.");
            GameObject item = Instantiate(Slime[i], transform);
            item.transform.position = new Vector3(i, 0, 0);
            //todo : [2]  [1]  [3]  ������ �����ǰ� ��.
        }
    }
}
