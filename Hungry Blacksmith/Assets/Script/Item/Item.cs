using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("둘중의 하나만 선택")]
    public ItemData item;
    public WeaponData weapon;

    SpriteRenderer itemIcon;

    private void Awake()
    {
        itemIcon = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (item != null)
        {
            itemIcon.sprite = item.itemIcon;
        }
        else
        {
            itemIcon.sprite = weapon.itemIcon;
        }
    }
}
