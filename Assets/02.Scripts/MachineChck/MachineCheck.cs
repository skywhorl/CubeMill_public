using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineCheck : MonoBehaviour
{
   
    public List<Transform> MachineList;
    List<Text> MachineListCheck;
    public bool[] MachineBool;
    // Start is called before the first frame update
    void Start()
    {
        
         MachineBool = MachineCheckManger.instance.MachineCheck;
        MachineListCheck = new List<Text>();
        for (int i = 0; i < MachineList.Count; i++)
        {
            MachineListCheck.Add(MachineList[i].GetChild(0).GetComponent<Text>());
            
            if (MachineBool[i] == true)
            {
                machineCheck(i);
            }
            
        }
    }
    void machineCheck(int num)
    {
        Debug.Log("a");
        MachineList[num].GetComponent<Image>().color = Color.green;
        MachineListCheck[num].text = "✔";
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            MachineList[0].GetComponent<Image>().color = Color.red;
            MachineListCheck[0].text = "X";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            MachineList[1].GetComponent<Image>().color = Color.red;
            MachineListCheck[1].text = "X";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            MachineList[2].GetComponent<Image>().color = Color.red;
            MachineListCheck[2].text = "X";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            MachineList[3].GetComponent<Image>().color = Color.red;
            MachineListCheck[3].text = "X";
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            MachineList[0].GetComponent<Image>().color = Color.green;
            MachineListCheck[0].text = "✔";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            MachineList[1].GetComponent<Image>().color = Color.green;
            MachineListCheck[1].text = "✔";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            MachineList[2].GetComponent<Image>().color = Color.green;
            MachineListCheck[2].text = "✔";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            MachineList[3].GetComponent<Image>().color = Color.green;
            MachineListCheck[3].text = "✔";
        }
    }
}
