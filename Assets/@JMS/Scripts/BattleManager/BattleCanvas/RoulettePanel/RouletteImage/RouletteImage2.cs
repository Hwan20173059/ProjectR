using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteImage2 : MonoBehaviour
{
    Image image;

    public void Init()
    {
        image = GetComponent<Image>();
        image.sprite = PlayerManager.Instance.equip[2].equipSprite;
    }

}
