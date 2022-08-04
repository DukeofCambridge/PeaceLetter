using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shoplogic : MonoBehaviour
{
    public static void Operate_Buy()//右侧确认键点击后的逻辑
    {
        int k = Exist(Resources.Load<Shop>("shopbag"), Slot.Confirm().物品名称);
        Resources.Load<Shop>("shopbag").shoplist[k].当前持有 -= 1;
        if(Resources.Load<Shop>("shopbag").shoplist[k].当前持有==0)
        {
            Resources.Load<Shop>("shopbag").shoplist[k].当前持有 = 1;
            Resources.Load<Shop>("shopbag").shoplist.RemoveAt(k);
        }
        //测试.Operate_add(Slot.Confirm().物品名称);
    }


    public static int Exist(Shop a, string b)//a 背包  b 物品名
    {
        for (int i = 0; i < a.shoplist.Count; i++)
        {
            if (a.shoplist[i].物品名称 == b)
            {
                return i;
            }
            else continue;
        }
        return -1;
    }
}

