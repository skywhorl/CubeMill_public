using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Assemble_ItemSelectCountChanger : MonoBehaviour
{
    GameObject ParentObj;
    public int Maxium;
    public int nowSelectItemCount;


    private void Awake()
    {
        ParentObj = transform.parent.gameObject;
        Maxium = int.Parse(ParentObj.GetComponent<Text>().text);
        nowSelectItemCount = int.Parse(transform.GetChild(0).GetComponent<Text>().text);
    }


    private void Update()
    {
        // 나중에 지우세요^^
        if (Input.GetKeyDown(KeyCode.A))
        {
            UpNum(1);
        }
    }


    public void UpButton()
    {
            Maxium++;
            transform.parent.GetComponent<Text>().text = Maxium.ToString();
    }
    public void DownButton()
    {
        if (Maxium > 0&&Maxium!=nowSelectItemCount)
        {
            Maxium--;
            transform.parent.GetComponent<Text>().text = Maxium.ToString();
        }
    }


    public void UpNum(int num)
    {
        if (nowSelectItemCount >= Maxium)
        {

        }
        else
        {
            nowSelectItemCount += num;
            transform.GetChild(0).GetComponent<Text>().text = nowSelectItemCount.ToString();
        }
        UpdateItemNum();
    }


    public void UpdateItemNum()
    {
        transform.parent.parent.parent.GetComponent<Assemble_ItemListMove>().nowSelectCount = nowSelectItemCount;
        transform.parent.GetComponent<Text>().text = Maxium.ToString();
        transform.GetChild(0).GetComponent<Text>().text = nowSelectItemCount.ToString();

        // 결과값 변경을 위해서
        Assemble.instance.Namings();
        Assemble.instance.ResultCheck();
    }


    public void DownNum(int num)
    {
        nowSelectItemCount -= num;
        transform.GetChild(0).GetComponent<Text>().text = nowSelectItemCount.ToString();
        UpdateItemNum();
    }


    public void SetNum(int num)
    {
        transform.GetChild(0).GetComponent<Text>().text = num.ToString();
    }
}
