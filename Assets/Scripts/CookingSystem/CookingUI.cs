using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingUI : MonoBehaviour
{
    private GameObject bag;                                     //opened bag UI
    private GameObject bagBar;                                  //closed bag UI
    public Bag mybag;
    public Slot slotPrefab;                                     //item template
    private List<Item> itemList = new List<Item>();             //food in bag
    private List<Item> selectedItemList = new List<Item>();     //selected food for cooking

    void Start()
    {
        bag = GameObject.Find("bag");
        bagBar = GameObject.Find("bagBar");
        itemList = mybag.HoldItem;
        //inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        CloseBag();
    }

    public void RefreshBagUI()
    {

    }

    public void RefreshSelectedUI()
    {

    }

    public void RemoveItemToBag(Item item)
    {
        if (selectedItemList.Contains(item))
        {
            selectedItemList.Remove(item);
            InventoryManager.Operate_add(item.ItemName);
        }
        else
        {
            Debug.Log("error:selected item list does not contains" + item);
        }

    }

    public void RemoveItemToSelected(Item item)
    {
        if (itemList.Contains(item))
        {
            selectedItemList.Add(item);
            InventoryManager.Operate_remove(item.ItemName);
        }
        else
        {
            Debug.Log("error:item list does not contains" + item);
        }
    }

    public void OnGridButtinClicked(int id)
    {
        Item item = itemList[id - 1];
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
            Item item = itemList[id - 1];
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
