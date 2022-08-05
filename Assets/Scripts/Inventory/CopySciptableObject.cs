using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        getname1();
        getname2();
        for(int i=0;i<40;i++)
        {
            Resources.Load<Item>("shopitem/" + list1[i]).ItemName = Resources.Load<Item>("item/" + list2[i]).ItemName;
            Resources.Load<Item>("shopitem/" + list1[i]).ItemDescription = Resources.Load<Item>("item/" + list2[i]).ItemDescription;
            Resources.Load<Item>("shopitem/" + list1[i]).ItemMeasure = Resources.Load<Item>("item/" + list2[i]).ItemMeasure;
            Resources.Load<Item>("shopitem/" + list1[i]).Hold = Resources.Load<Item>("item/" + list2[i]).Hold;
            Resources.Load<Item>("shopitem/" + list1[i]).ItemPrice = Resources.Load<Item>("item/" + list2[i]).ItemPrice;
            Resources.Load<Item>("shopitem/" + list1[i]).ItemCategory = Resources.Load<Item>("item/" + list2[i]).ItemCategory;
            Resources.Load<Item>("shopitem/" + list1[i]).ItemImg = Resources.Load<Item>("item/" + list2[i]).ItemImg;
        }
    }
    public List<string> list1=new List<string>();
    public List<string> list2=new List<string>();
    int a = 13750;
    int b = 41000;

    string[] dirs1 = Directory.GetFiles(@"D:/Unity/project/My project/Assets/Resources/shopitem", "*.asset");
    string[] dirs2 = Directory.GetFiles(@"D:/Unity/project/My project/Assets/Resources/item", "*.asset");
    string temp1 = "";
    string temp2 = "";
    void getname1()
    {
        for(int i=0; i<dirs1.Length; i++)
        {
            for (int j = 0; j < dirs1[i].Length; j++)
            {
                if (dirs1[i][j]>=((char)a)&&dirs1[i][j]<=((char)b))
                {
                    temp1 += dirs1[i][j];
                }
            }
            list1.Add(temp1+"s");
            temp1 = "";
        }
    }
    void getname2()
    {
        for (int i = 0; i < dirs2.Length; i++)
        {
            for (int j = 0; j < dirs2[i].Length; j++)
            {
                if (dirs2[i][j] >= ((char)a) && dirs2[i][j] <= ((char)b))
                {
                    temp2 += dirs2[i][j];
                }
            }
            list2.Add(temp2);
            temp2 = "";
            Debug.Log(list2[i]);
        }
    }


}
