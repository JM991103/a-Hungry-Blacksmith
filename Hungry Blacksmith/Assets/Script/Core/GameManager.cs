using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    UI ui;

    public UI UI => ui;

    protected override void Initialize()
    {
        base.Initialize();
        ui = FindObjectOfType<UI>();
    }
}
