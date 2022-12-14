using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PackageManager : MonoBehaviour
{
    public static PackageManager instance;
    [SerializeField]List<GameObject> Slots;
    [SerializeField] GameObject SlotParent;
    [SerializeField] GameObject BoxPacks;
    [SerializeField] GameObject StyrofoamPacks;
    [SerializeField] GameObject PlasticPacks;
    [SerializeField] AudioClip clip;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < DataManager.instance.SlotVect.Count; i++) //저장된 값
        {
            Slots[DataManager.instance.Slotint[i]].transform.position = DataManager.instance.SlotVect[i];
            
        }
    }

    public void Saves() //저장하고 나가기
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        Resets();
        for (int i = 0; i < Slots.Count; i++)
        {
            if(Slots[i].transform.parent != SlotParent.transform)
            {
                DataManager.instance.Slotint.Add(i);
                DataManager.instance.SlotVect.Add(Slots[i].transform.position);
                if(Slots[i].transform.parent == BoxPacks.transform)
                {
                    DataManager.instance.boxPack.Add(Slots[i].name);
                }else if (Slots[i].transform.parent == StyrofoamPacks.transform)
                {
                    DataManager.instance.styrofoamPack.Add(Slots[i].name);
                }else if(Slots[i].transform.parent == PlasticPacks.transform)
                {
                    DataManager.instance.PlastickPack.Add(Slots[i].name);
                }
            }
        }
        SceneManager.LoadScene("MainPlay");
    }

    public void Resets()  //리셋
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        DataManager.instance.Slotint.Clear();
        DataManager.instance.SlotVect.Clear();
        DataManager.instance.boxPack.Clear();
        DataManager.instance.styrofoamPack.Clear();
        DataManager.instance.PlastickPack.Clear();
        SceneManager.LoadScene("Packing");
    }

    public void XButton()
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        SceneManager.LoadScene("MainPlay");
    }
}
