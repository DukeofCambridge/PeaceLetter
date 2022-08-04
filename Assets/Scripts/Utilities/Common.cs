using System;
using UnityEngine;
public enum MessengerState
{
    IDLE = 0,
    TASK = 1,
};

public enum EffectValueType
{
    NULL = 0,
    PERCENTAGE = 1,
    ABSOLUTE = 2,
};
//特质类型
public enum EffectType
{
    UPGRADE_EXP = 0,
    INITIAL_TRUST = 1,
    INITIAL_SOCIAL = 2,
    INITIAL_KNOWLEDGE = 3,
    EXTRA_POINT = 4,
    TASK_TIME = 5,
    SALE_INCOME = 6,
    MOVE_SPEED = 7,
    TASK_INCOME = 8,
    TRUST = 9,
    FATIGUE_RECVER_SPEED = 10,
    DAILT_TRUST = 11,
    TASK_EXP = 12,
    TASK_FATIGUE = 13,
    LOAD = 14,
    TASK_TRUST = 15,
    TASK_INCOME_WITH_KNOWLEDGE = 16,
    FATIGUE_RECOVER_SPEED_WITH_ENERGY = 17,
    LOAD_WITH_SOCIAL = 18
}

public enum BuffType
{
    FOREVER = 0,
    TEMPORARY = 1,
}
//信使属性
public enum PointType
{
    ENERGY = 0,
    KNOWLEDGE = 1,
    SOCIAL = 2,
    TRUST = 3,
    FATIGUE = 4
};

public enum RecipeType
{
    SIMPLE = 0,
    FESTIVAL = 1,
}

public enum SoundName
{
    none, FootStepSoft, FootStepHard,
    Axe, Pickaxe, Hoe, Reap, Water, Basket,
    Pickup, Plant, TreeFalling, Rustle,
    AmbientCountryside1, AmbientCountryside2, MusicCalm1, MusicCalm2, MusicCalm3, MusicCalm4, MusicCalm5, MusicCalm6, AmbientIndoor1
}
//任务词条
public enum EntryType
{
    Speed,Fatigue,NoGoods,TimeLimit,EnergyLimit,KnowLimit,SocialLimit,Event,Income,Exp,DefaultGoods,Special
}
public enum LightShift
{
    Morning, Evening, Night
}

public static class Common
{
    public static string MessengerNumCfgPath = Application.dataPath + "/Json Data/MessengerCfg/messengerNumCfg.json";
    
    public static string BuffCfgPath = Application.dataPath + "/Json Data/MessengerCfg/BuffCfg.json";

    public static string MessengerLevelCfgPath = Application.dataPath + "/Json Data/MessengerCfg/messengerLevelCfg.json";

    public static string RecipeCfgPath = Application.dataPath + "/Json Data/CookingCfg/RecipeCfg.json";
    // 城市信息
    public static string statePath = Application.dataPath + "/Json Data/stateInfo.json";
    // 任务库
    public static string libraryPath = Application.dataPath + "/Json Data/taskLibrary.json";
    // 玩家任务
    public static string taskPath = Application.dataPath + "/Json Data/tasks.json";
    // 词条库
    public static string entryPath = Application.dataPath + "/Json Data/entry.json";

    public static string presPath = Application.dataPath + "/Json Data/prestigeCfg.json";
    // 灯光
    public static TimeSpan morningTime = new TimeSpan(6, 0, 0);
    public static TimeSpan evening = new TimeSpan(17, 0, 0);
    public static TimeSpan nightTime = new TimeSpan(19, 0, 0);
    public const float lightChangeDuration = 45f;
}