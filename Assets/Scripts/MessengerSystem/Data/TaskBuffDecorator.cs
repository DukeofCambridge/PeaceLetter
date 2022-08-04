using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskBuff : MessengerDecorator
{
    public BuffType type;
    public int lastRounds;
    public string preconditions;
    public TaskBuff(int buffId) : base(buffId)
    {
        JsonData buffCfg = Util.LoadCfgAsJsonData(Common.BuffCfgPath);
        JsonData buff = buffCfg[buffId - 1];
        type = (BuffType)System.Enum.Parse(typeof(BuffType), buff["type"].ToString().ToUpper());
        string lastRounds = buff["lastRounds"].ToString();
        this.lastRounds = (lastRounds == "infinity") ? -1 : int.Parse(lastRounds);
        preconditions = buff["preconditions"].ToString();
    }
}
