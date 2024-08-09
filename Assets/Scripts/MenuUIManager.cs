using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuUIManager : MonoBehaviour
{
    public GameObject infoButton;
    public GameObject info;
    public GameObject items;
    public GameObject volume;
    public GameObject levels;

    public TextMeshProUGUI shotsCounter;
    public TextMeshProUGUI totalCurrencyCounter;
    public TextMeshProUGUI itemCurrCounter;

    public static MenuUIManager instance;

    public Button[] buttons = new Button[3];

    public PopUp popUpHandler;

    PopUpWindow popUpWindow;

    private void Awake()
    {
        if (instance == null)
            instance = this;        
    }
    //usa los botones del arreglo y se fija si los puede habilitar
    public void CheckLevels()
    {
        EnableLevels(buttons, GameManager.instance.levelsFlags);
    }
    //habilita los botones en base al numero de flags, 0 = level 1...
    public void EnableLevels(Button[] button, int index)
    {
        for (int i = 0; i <= Mathf.Min(index, buttons.Length - 1); i++)
        {
            if(button[i] != null)
            {
                button[i].interactable = true;
            }
            else continue;
        }
    }

    public void CloseSettings()
    {
        info.SetActive(false);
        infoButton.SetActive(true);
    }

    public void OpenSettings()
    {
        info.SetActive(true);
        infoButton.SetActive(false);
        UpdateInfo();
    }
    public void OpenItems()
    {
        items.SetActive(true);
        infoButton.SetActive(false);
        itemCurrCounter.SetText($"Currency: {GameManager.instance.Currency}");
    }

    public void CloseItems()
    {
        items.SetActive(false);
        infoButton.SetActive(true);
    }

    public void OpenVolume()
    {
        volume.SetActive(true);
    }

    public void CloseVolume()
    {
        volume.SetActive(false);
    }

    public void CallEraseData()
    {
        popUpWindow = popUpHandler.ShowPopUp();
        popUpWindow.OnMethodCall += EraseData;
    }

    public void EraseData()
    {
        GameManager.instance.EraseData();
        UpdateInfo();
        popUpWindow.OnMethodCall -= EraseData;
    }

    public void CloseLevelSelect()
    {
        levels.SetActive(false);
    }

    public void OpenLevelSelect()
    {
        levels.SetActive(true);
    }

    void UpdateInfo()
    {
        shotsCounter.SetText($"Total Shots: {GameManager.instance.shots}");
        totalCurrencyCounter.SetText($"Total Currency: {GameManager.instance.totalCurrency}");
    }

    public void UpdateCurrency()
    {
        itemCurrCounter.SetText($"Currency: {GameManager.instance.Currency}");
    }

}
