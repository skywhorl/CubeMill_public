using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Assemble_ItemListMove : MonoBehaviour
{
    // 플레이어와의 Z축 간격
    float distance = 0;
    public GameObject ListItem; // 아이템 슬롯묶음
    public GameObject assemble; // 조합 부분

    // 로직들의 연계를 위해 필요한 bool값
    public bool isDrag = false;
    public  bool inHole = false;

    // isKinematic의 활성화를 위한 컴포넌트 변수
    public Rigidbody rigidbody;

    // 초기 위치와 회전값을 저장할 변수
    private Vector3 firstPosition;
    private Quaternion firstRotation;

    public List<Transform> ItemSlot; // 아이템 슬롯들 목록
    public int nowSelectCount; // 현재 들어온 값
    public Assemble_ItemSlotOrderText Assemble_ItemSlotOrderText; // 넣을 값 조절

    
    private void Start()
    {
        ListItem = GameObject.Find("ListItemSlot");
        assemble = GameObject.Find("Assemble");
        rigidbody = gameObject.GetComponent<Rigidbody>();
        Assemble_ItemSlotOrderText = gameObject.GetComponent<Assemble_ItemSlotOrderText>();

        //아이템슬롯 찾아서 넣어주기
        Transform ListItemSlot = GameObject.Find("ListItemSlot").transform;
            for (int i = 0; i < ListItemSlot.childCount; i++)
                ItemSlot.Add(ListItemSlot.GetChild(i).GetComponent<Transform>());
            
        // 초기 위치와 회전값을 저장
        firstPosition = transform.position;
        firstRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);

        // Material과 카메라의 위치 저장
        distance = gameObject.transform.position.z;

        

    }


   
    

    private void OnMouseDown()
    {
        //조합이 아니라면
        if (transform.parent.name != "Assemble")
            //현재 들어온 갯수 업데이트
            nowSelectCount = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Assemble_ItemSelectCountChanger>().nowSelectItemCount;
    }


    //해당 오브젝트의 Collider를 드래그할 때 호출
    void OnMouseDrag()
    {
        //고정이 아니라면
        /*
             for (int i = 0; i < assemble.transform.childCount; i++)
             {
             Debug.Log("b");
                 Destroy(assemble.transform.GetChild(i).gameObject);
             }
         */
        // 로직들의 연계를 위해 bool값 변형
        isDrag = true;
        rigidbody.isKinematic = true;

        // 마우스의 위치 값 & 회전값 고정
        Vector2 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        transform.rotation = Quaternion.identity;

        // 원래 장소에서 마우스의 위치값대로 위치 저장
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(new Vector2(mousePosition.x,mousePosition.y));

        // 이동
        transform.position = new Vector3(objPosition.x,objPosition.y,transform.position.z);
    }


    //해당 오브젝트에 마우스를 떼면 호출
    private void OnMouseUp()
    {
        CheckInHole();
        // 로직들의 연계를 위한 bool값 변형
        isDrag = false;

        // 드래그와 태그 둘다 false일 때
        if (!inHole && !isDrag)
            StartCoroutine(resetItemPosition()); //아이템 제자리로 돌아가기
        else if(inHole && !isDrag)
            StartCoroutine(inHoleItemPosition()); // 아이템 처음자리 재설정
    }


    private void CheckInHole()
    {
        // 터치 좌표를 담는 변수
        RaycastHit hit;
        // 터치한 곳에 ray를 보냄
        Physics.Raycast(transform.position, transform.forward, out hit);

        //Hole이라 이름붙은 태그에 닿았을 때
        if (hit.transform.CompareTag("Hole"))
        {
            // 로직들의 연계를 위한 bool값 변형
            inHole = true;
            
            transform.parent = assemble.transform;
            Assemble.instance.Namings();
            Assemble.instance.ResultCheck();
        }
        else
            inHole = false;
    }


    //아이템 처음자리 재설정
    IEnumerator inHoleItemPosition()
    {
        yield return new WaitForFixedUpdate();

        if (inHole && !isDrag) 
            firstPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, firstPosition.z);
        if (transform.parent.parent == ListItem.transform)
        { 
            // 결과값 변경을 위해서
            Assemble.instance.Namings();
            Assemble.instance.ResultCheck();
        }
    }


    IEnumerator resetItemPosition()
    {
        //동시에 진행되면 isDrag가 변경된후에 작동이 안되는 경우가 있어서 1업데이트 기다림
        yield return new WaitForFixedUpdate();

        if (!inHole && !isDrag)
        {
            // 터치 좌표를 담는 변수
            RaycastHit hit;
            // 터치한 곳에 ray를 보냄 
            Physics.Raycast(transform.position,transform.forward, out hit);

            // 이미 같은 이름의 재료가 있으면 겹치기
            if (hit.collider.name == transform.name)
            {
                firstPosition = new Vector3(hit.collider.transform.position.x, hit.collider.transform.position.y, firstPosition.z);
                transform.parent = hit.collider.transform.parent;
                hit.transform.GetComponent<Assemble_ItemListMove>().Assemble_ItemSlotOrderText.TextUp(transform.parent, nowSelectCount);
                Destroy(this.gameObject);
            }
            // 아니면 그 슬롯이 빈슬롯인지 확인하고 위치 저장
            else
            {
                bool isInItemSlot = false;
                bool isInItemSlotIsItem = false;
                if (transform.parent.childCount > 1) 
                    isInItemSlotIsItem = true;

                // ray가 오브젝트에 부딪힐 경우 
                for (int i = 0; i < ItemSlot.Count; i++)
                {
                    if (hit.collider == ItemSlot[i].GetComponent<Collider>())
                    {
                        isInItemSlot = true;
                        firstPosition = new Vector3(ItemSlot[i].position.x, ItemSlot[i].position.y, firstPosition.z);
                        transform.parent = ItemSlot[i].transform;
                    }
                }

                if (!isInItemSlot && isInItemSlotIsItem)
                {
                    transform.parent.GetChild(1).transform.GetComponent<Assemble_ItemListMove>().Assemble_ItemSlotOrderText.TextUp(transform.parent, nowSelectCount);
                    yield return new WaitForFixedUpdate();
                    //Destroy(this.gameObject);
                }
            }
            yield return new WaitForFixedUpdate();

            //위치 초기화
            transform.position = firstPosition;
            transform.rotation = firstRotation;
        }
    }


    //어떤 collider에 닿았을 경우 호출
    private void OnTriggerStay(Collider coll)
    {
        /*
            if (coll.transform.CompareTag("Hole") && inHole == false&&isDrag==false)
            //Hole이라 이름붙은 태그에 닿았을 때...
            {
             
                inHole = true;
                //로직들의 연계를 위한 bool값 변형
                transform.parent = assemble.transform;
                Assemble.instance.Namings();
                Assemble.instance.ResultCheck();
            }
           
          */
        // Hole이라 이름붙은 태그에 닿았을 때
        if (coll.transform.CompareTag("Hole"))
        {
            //로직들의 연계를 위한 bool값 변형
            inHole = true;

            transform.parent = assemble.transform;
            Assemble.instance.Namings();
            Assemble.instance.ResultCheck();
        }
    }


    // 어떤 collider에 닿았다가 떨어질 경우 호출
    private void OnTriggerExit(Collider other)
    {
        // Hole이라 이름붙은 태그에 떨어졌을 때
        if (other.transform.CompareTag("Hole"))
            // 로직들의 연계를 위한 bool값 변형
            inHole = false;
    }


    public void UpdateItemNum()
    {
        transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Assemble_ItemSelectCountChanger>().UpdateItemNum();
    }
}
