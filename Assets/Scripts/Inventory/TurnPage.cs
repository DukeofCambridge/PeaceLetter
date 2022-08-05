using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPage : MonoBehaviour
{
    public GameObject page;
    public int i = 1;

    public void OnClickUp()
    {
        if(i>1)
        {
            InventoryManager.RefreshItem(i-1);
            i--;
            page = GameObject.Find("Canvas/bag/page");
            page.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Index/page" + i.ToString(), typeof(Sprite)) as Sprite;
        }
    }
    public void OnClickDown()
    {
        if(i<4)
        {
            InventoryManager.RefreshItem(i+1);
            i++;
            page = GameObject.Find("Canvas/bag/page");
            page.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Index/page" + i.ToString(), typeof(Sprite)) as Sprite;
        }
    }
}
