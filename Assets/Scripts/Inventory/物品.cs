using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item",menuName ="Inventory/New Item")]
public class 物品 : ScriptableObject
{
    public string 物品名称;
    public Sprite 物品图片;
    [TextArea]
    public string 物品描述;
    public 分类 物品分类;
    public string 物品计量;
    public int 物品价格;
    public int 当前持有;
    public enum 分类
    {
        蔬菜, 水果, 肉, 水酒, 调味品, 药, 特殊, 贩卖, 菜品
    }
}
