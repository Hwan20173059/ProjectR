using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffDescriptionPanel : MonoBehaviour
{
    BuffDescriptionText buffDescriptionText;

    public void Init()
    {
        buffDescriptionText = GetComponentInChildren<BuffDescriptionText>();
        buffDescriptionText.Init();

        gameObject.SetActive(false);
    }

    public void UpdateBuffText(Buff buff)
    {
        buffDescriptionText.UpdateBuffText(buff);
    }
}
