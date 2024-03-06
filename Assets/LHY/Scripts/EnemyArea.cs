using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyArea : MonoBehaviour
{
    HorizontalLayoutGroup horizontalLayoutGroup;

    public float monsterCount;
    //todo : �� �κ� �������� ������ �޾ƿ;� ��.
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
    }
}
