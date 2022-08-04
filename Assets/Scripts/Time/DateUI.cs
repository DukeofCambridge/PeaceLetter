using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DateUI : MonoBehaviour
{
    private DateManager dm;
    public RectTransform dayNightImage;
    public RectTransform clockParent;
    public Image seasonImage;
    public TextMeshProUGUI dateText;
    public TextMeshProUGUI timeText;

    public Sprite[] seasonSprites;

    private List<GameObject> clockBlocks = new List<GameObject>();

    private void Start()
    {
        for (int i = 0; i < clockParent.childCount; i++)
        {
            clockBlocks.Add(clockParent.GetChild(i).gameObject);
            clockParent.GetChild(i).gameObject.SetActive(false);
        }
        dm = GameObject.Find("DateManager").GetComponent<DateManager>();
    }
    
    private void OnEnable()
    {
        EventHandler.onIncrementHour += OnGameHourEvent;
        EventHandler.onIncrementDay += OnGameDateEvent;
    }

    private void OnDisable()
    {
        EventHandler.onIncrementHour -= OnGameHourEvent;
        EventHandler.onIncrementDay -= OnGameDateEvent;
    }
    /// <summary>
    /// 每小时刷新UI
    /// </summary>
    /// <param name="hour"></param>
    private void OnGameHourEvent(int hour)
    {
        //Debug.Log("当前时间为"+ hour);
        timeText.text = dm.date.hours[hour];
        SwitchHourImage(hour);
        DayNightImageRotate(hour);
    }
    /// <summary>
    /// 每天刷新UI
    /// </summary>
    /// <param name="day"></param>
    /// <param name="month"></param>
    private void OnGameDateEvent(int day, int month)
    {
        string period = dm.date.months[month];
        int p = day / 10;
        switch (p)
        {
            case 0: period += "上旬";break;
            case 1: period += "中旬";break;
            case 2: period += "下旬";break;
        }
        int season = month / 3;

        dateText.text = "永贞元年" + " " + period;
        //设置季节图标
        seasonImage.sprite = seasonSprites[season];
    }

    /// <summary>
    /// 根据小时切换时间块显示
    /// </summary>
    /// <param name="hour"></param>
    private void SwitchHourImage(int hour)
    {
        int index = hour / 4;

        if (index == 0)
        {
            foreach (var item in clockBlocks)
            {
                item.SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < clockBlocks.Count; i++)
            {
                if (i < index + 1)
                    clockBlocks[i].SetActive(true);
                else
                    clockBlocks[i].SetActive(false);
            }
        }
    }
    /// <summary>
    /// 昼夜轮盘旋转
    /// </summary>
    /// <param name="hour"></param>
    private void DayNightImageRotate(int hour)
    {
        var target = new Vector3(0, 0, hour * 15 - 90);
        dayNightImage.DORotate(target, 1f, RotateMode.Fast);
    }
}
