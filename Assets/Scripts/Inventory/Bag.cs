using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "New Bag", menuName = "Inventory/New bag")]
public class Bag : ScriptableObject
{
    static Bag instance;
    public List<Item> HoldItem = new List<Item>();
    public List<Item> food=new List<Item>();
    public List<Item> menu=new List<Item>();//菜
    public List<Item> normalitem = new List<Item>();//Item
    public List<Item> forsale = new List<Item>();//商品
    public List<Item> others = new List<Item>();//杂项

    public List<Item> pagenow=new List<Item>();

}
