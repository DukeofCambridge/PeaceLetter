using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
[System.Serializable]
public class Messenger : MessengerBase
{
    public Messenger(int energy, int knowledge, int social, int trust, int fatigue,int level, int exp, bool possess, MessengerState state, string name, int id,List<int> Buffs) 
        : base(energy,knowledge,social,trust,fatigue,level,exp,possess,state,name,id,Buffs){ }

    public Messenger(int id):base(id) {}

    public Messenger() : base() { }

    public virtual int GetCurUpgradeNeedEXP()
    {
        JsonData MessengerLevelCfg = Util.LoadCfgAsJsonData(Common.MessengerNumCfgPath);
        return int.Parse(MessengerLevelCfg[Level]["UpgradeNeedEXP"].ToString());
    }

    public override bool Upgrade(int curEXP)
    {
        //because level is in range [0,7],so level == index
        if (!File.Exists(Common.MessengerNumCfgPath))
        {
            Debug.Log("File does not exist.");
            return false;
        }
        JsonData MessengerLevelCfg = Util.LoadCfgAsJsonData(Common.MessengerNumCfgPath);
        if (Level < MessengerLevelCfg.Count)
        {
            int UpgradeNeedEXP = GetCurUpgradeNeedEXP();
            if (curEXP >= UpgradeNeedEXP)
            {
                Level += 1;
                EXP = curEXP - UpgradeNeedEXP;
            }// upgrade and refresh EXP
        }// current Level has not reached the max Level,then try to upgrade
        else
        {
            EXP = 0;
        }// current Level has reached the max Levek,then clear the EXP
        return true;
    }

    public override void SetPoint(PointType pointType, int value)
    {
        switch (pointType)
        {
            case PointType.ENERGY:
                Energy += value;
                break;
            case PointType.KNOWLEDGE:
                Knowledge += value;
                break;
            case PointType.SOCIAL:
                Social += value;
                break;
            case PointType.TRUST:
                Trust += value;
                break;
            case PointType.FATIGUE:
                Fatigue += value;
                break;
        }
    }

    public override bool SetMessengerToTask(int taskId)
    {
        if (State == MessengerState.TASK)
        {
            Debug.Log(Name + " messenger is already in the task");
            return false;
        }//which means this messenger has task already
        else
        {
            //bound taskId here/////////////////////////////////
            State = MessengerState.TASK;
            return true;
        }
    }

    public override bool SetMessengerToIdle()
    {
        if (State == MessengerState.TASK)
        {
            State = MessengerState.IDLE;
            return true;
        }
        else
        {
            Debug.Log(Name + " messenger is already idle");
            return false;
        }
    }

    public override int CalcutaMaxLoad()
    {
        return 100 + 3 * (Energy-1);
    }

    public override double CalculateSpeed()
    {
        return 3 * (1 + 0.03 * Knowledge);
    }
    public override int CalculateTaskIncome(int income)
    {
        return (int)(3 * (1 + 0.03 * Social));
    }
    public override int CalculateSaleIncome(int income)
    {
        return (int)(3 * (1 + 0.03 * Social));
    }
}