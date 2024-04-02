using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataSave : Singleton<DataSave>
{
    public PlayerManager playerManager;

    private void Start()
    {
        playerManager = PlayerManager.Instance;
    }
}
