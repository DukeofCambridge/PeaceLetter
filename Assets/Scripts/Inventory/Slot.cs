using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    static public Slot instance;
    public 物品 slotItem;
    public Image slotImage;
    public Text slotNumber;

    public void ItemOnClick()
    {
        InventoryManager.UpdateItemInfo(slotItem.物品描述,slotImage);
    }

    public static 物品 Confirm()
    {
        return instance.slotItem;
    }
}
