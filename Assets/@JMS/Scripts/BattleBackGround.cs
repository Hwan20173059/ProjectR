using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleBackGround : MonoBehaviour
{
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ChangeBackGround();

        ChangeScale();
    }

    void ChangeScale()
    {
        if (spriteRenderer == null)
            return;

        gameObject.transform.localScale = new Vector3(1, 1, 1);

        float _width = spriteRenderer.bounds.size.x;
        float _height = spriteRenderer.bounds.size.y;

        float worldScreenHeight = (float)(Camera.main.orthographicSize * 2.0);
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;


        gameObject.transform.localScale = new Vector3(worldScreenWidth / _width, worldScreenHeight / _height, 1);
    }

    void ChangeBackGround()
    {
        switch (PlayerManager.Instance.currentState)
        {
            case CurrentState.field: spriteRenderer.sprite = Resources.Load<Sprite>("Sprite/Background/FieldBackGround"); break;
            case CurrentState.dungeon1: spriteRenderer.sprite = Resources.Load<Sprite>("Sprite/Background/forestdungeon"); break;
            case CurrentState.dungeon2: spriteRenderer.sprite = Resources.Load<Sprite>("Sprite/Background/ruinDungeon"); break;
            case CurrentState.dungeon3: spriteRenderer.sprite = Resources.Load<Sprite>("Sprite/Background/oceanDungeon"); break;
            case CurrentState.dungeon4: spriteRenderer.sprite = Resources.Load<Sprite>("Sprite/Background/helldungeon"); break;
        }
    }
}
