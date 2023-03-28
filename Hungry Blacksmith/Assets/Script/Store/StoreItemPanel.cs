using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreItemPanel : MonoBehaviour
{
    //Inventory inventory;
    StoreItemSlot[] itemSlots;
    StoreItemSlot itemSlot;

    Image selectSlotItemIcon;
    TextMeshProUGUI selectSlotItemName;
    TextMeshProUGUI selectSlotItemPrice;

    Button buyButton;
    Button saleButton;
    Button exitButton;

    Slider selectSlotSlider;
    TMP_InputField selectSlotInputField;
    TextMeshProUGUI selectSlotMinText;
    TextMeshProUGUI selectSlotInputFieldText;

    //int itemCount;
    int itemCountMin = 1;
    int itemCountMax = 999;

    int selectIndex;

    private void Awake()
    {
        //inventory = FindObjectOfType<Inventory>();
        itemSlots = GetComponentsInChildren<StoreItemSlot>();
        itemSlot = GetComponentInChildren<StoreItemSlot>();

        Transform select = transform.GetChild(11).GetChild(0).GetChild(0);
        selectSlotItemName = select.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        selectSlotItemPrice = select.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        selectSlotItemIcon = select.GetChild(0).GetChild(2).GetComponent<Image>();

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].onStoreItemSlot += onSelectItemSlot;
        }

        buyButton = select.GetChild(1).GetComponent<Button>();
        buyButton.onClick.AddListener(BuyStoreItem);

        saleButton = select.GetChild(2).GetComponent<Button>();
        saleButton.onClick.AddListener(SaleStoreItem);

        exitButton = select.GetChild(4).GetComponent<Button>();
        exitButton.onClick.AddListener(() =>
        {
            itemSlot.selectImage.gameObject.SetActive(false);
            selectSlotSlider.value = itemCountMin;
        });

        selectSlotSlider = select.GetChild(3).GetComponent<Slider>();
        selectSlotInputField = select.GetChild(3).GetChild(5).GetComponent<TMP_InputField>();
        selectSlotInputFieldText = select.GetChild(3).GetChild(5).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        selectSlotMinText = select.GetChild(3).GetChild(3).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        for (int i = 0; i < Inventory.Inst.itemData.Length; i++)
        {
            itemSlots[i].ItemName.text = Inventory.Inst.itemData[i].itemName;
            itemSlots[i].ItemPrice.text = Inventory.Inst.itemData[i].buyValue.ToString() + "G";
        }

        selectSlotSlider.maxValue = itemCountMax;
        selectSlotSlider.minValue = itemCountMin;
        selectSlotSlider.onValueChanged.AddListener(SliderValueChange);

        selectSlotInputField.onValueChanged.AddListener(OninputChange);
    }    

    void onSelectItemSlot(StoreItemSlot slot)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i] == slot)
            {
                selectIndex = i;
                selectSlotItemName.text = Inventory.Inst.itemData[i].itemName;
                selectSlotItemPrice.text = Inventory.Inst.itemData[i].buyValue.ToString() + "G";
                selectSlotItemIcon.sprite = Inventory.Inst.itemData[i].itemIcon;
            }
        }
    }

    void SliderValueChange(float value)
    {        
        selectSlotSlider.value = Mathf.Clamp(value, itemCountMin, itemCountMax);
        selectSlotMinText.text = selectSlotSlider.value.ToString("F0");
        selectSlotInputField.text = ((int)selectSlotSlider.value).ToString();
    }

    private void OninputChange(string text)
    {
        if (text != "")
        {
            if (int.Parse(text) >= itemCountMin && int.Parse(text) <= itemCountMax)
            {
                selectSlotInputField.text = text;
                selectSlotSlider.value = int.Parse(text);
                Debug.Log($"{text}를 입력함");
            }
            else if (int.Parse(text) < itemCountMin)
            {
                selectSlotInputField.text = itemCountMin.ToString();
                selectSlotSlider.value = int.Parse(text);
            }
            else if (int.Parse(text) > itemCountMax)
            {
                selectSlotInputField.text = itemCountMax.ToString();
                selectSlotSlider.value = int.Parse(text);
            }     
        }
        else
        {
            selectSlotInputField.text = itemCountMin.ToString();
            selectSlotSlider.value = itemCountMin;
        }
    }

    void BuyStoreItem()
    {
       Inventory.Inst.AddItem((int)selectSlotSlider.value, (ItemEnum)selectIndex);
       
    }

    void SaleStoreItem()
    {
        Inventory.Inst.SubItem((int)selectSlotSlider.value, (ItemEnum)Inventory.Inst.itemData[selectIndex].itemID);
    }
}
