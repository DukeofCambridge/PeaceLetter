using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Newtonsoft.Json;

public class ArchiveManager:Singleton<ArchiveManager>
{
    public GameSaveData gameSaveData;

    protected override void Awake()
    {
        //if load failed, then load original config file: Archive0.json
        string path = Application.dataPath + "/Json Data/Archive1.json";
        if (File.Exists(path))
        {
            LoadLocalData(1);
        }
        else
        {
            LoadLocalData(0);
            //��ʼˢ��ί��������(�ڶ���ſ�������ϵͳ�Ļ��Ͳ�����)
            //GameObject.Find("TaskManager").GetComponent<TaskManager>().RefreshTask(1,2);
        }
    }

    //parameter versionId is set for load different config files.
    //the original archive config file's id is 0
    public bool LoadLocalData(int versionId)
    {
        string path = Application.dataPath + "/Json Data/Archive" + versionId + ".json";
        string jsonStr = File.ReadAllText(path);
        gameSaveData = JsonConvert.DeserializeObject<GameSaveData>(jsonStr);
        /*
        JsonData GameData = Util.LoadCfgAsJsonData(path);

        //transform data - messenger system
        gameSaveData.PrestigeLevel = int.Parse(GameData["PrestigeLevel"].ToString());
        gameSaveData.PrestigeExp = int.Parse(GameData["PrestigeExp"].ToString());
        gameSaveData.Wealth = int.Parse(GameData["Wealth"].ToString());
        JsonData messengerArray = GameData["MessengerInfo"];
        for(int i = 0; i < messengerArray.Count; i++)
        {
            int id =int.Parse(messengerArray[i]["Id"].ToString());
            string name = messengerArray[i]["Name"].ToString();
            int energy = int.Parse(messengerArray[i]["Energy"].ToString());
            int knowledge = int.Parse(messengerArray[i]["Knowledge"].ToString());
            int social = int.Parse(messengerArray[i]["Social"].ToString());
            int trust = int.Parse(messengerArray[i]["Trust"].ToString());
            int fatigue = int.Parse(messengerArray[i]["Fatigue"].ToString());
            int EXP = int.Parse(messengerArray[i]["EXP"].ToString());
            int level = int.Parse(messengerArray[i]["Level"].ToString());
            MessengerState state = (MessengerState)int.Parse(messengerArray[i]["State"].ToString());
            bool possess = Convert.ToBoolean(messengerArray[i]["Possess"].ToString());
            JsonData buffIdList = messengerArray[i]["BuffIdList"];
            List<int> buffIdIntList = new List<int>();
            for (int j = 0; j < buffIdList.Count; j++)
            {
                buffIdIntList.Add(int.Parse(buffIdList[j].ToString()));
            }
            Messenger messenger = new Messenger(energy, knowledge, social, trust, fatigue,
                level, EXP, possess, state, name, id);
            gameSaveData.MessengerInfo.Add(messenger);
        }

        //tranform data - cooking system
        JsonData possessList = GameData["RecipeInfo"]["PossessList"];
        JsonData timesList = GameData["RecipeInfo"]["TimesList"];
        for (int i = 0; i < possessList.Count; i++)
        {
            gameSaveData.RecipePossessList.Add(Convert.ToBoolean(possessList[i].ToString()));
            gameSaveData.RecipeTimesList.Add(int.Parse(timesList[i].ToString()));
        }

        //transform data - object system
        JsonData bagArray = GameData["ObjectInfo"];
        for(int i = 0; i < bagArray.Count; i++)
        {

        }

        //transform data - task system
        JsonData taskArray = GameData["tasks"];
        Debug.Log(taskArray.ToString());
        gameSaveData.tasks = JsonConvert.DeserializeObject<List<Task>>(taskArray.ToString());
        Debug.Log("������:"+gameSaveData.tasks.Count);
        //transform data - date system
        gameSaveData.CurMonth = int.Parse(GameData["CurMonth"].ToString());
        gameSaveData.CurDay = int.Parse(GameData["CurDay"].ToString());
        gameSaveData.CurHour = int.Parse(GameData["CurHour"].ToString());
        gameSaveData.CurQuarterHour = int.Parse(GameData["CurQuarterHour"].ToString());*/

        return true;
    }
    /// <summary>
    /// �浵��ÿ��һ����Ϸ���Զ����û�ĳЩ�ؼ��ڵ��ֶ�����
    /// </summary>
    /// <param name="versionId"></param>
    /// <returns></returns>
    public bool SaveToLocal(int versionId)
    {
        //string gameDataStr = JsonMapper.ToJson(gameSaveData);
        //byte[] json = Encoding.UTF8.GetBytes(gameDataStr.ToCharArray());
        var gameDataStr= JsonConvert.SerializeObject(gameSaveData, Formatting.Indented);
        File.WriteAllText(Application.dataPath + "/Json Data/Archive" + versionId + ".json", gameDataStr);
        Debug.Log("Saved automatically");
        return true;
    }
}
