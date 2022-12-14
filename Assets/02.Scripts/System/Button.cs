using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
public class Button : MonoBehaviour
{
    [SerializeField]AudioClip clip;
    
    public void OnSave()
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        SaveNLoad.Save();
    }
    public void OnLoad()
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        SaveNLoad.Load();
    }

    public void Xbutton()
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        SoundCtrl.instance.audioChange.SetActive(false);
        SceneManager.LoadScene("MainPlay");
        
    }
}
