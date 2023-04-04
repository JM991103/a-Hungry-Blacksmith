using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData : MonoBehaviour
{
    public SaveData(int _day, int _gold, int _fame)
    {
        day = _day;
        gold = _gold;
        fame = _fame;
    }

    public int day;
    public int gold;
    public int fame;
}
