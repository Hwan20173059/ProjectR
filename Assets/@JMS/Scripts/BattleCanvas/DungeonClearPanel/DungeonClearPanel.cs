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

        gameObject.SetActive(false);
    }

    void TownSceneLoad()
    {
        gameObject.SetActive(false);
        if (PlayerManager.Instance.isField == true)
        {
            AudioManager.Instance.SetState();
            SceneManager.LoadScene("FieldScene");
        }
        else if (PlayerManager.Instance.isDungeon == true)
        {
            AudioManager.Instance.SetState();
            SceneManager.LoadScene("DungeonScene");
        }
        
    }
}
