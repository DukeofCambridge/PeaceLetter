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
    }

    public static void CreateNewItem(Item item)
    {
        Slot newItem = Instantiate(instance.slotPrefab, instance.slotGrid.transform.position, Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.ItemImg;
        newItem.slotImage.transform.localScale = new Vector3(1, 1, 1);
        newItem.slotNumber.text = item.Hold.ToString();
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

    public static void Operate_Buy()//成功购买逻辑
    {
        int k = Exist(Resources.Load<Shop>("shopbag"), Slot.tempname);
        Resources.Load<Shop>("shopbag").shoplist[k].Hold -= 1;
        if (Resources.Load<Shop>("shopbag").shoplist[k].Hold == 0)
        {
            Resources.Load<Shop>("shopbag").shoplist[k].Hold = 1;
            Resources.Load<Shop>("shopbag").shoplist.RemoveAt(k);
        }
        Test.Operate_add(Slot.tempname);
        Slot.tempname = "";
        RefreshItem();
    }

    public static int Exist(Shop a, string b)//a 背包  b 物品名
    {
        for (int i = 0; i < a.shoplist.Count; i++)
        {
            if (a.shoplist[i].ItemName == b)
            {
                return i;
            }
            else continue;
        }
        return -1;
    }
    public static void Confirm()
    {
        Operate_Buy();
    }
}
