using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreWeaponSlot : MonoBehaviour, IPointerClickHandler
{
    Image weaponImage;
    TextMeshProUGUI weaponName;
    TextMeshProUGUI weaponPrice;

    public Image WeaponImage => weaponImage;
    public TextMeshProUGUI WeaponName => weaponName;
    public TextMeshProUGUI WeaponPrice => weaponPrice;

    private void Awake()
    {
        weaponImage = transform.GetChild(0).GetComponent<Image>();
        weaponName = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        weaponPrice = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"{gameObject.name}를 선택했습니다");        
    }
}
