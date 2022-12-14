using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : MonoBehaviour
{
    public static ObjPool instance;
    public GameObject poolobj; //프리팹 생성할것

    public GameObject play_obj; //빈게임오브젝트(?)

    public int poolAmount_0 = 10; //0번째를 생성할 양
    public List<GameObject> poolobj_0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

    }
    private void Start()
    {
        poolobj_0 = new List<GameObject>();

        for (int i = 0; i < poolAmount_0; i++)
        {
            GameObject obj_0 = Instantiate(DataManager.instance.itemPrefeb[0]);
            obj_0.transform.parent = play_obj.transform;
            obj_0.name = "Clone" + i;
            obj_0.SetActive(false);
            poolobj_0.Add(obj_0);
            DataManager.instance.rot = obj_0.transform.eulerAngles;
        }

    }

    public GameObject GetPooledObj()
    {
        for (int i = 0; i < poolobj_0.Count; i++)
        {
            if (!poolobj_0[i].activeInHierarchy) //obj.setactive가 false이면 실행
            {
                return poolobj_0[i];
            }
        }
        return null;
    }
}
