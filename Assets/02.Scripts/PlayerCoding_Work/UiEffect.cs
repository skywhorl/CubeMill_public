using UnityEngine;

public class UiEffect : MonoBehaviour
{
    public static UiEffect instance;
    private GameObject Previous;         //이전에 눌린 오브젝트
    private GameObject Clickbutton; //현재 눌린 오브젝트 받아줄 변수
    public GameObject[] buttons;    //효과를 줄 버튼들 밖에서 정해두기.
    [HideInInspector]
    public bool effectbool;


    private void Awake()
    {
        instance = this;
    }


    public void ButtonUIEffect(GameObject hit)
    {
        // 선택된 오브젝트를 받아옴
        Clickbutton = hit;
        // 완전 처음 버튼을 눌렀을 때, 혹은 현재 버튼이랑 이전버튼이랑 같은지 비교
        if (Clickbutton != Previous || Previous == null) 
        {
            // 눌린버튼이 색상을 변경가능하게 할 오브젝트인지 확인
            for (int i = 0; i < buttons.Length; i++) 
            {
                // 빨간색으로 표시
                if (buttons[i].Equals(Clickbutton))
                {
                    Clickbutton.GetComponent<MeshRenderer>().material.color = Color.red; 
                    break;
                }
            }
        }
        if (effectbool.Equals(false))
        {
            if (Previous != Clickbutton && Previous != null) //이전 버튼은 흰색으로 변경
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    if (buttons[i].Equals( Previous))
                    {
                        if (Previous.GetComponent<MeshRenderer>().material.color == Color.red)
                        {
                            Previous.GetComponent<MeshRenderer>().material.color = Color.white;
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            effectbool = false;
        }
        Previous = Clickbutton;
    }
}
