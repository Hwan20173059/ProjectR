using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCircle : MonoBehaviour
{
    public void SetPosition(Vector3 target)
    {
        transform.position = target;
    }
}
