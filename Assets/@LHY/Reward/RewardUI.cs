using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RewardUI : MonoBehaviour
{
    public GameObject rewardUI;
    public void Off()
    {
        rewardUI.SetActive(!rewardUI.activeSelf);
        //RemoveSlot();
        //rewardUI.SetActive(false);
    }
}
