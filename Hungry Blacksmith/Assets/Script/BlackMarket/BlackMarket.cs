using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackMarket : MonoBehaviour
{
    ItemSlot selectSlot = null;
    ItemSlots itemSlots;
    SelectWindow selectWindow;

    float saleRatio;

    public float SaleRatio => saleRatio;

    private void Awake()
    {
        selectWindow = GetComponentInChildren<SelectWindow>();
        itemSlots = GetComponentInChildren<ItemSlots>();
    }

    private void Start()
    {
        saleRatio = Random.Range(0.6f, 0.7f);           // 30 ~ 40% 세일 가격

        itemSlots.RandomitemSetting();
    }

    /// <summary>
    /// 선택 슬롯
    /// </summary>
    /// <param name="slot"></param>
    public void SelectedSlot(ItemSlot slot)
    {
        selectSlot = slot;

        selectWindow.Open(selectSlot);
    }
}
