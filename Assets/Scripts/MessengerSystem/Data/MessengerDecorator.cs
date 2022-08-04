using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessengerDecorator : MessengerBase
{
    protected Messenger Messenger = null;
    public int buffId;
    public List<Effect> effectList = new List<Effect>();

    public MessengerDecorator(int buffId) {
        this.buffId = buffId;
        JsonData buffCfg = Util.LoadCfgAsJsonData(Common.BuffCfgPath);
        JsonData buff = buffCfg[buffId - 1];
        JsonData effectList = buff["effectList"];
        for (int i = 0; i < effectList.Count; i++)
        {
            this.effectList.Add(new Effect(effectList[i]));
        }
    }

    public void SetMessenger(Messenger messenger)
    {
        Messenger = messenger;
    }

    public override bool Upgrade(int curEXP)
    {
        return Messenger.Upgrade(curEXP);
    }

    public override void SetPoint(PointType pointType, int value)
    {
        Messenger.SetPoint(pointType, value);
    }

    public override bool SetMessengerToTask(int taskId)
    {
        return Messenger.SetMessengerToTask(taskId);
    }

    public override bool SetMessengerToIdle()
    {
        return Messenger.SetMessengerToIdle();
    }
    public override int CalcutaMaxLoad()
    {
        double load = Messenger.CalcutaMaxLoad();
        for (int i = 0; i < effectList.Count; i++)
        {
            // add extra load
            if (effectList[i].effect == EffectType.LOAD)
            {
                load = effectList[i].ApplyEffect(load);
            }
            if (effectList[i].effect == EffectType.LOAD_WITH_SOCIAL)
            {
                load = effectList[i].ApplyEffect(load, Messenger.Social);
            }
        }
        return (int)load;
    }

    public override double CalculateSpeed()
    {
        double speed = Messenger.CalculateSpeed();
        for (int i = 0; i < effectList.Count; i++)
        {
            if (effectList[i].effect == EffectType.MOVE_SPEED)
            {
                speed = effectList[i].ApplyEffect(speed);
            }
        }
        return speed;
    }

    public override int CalculateTaskIncome(int income)
    {
        double Income = Messenger.CalculateTaskIncome(income);
        for (int i = 0; i < effectList.Count; i++)
        {
            if (effectList[i].effect == EffectType.TASK_INCOME)
            {
                Income = effectList[i].ApplyEffect(Income);
            }
            if (effectList[i].effect == EffectType.TASK_INCOME_WITH_KNOWLEDGE)
            {
                Income = effectList[i].ApplyEffect(Income, Messenger.Knowledge);
            }
        }
        return (int)Income;
    }

    public override int CalculateSaleIncome(int income)
    {
        double Income = Messenger.CalculateSaleIncome(income);
        for (int i = 0; i < effectList.Count; i++)
        {
            if (effectList[i].effect == EffectType.TASK_INCOME)
            {
                Income = effectList[i].ApplyEffect(Income);
            }
        }
        return (int)Income;
    }
}
