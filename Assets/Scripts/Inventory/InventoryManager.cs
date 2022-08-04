using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class InventoryManager : MonoBehaviour
{
    static InventoryManager instance;

    public 加载物品 mybag;
    public GameObject slotGrid;
    public Slot slotPrefab;
    public Text iteminfo;
    public Image Img;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }

    private void OnEnable()
    {
        RefreshItem(1);
        instance.iteminfo.text = "";
    }

    public static void UpdateItemInfo(string itemdesciption,Image itemImage)
    {
        instance.iteminfo.text = itemdesciption;
        instance.Img = itemImage;
        
    }
    public static void CreateNewItem(物品 item)
    {
        Slot newItem=Instantiate(instance.slotPrefab,instance.slotGrid.transform.position,Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.物品图片;
        newItem.slotImage.transform.localScale=new Vector3(1,1,1);
        newItem.slotNumber.text = item.当前持有.ToString();
    }

    public static void RefreshItem(int index)
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }

        //for (int i = 0; i < instance.mybag.玩家物品.Count; i++)
        //{
        //    CreateNewItem(instance.mybag.玩家物品[i]);
        //}

       
        int count = instance.mybag.pagenow.Count;
        int fullcount = count / 15;
        List<物品> list = instance.mybag.pagenow;

        if (index <= fullcount)
        {
            for (int i = 15 * (index - 1); i < 15 * index; i++)
            {
                CreateNewItem(instance.mybag.pagenow[i]);
            }
        }
        else
        {
            for (int i = 15 * (index - 1); i < count; i++)
            {
                CreateNewItem(instance.mybag.pagenow[i]);
            }
        }
    }
    public static void Operate_add(string 物品名称)//增加物品
    {
        物品 item = Resources.Load<物品>("item/" + 物品名称);
        int index = Exist(Resources.Load<加载物品>("mybag"), item.物品名称);
        if (index == -1)
        {
            Resources.Load<加载物品>("mybag").玩家物品.Add(item);
            CreateNewItem(item);
        }
        else
        {
            Resources.Load<加载物品>("mybag").玩家物品[index].当前持有 += 1;
        }
        RefreshItem(1);
    }

    public static void Operate_remove(string 物品名称)
    {
        物品 item = Resources.Load<物品>("item/" + 物品名称);
        int index = Exist(Resources.Load<加载物品>("mybag"), item.物品名称);
        if (index == -1)
        {
            Debug.Log("不存在");
            //提示不存在
        }
        else
        {
            Resources.Load<加载物品>("mybag").玩家物品[index].当前持有 -= 1;
            if (Resources.Load<加载物品>("mybag").玩家物品[index].当前持有 == 0)
            {
                Resources.Load<加载物品>("mybag").玩家物品[index].当前持有 = 1;
                Resources.Load<加载物品>("mybag").玩家物品.RemoveAt(index);
            }
        }
        RefreshItem(1);
    }



    static int Exist(加载物品 itemLoad, string 物品名称)
    {
        for (int i = 0; i < itemLoad.玩家物品.Count; i++)
        {
            if (itemLoad.玩家物品[i].物品名称 == 物品名称)
            {
                return i;
            }
            else continue;
        }
        return -1;
    }
}
