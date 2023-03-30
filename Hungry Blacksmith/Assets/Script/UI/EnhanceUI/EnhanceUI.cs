using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceUI : MonoBehaviour
{
    public Color selectColor;
    public Color ideColor;

    Slot selectSlot;
    Slot[] slots;
    Image[] slotImages;
    Image selectBox;
    Button enhanceButton;

    // 강화 창 ---------------------------------------------------------------

    Button yesButton;
    Button noButton;

    CanvasGroup windowGroup;

    Image presentWeaponImage;
    Image nextWeaponImage;
    TextMeshProUGUI presenWeaponName;
    TextMeshProUGUI nextWeaponName;
    ItemMaterial[] itemMaterials;

    private void Awake()
    {
        enhanceButton = transform.GetChild(3).GetComponent<Button>();
        enhanceButton.onClick.AddListener(EnhanceAnnouncementText);
        enhanceButton.interactable = false;
        slots = GetComponentsInChildren<Slot>();

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].selectWeaponSlot += SettingSlot;
        }

        slotImages = new Image[slots.Length];
        for (int i = 0; i < slotImages.Length; i++)
        {
            slotImages[i] = transform.GetChild(i).GetComponent<Image>();
        }

        selectBox = transform.GetChild(4).GetComponent<Image>();
        selectBox.gameObject.SetActive(false);

        // 강화 창 컴포넌트 찾기 --------------------------------------------------------------------------

        windowGroup = GetComponentInChildren<CanvasGroup>();

        yesButton = windowGroup.transform.GetChild(4).GetComponent<Button>();
        yesButton.onClick.AddListener(EnhanceStart);
        noButton = windowGroup.transform.GetChild(5).GetComponent<Button>();
        noButton.onClick.AddListener(EnhanceWindowClose);

        presentWeaponImage = windowGroup.transform.GetChild(1).GetChild(0).GetComponent<Image>();
        nextWeaponImage = windowGroup.transform.GetChild(2).GetChild(0).GetComponent<Image>();

        presenWeaponName = presentWeaponImage.transform.parent.GetComponentInChildren<TextMeshProUGUI>();
        nextWeaponName = nextWeaponImage.transform.parent.GetComponentInChildren<TextMeshProUGUI>();

        itemMaterials = GetComponentsInChildren<ItemMaterial>();

        foreach( var itemMaterial in itemMaterials )
        {
            itemMaterial.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        GameManager gameManager = GameManager.Inst;
        if (gameManager.Weapons[0] != null && gameManager.Weapons[1] != null && gameManager.Weapons[2] != null)
        {
            // 세이브 본이 있을 때
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].weapon.EnganceRange = gameManager.Weapons[i].EnganceRange;
            }
        }
        else
        {
            // 세이브 본이 없을 때
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].weapon.Rest();
            }
        }

        EnhanceWindowClose();
    }

    /// <summary>
    /// 강화안내문구 함수. (강화 상세정보 출력 및 할지 말지 선택)
    /// </summary>
    void EnhanceAnnouncementText()
    {
        EnhanceWindowOpen();
    }

    void EnhanceStart()
    {
        if (selectSlot != null)
        {
            selectSlot.weapon.Enhance();        // 지금은 누르면 바로 강화 추후 상의 후 결정

            GameManager gameManager = GameManager.Inst;

            for (int i = 0; i < slots.Length; i++)
            {
                gameManager.WeaponSave(slots[i].weapon, i);
            }
        }

        EnhanceWindowClose();
    }

    /// <summary>
    /// 슬롯 선택
    /// </summary>
    /// <param name="slot"></param>
    void SettingSlot(Slot slot)
    {
        if (selectSlot != slot)
        {
            // 선택
            if (selectSlot == null)
            {
                enhanceButton.interactable = true;      // 버튼 활성화
            }

            selectSlot = slot;

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == slot)
                {
                    // 선택한 슬롯이면
                    slotImages[i].color = selectColor;
                    selectBox.transform.SetParent(slotImages[i].transform);
                    selectBox.rectTransform.anchoredPosition = Vector3.zero;
                }
                else
                {
                    // 선택한 슬롯이 아니면
                    slotImages[i].color = ideColor;
                }
            }
            selectBox.gameObject.SetActive(true);
        }
        else
        {
            // 선택 해제
            selectSlot = null;
            enhanceButton.interactable = false;
            for (int i = 0; i < slots.Length; i++)
            {
                slotImages[i].color = ideColor;
            }
            selectBox.transform.SetParent(transform);
            selectBox.gameObject.SetActive(false);
        }
    }

    void EnhanceWindowOpen()
    {
        if (selectSlot != null)
        {
            windowGroup.alpha = 1.0f;
            windowGroup.blocksRaycasts = true;

            // 무기 이미지 적용
            presentWeaponImage.sprite = selectSlot.weapon.WeaponModel.itemIcon;
            nextWeaponImage.sprite = selectSlot.weapon.NextWeaponModel.itemIcon;

            // 무기 이름 적용
            presenWeaponName.text = selectSlot.weapon.WeaponModel.itemName;
            nextWeaponName.text = selectSlot.weapon.NextWeaponModel.itemName;

            int index = selectSlot.weapon.WeaponModel.costData.Length;

            for (int i = 0; i < index; i++)
            {
                itemMaterials[i].gameObject.SetActive(true) ;
                itemMaterials[i].image.sprite = Inventory.Inst.itemData[(int)selectSlot.weapon.WeaponModel.costData[i].costItem].itemIcon;
                itemMaterials[i].TextChange(Inventory.Inst.inventory[(int)selectSlot.weapon.WeaponModel.costData[i].costItem].itemCount, selectSlot.weapon.WeaponModel.costData[i].costItemValue);
            }
        }
    }

    void EnhanceWindowClose()
    {
        windowGroup.alpha = 0.0f;
        windowGroup.blocksRaycasts = false;

        for (int i = 0; i < itemMaterials.Length; i++)
        {
            itemMaterials[i].gameObject.SetActive(false);
        }
    }
}
