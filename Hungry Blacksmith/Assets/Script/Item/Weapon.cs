using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    int enganceRange = int.MinValue;       // 강화 수치
    Image weaponImage;


    TotalWeapon model;
    WeaponData WeaponModel => model.WeaponDatas[EnganceRange];

    public int EnganceRange
    {
        get => enganceRange;
        set
        {
            if(enganceRange!= value)
            {
                if (model.WeaponDatas.Length > enganceRange)
                {
                    enganceRange = value;
                    gameObject.name = $"{WeaponModel.name}";
                    weaponImage.sprite = WeaponModel.itemIcon;
                }
            }
        }
    }

    private void Awake()
    {
        weaponImage = GetComponent<Image>();
    }

    private void Start()
    {
        model = GameManager.Inst.WeaponManager.weaponItems[0];
        Rest();
    }

    public void Rest()
    {
        EnganceRange = 0;
        gameObject.name = $"{WeaponModel.name}";
    }

    public void Enhance()
    {
        float rand = UnityEngine.Random.Range(0.0f, 1.0f);
        if (WeaponModel.enforceRatio > rand)
        {
            // 강화 성공
            EnhanceSuccess();
            Debug.Log($"강화 성공! 현재 강화 단계 : {EnganceRange}");
        }
        else if (!(WeaponModel.destroy <= 0.0f) && WeaponModel.DestroyRatio > rand)
        {
            // 파괴
            EnganceDestroy();
            Debug.Log($"무기가 파괴 되어 0강으로 돌아갑니다. 현재 강화 단계 : {EnganceRange}");
        }
        else
        {
            // 강화 실패 (하락)
            if (EnganceRange != 0)
            {
                EnhanceFail();
                Debug.Log($"강화 실패! 현재 강화 단계 : {EnganceRange}"); 
            }
            else
            {
                Debug.Log($"최하 단계이므로 하락 하지않습니다. : {enganceRange}");
            }
        }
    }

    void EnhanceSuccess()
    {
        if(model.WeaponDatas.Length > EnganceRange)
        {
            EnganceRange++;
        }
        else
        {
            Debug.Log($"현재 최고 단계입니다.");
        }
    }

    void EnhanceFail()
    {
        EnganceRange--;
    }

    void EnganceDestroy()
    {
        EnganceRange = 0;
    }
}
