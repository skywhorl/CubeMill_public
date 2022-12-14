using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStart : MonoBehaviour
{
    public static SpawnStart instance;
    [SerializeField] AudioClip clip;
    public GameObject Coll;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        if (DataManager.instance.itemsOnContainer.Count > 0)
            DataManager.instance.SceneLoadItemTr();

        if (DataManager.instance.startSpawn.Equals(true))
        {
            //DataManager.instance.CubeActive();
            UI_QuestOrCheck.isComplete_pick = true;
            //DataManager.instance.startSpawn = false;
        }
    }

    private void Update()
    {
        if (DataManager.instance.startSpawn == true)
        {
            if (DataManager.instance.Workitem == null)
            {
                if (DataManager.instance.timer <= 0)
                {
                    SoundCtrl.instance.SoundEffectPlay(clip);
                    SpawnCubes();
                    DataManager.instance.timer = 2;
                }
                else
                {
                    DataManager.instance.timer -= Time.deltaTime;
                }
            }
        }
    }

    void SpawnCubes()
    {
        DataManager.instance.CubeActive();
    }
}
