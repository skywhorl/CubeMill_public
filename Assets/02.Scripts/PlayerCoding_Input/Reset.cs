using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public static Reset instance;
    [SerializeField] List<GameObject> Slots;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < DataManager.instance.InputSlot.Count; i++) //저장된 값
        {
            Slots[DataManager.instance.InputSlortint[i]].transform.position = DataManager.instance.InputSlot[i];
        }
    }

    public void SaveandQuit()
    {
        if (DataManager.instance.startSpawn == true)
        {
            for (int i = 0; i < Slots.Count; i++)
            {
                DataManager.instance.InputSlortint.Add(i);
                DataManager.instance.InputSlot.Add(Slots[i].transform.position);
            }
            SceneManager.LoadScene("MainPlay");
        }
        else
        {
            SceneManager.LoadScene("MainPlay");
        }
    }

    // 씬 이동을 통해 위치 초기화
    public void PositionReset()
    {
        DataManager.instance.startSpawn = false;
        DataManager.instance.InputSlortint.Clear();
        DataManager.instance.InputSlot.Clear();
        SceneManager.LoadScene("PlayerCoding_Input");
    }
}
