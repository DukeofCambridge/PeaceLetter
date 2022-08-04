using System;
using System.Collections.Generic;

public abstract class MessengerBase
{
    private int energy;
    private int knowledge;
    private int social;
    private int trust;
    private int fatigue;
    private int level;
    private int exp;
    private bool possess;
    private MessengerState state;
    public int Energy { get { return energy; } set { energy = SetValueInRange(1, 10, value);}}
    public int Knowledge { get { return knowledge; } set { knowledge = SetValueInRange(1, 10, value);} }
    public int Social { get { return social; } set { social = SetValueInRange(1, 10, value);} }
    public int Trust { get { return trust; } set { trust = SetValueInRange(1, 10, value);} }
    public int Fatigue { get { return fatigue; } set { fatigue = SetValueInRange(1, 10, value);} }
    public int Level { get { return level; } protected set { level = SetValueInRange(0, 7, value);} }
    public int EXP { get; set; }
    public bool Possess { get; set; }
    public MessengerState State { get; set; }
    public string Name;
    public int Id;
    public List<int> BuffIdList;

    public MessengerBase(int energy,int knowledge,int social,int trust,int fatigue,
        int level,int exp,bool possess,MessengerState state,string name,int id,List<int> buffIdList)
    {
        Energy = energy;
        Knowledge = knowledge;
        Social = social;
        Trust = trust;
        Fatigue = fatigue;
        Level = level;
        EXP = exp;
        Possess = possess;
        State = state;
        Name = name;
        Id = id;
        BuffIdList = buffIdList;
    }

    public MessengerBase(int id)
    {
        Id = id;
    }

    public MessengerBase() { }

    //set the value in range [lowerBound,upperBound]
    private int SetValueInRange(int lowerBound, int upperBound, int value)
    {
        if (value < lowerBound)
        {
            return lowerBound;
        }
        else if (value > upperBound)
        {
            return upperBound;
        }
        else
        {
            return value;
        }
    }

    public abstract bool Upgrade(int curEXP);
    public abstract void SetPoint(PointType pointType, int value);
    public abstract bool SetMessengerToTask(int taskId);
    public abstract bool SetMessengerToIdle();
    public abstract int CalcutaMaxLoad();
    public abstract double CalculateSpeed();
    public abstract int CalculateTaskIncome(int income);
    public abstract int CalculateSaleIncome(int income);
}
