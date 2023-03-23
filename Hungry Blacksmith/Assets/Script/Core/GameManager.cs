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
}
