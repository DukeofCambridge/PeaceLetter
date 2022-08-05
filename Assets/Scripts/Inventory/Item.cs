using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/New Item")]
public class Item : ScriptableObject
{
    public string ItemName;//物品名
    public Sprite ItemImg;//图片
    [TextArea]
    public string ItemDescription;//描述
    public Category ItemCategory;//分类
    public string ItemMeasure;//计量单位
    public int ItemPrice;//单价
    public int Hold;//当前持有
    public enum Category
    {
        food,menu,normalitem,forsale, others
    }
}
