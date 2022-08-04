using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

public class InherentBuffDecorator : MessengerDecorator
{
    public string name;
    public InherentBuffDecorator(int buffId) : base(buffId)
    {
        JsonData buffCfg = Util.LoadCfgAsJsonData(Common.BuffCfgPath);
        JsonData buff = buffCfg[buffId - 1];
        name = buff["name"].ToString();
    }
}