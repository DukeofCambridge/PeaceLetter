using LitJson;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Recipe
{
    public int Id;
    public string Name;
    public RecipeType Type;
    public string Season;
    public List<string> Content = new List<string>();
    public double Timecost;
    public double Factor;
    public string Description;
    public bool Possess;
    public int Times;

    public Recipe(int id,string name,RecipeType type,string season,List<string> content,
        double timecost,double factor,string description,bool possess,int times)
    {
        Id = id;
        Name = name;
        Type = type;
        Season = season;
        Content = content;
        Timecost = timecost;
        Factor = factor;
        Description = description;
        Possess = possess;
        Times = times;
    }

    public Recipe(int id)
    {
        Id = id;
        JsonData recipeCfg = Util.LoadCfgAsJsonData(Common.RecipeCfgPath);
        JsonData recipe = recipeCfg[Id - 1];
        Name = recipe["Name"].ToString();
        Type = (RecipeType)Enum.Parse(typeof(RecipeType), recipe["Type"].ToString().ToUpper());
        Season = recipe["Season"].ToString();
        JsonData content = recipe["Content"];
        for (int i = 0; i < content.Count; i++)
        {
            Content.Add(content[i].ToString());
        }
        Timecost = double.Parse(recipe["Timecost"].ToString());
        Factor = double.Parse(recipe["Factor"].ToString());
        Description = recipe["Description"].ToString();
        Possess = GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>().gameSaveData.RecipePossessList[Id - 1];
        Times = GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>().gameSaveData.RecipeTimesList[id - 1];
    }

    public bool CheckIngredients(List<string> inputList)
    {
        if(inputList.Count != Content.Count)
        {
            return false;
        }//if the number of ingredients doesn't compare,that means they can't be equaled
        else
        {
            for(int i = 0; i < inputList.Count; i++)
            {
                if (!Content.Contains(inputList[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }

    public bool UnlockRecipe()
    {
        if (!Possess)
        {
            Possess = true;
            GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>().gameSaveData.RecipePossessList[Id - 1] = true;
            GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>().SaveToLocal(1);
            return true;
        }
        else
        {
            Debug.Log("this recipe has been learned.");
            return false;
        }
    }

    public bool Cook()
    {
        if (Possess)
        {
            //add to bag////////////////////////////////////
            Times++;
            GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>().gameSaveData.RecipeTimesList[Id - 1]++;
            GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>().SaveToLocal(1);
            return true;
        }
        else
        {
            return false;
        }
    }
}
