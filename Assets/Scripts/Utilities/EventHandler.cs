using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// 事件中心使用观察者模式，一些脚本中触发相应的事件，所有注册该事件的脚本会执行相应的方法
public static class EventHandler
{
    // 声望等级提升
    public static event UnityAction EnhancePrestigeEvent;
    public static void CallEnhancePrestigeEvent()
    {
        EnhancePrestigeEvent?.Invoke();
    }
    // 灯光
    public static event UnityAction<LightShift,float> LightShiftChangeEvent;
    public static void CallLightShiftChangeEvent(LightShift lightShift, float timeDifference)
    {
        LightShiftChangeEvent?.Invoke(lightShift, timeDifference);
    }

    // 切换场景
    public static event UnityAction BeforeSceneUnloadEvent;
    public static void CallBeforeSceneUnloadEvent()
    {
        BeforeSceneUnloadEvent?.Invoke();
    }

    public static event UnityAction AfterSceneLoadedEvent;
    public static void CallAfterSceneLoadedEvent()
    {
        AfterSceneLoadedEvent?.Invoke();
    }
    

    public static UnityAction onIncrementQuaterHour;
    public static UnityAction<int> onIncrementHour;
    public static UnityAction<int, int> onIncrementDay;
    public static UnityAction<int> onIncrementMonth;
}
