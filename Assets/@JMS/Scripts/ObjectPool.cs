using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Serializable]
    struct pool
    {
        public GameObject prefab;
        public string tag;
        public int size;
    }

    [SerializeField] pool[] pools;
    Dictionary<string, Queue<GameObject>> poolsDic = new Dictionary<string, Queue<GameObject>>();

    public void Init()
    {
        if(pools != null)
        {
            for (int i = 0; i < pools.Length; i++)
            {
                Queue<GameObject> pool = new Queue<GameObject>();
                for (int p = 0; p < pools[i].size; p++)
                {
                    GameObject temp = Instantiate(pools[i].prefab, transform);
                    temp.SetActive(false);
                    pool.Enqueue(temp);
                }
                poolsDic.Add(pools[i].tag, pool);
            }
        }
    }

    public GameObject GetFromPool(string key)
    {
        GameObject go = null;
        if (!poolsDic.ContainsKey(key))
        {
            Debug.Log($"풀에 해당 {key}의 프리팹이 없음.");
            return null;
        }
        if (poolsDic[key].Peek().activeSelf)
        {
            go = Instantiate(poolsDic[key].Peek(), transform);
            go.name = poolsDic[key].Peek().name;
            poolsDic[key].Enqueue(go);
            return go;
        }
        go = poolsDic[key].Dequeue();
        poolsDic[key].Enqueue(go);
        go.SetActive(true);
        return go;
    }

    public void SetActiveFalseAll(string key)
    {
        if (!poolsDic.ContainsKey(key))
        {
            Debug.Log($"풀에 해당 {key}의 프리팹이 없음.");
        }
        foreach (GameObject go in poolsDic[key])
        {
            go.SetActive(false);
        }
    }
}
