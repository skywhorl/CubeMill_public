using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click_Move_item : MonoBehaviour
{
    // Material과 플레이어와의 Z축 간격
    float distance = 0;
    //로직들의 연계를 위해 필요한 bool값
    bool isDrag = false;//드래그 중 여부
    bool inHole = false;//빈칸에 들어갔는지 여부
    // 고정될 위치값
    public GameObject hole;
    // isKinematic의 활성화를 위한 컴포넌트 변수
    public Rigidbody rigidbody;
    // collier 자체의 비활성화를 위한 컴포넌트 변수
    public Collider collider;

    // 초기 위치와 회전값을 저장할 변수
    private Vector3 firstPosition;
    private Quaternion firstRotation;

    
    private void Start()
    {
        // 초기 위치와 회전값을 저장
        firstPosition = transform.position;
        firstRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);

        // Material과 카메라의 위치 저장
        distance = 11;
    }


    private void Update()
    {
        // 드래그 상태가 해제되고, 특정 태그가 붙어있는 오브젝트에 닿았을때
        if (isDrag == false && inHole == true)
        {
            // collider 자체를 비활성화 시킴으로써 드래그를 불가하게 만듦
            collider.enabled = false;
            //투명하게 있는 Hole오브젝트에 시각적으로 부딫히지 않도록 isKinematic을 true로 변경
            rigidbody.isKinematic = true;
            //hole오브젝트의 위치값으로 이동
            transform.position = hole.transform.position;
            hole.SetActive(false);
            DataManager.instance.cubename = this.gameObject.name;
            // hole오브젝트를 파괴해 중복방지
            //Destroy(hole);
        }
        // 드래그와 태그 둘다 false일 때
        else if (!inHole && !isDrag)
        {
            //위치 초기화
            transform.position = firstPosition;
            transform.rotation = firstRotation;
        }
    }


    // 해당 오브젝트의 collider를 드래그할 때 호출
    void OnMouseDrag()
    {
        // 로직들의 연계를 위해 bool값 변형
        isDrag = true;
        
        rigidbody.isKinematic = true;
        // 마우스의 위치 값 & 회전값 고정
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        transform.rotation = Quaternion.Euler(0, 0, 0);
        // 원래 장소에서 마우스의 위치값대로 위치 저장
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        // 이동
        transform.position = objPosition;
    }


    // 해당 오브젝트에 마우스를 떼면 호출
    private void OnMouseUp() 
    {
        // 로직들의 연계를 위한 bool값 변형
        isDrag = false;
    }


    // 어떤 collider에 닿았을 경우 호출
    private void OnTriggerEnter(Collider coll)
    {
        // Hole이라 이름 붙은 태그에 닿았을 때
        if (coll.transform.CompareTag("Hole") && !inHole) 
            // 로직들의 연계를 위한 bool값 변형
            inHole = true;
    }

    // 어떤 collider에 닿았다가 떨어질 경우 호출
    private void OnTriggerExit(Collider other)
    {
        // Hole이라 이름붙은 태그에 떨어졌을 때
        if (other.transform.CompareTag("Hole"))
            // 로직들의 연계를 위한 bool값 변형
            inHole = false;
    }
}
