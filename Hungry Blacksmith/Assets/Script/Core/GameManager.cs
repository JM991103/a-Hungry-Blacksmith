using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    WeaponItemManager weaponManager;

    public WeaponItemManager WeaponManager => weaponManager;

    protected override void Initialize()
    {
        base.Initialize();
        weaponManager = GetComponent<WeaponItemManager>();
    }
}
