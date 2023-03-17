using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Data", menuName = "ScriptableObject/ItemData", order = 0)]
public class ItemData : ScriptableObject
{
    [Header("������ �⺻ ������")]
    public int itemID;
    public string itemName;
    public GameObject itemPrefab;
    public Sprite itemIcon;
    [Header("������ ���� ���")]
    public int buyValue;
    [Header("������ �Ǹ� ����")]
    [Range(0f, 1f)]
    public float saleValue;
    public int SaleValue
    {
        get => (int)(buyValue * saleValue);       // �����ǼҼ��� �߶�
    }

}
