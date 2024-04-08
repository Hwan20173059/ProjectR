using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardTrigger : MonoBehaviour
{
    [Header("Reward")]
    public int exp;
    public int gold;
    public EquipReward equipReward;
    public ConsumeReward consumeReward;

    public void TriggerReward()
    {
        //RewardManager.instance.Rewading(equipReward, consumeReward, gold, exp);
    }
}
