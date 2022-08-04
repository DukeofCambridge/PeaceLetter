using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingUI : MonoBehaviour
{
    private GameObject bag;                                     //opened bag UI
    private GameObject bagBar;                                  //closed bag UI
    public 加载物品 mybag;
    public Slot slotPrefab;                                     //item template
    private List<物品> itemList = new List<物品>();             //food in bag
    private List<物品> selectedItemList = new List<物品>();     //selected food for cooking

    void Start()
    {
        bag = GameObject.Find("bag");
        bagBar = GameObject.Find("bagBar");
        itemList = mybag.玩家物品;
        //inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        CloseBag();
    }

    public void RefreshBagUI()
    {

    }

    public void RefreshSelectedUI()
    {

    }

    public void RemoveItemToBag(物品 item)
    {
        if (selectedItemList.Contains(item))
        {
            selectedItemList.Remove(item);
            InventoryManager.Operate_add(item.物品名称);
        }
        else
        {
            Debug.Log("error:selected item list does not contains" + item);
        }

    }

    public void RemoveItemToSelected(物品 item)
    {
        if (itemList.Contains(item))
        {
            selectedItemList.Add(item);
            InventoryManager.Operate_remove(item.物品名称);
        }
        else
        {
            Debug.Log("error:item list does not contains" + item);
        }
    }

    public void OnGridButtinClicked(int id)
    {
        物品 item = itemList[id - 1];
        if(!selectedItemList.Contains(item))
        {
            RemoveItemToSelected(item);
        }
    }
    
    public void OnPlusButtonClicked(int id)
    {
        if (id > selectedItemList.Count)
        {
            OpenBag();
        }// grid without item
        else
        {
            物品 item = itemList[id - 1];
            RemoveItemToBag(item);
        }// grid with item
    }
    public void OpenBag()
    {
        bag.SetActive(true);
        bagBar.SetActive(false);
    }

    public void CloseBag()
    {
        bag.SetActive(false);
        bagBar.SetActive(true);
    }
}
