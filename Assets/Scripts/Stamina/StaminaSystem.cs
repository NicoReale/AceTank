using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class StaminaSystem : MonoBehaviour
{
    [SerializeField] int maxStamina = 10;
    [SerializeField] float timeToRecharge = 10f;
    public int currStamina;

    DateTime nextStaminaTime;
    DateTime lastStaminaTime;

    bool recharging = false;

    [SerializeField] TextMeshProUGUI staminaText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] GameObject rechargeButton;

    [SerializeField] string notifTitle = "Full Ice";
    [SerializeField] string notifText = "Sera tu futuro";

    int id;

    TimeSpan timer;

    private void Start()
    {
        if(!PlayerPrefs.HasKey("currStamina"))
        {
            PlayerPrefs.SetInt("currStamina", maxStamina);
        }

        Load();
        StartCoroutine(RechargeStamina());

        if(currStamina < maxStamina)
        {
            timer = nextStaminaTime - DateTime.Now;
            id = NotificationsManager.Instance.DisplayNot(notifTitle, notifText, AddDuration(DateTime.Now, ((maxStamina - (currStamina ) + 1) * timeToRecharge) + 1 + (float)timer.TotalSeconds));
        }
    }

    public bool hasEnoughStamina(int stamina) => currStamina - stamina >= 0;

    public void FillStamina()
    {
        currStamina = maxStamina;
        nextStaminaTime = DateTime.Now;
        Save();
        UpdateStamina();
        UpdateTimer();
    }
    IEnumerator RechargeStamina()
    {
        recharging = true;
        UpdateTimer();

        while (currStamina < maxStamina)
        {
            DateTime currentTime=DateTime.Now;
            DateTime nextTime = nextStaminaTime;
            bool staminaAdd = false;

            while(currentTime > nextTime)
            {
                if (currStamina >= maxStamina) break;
                currStamina++;
                staminaAdd = true;
                UpdateStamina();

                DateTime timeToAdd = nextTime;
                if(lastStaminaTime > nextTime)
                    timeToAdd = lastStaminaTime;
                nextTime = AddDuration(timeToAdd, timeToRecharge);
            }

            if(staminaAdd)
            {
                nextStaminaTime = nextTime;
                lastStaminaTime= DateTime.Now;
            }

            UpdateTimer();
            UpdateStamina();
            Save();
            yield return new WaitForEndOfFrame();

        }
        NotificationsManager.Instance.cancelNot(id);
        recharging = false;
    }

    DateTime AddDuration(DateTime date, float duration)
    {
        return date.AddSeconds(duration);
    }

    public void UseStamina(int uses)
    {
        if(currStamina - uses >= 0)
        {
            AdsManager.Instance.LoadAd();
            currStamina -= uses;
            UpdateStamina();
            NotificationsManager.Instance.cancelNot(id);
            TimeSpan timer = nextStaminaTime - DateTime.Now;
            id = NotificationsManager.Instance.DisplayNot(notifTitle, notifText, AddDuration(DateTime.Now, ((maxStamina - (currStamina) + 1) * timeToRecharge) + 1 + (float)timer.TotalSeconds));
            if (!recharging)
            {
                nextStaminaTime = AddDuration(DateTime.Now, timeToRecharge);
                StartCoroutine(RechargeStamina());
            }

        }
    }

    public int getStamina()
    {
        return currStamina;
    }

    void UpdateTimer()
    {
        if(currStamina >= maxStamina)
        {
            timerText.SetText("Full!");
            if(rechargeButton != null)
                rechargeButton.SetActive(false);
            return;
        }
        //TimeSpan.Compare(TimeSpan.Parse(DateTime.Now.ToString()), TimeSpan.Parse(timeToRecharge.ToString()));
        TimeSpan timer = nextStaminaTime - DateTime.Now;
        if (rechargeButton != null)
            rechargeButton.SetActive(true);

        timerText.SetText($"{timer.Minutes.ToString("00")} : {timer.Seconds.ToString("00")}");
    }
    void UpdateStamina()
    {
        staminaText.SetText($"{currStamina}/{maxStamina}");
    }

    void Save()
    {
        PlayerPrefs.SetInt("currStamina", currStamina);
        PlayerPrefs.SetString("nextStaminaTime", nextStaminaTime.ToString());
        PlayerPrefs.SetString("lastStaminaTime", lastStaminaTime.ToString());
    }
    void Load()
    {
        currStamina = PlayerPrefs.GetInt("currStamina");
        nextStaminaTime = StringToDateTime(PlayerPrefs.GetString("nextStaminaTime"));
        lastStaminaTime = StringToDateTime(PlayerPrefs.GetString("lastStaminaTime"));
    }

    DateTime StringToDateTime(string dateString)
    {
        if (string.IsNullOrEmpty(dateString))
            return DateTime.Now;
        else return DateTime.Parse(dateString);
    }
}
