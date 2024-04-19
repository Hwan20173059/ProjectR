using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingUI : MonoBehaviour
{
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OpenScreen()
    {
        StartCoroutine(Open());
    }

    public void CloseScreen(string loadScene)
    {
        StartCoroutine(Close(loadScene));
    }

    IEnumerator Open()
    {
        image.fillAmount = 1;
        image.fillClockwise = false;

        while (image.fillAmount > 0) { image.fillAmount -= Time.deltaTime; yield return null; }
        image.fillClockwise = true;

        gameObject.SetActive(false);
    }

    IEnumerator Close(string loadScene)
    {
        image.fillAmount = 0;
        image.fillClockwise = true;

        while (image.fillAmount < 1) { image.fillAmount += Time.deltaTime; yield return null; }
        image.fillClockwise = false;

        SceneManager.LoadScene(loadScene);
    }
}
