using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DetailArea : MonoBehaviour
{
    private ItemManager itemManager;
    public PlayerManager playerManager;
    [SerializeField] private GameObject detailObject;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemGrade;
    [SerializeField] private TextMeshProUGUI abilityInfo;
    [SerializeField] private TextMeshProUGUI attackInfo;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _unEquipButton;
    [SerializeField] private Button _useButton;
    [SerializeField] private Button mergeButton;
    [SerializeField] private Button expUseButton;
    [SerializeField] private GameObject _activateFalseObj;
    [SerializeField] private GameObject mergeFailObj;
    [SerializeField] private GameObject mergeSuccessObj;
    [SerializeField] private GameObject equipDetail;
    [SerializeField] private TextMeshProUGUI gold;
    [SerializeField] private Image charImage;
    public ConsumeItem nowConsumeItem;
    public EquipItem nowSelectedEquip;
    public EquipItem lastSelectedEquip;
    public bool isEquipping;
    public Animator anim;

    private void Start()
    {
        itemManager = ItemManager.Instance;
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
        ItemGradeColor(e);
        abilityInfo.text = e.data.abilityInfo;
        attackInfo.text = "공격력: " + e.data.singleValue;

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
        equipDetail.SetActive(true);
        ChangeDetailActivation(true);
    }

    public void ChangeSelectedItem(ConsumeItem c)
    {
        equipDetail.SetActive(false);
        ChangeDetailActivation(false);
        nowConsumeItem = c;
        _image.sprite = c.consumeSprite;
        _text.text = c.data.info;
        itemName.text = c.data.consumeName;
        if(c.type == Type.AttackBuffPotion || c.type == Type.HpPotion || c.type == Type.SpeedBuffPotion)
        {
            if (_useButton != null) _useButton.gameObject.SetActive(true);
        }
        else if(c.type == Type.ExpItem)
        {
            if (expUseButton != null) expUseButton.gameObject.SetActive(true);
        }
        else if(c.type == Type.CharacterPiece)
        {
            if (mergeButton != null) mergeButton.gameObject.SetActive(true);
        }
        ChangeDetailActivation(true);
    }

    public void ChangeDetailActivation(bool detailActive)
    {
        detailObject.SetActive(detailActive);
        if (!detailActive)
        {
            if (_equipButton != null) _equipButton.gameObject.SetActive(false);
            if (_unEquipButton != null) _unEquipButton.gameObject.SetActive(false);
            if (_useButton != null) _useButton.gameObject.SetActive(false);
            if (mergeButton != null) mergeButton.gameObject.SetActive(false);
            if (expUseButton != null) expUseButton.gameObject.SetActive(false);
        }
    }

    public void ActiveEquippingState()
    {
        AudioManager.Instance.PlayUISelectSFX();

        _activateFalseObj.SetActive(true);
        isEquipping = true;
    }

    public void UnActiveEquippingState()
    {
        AudioManager.Instance.PlayUISelectSFX();

        _activateFalseObj.SetActive(false);
        isEquipping = false;
    }

    public void MergeCharacterPiece()
    {
        if (nowConsumeItem.count >= nowConsumeItem.data.value)
        {
            if (itemManager == null) itemManager = ItemManager.Instance;
            itemManager.ReduceConsumeItem(nowConsumeItem, nowConsumeItem.count);
            int charID = nowConsumeItem.data.id - 32;
            playerManager.AddCharacter(charID, 1, 0);
            itemManager.inventory.FreshConsumeSlot();

            MergeSuccessPopup(nowConsumeItem.consumeSprite);
            ChangeDetailActivation(false);
        }
        else
        {
            mergeFailObj.SetActive(true);
        }
    }

    public void ExpScrollUse()
    {
        playerManager.townUiManager.characterUI.CharacterInfoUIRefresh();

        if (nowConsumeItem.count == 1) ChangeDetailActivation(false);
        itemManager.ReduceConsumeItem(nowConsumeItem);
        int exp = nowConsumeItem.data.value;
        playerManager.characterList[playerManager.selectedCharacterIndex].ChangeExp(exp);
        itemManager.inventory.FreshConsumeSlot();
    }
    
    public void MergeFailPopupClose()
    {
        mergeFailObj.SetActive(false);
    }

    public void ItemGradeColor(EquipItem e)
    {
        if (e.data.grade == 0)
        {
            itemGrade.text = "노멀";
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
    }

    public void MergeSuccessPopup(Sprite characterSprite)
    {
        charImage.sprite = characterSprite;
        mergeSuccessObj.SetActive(true);
        anim.Play("merge");
    }

    public void MergeSuccessPopupClose()
    {
        mergeSuccessObj.SetActive(false);
    }

    public void RefreshGoldUI()
    {
        if (playerManager == null) playerManager = PlayerManager.Instance;
        gold.text = "<sprite=0> " + playerManager.gold;
    }
}