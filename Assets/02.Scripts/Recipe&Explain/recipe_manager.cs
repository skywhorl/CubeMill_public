using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class recipe_manager : MonoBehaviour
{
    int index;
    public List<Sprite> imgs;
    Image Co_Img;
    private void Start()
    {
        Co_Img = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
    }
    public void OnOff()
    {
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    public void next()
    {
        if (imgs.Count - 1 == index)
        {
            return;
        }
        else
        {
            index++;
            updateImg();
        }
    }
    public void back()
    {
        if (0 == index)
        {
            return;
        }
        else
        {
            index--;
            updateImg();
        }
    }
    void updateImg()
    {
        Co_Img.sprite = imgs[index];
    }
}
