using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assemble_ItemOrder : MonoBehaviour
{
    //아이템 추가 시스템
    public static Assemble_ItemOrder instance;
    List<Transform> Items=new List<Transform>();


    public void Start()
    {
        instance = this;
        GameObject[] ItemCount = GameObject.FindGameObjectsWithTag("Assemble_Item");

        for (int i = 0; i < ItemCount.Length; i++)
            Items.Add(ItemCount[i].transform);
    }


    public void Update()
    {
        // 나중에 지우세요^^
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Add_Item("Wood", 1);
        }
    }


    //추가될 아이템 이름과 개수로 추가 가능
    public void Add_Item(string Item_Name, int num)
    {
        for (int i = 0; i < Items.Count; i++)
            if (Items[i].name == Item_Name)
                Items[i].GetChild(0).GetChild(0).GetChild(0).GetComponent<Assemble_ItemSelectCountChanger>().UpNum(num);
    }
}