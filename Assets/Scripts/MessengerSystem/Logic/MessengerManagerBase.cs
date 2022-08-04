using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public abstract class MessengerManagerBase : Singleton<MessengerManagerBase>
{
    //public List<MessengerBase> MessengerList = new List<MessengerBase>();
    protected int MaxMessengerNum;
    
    /*public int GetCurMessengerNum()
    {
        return MessengerList.Count;
    }*/

    public abstract bool SetExpAndUpgrade(int messengerId, int exp);
    public abstract bool SetPoint(int messengerId, PointType pointType, int value);
    public abstract bool SetMessengerToTask(int messengerId, int taskId);
    public abstract bool SetMessengerToIdle(int messengerId);
    //public abstract MessengerBase CreateMessenger(int id);
    public abstract void AddMessenger(int id);
    public abstract void RemoveMessenger(int id);
}
