using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardTrigger : MonoBehaviour
{
    [Header("Reward")]
    public int exp;
    public int gold;
    public List<Reward> rewards;


    public void TriggerReward()
    {
        RewardManager.instance.Rewading();
    }
}
