using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item_Move : MonoBehaviour
{
    Vector3 Pos;
    GameObject thisparent;

    private void Awake()
    {
        Pos = this.transform.position;
        thisparent = this.transform.parent.gameObject;
    }
    void OnMouseDrag()
    {
      
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        // 원래 장소에서 마우스의 위치값대로 위치 저장
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // 이동
        transform.position = objPosition;
    }

    private void OnMouseUp()
    {
        if(this.transform.parent == thisparent.transform)
        {
            this.transform.position = Pos;
        }      
    }

    private void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Container_1")
        {
            transform.parent = coll.gameObject.transform;
        }
        else if (coll.tag == "Container_2")
        {
            transform.parent = coll.gameObject.transform;
        }
        else if (coll.tag == "Container_3")
        {
            transform.parent = coll.gameObject.transform;
        }
    }
    // 어떤 collider에 닿았다가 떨어질 경우 호출
    private void OnTriggerExit(Collider other)
    {
        transform.parent = thisparent.transform;
    }
}
