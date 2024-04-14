using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaSlot : MonoBehaviour
{
    private Image panelImage;
    public Image image;
    public Animator anim;

    private void Awake()
    {
        panelImage = GetComponent<Image>();
        anim = image.GetComponent<Animator>();
    }

    private void OnEnable()
    {
        Color c = panelImage.color;
        Color c1 = image.color;
        c.a = 0;
        c1.a = 0;
        panelImage.color = c;
        image.color = c1;
        StartCoroutine(FadeIn());
    }

    public void SetImage(Sprite sprite)
    {
        image.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0, 0);
        image.sprite = sprite;
    }

    public void PlayAnim(string methodName)
    {
        Invoke(methodName, 1f);
    }

    public void CoinAnim()
    {
        anim.Play("coin");
    }

    public void ScrollAnim()
    {
        anim.Play("scroll");
    }

    public IEnumerator FadeIn()
    {
        for (float f = 0f; f <= 1; f += 0.02f)
        {
            Color c = panelImage.color;
            c.a = f;
            panelImage.color = c;
            Color c1 = image.color;
            c1.a = f;
            image.color = c1;
            yield return null;
        }
        yield return new WaitForSeconds(1);
    }
}
