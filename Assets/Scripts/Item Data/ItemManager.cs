using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
[System.Serializable]
public class ItemManager
{
    [SerializeField] List<Item> items = new List<Item>(Resources.LoadAll("Items", typeof(Item)).Cast<Item>().ToArray());


    //Dic Item.id / Amount;
    Dictionary<int, int> m_Items = new Dictionary<int, int>();
    public Dictionary<int, int> Items { get { return m_Items; } }



    public void AddItem(int id)
    {
        if (!m_Items.ContainsKey(id))
            return;
        if (GameManager.instance.Currency >= getItem(id).cost)
        {
            Debug.Log($"Bough 1 of {GameManager.instance.p_ItemManager.getItem(id).name}");
            GameManager.instance.Currency-=getItem(id).cost;
            m_Items[id] += 1;
        }
    }

    public Item RemoveItem(int id)
    {
        if (!m_Items.ContainsKey(id))
            return null;
        if (m_Items[id] <= 0) return null;
        m_Items[id] -= 1;
        return getItem(id);
    }

    public Dictionary<int,int> ClearItems()
    {
       for(int i =0; i< m_Items.Count;i++)
       {
            if(m_Items[i] != 0)
            {
                m_Items[i] = 0;
                Debug.Log(m_Items[i]);
            }
       }
       return m_Items;
    }

    public int GetItemAmount(int id)
    {
        return m_Items[id];
    }

    public Item getItem(int id)
    {
        foreach (Item item in items)
            if (item.id == id)
                return item;
        return null;
    }

    void checkForItems()
    {
        foreach (var item in items)
        {
            if (m_Items.ContainsKey(item.id))
                continue;
            m_Items.Add(item.id, item.amount);
        }
    }
    public ItemManager(Dictionary<int, int> itemDictionary)
    {
        m_Items = itemDictionary;
        checkForItems();
    }
}
