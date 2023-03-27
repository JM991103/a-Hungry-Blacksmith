using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    UI ui;
    WeaponItemManager weaponManager;

    Weapon[] weapons;

    public UI UI => ui;
    public WeaponItemManager WeaponManager => weaponManager;
    public Weapon[] Weapons => weapons;

    protected override void Initialize()
    {
        base.Initialize();
        ui = FindObjectOfType<UI>();
        weaponManager = GetComponent<WeaponItemManager>();
        weapons = new Weapon[3];
        for (int i = 0; i < weapons.Length; i++)
        {
            GameObject obj = new GameObject();
            obj.transform.parent = transform;
            obj.name = $"Weapon({i})";
            obj.AddComponent<Weapon>();
            weapons[i] = obj.GetComponent<Weapon>();
        }
    }

    public void WeaponSave(Weapon waepon, int count)
    {
        if(weapons != null)
        {
            weapons[count].EnganceRange = waepon.EnganceRange;
        }
    }

    public void SaleWeapon(int index)
    {
        if (weapons.Length <= index)
        {
            Debug.Log("잘못된 인덱스");
        }
        else
        {
            // 판매
            // 판매시 골드 증가
            int gold = weapons[index].WeaponModel.saleValue;

            // 판매시 명성치 증가
            int fame = weapons[index].WeaponModel.fameValue;

            // 판매시 해당 무기 강화수치 0으로 만들기
            weapons[index].EnganceRange = 0;
        }
    }
}
