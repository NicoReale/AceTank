using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
[Serializable]
public class SaveData
{
    public int Currency;
    public int totalCurr;
    public int shots;
    public int levelsUnlocked;

    public List<int> items;
    public List<int> amount;

    public void ConvertItemDicToList(Dictionary<int,int> dictionary)
    {
        items = dictionary.Keys.ToList();
        amount = dictionary.Values.ToList();
    }

    public Dictionary<int,int> ConvertItemsToDic()
    {
        var dic = new Dictionary<int, int>();
        for (int i = 0; i < items.Count; i++)
        {
                if (dic.ContainsKey(items[i])) continue;
                dic.Add(items[i], amount[i]);
        }
        return dic;
    }
}
