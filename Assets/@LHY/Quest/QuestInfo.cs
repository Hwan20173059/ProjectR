using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestInfo
{
    [field: SerializeField] public string id { get; private set; }

    [Header("General")]
    public string displayName;
    public string questDescription;

    [Header("Requirements")]
    public int levelRequirement;
    public QuestInfoSO[] questPrerequisites;

    [Header("Steps")]
    public GameObject[] questStepPrefebs;
    //todo : ����� ��ȹ) ����Ǵ� ����Ʈ�� General in NextID�� ���� �䱸���� üũ ����Ʈ�� �ݿ�
    // �׷��� ���� �迭�� ���� ������.


    [Header("Rewards")]
    public int goldReward;
    public int expReward;
    public ConsumeReward[] consumeRewards;
    public EquipReward[] equipRewards;
    //public Item itemID;??????????????
    //todo : ���� ������ �߰�

    private void OnValidate()
    {
        //�ϴ� ���� ID ��ü ����


        //Unity�󿡼� ���ϸ��̶� id�� �������ѹ�����.
        //id�� ����Ʈ�� key�ν� �ʹ� �߿��ϰ� �ٸ� �ʿ䵵 ����.
        //#if UNITY_EDITOR
        //id = this.name;
        //UnityEditor.EditorUtility.SetDirty(this);
        //#endif
    }
    private void Awake()
    {

    }
}
