using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectWindow : MonoBehaviour
{
    CanvasGroup canvasGroup;
    BlackMarket shop;
    ItemSlot selectslot = null;

    // 선택된 아이템 창 출력할 것들 (이미지 텍스트 등등)
    Image selectImage;
    TextMeshProUGUI selectNameText;
    TextMeshProUGUI selectGoldText;
    // ----------------------------------------------------------

    Button buyButton;
    Button exitButton;

    Slider slider;
    TextMeshProUGUI sliderMaxText;

    TMP_InputField inputField;

    int itemCount;
    int ItemCount
    {
        get => itemCount;
        set
        {
            itemCount = value;
            if (selectslot != null)
            {
                selectGoldText.text = $"{itemCount * selectslot.goldValue} g"; 
            }
        }
    }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        shop = GetComponentInParent<BlackMarket>();
        // 선택된 아이템 창 출력할 것들 (이미지 텍스트 등등)
        selectImage = transform.GetChild(1).GetChild(3).GetComponent<Image>();
        selectNameText = transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        selectGoldText = transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>();
        // ----------------------------------------------------------

        buyButton = transform.GetChild(2).GetComponent<Button>();
        buyButton.onClick.AddListener(BuyItem);

        exitButton = transform.GetChild(3).GetComponent<Button>();
        exitButton.onClick.AddListener(Close);

        slider = GetComponentInChildren<Slider>();
        slider.onValueChanged.AddListener(SliderChangeValue);
        sliderMaxText = slider.transform.GetChild(4).GetComponent<TextMeshProUGUI>();

        inputField = GetComponentInChildren<TMP_InputField>();
        inputField.onValueChanged.AddListener(InputFieldValueChange);
    }


    private void Start()
    {
        Close();
    }

    public void Open(ItemSlot slot)
    {
        selectslot = slot;
        canvasGroup.alpha = 1.0f;
        canvasGroup.blocksRaycasts = true;

        ItemData selectItemData = Inventory.Inst.itemData[(int)slot.itemEnum];

        selectImage.sprite = selectItemData.itemIcon;
        selectNameText.text = selectItemData.itemName;
        ItemCount = 1;

        int amount = slot.RemainingCount;           // 해당 슬롯의 남은 수량
        slider.minValue = 1;
        slider.value = 1;
        slider.maxValue = amount;
        sliderMaxText.text = $"{amount}(MAX)";
    }

    void Close()
    {
        canvasGroup.alpha = 0.0f;
        canvasGroup.blocksRaycasts = false;
    }

    private void SliderChangeValue(float value)
    {
        inputField.text = ((int)value).ToString();
    }

    private void InputFieldValueChange(string text)
    {
        if (text != "")
        {
            int value = int.Parse(text);

            if (value <= selectslot.RemainingCount)
            {
                ItemCount = value;
                slider.value = value;
            }
            else
            {
                inputField.text = selectslot.RemainingCount.ToString();
            }
        }
    }

    void BuyItem()
    {
        selectslot.BuyCount += ItemCount;

        Inventory.Inst.AddItem(ItemCount, selectslot.itemEnum);

        Close();
    }
}
