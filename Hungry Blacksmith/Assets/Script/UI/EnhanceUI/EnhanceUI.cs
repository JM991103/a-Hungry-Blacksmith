using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceUI : MonoBehaviour
{
    public Color selectColor;
    public Color ideColor;

    Button enhanceButton;
    Button yesButton;
    Button noButton;

    Slot selectSlot;
    Slot[] slots;
    Image[] slotImages;

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
    }

    /// <summary>
    /// 강화안내문구 함수. (강화 상세정보 출력 및 할지 말지 선택)
    /// </summary>
    void EnhanceAnnouncementText()
    {
        if (selectSlot != null)
        {
            selectSlot.weapon.Enhance();        // 지금은 누르면 바로 강화 추후 상의 후 결정

        }
    }

    void SettingSlot(Slot slot)
    {
        if (selectSlot != slot)
        {
            if (selectSlot == null)
            {
                enhanceButton.interactable = true;
            }

            selectSlot = slot;

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i] == slot)
                {
                    slotImages[i].color = selectColor;
                }
                else
                {
                    slotImages[i].color = ideColor;
                }
            }
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
        }
    }
}
