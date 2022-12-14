using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 아이템 조합시 필요한 데이터를 넘기기위해 만든 스크립트
public class Element_Item
{
    // 아이템 이름
    string name;
    // 조합에 필요한 아이템들
    ArrayList items;


   public Element_Item(string name, ArrayList Items)
    {
        this.name = name;
        items = Items;
    }


    public string GetName()
    {
        return name;
    }


    public ArrayList GetItems()
    {
        return items;
    }


    public int Length()
    {
        return items.Count;
    }
}
