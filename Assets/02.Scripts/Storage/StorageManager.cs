using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StorageManager : MonoBehaviour
{
    [SerializeField] Text Name;
    [SerializeField] Text Count;
    [SerializeField] Text ExText;
    [SerializeField] SpriteRenderer Result;
    [SerializeField] List<Sprite> sprite;
    [SerializeField] AudioClip clip;
    int dataint =0;
    // Start is called before the first frame update
    void Start()
    {
        dataint = 0;
        CheckData();
    }

    public void CheckData()
    {
        if(DataManager.instance.boxdata.Count > 0)
        {
            Name.text = DataManager.instance.boxdata[dataint].name;
            Count.text = DataManager.instance.boxdata[dataint].counts.ToString() + " 개";
            if (DataManager.instance.boxdata[dataint].name == "Desk-Box")
            {
                Result.sprite = sprite.Find(x => x.name == "책상");
                ExText.text = "책상입니다. 박스로 포장했어요!";
            }
            else if (DataManager.instance.boxdata[dataint].name == "Desk-PlastickPack")
            {
                Result.sprite = sprite.Find(x => x.name == "책상");
                ExText.text = "책상입니다. 플라스틱으로 포장했어요!";
            }
            else if (DataManager.instance.boxdata[dataint].name == "Desk-StyrofoamPack")
            {
                Result.sprite = sprite.Find(x => x.name == "책상");
                ExText.text = "책상입니다. 스티로폼으로 포장했어요!";
            }
            else if(DataManager.instance.boxdata[dataint].name == "Window-Box")
            {
                Result.sprite = sprite.Find(x => x.name == "창문");
                ExText.text = "창문입니다. 박스로 포장했어요!";
            }else if(DataManager.instance.boxdata[dataint].name == "Window-PlastickPack")
            {
                Result.sprite = sprite.Find(x => x.name == "창문");
                ExText.text = "창문입니다. 플라스틱으로 포장했어요!";
            }
            else if (DataManager.instance.boxdata[dataint].name == "Window-StyrofoamPack")
            {
                Result.sprite = sprite.Find(x => x.name == "창문");
                ExText.text = "창문입니다. 스티로폼으로 포장했어요!";
            }
        }        
    }

    public void Rightbutton()
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        dataint++;
        if(dataint == DataManager.instance.boxdata.Count)
        {
            dataint = 0;
        }
        CheckData();
    }
    public void LeftButton()
    {
        Debug.Log("눌렸어");
        SoundCtrl.instance.SoundEffectPlay(clip);
        dataint--;
        if(dataint == -1)
        {
            dataint = DataManager.instance.boxdata.Count - 1;
        }
        CheckData();
    }

    public void XButton()
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        SceneManager.LoadScene("MainPlay");
    }
}
