using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoidRoulette : MonoBehaviour
{
    public Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
}
