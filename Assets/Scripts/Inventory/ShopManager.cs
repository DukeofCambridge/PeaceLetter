using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    static ShopManager instance;

    public Shop shopbag;
    public GameObject slotGrid;
    public Slot slotPrefab;
    public Text iteminfo;
    public Image Img;
    private void Awake()
    {
        if(instance!=null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable()
    {
        RefreshItem();
        instance.iteminfo.text = "";
    }

    public static void CreateNewItem(物品 item)
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.物品图片;
        newItem.slotNumber.text = item.当前持有.ToString();
    }

    public static void RefreshItem()
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < instance.shopbag.shoplist.Count; i++)
        {
            CreateNewItem(instance.shopbag.shoplist[i]);
        }
    }
}
