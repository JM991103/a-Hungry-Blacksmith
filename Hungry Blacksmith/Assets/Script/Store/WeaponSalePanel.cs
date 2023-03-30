using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSalePanel : MonoBehaviour
{
    Image weaponSalePanel;
    Button exitButton;

    StoreWeaponSlot[] storeWeaponSlot;

    private void Awake()
    {
        storeWeaponSlot = GetComponentsInChildren<StoreWeaponSlot>();

        weaponSalePanel = transform.GetComponent<Image>();

        Transform select = transform.GetChild(0).GetChild(0);
        exitButton = select.GetChild(5).GetComponent<Button>();
        exitButton.onClick.AddListener(() => WeaponSalePanelOnOff(false));
    }

    private void Start()
    {
        WeaponSalePanelOnOff(false);
    }

    public void WeaponSalePanelOnOff(bool flag)
    {
        if (flag)
        {
            for (int i = 0; i < storeWeaponSlot.Length; i++)
            {
                storeWeaponSlot[i].WeaponImage.sprite = GameManager.Inst.Weapons[i].WeaponModel.itemIcon;
                storeWeaponSlot[i].WeaponName.text = GameManager.Inst.Weapons[i].WeaponModel.itemName;
                storeWeaponSlot[i].WeaponPrice.text = GameManager.Inst.Weapons[i].WeaponModel.saleValue.ToString() + "G";
            }
        }

        weaponSalePanel.gameObject.SetActive(flag);        
    }
}
