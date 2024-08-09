using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;
using System;

public class NotificationsManager : MonoBehaviour 
{
    public static NotificationsManager Instance { get; private set; }
    AndroidNotificationChannel notificationChannel;
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        AndroidNotificationCenter.CancelAllDisplayedNotifications();
        AndroidNotificationCenter.CancelAllScheduledNotifications();

        notificationChannel = new AndroidNotificationChannel()
        {
            Id = "n_reminder_ch",
            Name = "Notification Reminder",
            Description = "Reminders Channel",
            Importance = Importance.High           
        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        DisplayNot("Freedom awaits", "Come back :)", DateTime.Now.AddHours(36));
    }

    public int DisplayNot(string title, string text, DateTime fireTime)
    {
        var notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.FireTime = fireTime;

        return AndroidNotificationCenter.SendNotification(notification, notificationChannel.Id);
    }

    public void cancelNot(int id)
    {
        AndroidNotificationCenter.CancelDisplayedNotification(id);
    }

}
