using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class Assemble : MonoBehaviour
{
    public static Assemble instance;
    public List<string> names; // Assemble에 들어와있는 아이템 이름들

    public bool isTrue; // 아이템 일치 확인, 굳이 안써도 되지만 불일치시 for문에서 최대한 빨리 나오게 하기 위해 넣음
    public GameObject resultItem; // 조합 결과 아이템
    public GameObject result; // 조합결과 창
    public Material Desk;
    public Material normal;
    public Material Phone;
    public Material Car;
    public Material Window;
    public Material Tire;

    List<Element_Item> items; // 조합 아이템 목록


    private void Awake()
    {
        instance = this;
        //DataManager.instance.Assembleitem.GetComponent<Container_Move>().startMove = true;
        ItemSetting(); // 아이템 조합법 설정

    }


    public void Start()
    {
        Assemble_Data.instance.LoadData();
    }


    public void Namings()
    {
        names = new List<string>();

        for (int i = 0; i < transform.childCount; i++)
            for (int j = 0; j < transform.GetChild(i).GetComponent<Assemble_ItemListMove>().nowSelectCount; j++)
                names.Add(transform.GetChild(i).name);
    }


    // 아이템 조합 모음 ArrayList
    void ItemSetting()
    {
        items = new List<Element_Item>();

        // 책상 
        ArrayList combination = new ArrayList();
        combination.Add("Wood");
        combination.Add("WoodPlank");
        combination.Add("WoodPlank");
        combination.Add("WoodPlank");
        combination.Add("WoodPlank");
        
        items.Add(new Element_Item("Desk", combination));

        // 휴대폰
        combination = new ArrayList();
        combination.Add("Lithium");
        combination.Add("Plastic");
        combination.Add("Semiconductor");
        items.Add(new Element_Item("Phone", combination));

        // 창문1
        combination = new ArrayList();
        combination.Add("Glass");
        combination.Add("Plastic");
        items.Add(new Element_Item("Window", combination));

        // 창문2
        combination = new ArrayList();
        combination.Add("Glass");
        combination.Add("WoodPlank");
        items.Add(new Element_Item("Window", combination));

        // 타이어
        combination = new ArrayList();
        combination.Add("Iron");
        combination.Add("Rubber");
        items.Add(new Element_Item("Tire", combination));

        // 자동차
        combination = new ArrayList();
        combination.Add("Lithium");
        combination.Add("Glass");
        combination.Add("Plastic");
        combination.Add("Tire");
        combination.Add("Iron");
        items.Add(new Element_Item("Car", combination));

        // 테스트 용도
        combination = new ArrayList();
        combination.Add("Wood");
        combination.Add("Wood");
        combination.Add("WoodPlank");
        combination.Add("WoodPlank");
        items.Add(new Element_Item("normal", combination));
    }


    public void ResultCheck()
    {
        isTrue = false;

        // 아이템 조합 갯수만큼
        for (int i = 0; i < items.Count; i++)
        {
            // 현재 조합아이템 받아오기
            Element_Item nowAssemble = items[i];

            // 아이템 갯수와 조합 아이템에 필요한  아이템 갯수가 같을 때만 실행되게
            if (names.Count == nowAssemble.Length())
            {
                // 조합 아이템의 필요한 아이템 갯수
                for (int j = 0; j < nowAssemble.Length(); j++)
                {

                    // Assemble 아이템 갯수만큼 반복
                    for (int k = 0; k < names.Count; k++)
                    {
                        // 만약 아이템의 이름과 필요한 아이템의 이름이 같으면
                        if (names[k] == (string)nowAssemble.GetItems()[j])
                        {
                            isTrue = true; // 일치
                            names.RemoveAt(k); // 중복을 피하기위해 일치한 아이템은 목록에서 삭제

                            break; // 다음으로 필요아이템 체크
                        }
                        else
                        {
                            isTrue = false; // 불일치
                        }
                    }

                    // 불일치 아이템이 나오면
                    if (!isTrue)
                    {
                        Namings();
                        break; // 다음 아이템 조합법으로
                    }
                }

                // 만약 조합법이랑 모두 일치하면
                if (names.Count == 0) // if(isTrue==true)도 가능
                {
                    // 그 조합법의 아이템이름에 따라 분배
                    switch (nowAssemble.GetName().ToString())
                    {
                        case "Desk":
                            GameObject MachineResultItem = Instantiate(resultItem, result.transform);
                            MachineResultItem.name = "Desk";
                            MachineResultItem.GetComponent<MeshRenderer>().material = Desk;
                            break;

                        case "normal":
                            result.GetComponent<MeshRenderer>().material = normal;
                            break;

                        case "Phone":
                            MachineResultItem = Instantiate(resultItem, result.transform);
                            MachineResultItem.name = "Phone";
                            MachineResultItem.GetComponent<MeshRenderer>().material = Phone;
                            break;

                        case "Window":
                            MachineResultItem = Instantiate(resultItem, result.transform);
                            MachineResultItem.name = "Window";
                            MachineResultItem.GetComponent<MeshRenderer>().material = Window;
                            break;

                        case "Tire":
                            MachineResultItem = Instantiate(resultItem, result.transform);
                            MachineResultItem.name = "Tire";
                            MachineResultItem.GetComponent<MeshRenderer>().material = Tire;
                            break;

                        case "Car":
                            MachineResultItem = Instantiate(resultItem, result.transform);
                            MachineResultItem.name = "Car";
                            MachineResultItem.GetComponent<MeshRenderer>().material = Car;
                            break;

                        default:
                            for (int k = 0; k < result.transform.childCount; k++)
                                Destroy(result.transform.GetChild(k).gameObject);

                            result.GetComponent<MeshRenderer>().material = normal;
                            UI_QuestOrCheck.isComplete_assemble = false;
                            break;
                    }

                    UI_QuestOrCheck.isComplete_assemble = true;
                    break; // 아이템이 생성 됐음으로 나가기
                }
            }
        }

        // 만약 불일치
        if (!isTrue)
        {
            for (int i = 0; i < result.transform.childCount; i++)
                Destroy(result.transform.GetChild(i).gameObject);
            
            result.GetComponent<MeshRenderer>().material = normal;
            UI_QuestOrCheck.isComplete_assemble = false;
        }

    }


    public string GetResultItemName()
    {
        try
        {
            string result = GameObject.Find("Result").transform.GetChild(0).name;
            Assemble_ItemSelectCountChanger[] Assemble_Item = gameObject.transform.GetComponentsInChildren<Assemble_ItemSelectCountChanger>();

            for (int i = 0; i < Assemble_Item.Length; i++)
            {
                Assemble_Item[i].Maxium = 1;
                Assemble_Item[i].nowSelectItemCount = 0;
                Assemble_Item[i].UpdateItemNum();
            }

            Debug.Log("Success");
            return result;
        }

        catch (Exception e)
        {
            Debug.Log("faiil");
            return null;
        }
    }
}
