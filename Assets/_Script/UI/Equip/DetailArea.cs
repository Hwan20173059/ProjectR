using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailArea : MonoBehaviour
{
    private PlayerManager playerManager;
    [SerializeField] private GameObject detailObject;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemGrade;
    [SerializeField] private TextMeshProUGUI abilityInfo;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _unEquipButton;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button mergeButton;
    [SerializeField] private GameObject _activateFalseObj;
    [SerializeField] private GameObject mergeFailObj;
    public ConsumeItem nowConsumeItem;
    public EquipItem nowSelectedEquip;
    public EquipItem lastSelectedEquip;
    public bool isEquipping;

    private void Start()
    {
        playerManager = PlayerManager.Instance;
        playerManager.detailArea = this;
    }
    public void ChangeSelectedItem(EquipItem e)
    {
        ChangeDetailActivation(false);
        lastSelectedEquip = nowSelectedEquip;
        nowSelectedEquip = e;
        _image.sprite = e.equipSprite;
        _text.text = e.data.info;
        itemName.text = e.data.equipName;
        if (e.data.grade == 0)
        {
            itemGrade.text = "노말";
            itemGrade.color = Color.gray;
        }
        else if (e.data.grade == 1)
        {
            itemGrade.text = "레어";
            itemGrade.color = Color.blue;
        }
        else if (e.data.grade == 2)
        {
            itemGrade.text = "유니크";
            itemGrade.color = new Color(132f/255f, 0, 231f/255f);
        }
        else if (e.data.grade == 3)
        {
            itemGrade.text = "레전더리";
            itemGrade.color = new Color(230f / 255f, 160f/255f, 0);
        }
        abilityInfo.text = e.data.abilityInfo;

        if (e.data.id != 0)
        {
            if (e.isEquipped)
            {
                _unEquipButton.gameObject.SetActive(true);
            }
            else
            {
                _equipButton.gameObject.SetActive(true);
            }
        }
        ChangeDetailActivation(true);
    }

    public void ChangeSelectedItem(ConsumeItem c)
    {
        ChangeDetailActivation(false);
        nowConsumeItem = c;
        _image.sprite = c.consumeSprite;
        _text.text = c.data.info;
        itemName.text = c.data.consumeName;
        if(c.type == Type.AttackBuffPotion || c.type == Type.HpPotion || c.type == Type.SpeedBuffPotion)
        {
            _useButton.gameObject.SetActive(true);
        }
        else if(c.type == Type.CharacterPiece)
        {
            mergeButton.gameObject.SetActive(true);
        }
        ChangeDetailActivation(true);
    }

    public void ChangeDetailActivation(bool detailActive)
    {
        detailObject.SetActive(detailActive);
        if (!detailActive)
        {
            _equipButton.gameObject.SetActive(false);
            _unEquipButton.gameObject.SetActive(false);
            _useButton.gameObject.SetActive(false);
        }
    }

    public void ActiveEquippingState()
    {
        _activateFalseObj.SetActive(true);
        isEquipping = true;
    }

    public void UnActiveEquippingState()
    {
        _activateFalseObj.SetActive(false);
        isEquipping = false;
    }

    public void MergeCharacterPiece()
    {
        if (nowConsumeItem.count >= nowConsumeItem.data.value)
        {
            ItemManager.Instance.ReduceConsumeItem(nowConsumeItem, nowConsumeItem.count);
            int charID = nowConsumeItem.data.id - 32;
            playerManager.AddCharacter(charID, 1, 0);
        }
        else
        {
            mergeFailObj.SetActive(true);
        }
    }
    
    public void MergeFailPopupClose()
    {
        mergeFailObj.SetActive(false);
    }
}