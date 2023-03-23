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
    }

    public void WeaponSave(Weapon waepon, int count)
    {
        if(weapons != null)
        {
            weapons[count] = waepon;
        }
    }
}
