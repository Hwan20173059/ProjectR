using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleDefeatPanel : MonoBehaviour
{
    ReturnTownButton returnTownButton;

    public void Init()
    {
        returnTownButton = GetComponentInChildren<ReturnTownButton>();
        returnTownButton.button.onClick.AddListener(TownSceneLoad);

        gameObject.SetActive(false);
    }

    void TownSceneLoad()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("TownScene");
        AudioManager.Instance.SetState();
    }
}
