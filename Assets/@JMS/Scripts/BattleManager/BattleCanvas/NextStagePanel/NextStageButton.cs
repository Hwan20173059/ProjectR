using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextStageButton : MonoBehaviour
{
    NextStagePanel nextStagePanel;

    Button button;

    public void Init(NextStagePanel nextStagePanel)
    {
        this.nextStagePanel = nextStagePanel;

        button = GetComponent<Button>();
        button.onClick.AddListener(NextStageStart);
    }

    void NextStageStart()
    {
        nextStagePanel.NextStageStart();
    }
}
