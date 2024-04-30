using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouletteBase : MonoBehaviour
{
    public RouletteImage0 rouletteImage0;
    public RouletteImage1 rouletteImage1;
    public RouletteImage2 rouletteImage2;
    public RouletteImage3 rouletteImage3;

    public Vector3 startPosition;
    public Vector3 resultPosition1;
    public Vector3 resultPosition2;
    public Vector3 endPosition;

    public void Init(int rouletteNumber)
    {
        rouletteImage0 = GetComponentInChildren<RouletteImage0>();
        rouletteImage1 = GetComponentInChildren<RouletteImage1>();
        rouletteImage2 = GetComponentInChildren<RouletteImage2>();
        rouletteImage3 = GetComponentInChildren<RouletteImage3>();

        rouletteImage0.Init();
        rouletteImage1.Init();
        rouletteImage2.Init();
        rouletteImage3.Init();

        SetPosition(rouletteNumber);
    }

    void SetPosition(int rouletteNumber)
    {
        startPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        resultPosition1 = new Vector3(transform.localPosition.x, transform.localPosition.y - 150f, transform.localPosition.z);
        resultPosition2 = new Vector3(transform.localPosition.x, transform.localPosition.y - 300f, transform.localPosition.z);
        endPosition = new Vector3(transform.localPosition.x, transform.localPosition.y - 450f, transform.localPosition.z);
        
        switch (rouletteNumber)
        {
            case 1:
                transform.localPosition = resultPosition1;
                break;
            case 2:
                transform.localPosition = resultPosition2;
                break;
            default:
                break;
        }
    }

}
