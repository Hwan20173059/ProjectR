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
            text = $"<size=40>{value}";
        }
        else
        {
            text = $"<size=40>+{value}";
        }
        StartCoroutine(MoveTowardsText());
    }

    IEnumerator MoveTowardsText()
    {
        Vector3 target = new Vector3(transform.position.x, transform.position.y + 50);
        while (MoveTowardsText(target)) { yield return null; }
        gameObject.SetActive(false);
    }

    private bool MoveTowardsText(Vector3 target)
    {
        return target != (transform.position =
            Vector3.MoveTowards(transform.position, target, Time.deltaTime * 50f));
    }
}
