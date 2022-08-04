using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : Singleton<DateManager>
{
    // 最基本单位是一刻种，加上古今差异补正
    // 1s in real world = 28.8s in game
    private GameSaveData data;
    public const float threshold = 30f;
    public Date date;
    //控制游戏暂停
    public bool gameClockPause;
    //灯光时间差
    private float timeDifference;
    public TimeSpan GameTime => new TimeSpan(date.currentHour, date.min, 0);
    private void Start()
    {
        //Coroutine timer = StartCoroutine(Tick(TICK_INTERVAL));
        data = GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>().gameSaveData;
        //读档
        date = new MainDate(data.CurQuarterHour,data.CurHour,data.CurDay,data.CurMonth);
        gameClockPause = false;
        //开局自增时间从而刷新UI
        //date.IncrementHour();
    }
    private void Update()
    {
        if (!gameClockPause)
        {
            date.tick += Time.deltaTime;
            /*if (date.tick >= threshold)
            {
                date.tick -= threshold;
                date.IncrementQuarterHour();
            }*/
            if (date.tick >= 2f)
            {
                date.tick -= 2f;
                date.IncrementMinute();
                EventHandler.CallLightShiftChangeEvent(GetCurrentLightShift(), timeDifference);
            }
        }
        //按T为小山加速
        if (Input.GetKeyDown(KeyCode.T))
        {
            date.IncrementHour();
        }
    }
    /// <summary>
    /// 返回lightshift同时计算时间差
    /// </summary>
    /// <returns></returns>
    private LightShift GetCurrentLightShift()
    {
        if (GameTime >= Common.morningTime && GameTime < Common.evening)
        {
            timeDifference = (float)(GameTime - Common.morningTime).TotalMinutes;
            return LightShift.Morning;
        }
        if(GameTime >= Common.evening && GameTime < Common.nightTime)
        {
            timeDifference = (float)(GameTime - Common.evening).TotalMinutes;
            return LightShift.Evening;
        }
        if (GameTime < Common.morningTime || GameTime >= Common.nightTime)
        {
            timeDifference = Mathf.Abs((float)(GameTime - Common.nightTime).TotalMinutes);
            // Debug.Log(timeDifference);
            return LightShift.Night;
        }

        return LightShift.Morning;
    }

    /*
    private IEnumerator Tick(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            date.IncrementQuarterHour();
        }
    }*/
}

public class Date
{
    public float tick;
    public int currentQuarterHour;
    public int min;
    public int currentHour;
    public int currentDay;
    public int currentMonth;

    public Date()
    {
        tick = 0;
        min = 0;
        currentQuarterHour = 0;
        //初始时间为8点（辰正）这里-1
        currentHour = 7;
        currentDay = 0;
        currentMonth = 0;
    }
    
    public Date(string name)
    {
        // at this point, s can be month or something else
        string s = name.Substring(0, 2);
        for (int i = 0; i < months.Length; i++)
        {
            string monthName = months[i];
            if (monthName.Equals(s))
            {
                name = name.Substring(2);
                s = name.Substring(0, 2);
            }
        }
    }

    //载入存档时间
    public Date(int currentQuarterHour,int currentHour,int currentDay,int currentMonth)
    {
        tick = 0;
        min = 0;
        this.currentQuarterHour = currentQuarterHour;
        this.currentHour = currentHour;
        this.currentDay = currentDay;
        this.currentMonth = currentMonth;
    }
    public virtual void IncrementMinute()
    {
        min += 1;
        if (min == 60)
        {
            min = 0;
            IncrementHour();
        }
    }
    public virtual void IncrementQuarterHour()
    {
        currentQuarterHour += 1;
        if (currentQuarterHour == 4)
        {
            currentQuarterHour = 0;
            IncrementHour();
        }
    }

    public virtual void IncrementHour()
    {
        currentHour += 1;
        if (currentHour == 24)
        {
            currentHour = 0;
            IncrementDay();
        }
    }

    public virtual void IncrementDay()
    {
        currentDay += 1;
        if (currentDay == 30)
        {
            currentDay = 0;
            IncrementMonth();
        }
    }

    public virtual void IncrementMonth()
    {
        currentMonth += 1;
        if (currentMonth == 12)
        {
            currentMonth = 0;
        }
    }
    
    public readonly string[] quarterHours = new string[]
    {
        "", "一刻", "二刻", "三刻", "四刻", "五刻", "六刻", "七刻"
    };
    
    public readonly string[] hours = new string[]
    {
        "子正", "丑初", "丑正", "寅初", "寅正", "卯初",
        "卯正", "辰初", "辰正", "巳初", "巳正", "午初",
        "午正", "未初", "未正", "申初", "申正", "酉初",
        "酉正", "戌初", "戌正", "亥初", "亥正", "子初"
    };

    public readonly string[] days = new string[]
    {
        "上旬", "中旬", "下旬" 
    };

    public readonly string[] months = new string[]
    {
        "孟春", "仲春", "季春",
        "孟夏", "仲夏", "季夏",
        "孟秋", "仲秋", "季秋",
        "孟冬", "仲冬", "季冬"
    };

    public override string ToString()
    {
        return months[currentMonth] + days[currentDay] + " " + hours[currentHour] + quarterHours[currentQuarterHour];
    }
}

public class MainDate : Date
{
    public MainDate(int currentQuarterHour, int currentHour, int currentDay, int currentMonth) : base(currentQuarterHour, currentHour, currentDay, currentMonth) { }
    public override void IncrementQuarterHour()
    {
        base.IncrementQuarterHour();
        EventHandler.onIncrementQuaterHour?.Invoke();
    }

    public override void IncrementHour()
    {
        base.IncrementHour();
        EventHandler.onIncrementHour?.Invoke(currentHour);
    }

    public override void IncrementDay()
    {
        base.IncrementDay();
        EventHandler.onIncrementDay?.Invoke(currentDay, currentMonth);
    }

    public override void IncrementMonth()
    {
        base.IncrementMonth();
        EventHandler.onIncrementMonth?.Invoke(currentMonth);

    }
}
