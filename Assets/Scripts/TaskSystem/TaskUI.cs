using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    [Header("任务面板UI")]
    [SerializeField] private GameObject taskUI;
    private bool Opened;
    private TaskManager tm;
    private GameSaveData data;
    private List<Task> tasks;
    private List<Messenger> ms;
    private int currentMes=0;
    private int currentPanel = 1;
    private int currentTask = 0;
    public GameObject[] panels;
    public GameObject[] taskList;
    public Sprite[] buttonShow;
    public Sprite[] mes;
    public GameObject[] mesList;


    private void Start()
    {
        Opened = false;
        tm = GameObject.Find("TaskManager").GetComponent<TaskManager>();
        data = GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>().gameSaveData;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            AlterTask();
        }
    }

    /// <summary>
    /// 打开关闭任务UI，Button调用事件
    /// </summary>
    public void AlterTask()
    {
        Opened = !Opened;
        taskUI.SetActive(Opened);
        //GameObject.Find("Mask").SetActive(Opened);
        if (Opened)
        {
            openUI();
        }
    }
    /// <summary>
    /// 打开任务面板默认数据
    /// </summary>
    public void openUI()
    {
        GameObject.Find("level").GetComponent<Text>().text = "雁社声望等级  " + data.PrestigeLevel;
        SwitchPanel(1);
    }

    /// <summary>
    /// 切换面板（根据任务完成状态）
    /// </summary>
    /// <param name="index"></param>
    public void SwitchPanel(int index)
    {
        currentPanel = index;
        panels[0].transform.SetAsLastSibling();
        for (int i = 0; i < panels.Length; i++)
        {
            if (i == index)
            {
                panels[i].transform.SetAsLastSibling();
            }
        }
        GetList(index);
        //Debug.Log("任务数：" + tasks.Count);
        if (tasks.Count != 0)
        {
            LoadTask(0);
        }
        //else{ClearTask();}

    }
    /// <summary>
    /// 获取任务列表
    /// </summary>
    /// <param name="status"></param>
    public void GetList(int status)
    {
        tasks = tm.GetTasks(status);
        for (int i = 0; i < tasks.Count; i++)
        {
            string type = "";
            if (tasks[i].type == 0)
            {
                type = "主线";
            } else if (tasks[i].type == 2)
            {
                type = "日常";
            }
            taskList[i].SetActive(true);
            taskList[i].GetComponent<Button>().GetComponentInChildren<Text>().text = "<color='brown'>" + type + "</color>  " + tasks[i].taskName;
        }
        for (int i = tasks.Count; i < 8; i++)
        {
            taskList[i].SetActive(false);
        }
    }
    /// <summary>
    /// 加载任务详情
    /// </summary>
    /// <param name="id"></param>
    public void LoadTask(int index)
    {
        //切换选中状态
        for (int i = 0; i < 8; i++)
        {
            if (i == index)
            {
                taskList[i].GetComponent<Image>().sprite = buttonShow[0];
            }
            else
            {
                taskList[i].GetComponent<Image>().sprite = buttonShow[1];
            }
        }
        currentTask = tasks[index].taskID;
        Task task = tm.GetById(tasks[index].taskID);
        //Debug.Log(tasks[index].targetID);
        State state = tm.GetStateInfo(task.targetID);
        GameObject.Find("Title").GetComponent<Text>().text = task.taskName;
        GameObject.Find("TaskDesc").GetComponent<TextVerticalComponent>().text = task.taskDesc;
        GameObject.Find("TaskDesc (1)").GetComponent<TextVerticalComponent>().text = task.address + "  " + task.sender + " 寄";
        GameObject.Find("TaskDesc (2)").GetComponent<TextVerticalComponent>().text = state.name + "  " + task.receiver + " 收";
        GameObject.Find("Destination").GetComponent<Text>().text = "送至：<color='blue'>" + state.name + "</color> <color='brown'>" + (int)(state.distance * 2) + "里路 </color>";
        if (task.entries.Length != 0)
        {
            GameObject.Find("EntryName").GetComponent<Text>().text = task.entries[0].entryName;
            GameObject.Find("EntryDesc").GetComponent<Text>().text = task.entries[0].desc + "\r\n" + task.entries[0].effect;
        }
        else
        {
            GameObject.Find("EntryName").GetComponent<Text>().text = "一路顺风";
            GameObject.Find("EntryDesc").GetComponent<Text>().text = "似乎是一趟轻松的旅途";
        }
        string buffstr = "钱财：" + state.income + "    声望：" + state.prestige;
        if (task.Buff != 0)
        {
            buffstr += "\r\n完成任务的信使获得特质：<color='brown'>" + task.Buff + "</color>";
        }
        GameObject.Find("Gold").GetComponent<Text>().text = buffstr;

    }
    public void ClearTask()
    {
        for (int i = 0; i < 8; i++)
        {
            taskList[i].GetComponent<Image>().sprite = buttonShow[1];
        }
        GameObject.Find("Title").GetComponent<Text>().text = "";
        GameObject.Find("TaskDesc").GetComponent<TextVerticalComponent>().text = "";
        GameObject.Find("TaskDesc (1)").GetComponent<TextVerticalComponent>().text = "";
        GameObject.Find("TaskDesc (2)").GetComponent<TextVerticalComponent>().text = "";
        GameObject.Find("Destination").GetComponent<Text>().text = "";

        GameObject.Find("EntryName").GetComponent<Text>().text = "";
        GameObject.Find("EntryDesc").GetComponent<Text>().text = "";

        GameObject.Find("EntryName").GetComponent<Text>().text = "";
        GameObject.Find("EntryDesc").GetComponent<Text>().text = "";

        GameObject.Find("Gold").GetComponent<Text>().text = "";
    }
    /// <summary>
    /// 进入选信使界面
    /// </summary>
    public void EnterSelect()
    {
        GameObject.Find("TaskList").SetActive(false);
        GameObject.Find("Select").SetActive(false);
        GameObject.Find("TaskRewards").SetActive(false);
        GameObject.Find("MesBoard").SetActive(true);
        GameObject.Find("Dispatch").SetActive(true);
        GameObject.Find("Cancel").SetActive(true);
        GameObject.Find("MesReward").SetActive(true);
        GetMesList();
        Pick(0);
    }
    /// <summary>
    /// 返回查看任务界面
    /// </summary>
    public void back()
    {
        GameObject.Find("TaskList").SetActive(true);
        GameObject.Find("Select").SetActive(true);
        GameObject.Find("TaskRewards").SetActive(true);
        GameObject.Find("MesBoard").SetActive(false);
        GameObject.Find("Dispatch").SetActive(false);
        GameObject.Find("Cancel").SetActive(false);
        GameObject.Find("MesReward").SetActive(false);
    }
    /// <summary>
    /// 获取信使列表
    /// </summary>
    public void GetMesList()
    {
        //其实应该经过筛选
        ms = data.MessengerInfo;
    }
    public void Pick(int index)
    {
        //切换选中状态
        for (int i = 0; i < 2; i++)
        {
            if (i == index)
            {
                mesList[i].GetComponent<Image>().sprite = mes[0];
            }
            else
            {
                mesList[i].GetComponent<Image>().sprite = mes[1];
            }
        }
        currentMes = index + 2;

    }
    /// <summary>
    /// 点击派遣按钮
    /// </summary>
    public void Dispatch()
    {
        tm.Dispatch(currentMes, currentTask, new int[0]);
        //应该有一个工具类负责全局消息提示（弹窗显示）
    }
}
