using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    Image selectBox;

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
    }

    /// <summary>
    /// 강화안내문구 함수. (강화 상세정보 출력 및 할지 말지 선택)
    /// </summary>
    void EnhanceAnnouncementText()
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
}
