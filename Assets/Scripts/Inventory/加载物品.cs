using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Bag", menuName = "Inventory/New bag")]
public class 加载物品 : ScriptableObject
{
    static 加载物品 instance;
    public List<物品> 玩家物品 = new List<物品>();//避免混乱，这个list名不敢改了，这个list是食材
    public List<物品> menu=new List<物品>();//菜
    public List<物品> normalitem = new List<物品>();//物品
    public List<物品> forsale = new List<物品>();//商品
    public List<物品> others = new List<物品>();//杂项

    public List<物品> pagenow=new List<物品>();

}
