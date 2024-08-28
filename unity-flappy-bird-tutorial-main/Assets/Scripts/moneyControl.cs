using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moneyControl : MonoBehaviour
{
    void AddMoney(string key)
    {
        switch(key)
        {
            case "100":
                DataHolder.Doncoins += 1000000;
                break;
        }
        PlayerPrefs.SetInt("Dmanys", DataHolder.Doncoins);
    }
}
