using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemButton : MonoBehaviour
{
    public Item refItem;

    public TextMeshProUGUI itemName;
    public TextMeshProUGUI itemAmount;
    public TextMeshProUGUI itemPrice;

    public PopUp popUpHandler;

    PopUpWindow popUpWindow;

    private void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        itemName.SetText(refItem.name);
        itemAmount.SetText($"{GameManager.instance.p_ItemManager.GetItemAmount(refItem.id)}");
        itemPrice.SetText($"{GameManager.instance.p_ItemManager.getItem(refItem.id).cost}");
        MenuUIManager.instance.UpdateCurrency();
        GameManager.instance.SaveGame();
        PlayerPrefs.Save();
    }

    public void UsePotion()
    {
        var item = GameManager.instance.p_ItemManager.RemoveItem(refItem.id);
        if (item == null) return;
        if(GameManager.instance.p_ItemManager.GetItemAmount(item.id) >= 0)
        {
            if(refItem.id == 0)
            {
                PlayerPrefs.SetFloat("SpeedMult", PlayerPrefs.GetInt("SpeedMult") + 5);
                //GameManager.instance.playerStats[0] += 5f;
            }
            else if (refItem.id == 1)
            {
                PlayerPrefs.SetFloat("DamageMult", PlayerPrefs.GetInt("DamageMult") + 5);
                //GameManager.instance.playerStats[1] += 0.5f;
            }
        }
        UpdateUI();
    }
    public void CallAddPotion()
    {
        popUpWindow = popUpHandler.ShowPopUp();     
        popUpWindow.OnMethodCall += AddPotion;

    }
    public void AddPotion()
    {
        GameManager.instance.p_ItemManager.AddItem(refItem.id);
        UpdateUI();
    }

    private void OnDisable()
    {
        if(popUpWindow != null)
            popUpWindow.OnMethodCall -= AddPotion;
    }

}
