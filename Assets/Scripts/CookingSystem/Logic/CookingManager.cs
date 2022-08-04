using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    private ArchiveManager archiveManager;
    public List<Recipe> recipeList = new List<Recipe>();

    public void Start()
    {
        archiveManager = GameObject.Find("ArchiveManager").GetComponent<ArchiveManager>();
        JsonData recipeCfg = Util.LoadCfgAsJsonData(Common.RecipeCfgPath);
        for(int i = 0; i < recipeCfg.Count; i++)
        {
            recipeList.Add(new Recipe(i+1));
        }
    }

    // when users select to cook refering to recipe
    public bool Cook(List<string> inputList,int recipeId)
    {
        //make sure users already have input at lease one ingredients
        if(inputList.Count > 0)
        {
            if(recipeList[recipeId - 1].Possess)
            {
                recipeList[recipeId - 1].Cook();
                return true;
            }
            else
            {
                Debug.Log("logic conflict:recipe has not been unlocked but try to cook with recipe");
                return false;
            }
        }
        else
        {
            Debug.Log("please input ingredients");
            return false;
        }
    }

    // when users try to cook without recipe
    public bool Cook(List<string> inputList)
    {
        if (inputList.Count > 0)
        {
            for(int i = 0; i < recipeList.Count; i++)
            {
                if (recipeList[i].CheckIngredients(inputList))
                {
                    if (recipeList[i].Possess)
                    {
                        recipeList[i].Cook();
                    }// if the recipe has been possessed,then cook and add Times
                    else
                    {
                        recipeList[i].UnlockRecipe();
                        recipeList[i].Cook();
                    }
                }// check if the ingredients compare
            }
            return true;
        }
        else
        {
            Debug.Log("please input ingredients");
            return false;
        }
    }
}
