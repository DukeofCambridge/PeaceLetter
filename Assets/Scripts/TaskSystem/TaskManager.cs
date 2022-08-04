using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

public class TaskManager : Singleton<TaskManager>
{
    private GameSaveData data;
    protected override void Awake()
    {
    }
    private void OnEnable()
    {
        EventHandler.onIncrementDay += RefreshTask;
    }
    private void OnDisable()
    {
        EventHandler.onIncrementDay -= RefreshTask;
    }
    private void Start()
    {
        data = GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>().gameSaveData;
    }

    /// <summary>
    /// 按完成情况查询任务
    /// </summary>
    /// <returns></returns>
    public List<Task> GetTasks(int completed)
    {
        List<Task> res = new List<Task>();
        foreach (Task task in data.tasks)
        {
            if (task.completed == completed)
            {
                res.Add(task);
            }
        }
        return res;
    }

    /// <summary>
    /// 按任务类型查询
    /// </summary>
    /// <returns></returns>
    public List<Task> GetCurrentTasks(int type)
    {
        List<Task> res = new List<Task>();
        if (type == -1)
        {
            return data.tasks;
        }
        foreach (Task task in data.tasks)
        {
            if (task.type == type)
            {
                res.Add(task);
            }
        }
        return res;
    }
    /// <summary>
    /// 获取委托栏任务（待派遣，类型为日常）
    /// </summary>
    /// <returns></returns>
    public List<Task> GetBoard()
    {
        List<Task> res = new List<Task>();
        foreach (Task task in data.tasks)
        {
            if (task.type == 2&&task.completed==1)
            {
                res.Add(task);
            }
        }
        return res;
    }
    public Task GetById(int id)
    {
        foreach(Task t in data.tasks)
        {
            if (t.taskID == id)
            {
                //Debug.Log("找到了"+id);
                return t;
            }

        }
        return null;
    }
    /// <summary>
    /// 查询某个城市信息
    /// </summary>
    /// <param name="stateId"></param>
    /// <returns></returns>
    public State GetStateInfo(int stateId)
    {
        string JsonStr = File.ReadAllText(Common.statePath);
        State[] states = JsonConvert.DeserializeObject<State[]>(JsonStr);
        //Debug.Log(taskLib[1].name);
        return states[stateId-1];
    }
    /// <summary>
    /// 查询某个任务详细信息
    /// </summary>
    /// <param name="taskId"></param>
    /// <returns></returns>
    public Task GetTaskById(int taskId)
    {
        string JsonStr = File.ReadAllText(Common.libraryPath);
        Task[] taskLib = JsonConvert.DeserializeObject<Task[]>(JsonStr);
        return taskLib[taskId-1];
    }
    /*
    /// <summary>
    /// 根据等级划分特质
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public int[] GetBuffByLevel(int level)
    {
        string JsonStr = File.ReadAllText(Common.libraryPath);
        Task[] taskLib = JsonConvert.DeserializeObject<Task[]>(JsonStr);
    }*/
    /// <summary>
    /// 生成新任务，每日调用
    /// </summary>
    /// <returns></returns>
    public void Generate()
    {
        PrestigeSetting ps = GameObject.Find("PrestigeManager").GetComponent<PrestigeManager>().GetPrescfg();
        int len = GetCurrentTasks(-1).Count;
        //Debug.Log("任务数:" + len);
        List<Task> board = GetBoard();
        //生成一个新任务
        Task newT = new Task();
        newT.taskID = len + 1;
        string JsonStr = File.ReadAllText(Common.libraryPath);
        TaskInfo[] taskInfos = JsonConvert.DeserializeObject<TaskInfo[]>(JsonStr);
        int num = Random.Range(0, taskInfos.Length);
        TaskInfo taskInfo = taskInfos[num];
        newT.address = taskInfo.address;
        newT.receiver = taskInfo.receiver;
        newT.sender = taskInfo.sender;
        newT.taskDesc = taskInfo.taskDesc;
        newT.taskSeason = taskInfo.time;
        newT.taskName = newT.sender + "的委托";
        newT.type = 2;
        newT.completed = 1;
        Entry[] es = GameObject.Find("PrestigeManager").GetComponent<PrestigeManager>().entries;
        if (es.Length==0)
        {
            newT.entries = new Entry[0];
        }
        else
        {
            Entry e = es[Random.Range(0, es.Length)];
            newT.entries[0] = e;
        }
        //这里hard-code，因为还没有Buff的数据结构
        int buffID = Random.Range(1, 49);
        newT.Buff = buffID;
        if (board.Count < ps.maxTask)
        {

            data.tasks.Add(newT);
        }
        else
        {
            //顶掉最旧的未派遣的任务
            int id = board[0].taskID;
            for(int i=0;i<data.tasks.Count;i++)
            {
                if (data.tasks[i].taskID == id)
                {
                    data.tasks.RemoveAt(i);
                    break;
                }

            }
            data.tasks.Add(newT);
        }
    }
    /// <summary>
    /// 每天刷新委托栏任务(参数无用)
    /// </summary>
    public void RefreshTask(int a,int b)
    {
        int daily = GameObject.Find("PrestigeManager").GetComponent<PrestigeManager>().GetPrescfg().newTask;
        int i = 0;
        while (i++ < daily)
        {
            Generate();
        }
        GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>().SaveToLocal(1);
    }
    /// <summary>
    /// 派遣信使按钮
    /// </summary>
    /// <param name="messengerId"></param>
    /// <param name="taskId"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public string Dispatch(int messengerId, int taskId, int[] itemId)
    {
        Task taskInfo = GetTaskById(taskId);
        Entry[] entries = taskInfo.entries;
        MessengerBase messenger = data.MessengerInfo[messengerId-1];
        if (messenger.State == MessengerState.TASK)
        {
            return "信使已经被派遣";
        }
        State state = GetStateInfo(taskInfo.targetID);
        //预计消耗疲劳度
        int fat = state.fatigue;
        //信使个人速度
        MessengerDecorator md = new MessengerDecorator(messenger.BuffIdList[0]);
        double speed = md.CalculateSpeed();
        int spend = (int)(state.distance * 2 / speed);
        //Debug.Log("spend:"+spend);
        //检查任务词条
        foreach (Entry entry in entries)
        {
            switch (entry.type)
            {
                //先检查信使能力值是否符合要求
                case EntryType.EnergyLimit:
                    if (messenger.Energy < entry.value)
                    {
                        return "信使体力不足";
                    };break;
                case EntryType.KnowLimit:
                    if(messenger.Knowledge < entry.value)
                    {
                        return "信使学识不足";
                    }; break;
                case EntryType.SocialLimit:
                    if (messenger.Social < entry.value)
                    {
                        return "信使社交能力不足";
                    }; break;
                case EntryType.Speed: speed *= entry.value;break;
                case EntryType.Fatigue: fat += (int)entry.value;break;
                case EntryType.Special: if (messengerId != 1) return "该任务只能由兰晏执行";break;
            }
            //经查阅资料反射方法对性能有影响，因为我们的程序对灵活性要求并没有那么高，所以不用了
            /*
            if (entry.type == EntryType.AttrLimit)
            {
                //利用反射机制获取指定成员变量的值
                PropertyInfo propertyInfo = messenger.GetType().GetProperty(entry.effect);
                if (Convert.ToSingle(propertyInfo.GetValue(messenger)) < entry.value)
                {
                    return 2;//信使不满足条件
                }
            }*/

        }


        if (messenger.Fatigue < fat)
        {
            return "信使有些累了，暂时走不了那么远";
        }

        // 信使状态
        messenger.State = MessengerState.TASK;
        // 好感度
        // 存档
        GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>().SaveToLocal(1);
        return "派遣成功";
    }
    /// <summary>
    /// 任务结算
    /// </summary>
    /// <param name="messengerId"></param>
    /// <param name="taskId"></param>
    /// <param name="itemId"></param>
    public void complete(int messengerId, int taskId, int[] itemId)
    {
        // 计算基础金钱、声望收入
        Task taskInfo = GetTaskById(taskId);
        State state = GetStateInfo(taskInfo.targetID);
        int income = state.income;
        int prestigeGain = state.prestige;
        // 收入声望补正
        PrestigeSetting prescfg = GameObject.Find("PrestigeManager").GetComponent<PrestigeManager>().GetPrescfg();
        income = (int)(income*prescfg.incomeFactor);
        //信使收入加成
        MessengerBase messenger = data.MessengerInfo[messengerId - 1];
        MessengerDecorator md = new MessengerDecorator(messenger.BuffIdList[0]);
        income = md.CalculateTaskIncome(income);
        //未修正消耗疲劳度
        int fat = state.fatigue;
        Entry[] entries = taskInfo.entries;
        //检查任务词条
        foreach (Entry entry in entries)
        {
            switch (entry.type)
            {
                case EntryType.Fatigue: fat += (int)entry.value; break;
                case EntryType.Income: income = (int)(income * entry.value); break;
                case EntryType.Exp: prestigeGain = (int)(prestigeGain * entry.value); break;
            }
        }
        //计算商品倒卖收益

        //金钱声望结算
        data.Wealth+=income;
        GameObject.Find("PrestigeManager").GetComponent<PrestigeManager>().GainExp(prestigeGain);
        //信使经验
        GameObject.Find("MessengerManager").GetComponent<MessengerManager>().SetExpAndUpgrade(messengerId, (int)(data.PrestigeLevel* state.distance));
        //赋予信使特质
        messenger.BuffIdList.Add(GetTaskById(taskId).Buff);
        //完成主线任务解锁下一任务
    }
}

