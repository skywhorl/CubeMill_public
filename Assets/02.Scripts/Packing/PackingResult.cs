using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackingResult : MonoBehaviour
{
    [SerializeField] GameObject boxPack;
    [SerializeField] GameObject plasticbagPack;
    [SerializeField] GameObject StyrofoamPack;
    int count =0;
    private void PackResult()
    {
        if (boxPack.transform.childCount>=1)
        {
            looping(boxPack);
        }
        if (plasticbagPack.transform.childCount >= 1)
        {
            looping(plasticbagPack);
        }
        if (StyrofoamPack.transform.childCount >= 1)
        {
            looping(StyrofoamPack);
        }
    }
    void looping(GameObject obj)
    {
        count = obj.transform.childCount;
        UI_QuestOrCheck.isComplete_packing = true;
        //for (int i = 0; i < count; i++)
        //{
        //    //Debug.Log(obj.transform.GetChild(i).name +"이당");
        //}                         
    }
    private void Update()
    {
        PackResult();
    }
}
