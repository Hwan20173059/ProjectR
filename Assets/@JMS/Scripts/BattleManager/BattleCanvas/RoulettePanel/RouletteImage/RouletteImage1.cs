using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RouletteImage1 : MonoBehaviour
{
    public Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }
}