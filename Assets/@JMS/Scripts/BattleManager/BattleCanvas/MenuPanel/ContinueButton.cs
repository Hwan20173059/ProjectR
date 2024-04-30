using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour
{
    MenuPanel menuPanel;

    Button button;

    public void Init(MenuPanel menuPanel)
    {
        this.menuPanel = menuPanel;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickContinueButton);
    }

    void OnClickContinueButton()
    {
        menuPanel.gameObject.SetActive(false);
    }
}
