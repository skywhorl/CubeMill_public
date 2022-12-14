using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_QuestOrCheck : MonoBehaviour
{
    public Image[] pannels = new Image[8];

    public static bool isComplete_pick = false;
    public static bool isComplete_work = false;
    public static bool isComplete_assemble = false;
    public static bool isComplete_packing = false;


    void Start()
    {
        pannels[1].enabled = false;
        pannels[3].enabled = false;
        pannels[5].enabled = false;
        pannels[7].enabled = false;
        pannels[0].enabled = true;
        pannels[2].enabled = true;
        pannels[4].enabled = true;
        pannels[6].enabled = true;
    }


    void Update()
    {
        if (isComplete_pick)
        {
            pannels[0].enabled = false;
            pannels[1].enabled = true;
        }
        if (isComplete_work)
        {
            pannels[2].enabled = false;
            pannels[3].enabled = true;
        }
        if (isComplete_assemble)
        {
            pannels[4].enabled = false;
            pannels[5].enabled = true;
        }
        if (isComplete_packing)
        {
            pannels[6].enabled = false;
            pannels[7].enabled = true;
        }
    }
}
