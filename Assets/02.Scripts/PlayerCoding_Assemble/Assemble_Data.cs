using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Assemble_Data : MonoBehaviour
{
    public List<Vector3> InHole_ItemPos;
    public List<string> InHole_name;
    public List<int> InHole_ItemMaxium;
    public List<int> InHole_ItemNow;
    public static Assemble_Data instance;
    string result= null;

    List<Element_Item> items; // 조합 아이템 목록

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        ItemSetting();
        DontDestroyOnLoad(this);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            UpNum("Wood");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            UpNum("WoodPlank");
        }
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

    public void Save()
    {
        InHole_ItemPos = new List<Vector3>();
        InHole_name = new List<string>();
        InHole_ItemMaxium = new List<int>();
        InHole_ItemNow = new List<int>();
        GameObject[] Assemble_Item= GameObject.FindGameObjectsWithTag("Assemble_Item");
        for (int i = 0; i < Assemble_Item.Length; i++)
        {
            if (Assemble_Item[i].transform.GetComponent<Assemble_ItemListMove>().inHole == true)
            {
                InHole_ItemPos.Add(Assemble_Item[i].transform.position);
                InHole_name.Add(Assemble_Item[i].transform.name);
                InHole_ItemMaxium.Add(Assemble_Item[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Assemble_ItemSelectCountChanger>().Maxium);
                InHole_ItemNow.Add(Assemble_Item[i].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Assemble_ItemSelectCountChanger>().nowSelectItemCount);
            }
        }

        SceneManager.LoadScene("MainPlay");
        Debug.Log("save");
    }


   public void LoadData()
    {
        for (int i = 0; i < InHole_ItemPos.Count; i++)
        {
            GameObject InHole_Item = GameObject.Find(InHole_name[i]);
            InHole_Item.transform.GetComponent<Assemble_ItemListMove>().inHole = true;
            InHole_Item.transform.position = InHole_ItemPos[i];
            InHole_Item.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Assemble_ItemSelectCountChanger>().Maxium = InHole_ItemMaxium[i];
            InHole_Item.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Assemble_ItemSelectCountChanger>().nowSelectItemCount = InHole_ItemNow[i];
            
            InHole_Item.transform.parent = GameObject.Find("Assemble").transform;
            InHole_Item.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Assemble_ItemSelectCountChanger>().UpdateItemNum();
        }

        Debug.Log("Load");
    }


    public void Reset()
    {
        InHole_ItemPos = new List<Vector3>();
        InHole_name = new List<string>();
        InHole_ItemMaxium = new List<int>();
        InHole_ItemNow = new List<int>();
        SceneManager.LoadScene("PlayerCoding_Assemble");
    }

    public void UpNum(string name)
    {
        for (int i = 0; i < InHole_name.Count; i++)
        {
            if (InHole_name[i] == name)
            {
                if(InHole_ItemMaxium[i]> InHole_ItemNow[i])
                {
                    InHole_ItemNow[i]++;
                }
            }
        }
        CheckResult();
    }
    public bool isTrue;
    public List<string> name = new List<string>();
    private void CheckResult()
    {
        isTrue = false;
        name = new List<string>();
        for (int i = 0; i < InHole_name.Count; i++)
        {
            for (int j = 0; j < InHole_ItemNow[i]; j++)
            {
                name.Add(InHole_name[i]);
            }
        }
        

        // 아이템 조합 갯수만큼
        for (int i = 0; i < items.Count; i++)
        {
            // 현재 조합아이템 받아오기
            Element_Item nowAssemble = items[i];

            // 아이템 갯수와 조합 아이템에 필요한  아이템 갯수가 같을 때만 실행되게
            if (name.Count == nowAssemble.Length())
            {
                // 조합 아이템의 필요한 아이템 갯수
                for (int j = 0; j < nowAssemble.Length(); j++)
                {

                    // Assemble 아이템 갯수만큼 반복
                    for (int k = 0; k < name.Count; k++)
                    {
                        
                        // 만약 아이템의 이름과 필요한 아이템의 이름이 같으면
                        if (name[k] == (string)nowAssemble.GetItems()[j])
                        {
                            isTrue = true; // 일치
                            name.RemoveAt(k); // 중복을 피하기위해 일치한 아이템은 목록에서 삭제
                          
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
                        name = new List<string>();
                        
                        for (int p = 0; p < InHole_name.Count; p++)
                        {
                            for (int l = 0; l < InHole_ItemNow[p]; l++)
                            {
                                name.Add(InHole_name[p]);
                            }
                        }
                        break; // 다음 아이템 조합법으로
                    }
                }

                // 만약 조합법이랑 모두 일치하면
                if (name.Count == 0) // if(isTrue==true)도 가능
                {
                    // 그 조합법의 아이템이름에 따라 분배
                    switch (nowAssemble.GetName().ToString())
                    {
                        case "Desk":
                            result = "Desk";
                            break;

                        case "normal":
                            result = "normal";
                            break;

                        case "Phone":
                            result = "Phone";
                            break;

                        case "Window":
                            result = "Window";
                            break;

                        case "Tire":
                            result = "Tire";
                            break;

                        case "Car":
                            result = "Car";
                            break;

                        default:
                            result = null;
                            break;
                    }
                    for (int x = 0; x < InHole_name.Count; x++)
                    {
                        InHole_ItemNow[x] = 0;
                    }
                    
                    break; // 아이템이 생성 됐음으로 나가기
                }
            }
        }

        // 만약 불일치
        if (!isTrue)
        {
            result = null;
        }
        
    }
    public string GetResult()
    {
        return result;
    }
}
