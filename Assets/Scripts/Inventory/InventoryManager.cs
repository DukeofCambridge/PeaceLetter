using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public Bag mybag;
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
        instance.Img.sprite = itemImage.sprite;
        instance.Img.transform.localScale=new Vector3(3,3,3);
        
    }
    public static void CreateNewItem(Item item)
    {
        Slot newItem=Instantiate(instance.slotPrefab,instance.slotGrid.transform.position,Quaternion.identity);
        newItem.gameObject.transform.SetParent(instance.slotGrid.transform);
        newItem.slotItem = item;
        newItem.slotImage.sprite = item.ItemImg;
        newItem.slotImage.transform.localScale=new Vector3(1,1,1);
        newItem.slotNumber.text = item.Hold.ToString();
    }

    public static void RefreshItem(int index)
    {
        for (int i = 0; i < instance.slotGrid.transform.childCount; i++)
        {
            Destroy(instance.slotGrid.transform.GetChild(i).gameObject);
        }

        //for (int i = 0; i < instance.mybag.HoldItem.Count; i++)
        //{
        //    CreateNewItem(instance.mybag.HoldItem[i]);
        //}

       
        int count = instance.mybag.pagenow.Count;
        int fullcount = count / 15;
        List<Item> list = instance.mybag.pagenow;

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
    public static void Operate_add(string a)//增加Item
    {
        Item temp = Resources.Load<Item>("item/" + a);
        int k = Exist(Resources.Load<Bag>("mybag"), temp.ItemName);
        if (k == -1)
        {
            Resources.Load<Bag>("mybag").HoldItem.Add(temp);
            InventoryManager.CreateNewItem(temp);
        }
        else
        {
            Resources.Load<Bag>("mybag").HoldItem[k].Hold += 1;
        }
        InventoryManager.RefreshItem(1);
    }
    public static void Operate_remove(string a)
    {
        Item temp = Resources.Load<Item>("item/" + a);
        int k = Exist(Resources.Load<Bag>("mybag"), temp.ItemName);
        if (Input.GetKeyDown(KeyCode.D))
        {
            if (k == -1)
            {
                //提示不存在
            }
            else
            {
                Resources.Load<Bag>("mybag").HoldItem[k].Hold -= 1;
                if (Resources.Load<Bag>("mybag").HoldItem[k].Hold == 0)
                {
                    Resources.Load<Bag>("mybag").HoldItem[k].Hold = 1;
                    Resources.Load<Bag>("mybag").HoldItem.RemoveAt(k);
                }
            }
        }
        InventoryManager.RefreshItem(1);
    }



    public static int Exist(Bag a, string b)//a 背包  b 物品名
    {
        for (int i = 0; i < a.HoldItem.Count; i++)
        {
            if (a.HoldItem[i].ItemName == b)
            {
                return i;
            }
            else continue;
        }
        return -1;
    }
}
