using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerCoding_Work : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    public static PlayerCoding_Work instance;
    public GameObject[] leftButton;  //왼쪽 버튼 3개 
    public GameObject[] rightButton; //오른쪽 버튼 3개(가운데)
    private GameObject fristButton;  //첫번째로 선택된 버튼(색상변경용)
    private GameObject secondButton; //두번째로 선택된 버튼(색상변경용)
    private bool overlap = false;  // 라인이 겹치지 않게 만들어둠
    private bool checking = false; //선을 그릴 위치인지 확인용
    private LineRenderer line;     //선그리기 위함
    GameObject lines;               //삭제용 라인
                                    //public List<int> currlist;     //왼쪽 각 버튼마다 선이 연결됐는지 확인용 리스트
                                    //private int currline;               //어떤 선인지 구분하기 위해
                                    //public List<int> templist;     //오른쪽 버튼의 선이 연결됐는지 확인용 리스트
                                    // private int temp;               //어떤 버튼에 연결 됐는지
                                    // private int Righttemp;          //위치 바꿀때 temp리스트에 사용
    private bool Making = false;    //가공을 성공했는지
    private GameObject beMaked;     //가공될 오브젝트
    private GameObject returnobj;   //결과물(오브젝트)
    private GameObject Point;       //새로 만들어질 오브젝트를 생성할 위치
    private LineRenderer Lines;    //만들어질 오브젝트를 연결할 라인
    public GameObject[] cutSources; //자르기 가능한 오브젝트들
    public GameObject[] burnSources;//태우기 가능한 오브젝트들
    public GameObject[] smeltSources;   //제련하기 가능한 오브젝트들
    public GameObject[] CombinationSources; // 화학적 결합하기 가능한 오브젝트들
    public GameObject[] MakingObject;   //만들어 질 수 있는 모든 오브젝트
    private List<GameObject> cutList;       //자리기 가능한 오브젝트들을 저장할 장소
    private List<GameObject> burnList;      //태우기 가능한 오브젝트들을 저장할 장소
    private List<GameObject> CombinationList;   //화학적 결합하기가 가능한 오브젝트들을 저장할 장소
    private List<GameObject> smeltList;     //제련하기 가능한 오브젝트들을 저장할 장소
    private List<GameObject> MakingObjList; //만들어 질 오브젝트들 저장할 장소
    GameObject Clone;  //생성하고 삭제가 유용하게 하기 위해서
    public static string WillMake; // 무엇으로 만들 것인지 데이터를 보낼 String값
    public static string HowtoMake; // 어떤식으로 가공할 지 데이터를 보낼 String값
    Vector3 MousePos;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //currlist = new List<int>();
        // templist = new List<int>();
        cutList = new List<GameObject>();
        burnList = new List<GameObject>();
        CombinationList = new List<GameObject>();
        smeltList = new List<GameObject>();
        MakingObjList = new List<GameObject>();
        //프리펩, 오브젝트들을 모두 저장
        for (int i = 0; i < cutSources.Length; i++)
        {
            cutList.Add(cutSources[i]);
        }
        for (int i = 0; i < burnSources.Length; i++)
        {
            burnList.Add(burnSources[i]);
        }
        for (int i = 0; i < smeltSources.Length; i++)
        {
            smeltList.Add(smeltSources[i]);
        }
        for (int i = 0; i < CombinationSources.Length; i++)
        {
            CombinationList.Add(CombinationSources[i]);
        }
        for (int i = 0; i < MakingObject.Length; i++)
        {
            MakingObjList.Add(MakingObject[i]);
        }
        overlap = false;
        checking = false;
        for (int i = 0; i < DataManager.instance.currlist.Count; i++)
        {
            DataManager.instance.currline = DataManager.instance.currlist[i];
            DataManager.instance.temp = DataManager.instance.templist[i];
            Debug.Log("start for문");
            CreateLine();
            fristButton = leftButton[DataManager.instance.currline];
            secondButton = rightButton[DataManager.instance.temp];
            line.SetPosition(0, fristButton.transform.position);
            line.SetPosition(1, secondButton.transform.position);
            beMaked = secondButton.transform.parent.gameObject;
            LineResult(); //결과
            line = null;
        }


    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))  //버튼 클릭1회
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
                MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
                MousePos.z = -3.09f;
                UiEffect.instance.ButtonUIEffect(hit.collider.gameObject);
                ButtonDown(hit.collider.gameObject, MousePos);
            }
        }
        else if (Input.GetMouseButton(0) && line) //버튼이 꾹눌리는동안
        {
            MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
            MousePos.z = -3.09f;
            ButtonState(MousePos);
        }
        else if (Input.GetMouseButtonUp(0) && line) //버튼을 뗐을때
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
                UiEffect.instance.ButtonUIEffect(hit.collider.gameObject);
                ButtonUp(hit.collider.gameObject);
            }
        }
    }
    public void ButtonDown(GameObject hit, Vector3 LinePos)  //버튼이 1회 눌렸을때, 
    {
        CheckingStartButton(hit); //클릭되었을때 클릭된 오브젝트를 넣어줌
        if (checking.Equals(true))
        {
            if (line == null)
            {
                OverlapLineChecking(); //라인이 있는지 첵크
                //CreateLine();
            }
            line.SetPosition(0, fristButton.transform.position);
            line.SetPosition(1, LinePos);
        }
        checking = false; //초기화 (두번째버튼을 위해)
    }
    public void ButtonState(Vector3 LinePos) //버튼이 눌리는 동안
    {
        line.SetPosition(1, LinePos);
    }
    public void ButtonUp(GameObject hit) //버튼을 땠을때
    {
        CheckingEndButton(hit);
        if (checking.Equals(true))
        {
            line.SetPosition(1, secondButton.transform.position);
            line = null;
            OverlapEndLineChecking();
            if (overlap.Equals(false))
            {
                DataManager.instance.templist.Add(DataManager.instance.temp);
                LineResult(); //결과
            }
            else
            {
                Destroy(lines);
                DataManager.instance.currlist.Remove(DataManager.instance.currline);
            }
        }
        else //false면 연결되지 않은 선 삭제
        {
            Destroy(lines);
            DataManager.instance.currlist.Remove(DataManager.instance.currline);
            DataManager.instance.templist.Remove(DataManager.instance.temp);
        }
    }
    void OverlapEndLineChecking() //두번째 버튼에 선이 연결되었는지 확인
    {
        if (!DataManager.instance.templist.Contains(DataManager.instance.temp))
        {
            overlap = false;
        }
        else
        {
            overlap = true;
        }
    }
    void OverlapLineChecking()     //(왼쪽 버튼 기준)중복체크
    {
        if (DataManager.instance.currlist.Contains(DataManager.instance.currline))        //선이 있으면 실행
        {
            lines = GameObject.Find("Line" + DataManager.instance.currline); //오브젝트 찾기
            Destroy(lines);
            line = null;
            DataManager.instance.templist.Remove(DataManager.instance.temp);               //바꾸기 전 것을 제거                
            Destroy(GameObject.Find("Temp" + DataManager.instance.currline));
            CreateLine();
        }
        else        //선이 없을때 실행
        {
            DataManager.instance.currlist.Add(DataManager.instance.currline);         //구분을 위해 추가
            CreateLine();
        }
    }
    void LineResult() //선 열결 결과 후 초기화
    {
        MakeCheck();
        checking = false;           //초기화
        overlap = false;            //초기화
        Making = false;             //초기화
    }
    void CheckingStartButton(GameObject hit) //left버튼확인
    {
        for (int i = 0; i < leftButton.Length; i++) //왼쪽 버튼인지 확인(정방향 반대방향 확인)
        {
            if (leftButton[i].Equals(hit))   //맞을 경우
            {
                Debug.Log(hit.name);
                checking = true;        //맞다
                DataManager.instance.currline = i;
                fristButton = hit;
                break;
            }
        }
    }
    void CheckingEndButton(GameObject hit)  //Right 버튼 확인
    {
        for (int i = 0; i < rightButton.Length; i++) //오른쪽 버튼이 눌렸는지
        {
            if (rightButton[i].Equals(hit))
            {
                checking = true;
                Debug.Log("End : " + hit.name);
                DataManager.instance.temp = i;
                secondButton = hit;
                beMaked = hit.transform.parent.gameObject;
                break;
            }
        }
    }
    void CreateLine()  //라인 생성
    {
        line = new GameObject("Line" + DataManager.instance.currline).AddComponent<LineRenderer>();
        line.startWidth = 0.15f;
        line.endWidth = 0.15f;
        lines = line.gameObject;
    }
    void Cutting(GameObject source)   //자르기
    {
        if (cutList.Contains(source))
        {
            //잘라라
            if (source.name.Equals("Wood"))            //들어온게 무엇인지 확인
            {
                returnobj = MakingObjList.Find(x => x.name.Equals("WoodPlank")); //완성품중 찾아 만들 완성품 리턴
                Making = true;          //만들어짐

                WillMake = "Wood";
                HowtoMake = "Cutting";

                //컨테이너에서 움직일 오브젝트에게 데이터 전송
            }
            DataManager.instance.Cutname = source.name;
        }
        else
        {
            Making = false; //자를 수 없는 재료
        }
    }
    void Burning(GameObject source) //태우기
    {
        if (burnList.Contains(source))
        {
            if (source.name.Equals("Sand"))            //들어온게 무엇인지 확인
            {
                returnobj = MakingObjList.Find(x => x.name.Equals("Glass")); //완성품중 찾아 만들 완성품 리턴
                Making = true;          //만들어짐
            }
            else if (source.name == "Wood")
            {
                returnobj = MakingObjList.Find(x => x.name.Equals("Charcoal")); //완성품중 찾아 만들 완성품 리턴
                Making = true;          //만들어짐

                WillMake = "Wood";
                HowtoMake = "Burning";
                //컨테이너에서 움직일 오브젝트에게 데이터 전송
            }
            DataManager.instance.bunname = source.name;
        }
        else
        {
            Making = false;//태울 수 없는 재료
        }
    }
    void Smelting(GameObject source)  //제련하기
    {
        if (smeltList.Contains(source))
        {
            if (source.name.Equals("IronStone"))            //들어온게 무엇인지 확인
            {
                returnobj = MakingObjList.Find(x => x.name.Equals("Iron")); //완성품중 찾아 만들 완성품 리턴
                Making = true;          //만들어짐
            }
            DataManager.instance.smeltname = source.name;
        }
        else
        {
            Making = false; //제련할 수 없는 재료
        }
    }
    void ChemicalCombination(GameObject source)  //화학적 결합하기
    {
        if (CombinationList.Contains(source))
        {
            if (source.name.Equals("Silicon"))            //들어온게 무엇인지 확인
            {
                returnobj = MakingObjList.Find(x => x.name.Equals("Semiconductor")); //완성품중 찾아 만들 완성품 리턴
                Making = true;          //만들어짐
            }
            DataManager.instance.combintname = source.name;
        }
        else
        {
            Making = false; //화학적 결합할 수 없는 재료
        }
    }
    public void MakeCheck() //어떤 방식을 사용할 건지
    {
        switch (DataManager.instance.currline)
        {
            case 0:
                Cutting(beMaked);
                break;
            case 1:
                Burning(beMaked);
                break;
            case 2:
                Smelting(beMaked);
                break;
            case 3:
                ChemicalCombination(beMaked);
                break;
        }
        if (Making.Equals(true)) //가공에 성공했는지
        {
            if (GameObject.Find("Temp" + DataManager.instance.currline) == null)
            {
                Point = GameObject.Find("ResultMaterial" + DataManager.instance.temp);    //재료 바로 옆에 생성할 수 있게 포인트 설정
                Vector3 PointPos = Point.transform.position;
                PointPos.z -= 0.01f;
                GameObject obj = new GameObject("Temp" + DataManager.instance.currline);
                Clone = Instantiate(returnobj, PointPos, Quaternion.identity); //완성물 생성
                Clone.transform.parent = obj.transform;
                fristButton.GetComponent<MeshRenderer>().material.color = Color.cyan;
                secondButton.GetComponent<MeshRenderer>().material.color = Color.magenta;
                UiEffect.instance.effectbool = true;
                UI_QuestOrCheck.isComplete_work = true;
            }
            else //이미 선이 있는경우
            {
                Destroy(GameObject.Find("Temp" + DataManager.instance.currline));
                Destroy(GameObject.Find("Line" + DataManager.instance.currline));
                Destroy(Clone);  //삭제
                DataManager.instance.currlist.Remove(DataManager.instance.currline);
                DataManager.instance.templist.Remove(DataManager.instance.temp);
                rightButton[DataManager.instance.Righttemp].GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }
        else //실패시
        {
            Destroy(GameObject.Find("Line" + DataManager.instance.currline));
            DataManager.instance.currlist.Remove(DataManager.instance.currline);
            DataManager.instance.templist.Remove(DataManager.instance.temp);
            rightButton[DataManager.instance.Righttemp].GetComponent<MeshRenderer>().material.color = Color.white;
        }
        DataManager.instance.Righttemp = DataManager.instance.temp;
        Debug.Log(Making);
    }
    public void Resetbutton() //리셋버튼용.
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        for (int i = 0; i < DataManager.instance.currlist.Count; i++)
        {
            Destroy(GameObject.Find("Temp" + DataManager.instance.currlist[i]));
            Destroy(GameObject.Find("Line" + DataManager.instance.currlist[i]));
            Destroy(Clone);  //삭제
        }
        DataManager.instance.currlist.Clear();
        DataManager.instance.templist.Clear();
        SceneManager.LoadScene("PlayerCoding_Work");
    }

    public void Xbutton()
    {
        SoundCtrl.instance.SoundEffectPlay(clip);
        SceneManager.LoadScene("MainPlay");
    }
}