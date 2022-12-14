using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MachineCheckManger : MonoBehaviour
{
   public  static MachineCheckManger instance;
    public bool[] MachineCheck;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        SceneManager.sceneLoaded += OnSceneLoaded;
        MachineCheck = new bool[4];
        
}
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "PlayerCoding_Input":
                MachineCheck[0] = true;
                break;
            case "PlayerCoding_Work":
                MachineCheck[1] = true;
                break;
            case "PlayerCoding_Assemble":
                MachineCheck[2] = true;
                break;
            case "Packing":
                MachineCheck[3] = true;
                break;

        }
        Debug.Log("OnSceneLoaded: " + scene.name);
        
    }
}
