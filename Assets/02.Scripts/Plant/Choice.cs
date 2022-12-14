using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Choice : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    public Dialogue dialogue;
    public GameObject hole;
    public void topCreate()
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        if (hole.activeSelf.Equals(false))
        {
            Debug.Log("들어왔어");
            DataManager.instance.startPos = new Vector3(-8.78f, 6.44f, -21.11f);
            DataManager.instance.startSpawn = true;
        }
       
    }

    public void bottomCreate()
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        if (hole.activeSelf.Equals(false))
        {
            Debug.Log("들어왔어");
            DataManager.instance.startPos = new Vector3(-8.08f, 2.6f, -21f);
            DataManager.instance.startSpawn = true;
        }
    }

    public void Explanation()
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        DialogueManager.instance.ShowDialogue(dialogue);
    }

    public void Xbutton()
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        SceneManager.LoadScene("MainPlay");
    }
}
