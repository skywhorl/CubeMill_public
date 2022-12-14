using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LineDraw : MonoBehaviour
{
    public GameObject[] leftButton;  //왼쪽 버튼 3개 
    public GameObject[] rightButton; //오른쪽 버튼 3개(가운데)
    private GameObject fristButton;  //첫번째로 선택된 버튼
    private GameObject secondButton; //두번째로 선택된 버튼
    private bool overlap = false;  // 라인이 겹치지 않게 만들어둠
    private bool checking = false; //이상한 버튼 눌렸을 경우
    private LineRenderer line;     //선그리기 위함

    private List<int> currlist;     //왼쪽 각 버튼마다 선이 연결됐는지 확인용 리스트
    private int currline;               //어떤 선인지 구분하기 위해

    private List<int> templist;     //오른쪽 버튼의 선이 연결됐는지 확인용 리스트
    private int temp;               //어떤 버튼에 연결 됐는지

    private List<LineRenderer> linelist;

    private void Start()
    {
        linelist=new List<LineRenderer>();
        currlist = new List<int>();
        templist = new List<int>();
        overlap = false;
        checking = false;
    }
    void Update()
    {
        touchClick();
    }
    void CreateLine()
    {
        for (int i = 0; i < leftButton.Length; i++)
        {
            line = new GameObject("Line" + currline).AddComponent<LineRenderer>(); //새로 생성
            line.startWidth = .05f;                                     //처음 굵기
            line.endWidth = .05f;                                       //마지막 굵기
            linelist.Add(line);
        }
        
    }

    void touchClick()// 터치 시 오브젝트 확인 함수
    {
        // 터치 입력이 들어올 경우
        if (Input.GetMouseButtonDown(0))
        {
            // 오브젝트 정보를 담을 변수 생성 
            RaycastHit hit;
            // 터치 좌표를 담는 변수 
            Ray touchray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // 터치한 곳에 ray를 보냄 
            Physics.Raycast(touchray, out hit);
            // ray가 오브젝트에 부딪힐 경우 
            if (hit.collider != null)
            {
                if (fristButton == null)
                {
                    fristButton = hit.collider.gameObject;  //선택된 버튼을 첫번쨰 버튼에 저장(UI만 가능한듯)
                    for (int i = 0; i < leftButton.Length; i++) //왼쪽 버튼인지 확인(정방향 반대방향 확인)
                    {
                        if (leftButton[i] == fristButton)   //맞을 경우
                        {
                            Debug.Log("첫번째 버튼은" + fristButton.name);
                            overlap = true;                 //왼쪽선택을 그냥 true로 지정
                            currline = i;                       //만들어질 선 저장
                            break;
                        }
                    }
                    if (overlap == false) //버튼이 아니라 이상한거 클릭했을때를 확인해보자! 
                    {
                        for (int i = 0; i < rightButton.Length; i++) //오른쪽 버튼이 눌렸는지
                        {
                            if (rightButton[i] == fristButton)
                            {
                                checking = true;
                                Debug.Log("첫번째 버튼은" + fristButton.name);
                                temp = i;
                                break;
                            }
                        }
                        if (checking != true)           //이상한 버튼 눌렀을때
                        {
                            checking = false;
                            fristButton = null;         //초기화
                            Debug.Log("첫번째 버튼 오류! 다시 선택해 주세요.");
                        }
                    }
                }
                else
                {
                    if (secondButton == null)
                    {
                        secondButton = hit.collider.gameObject;
                        if (overlap == true)        //왼쪽선택시
                        {
                            for (int i = 0; i < rightButton.Length; i++)  //오른쪽을 선택했는지 확인
                            {
                                if (rightButton[i] == secondButton) //맞을 경우
                                {
                                    Debug.Log("두번째버튼은" + secondButton.name);
                                    checking = true;
                                    temp = i;
                                    break;
                                }
                            }
                            if (checking != true) //같은 라인인지 확인(이상한거 클릭했을때)
                            {
                                Debug.Log("두번째 버튼을 다시 선택해 주세요.");
                                checking = false;
                                secondButton = null;    //같은 라인이라면 다시 선택할 수 있게.
                            }
                        }
                        else
                        {
                            checking = false;                   //첫번째버튼과 겹치지 않게 초기화
                            for (int i = 0; i < leftButton.Length; i++)
                            {
                                if (leftButton[i] == secondButton)  //왼쪽 버튼에 있는지확인
                                {
                                    Debug.Log("두번째버튼은" + secondButton.name);
                                    checking = true;
                                    currline = i;       //어떤 선인지
                                    break;
                                }
                            }
                            if (checking != true) //같은 라인인지 확인
                            {
                                Debug.Log("두번째 버튼을 다시 선택해 주세요.");
                                secondButton = null;    //같은 라인이라면 다시 선택할 수 있게.
                            }
                        }
                    }
                    if (fristButton != null && secondButton != null)     //두가지의 버튼이 눌렸을때(실행)
                    {
                        //Debug.Log(currline);
                        //Debug.Log(currlist.Contains(currline)&&templist.Contains(temp));
                        if (currlist.Contains(currline))        //선이 있으면 실행
                        {
                            line = GameObject.Find("Line" + currline).GetComponent<LineRenderer>(); //오브젝트 찾기
                            line.startWidth = .05f;                                     //처음 굵기
                            line.endWidth = .05f;                                       //마지막 굵기
                            line.SetPosition(0, fristButton.transform.position);        //처음 위치
                            line.SetPosition(1, secondButton.transform.position);       //마지막위치(라인렌더러의 position size의 따라 다름)
                        }
                        else        //선이 없을때 실행
                        {
                            line = new GameObject("Line" + currline).AddComponent<LineRenderer>(); //새로 생성
                            line.startWidth = .05f;                                     //처음 굵기
                            line.endWidth = .05f;                                       //마지막 굵기
                            line.SetPosition(0, fristButton.transform.position);
                            line.SetPosition(1, secondButton.transform.position);
                            currlist.Add(currline);         //구분을 위해 추가
                            templist.Add(temp);             //저장
                        }
                        fristButton = null;         //초기화
                        secondButton = null;        //초기화
                        checking = false;           //초기화
                        overlap = false;            //초기화
                    }
                }
            }
        }
    }
}