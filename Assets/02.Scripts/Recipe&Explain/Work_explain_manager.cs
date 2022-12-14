using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Work_explain_manager : MonoBehaviour
{
   int index = 0;
    Text text;
    List<string> script
    {
        get
        {
            List<string> script1 = new List<string>();
            script1.Add("1. 가공 선택\n\n가공방식을 선택하여 알맞는 재료에 이어주세요.");
            script1.Add("2. 다른 가공 선택 or 리셋\n\n만약 가공을 바꾸거나 선택을 바꿀 때는 reset버튼을 누르세요.");
            return script1;
        }
    }
    VideoPlayer vp;
    // Start is called before the first frame update
    void Start()
    {
        vp = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<VideoPlayer>();
        text = transform.GetChild(0).GetChild(1).GetComponent<Text>();
        
    }
    public void OnOff()
    {
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
   public void next()
    {
        if (script.Count-1 == index)
        {
            return;
        }
        else
        {
            index++;
            updateExplain();
        }
    }
    public void back()
    {
        if (0 == index)
        {
            return;
        }
        else
        {
            index--;
            updateExplain();
        }
    }
    void updateExplain()
    {
        
        text.text = script[index];
        for (int i = 0; i < transform.GetChild(0).GetChild(0).childCount; i++)
        {
            transform.GetChild(0).GetChild(0).GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(0).GetChild(0).GetChild(index).gameObject.SetActive(true);
    }
}
