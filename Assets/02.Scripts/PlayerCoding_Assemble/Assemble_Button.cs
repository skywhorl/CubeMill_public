using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Assemble_Button : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    public void reset()
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        Assemble_Data.instance.Reset();
    }


    public void save()
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        Assemble_Data.instance.Save();
        DataManager.instance.SampleBoolen = true;
    }


    public void Update()
    {
        // 나중에 키 입력은 지우세요^^
        if (Input.GetKeyDown(KeyCode.W)){
            //SceneManager.LoadScene("PlayerCoding_Assemble");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Assemble_Data.instance.Save();
        }
        if (Input.GetKeyDown(KeyCode.R)){
            Assemble.instance.GetResultItemName();
        }
    }
}
