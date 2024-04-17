using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class QuestNotification : MonoBehaviour
{
    public GameObject notification;

    private void OnEnable()
    {
        GameEventManager.instance.questEvent.onQuestNofication += NotificationOn;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvent.onQuestNofication -= NotificationOn;
    }

    public void NotificationOn()
    {
        notification.SetActive(true);
    }

    //Button¿¡ ¿¬°á
    public void notificationOff()
    {
        notification.SetActive(false);
    }
}
