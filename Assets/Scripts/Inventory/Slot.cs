using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    static public Slot instance;
    public Item slotItem;
    public Image slotImage;
    public Text slotNumber;

    static public string tempname;

    public void ItemOnClick()
    {
        InventoryManager.UpdateItemInfo(slotItem.ItemDescription,slotImage);
    }
    
    public void ShopItemOnClick()
    {
        tempname = "";
        tempname = slotItem.ItemName;
    }
}
