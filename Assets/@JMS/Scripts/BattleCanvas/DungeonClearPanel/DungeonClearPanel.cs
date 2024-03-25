using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DungeonClearPanel : MonoBehaviour
{
    DungeonClearButton dungeonClearButton;

    public void Init()
    {
        dungeonClearButton = GetComponentInChildren<DungeonClearButton>();

        dungeonClearButton.button.onClick.AddListener(TownSceneLoad);
    }

    void TownSceneLoad()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("FieldScene");
    }
}
