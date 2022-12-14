using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assemble_ItemSlotOrderText : MonoBehaviour
{
    public Transform ItemSlot;
    public Transform ItemSlotOrderText;


    void Start()
    {
        ItemSlot = transform.parent;
        ItemSlotOrderText = transform.GetChild(0).GetChild(0);
    }


    // 숫자 업데이트
    public void TextUpdate()
    {
           ItemSlotOrderText.GetComponent<Text>().text= ItemSlot.childCount.ToString();
           if (ItemSlot.childCount == 0)
                ItemSlotOrderText.GetComponent<Text>().text = "0";
    }


    public void SetText(int num)
    {
        ItemSlotOrderText.GetComponent<Text>().text = num.ToString();
    }


    public int SlotOrder()
    {
        return int.Parse(ItemSlotOrderText.GetComponent<Text>().text);
    }


    public void TextUp(Transform SlotNum,int num)
    {
        if (ItemSlot == SlotNum)
        {
            if (ItemSlotOrderText.GetComponent<Text>().text != "0")
                ItemSlotOrderText.GetComponent<Text>().text = (int.Parse(ItemSlotOrderText.GetComponent<Text>().text) + num).ToString();
            else
                ItemSlotOrderText.GetComponent<Text>().text = num.ToString();
        }
    }


    public void TextDown(Transform SlotNum,int num)
    {
        if (ItemSlot == SlotNum)
        {
            if (int.Parse(ItemSlotOrderText.GetComponent<Text>().text) == 1)
                ItemSlotOrderText.GetComponent<Text>().text = "0";
            else
                ItemSlotOrderText.GetComponent<Text>().text = (int.Parse(ItemSlotOrderText.GetComponent<Text>().text) - num).ToString();
        }
    }
}
