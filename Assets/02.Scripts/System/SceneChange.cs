using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    void Update()
    {

    }

    //버튼으로 메인화면갈때
    public void GotoPlant()
    {
        // Container_Move.startMove = true;
        FadInOut.instance.BlackOut(1.0f, 0.1f, DataManager.instance.SceneName);
        // SceneManager.LoadScene("MainPlay");
    }
    

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("TeleportToSetting"))
        {
            DataManager.instance.SceneSaveItemTr();
            SoundCtrl.instance.audioChange.SetActive(true);
            DataManager.instance.PlayerX = transform.position.x;
            DataManager.instance.PlayerY = transform.position.y;
            DataManager.instance.PlayerZ = transform.position.z + 3;
            SceneManager.LoadScene("VolumeControl");
        }
        else if (coll.collider.CompareTag("TeleportToWork"))
        {
            DataManager.instance.SceneSaveItemTr();
            DataManager.instance.PlayerX = transform.position.x;
            DataManager.instance.PlayerY = transform.position.y;
            DataManager.instance.PlayerZ = transform.position.z - 3;
            SceneManager.LoadScene("PlayerCoding_Work");
        }
        else if (coll.collider.CompareTag("TeleportToAssemble"))
        {
            DataManager.instance.SceneSaveItemTr();
            DataManager.instance.PlayerX = transform.position.x - 3;
            DataManager.instance.PlayerY = transform.position.y;
            DataManager.instance.PlayerZ = transform.position.z;
            SceneManager.LoadScene("PlayerCoding_Assemble");
        }
        else if (coll.collider.CompareTag("TeleportToPick"))
        {
            DataManager.instance.SceneSaveItemTr();
            DataManager.instance.PlayerX = transform.position.x + 3;
            DataManager.instance.PlayerY = transform.position.y;
            DataManager.instance.PlayerZ = transform.position.z;
            SceneManager.LoadScene("PlayerCoding_Input");
        }
        else if (coll.collider.CompareTag("TeleportToPack"))
        {
            DataManager.instance.SceneSaveItemTr();
            DataManager.instance.PlayerX = transform.position.x - 3;
            DataManager.instance.PlayerY = transform.position.y;
            DataManager.instance.PlayerZ = transform.position.z;
            SceneManager.LoadScene("Packing");
        }else if (coll.collider.CompareTag("TeleportToStorage"))
        {
            DataManager.instance.SceneSaveItemTr();
            DataManager.instance.PlayerX = transform.position.x - 3;
            DataManager.instance.PlayerY = transform.position.y;
            DataManager.instance.PlayerZ = transform.position.z+3;
            SceneManager.LoadScene("Storage");
        }
    }
}
