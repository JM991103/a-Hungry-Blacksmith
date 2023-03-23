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
            if (enganceRange != value)
            {
                if (model.WeaponDatas.Length > enganceRange)
                {
                    enganceRange = value;
                    gameObject.name = $"{WeaponModel.name}";
                    if(weaponImage != null)
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
        
        if(enganceRange == int.MinValue)
        {
            Rest();
        }
    }

    public void Rest()
    {
        EnganceRange = 0;
        gameObject.name = $"{WeaponModel.name}";
    }

    public void Enhance()
    {
        if (model.WeaponDatas.Length - 1 > enganceRange)
        {
            // 최고 단계인지 검사
            if (MaterialInspection())
            {
                // 재료가 충분한지 검사
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
        }
        else
        {
            Debug.Log($"현재 최고 단계입니다.");
        }
    }

    void EnhanceSuccess()
    {
        EnganceRange++;
    }

    void EnhanceFail()
    {
        EnganceRange--;
    }

    void EnganceDestroy()
    {
        EnganceRange = 0;
    }

    bool MaterialInspection()
    {
        bool result = false;

        if (WeaponModel.costData.Length != 0)
        {
            // 강화 재료가 하나라도 필요하면

        }
        else
        {
            result = true; // 강화 재료가 필요가 없다.
        }

        return result;
    }
}
