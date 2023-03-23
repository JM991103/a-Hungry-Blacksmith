using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    UI ui;
    WeaponItemManager weaponManager;

    public UI UI => ui;
    public WeaponItemManager WeaponManager => weaponManager;

    protected override void Initialize()
    {
        base.Initialize();
        ui = FindObjectOfType<UI>();
        weaponManager = GetComponent<WeaponItemManager>();
    }
}
