using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject bag;

    void Update()
    {
        Openbag();
       // Operate_add("大米");
        //Operate_remove("大米");
    }
     void Openbag()
    {
        if(Input.GetKeyDown(KeyCode.O))
        {
            bag.SetActive(!bag.activeSelf);
            InventoryManager.RefreshItem(1);
        }
    }

    public static void Operate_add(string a)//增加Item
    {
        Item temp = Resources.Load<Item>("item/"+a);
        int k = Exist(Resources.Load<Bag>("mybag"), temp.ItemName);
        if (k == -1)
        {
            Resources.Load<Bag>("mybag").HoldItem.Add(temp);
            InventoryManager.CreateNewItem(temp);
        }
        else
        {
            Resources.Load<Bag>("mybag").HoldItem[k].Hold += 1;
        }
        InventoryManager.RefreshItem(1);
    }
    void Operate_remove(string a)
    {
        Item temp = Resources.Load<Item>("item/" + a);
        int k = Exist(Resources.Load<Bag>("mybag"), temp.ItemName);
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (k == -1)
            {
                //提示不存在
            }
            else
            {
                Resources.Load<Bag>("mybag").HoldItem[k].Hold -= 1;
                if (Resources.Load<Bag>("mybag").HoldItem[k].Hold == 0)
                {
                    Resources.Load<Bag>("mybag").HoldItem[k].Hold = 1;
                    Resources.Load<Bag>("mybag").HoldItem.RemoveAt(k);
                }
            }
        }
        InventoryManager.RefreshItem(1);
    }

 

    static int Exist(Bag a,string b)//a 背包  b 物品名
    {
        for (int i=0; i < a.HoldItem.Count;i++)
        {
            if (a.HoldItem[i].ItemName == b)
            {
                return i;
            }
            else continue;
        }
        return -1;
    }
}
