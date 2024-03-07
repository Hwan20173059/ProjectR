using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyArea : MonoBehaviour
{
    HorizontalLayoutGroup horizontalLayoutGroup;

    private float monsterCount = 100;
    //todo : 이 부분 던전에서 데이터 받아와야 함.
    void Awake()
    {
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
    }

    private void Start()
    {
        Settings();
    }

    void Settings()
    {
        if (monsterCount != 0)
            horizontalLayoutGroup.spacing = monsterCount;
        //반복문 통해서 몬스터 생성
    }
}
