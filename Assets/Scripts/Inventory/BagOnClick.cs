using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagOnClick : MonoBehaviour
{
    static BagOnClick instance;
    public 加载物品 mybag;
    public GameObject page;
    public GameObject button;

    private void Awake()
    {
        if (instance != null)
            Destroy(this);
        instance = this;
    }
    public static void BagOnClick_1()
    {
        instance.mybag.pagenow = instance.mybag.玩家物品;
        InventoryManager.RefreshItem(1);

        instance.page = GameObject.Find("Canvas/bag/page");
        instance.page.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Index/page1", typeof(Sprite)) as Sprite;

        instance.button = GameObject.Find("Canvas/bag/Buttons/btn1");
        instance.button.GetComponent<Image>().sprite=Resources.Load("Img/Bag/Classes/OnClick/Foods",typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn2");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/Menus", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn3");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/NormalItems", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn4");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/ForSales", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn5");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/Others", typeof(Sprite)) as Sprite;
    }
    public static void BagOnClick_2()
    {
        instance.mybag.pagenow = instance.mybag.menu;
        InventoryManager.RefreshItem(1);

        instance.page = GameObject.Find("Canvas/bag/page");
        instance.page.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Index/page1", typeof(Sprite)) as Sprite;

        instance.button = GameObject.Find("Canvas/bag/Buttons/btn1");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/Foods", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn2");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/OnClick/Menus", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn3");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/NormalItems", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn4");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/ForSales", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn5");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/Others", typeof(Sprite)) as Sprite;
    }
    public static void BagOnClick_3()
    {
        instance.mybag.pagenow = instance.mybag.normalitem;
        InventoryManager.RefreshItem(1);

        instance.page = GameObject.Find("Canvas/bag/page");
        instance.page.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Index/page1", typeof(Sprite)) as Sprite;

        instance.button = GameObject.Find("Canvas/bag/Buttons/btn1");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/Foods", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn2");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/Menus", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn3");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/OnClick/NormalItems", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn4");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/ForSales", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn5");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/Others", typeof(Sprite)) as Sprite;
    }
    public static void BagOnClick_4()
    {
        instance.mybag.pagenow = instance.mybag.forsale;
        InventoryManager.RefreshItem(1);

        instance.page = GameObject.Find("Canvas/bag/page");
        instance.page.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Index/page1", typeof(Sprite)) as Sprite;

        instance.button = GameObject.Find("Canvas/bag/Buttons/btn1");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/Foods", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn2");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/Menus", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn3");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/NormalItems", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn4");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/OnClick/ForSales", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn5");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/Others", typeof(Sprite)) as Sprite;
    }
    public static void BagOnClick_5()
    {
        instance.mybag.pagenow = instance.mybag.others;
        InventoryManager.RefreshItem(1);

        instance.page = GameObject.Find("Canvas/bag/page");
        instance.page.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Index/page1", typeof(Sprite)) as Sprite;

        instance.button = GameObject.Find("Canvas/bag/Buttons/btn1");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/Foods", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn2");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/Menus", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn3");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/NormalItems", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn4");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/NotOnClick/ForSales", typeof(Sprite)) as Sprite;
        instance.button = GameObject.Find("Canvas/bag/Buttons/btn5");
        instance.button.GetComponent<Image>().sprite = Resources.Load("Img/Bag/Classes/OnClick/Others", typeof(Sprite)) as Sprite;
    }
}
