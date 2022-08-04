using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MessengerManager : MessengerManagerBase
{
    private ArchiveManager archiveManager;
    private const float TICK_INTERVAL = 120;
    private List<Messenger> MessengerList = new List<Messenger>();

    private void OnEnable()
    {
        EventHandler.onIncrementHour += Recover;
    }

    void Start()
    {
        //Coroutine timer = StartCoroutine(Tick(TICK_INTERVAL));
        //init MaxMessengerNum
        archiveManager = GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>();
        RefreshMaxMessengerNum();
        MessengerList = archiveManager.gameSaveData.MessengerInfo;
        //init messenger list
        /*for (int i = 0; i < archiveManager.gameSaveData.MessengerInfo.Count; i++)
        {
            MessengerList.Add(archiveManager.gameSaveData.MessengerInfo[i]);
        }*/
    }

    // 1s real world = 1 min game
    // 120 s real world = 120 min game
    // fatigue recovery speed: 0.5 point/h
    /*
    private IEnumerator Tick(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            //recover fatigue point automatically
            for (int i = 0; i < mList.Count; i++)
            {
                if (mList[i].State == MessengerState.IDLE)
                {
                    mList[i].Fatigue += 1;
                }
            }
        }
    }*/
    private void Recover(int none)
    {
        //recover fatigue point automatically
        for (int i = 0; i < MessengerList.Count; i++)
        {
            if (MessengerList[i].State == MessengerState.IDLE)
            {
                MessengerList[i].Fatigue += 1;
            }
        }
    }

    public override bool SetExpAndUpgrade(int messengerId, int exp)
    {
        if (messengerId > 0 && messengerId <= MessengerList.Count)
        {
            MessengerList[messengerId - 1].EXP = exp;
            MessengerList[messengerId - 1].Upgrade(exp);
            archiveManager.gameSaveData.MessengerInfo[messengerId - 1] = MessengerList[messengerId - 1];
            archiveManager.SaveToLocal(1);
            return true;
        }
        else
        {
            Debug.Log("invalid messenger id input");
            return false;
        }
    }

    public override bool SetPoint(int messengerId, PointType pointType, int value)
    {
        if (messengerId > 0 && messengerId <= MessengerList.Count)
        {
            MessengerList[messengerId - 1].SetPoint(pointType,value);
            archiveManager.gameSaveData.MessengerInfo[messengerId - 1] = MessengerList[messengerId - 1];
            archiveManager.SaveToLocal(1);
            return true;
        }
        else
        {
            Debug.Log("invalid messenger id input");
            return false;
        }
    }

    public override bool SetMessengerToTask(int messengerId, int taskId)
    {
        if (messengerId > 0 && messengerId <= MessengerList.Count)
        {
            MessengerList[messengerId - 1].SetMessengerToTask(taskId);
            archiveManager.gameSaveData.MessengerInfo[messengerId - 1] = MessengerList[messengerId - 1];
            archiveManager.SaveToLocal(1);
            return true;
        }
        else
        {
            Debug.Log("invalid messenger id input");
            return false;
        }
    }

    public override bool SetMessengerToIdle(int messengerId)
    {
        if (messengerId > 0 && messengerId <= MessengerList.Count)
        {
            MessengerList[messengerId - 1].SetMessengerToIdle();
            archiveManager.gameSaveData.MessengerInfo[messengerId - 1] = MessengerList[messengerId - 1];
            archiveManager.SaveToLocal(1);
            return true;
        }
        else
        {
            Debug.Log("invalid messenger id input");
            return false;
        }
    }

    public Messenger CreateMessenger(int id)
    {
        ArchiveManager archiveManager = GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>();
        if (archiveManager)
        {
            return archiveManager.gameSaveData.MessengerInfo[id - 1];
        }
        else
        {
            return new Messenger();
        }
    }

    public override void AddMessenger(int id)
    {
        ArchiveManager archiveManager = GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>();
        if (id <= archiveManager.gameSaveData.MessengerInfo.Count && id >= 0)
        {
            MessengerList.Add(CreateMessenger(id));
        }
        else
        {
            Debug.Log("Error:input messenger id > max messenger id");
        }
    }

    public override void RemoveMessenger(int id)
    {
        ArchiveManager archiveManager = GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>();
        if (id <= archiveManager.gameSaveData.MessengerInfo.Count && id >= 0)
        {
            MessengerList.Remove(CreateMessenger(id));
        }
        else
        {
            Debug.Log("Error:input messenger id > max messenger id");
        }
    }

    public bool RefreshMaxMessengerNum()
    {
        //get current level and check config file
        ArchiveManager archiveManager = GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>();
        int level = archiveManager.gameSaveData.PrestigeLevel;

        string JsonStr = File.ReadAllText(Common.MessengerNumCfgPath);
        JsonData MessengerNumCfg = JsonMapper.ToObject(JsonStr);
        JsonData PrestigeLevelList = MessengerNumCfg["PrestigeLevelList"];
        JsonData MessengerNumList = MessengerNumCfg["MessengerNumList"];
        List<int> prestigeLevelList = new List<int>();
        List<int> messengerNumList = new List<int>();
        for(int i = 0; i < PrestigeLevelList.Count; i++)
        {
            prestigeLevelList.Add(int.Parse(PrestigeLevelList[i].ToString()));
        }
        for (int i = 0; i < MessengerNumList.Count; i++)
        {
            messengerNumList.Add(int.Parse(MessengerNumList[i].ToString()));
        }
        if (prestigeLevelList.Contains(level))
        {
            MaxMessengerNum = messengerNumList[level - 1];
            return true;
        }
        else
        {
            Debug.Log("Invalid prestige level input.");
            return false;
        }
    }
}
