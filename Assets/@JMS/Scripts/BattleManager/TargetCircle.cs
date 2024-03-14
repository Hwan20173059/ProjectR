using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCircle : MonoBehaviour
{
    public Transform targetTransform;

    private void Update()
    {
        if (targetTransform != null)
            transform.position = targetTransform.position;
    }
}
