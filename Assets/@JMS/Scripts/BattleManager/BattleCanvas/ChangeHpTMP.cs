using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeHpTMP : MonoBehaviour
{
    TextMeshProUGUI textMeshProUGUI;
    public string text { get { return textMeshProUGUI.text; } set { textMeshProUGUI.text = value; } }

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void SetChangeHpTMP(int value)
    {
        if (value < 0)
        {
            if (value < -400)
            {
                text = $"</size><size=65></color><#FF0000>{value}!";
            }
            else if (value < -200)
            {
                text = $"</size><size=50></color><#FF0000>{value}";
            }
            else if (value < -100)
            {
                text = $"</size><size=45></color><#FF0000>{value}";
            }
            else
            {
                text = $"</size><size=40></color><#FF0000>{value}";
            }
        }
        else if (value == 0)
        {
            text = $"</size><size=40></color>0";
        }
        else
        {
            if (value > 400)
            {
                text = $"</size><size=65></color><#00FF00>+{value}!";
            }
            else if (value > 200)
            {
                text = $"</size><size=50></color><#00FF00>+{value}";
            }
            else if (value > 100)
            {
                text = $"</size><size=45></color><#00FF00>+{value}";
            }
            else
            {
                text = $"</size><size=40></color><#00FF00>+{value}";
            }
        }
        StartCoroutine(MoveTowardsText());
    }

    IEnumerator MoveTowardsText()
    {
        Vector3 target = new Vector3(transform.position.x, transform.position.y + 30);
        while (MoveTowardsText(target)) { yield return null; }
        gameObject.SetActive(false);
    }

    private bool MoveTowardsText(Vector3 target)
    {
        return target != (transform.position =
            Vector3.MoveTowards(transform.position, target, Time.deltaTime * 50f));
    }
}
