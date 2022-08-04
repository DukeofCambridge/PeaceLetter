using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pay attention to the names of attribute should start with an UpperCase(keep same with json file's key)
//otherwise it cannot load config file correctly

[System.Serializable]
public class GameSaveData
{
    /// <summary>
    /// 游戏存档要持久化的数据
    /// </summary>
    //public string DataSceneName; 
    public int PrestigeLevel;   //声望等级
    public int PrestigeExp;     //声望经验
    public int Wealth;
    public List<Messenger> MessengerInfo = new List<Messenger>();             //信使系统信息
    public List<ObjectEntity> ObjectInfo = new List<ObjectEntity>();          //背包系统信息
    public List<Task> tasks=new List<Task>();                //任务系统信息
    public List<bool> RecipePossessList = new List<bool>();
    public List<int> RecipeTimesList = new List<int>();
    public int CurMonth;        //月
    public int CurDay;          //日
    public int CurHour;         //时
    public int CurQuarterHour;  //刻

    public GameSaveData(){}
}

public class ObjectEntity
{

}

public class TaskEntity
{
    public int taskID;
    public int completed; //0不可用 1待分配 2已分配 3已完成
}