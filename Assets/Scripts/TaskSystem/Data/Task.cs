[System.Serializable]
public class Task
{
    public int taskID;      
    public string taskName;  
    public string taskDesc; 
    public int targetID;
    public int type;  // 0主线任务，1NPC任务，2日常任务
    public int level; // 难度
    public int completed; //0不可用 1待分配 2进行中 3已完成
    public Entry[] entries; // 任务词条
    public string taskSeason; //什么时间段的任务
    public string sender;  //发信人
    public string receiver; //收信人
    public string address; //寄信人地址（暂定）
    public int Buff; //完成给予信使特质
}

public class TaskInfo
{
    public string address;
    public string receiver;
    public string sender;
    public string taskDesc;
    public string time;
}
