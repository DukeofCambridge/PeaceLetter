using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ShopBag", menuName = "Inventory/New Shopbag")]
public class Shop : ScriptableObject
{
    public List<Item> shoplist=new List<Item>();
}
