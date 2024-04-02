using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    public GameObject settings;

    public void settingsOnClick()
    {
        hide();
        settings.SetActive(true);
    }

    private void hide()
    {
        this.gameObject.SetActive(false);
    }
}
