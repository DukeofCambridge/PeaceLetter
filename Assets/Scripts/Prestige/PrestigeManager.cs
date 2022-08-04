using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;
using Newtonsoft.Json;

public class PrestigeManager : Singleton<PrestigeManager>
{
    private GameSaveData data;
    public Entry[] entries;
    
    private void OnEnable()
    {
        EventHandler.EnhancePrestigeEvent += UpdateEntries;
    }

    private void OnDisable()
    {
        EventHandler.EnhancePrestigeEvent -= UpdateEntries;
    }
    protected override void Awake()
    {
        data = GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>().gameSaveData;
        entries = new Entry[0];
    }

    /// <summary>
    /// 获取声望配置
    /// </summary>
    /// <returns></returns>
    public PrestigeSetting GetPrescfg()
    {
        //加载当前声望下的配置参数
        string path = Common.presPath;
        string jsonStr = File.ReadAllText(path);
        return JsonConvert.DeserializeObject<PrestigeSetting[]>(jsonStr)[data.PrestigeLevel - 1];
    }
    public void GainExp(int exp)
    {
        data.PrestigeExp += exp;
        int need = (data.PrestigeLevel + 1) * 15;
        //声望升级
        if (data.PrestigeExp >= need)
        {
            data.PrestigeLevel += 1;
            data.PrestigeExp -= need;
            EventHandler.CallEnhancePrestigeEvent();
        }
    }
    /// <summary>
    /// 根据声望等级获取对应等级的词条
    /// </summary>
    /// <returns></returns>
    public void UpdateEntries()
    {
        string JsonStr = File.ReadAllText(Common.entryPath);
        Entry[] es = JsonConvert.DeserializeObject<Entry[]>(JsonStr);
        PrestigeSetting ps = GetPrescfg();
        List<Entry> el = new List<Entry>();
        foreach (Entry et in es)
        {
            foreach (int i in ps.taskLevel)
            {
                if (et.level == i)
                {
                    el.Add(et);
                    break;
                }
            }
        }
        entries = el.ToArray();
    }
}

