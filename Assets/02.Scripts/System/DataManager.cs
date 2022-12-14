using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    public Material M_Wood;
    public Material M_Sand;
    public Material M_IronOre;
    public Material M_Copper;
    public Material M_Rubber;
    public Material M_Sillicon;

    public Material M_Car;
    public Material M_Window;
    public Material M_Tire;
    public Material M_Desk;
    public Material M_Iron;
    public Material M_Glass;
    public Material M_CutWood;
    public Material M_Semiconductor;
    public Material M_Lithum;




    public class BoxData
    {
        public string name;
        public int counts;
        public BoxData(string name, int count)
        {
            this.name = name;
            this.counts = count;
        }
    }
    public class GameObjTr
    {
        public GameObject obj; //오브젝트 위치값저장할때 사용        
        public Vector3 objTr;  //오브젝트의 윗칫값
        public int num;
        public string colors;
        public GameObjTr(GameObject obj, Vector3 objTr, int num, string colors)  //새로운 녀석을 만들때
        {
            this.num = num;
            this.obj = obj;
            this.objTr = objTr;
            this.colors = colors;
        }
    }
    public static DataManager instance;

    [HideInInspector]
    public float PlayerX;
    [HideInInspector]
    public float PlayerY;
    [HideInInspector]
    public float PlayerZ;
    [HideInInspector]
    public Vector3 startPos; //임시용 박스위치
    public bool startSpawn; //스폰이 true면 실행   
    public GameObject Workitem = null; //playerCoding_Work에 들어간 아이템 기본값은 null 들어갔을경우 -> 값을 넣어주고 빠져나갈땐 다시 null로 변경시켜줄 것!
    public GameObject AssembleItem = null;
    public GameObject Packitem = null; 
    public string SceneName;
    public List<GameObjTr> itemsOnContainer; //생성된 아이템들의 저장용 리스트
    public GameObject[] itemPrefeb; //처음 생성될 아이템들
    GameObject obj;
    GameObjTr gameObjTr;

    [HideInInspector] public Vector3 rot;
    [HideInInspector] public float timer = 2;

    //PlayerCoding_Work 의 사용 될 변수들임.
    public List<int> currlist;     //왼쪽 각 버튼마다 선이 연결됐는지 확인용 리스트
    [HideInInspector]
    public int currline;               //어떤 선인지 구분하기 위해
    public List<int> templist;     //오른쪽 버튼의 선이 연결됐는지 확인용 리스트
    [HideInInspector]
    public int temp;               //어떤 버튼에 연결 됐는지
    [HideInInspector]
    public int Righttemp;          //위치 바꿀때 temp리스트에 사용
    //여기까지

    public string Cutname; //자르기 이름
    public string bunname; //태우기 이름
    public string combintname;  //결합하기 이름
    public string smeltname; //제련하기 이름

    public string cubename;         //input에서 선택한 재료의 이름

    //Package
    public List<Vector3> SlotVect; //패키지의 슬롯 위칫값 저장
    public List<int> Slotint;
    public List<string> boxPack; //박스포장의 슬롯이름들 저장
    public List<string> styrofoamPack;
    public List<string> PlastickPack;

    
    //여기까지

    //PlayerCoding_Input
    public List<Vector3> InputSlot;
    public List<int> InputSlortint;

    [HideInInspector] public bool SampleBoolen;

    //CubeData
    public List<BoxData> boxdata;
    public List<string> boxname;


    private void Awake()
    {
        boxdata = new List<BoxData>();
        InputSlortint = new List<int>();
        InputSlot = new List<Vector3>();
        Slotint = new List<int>();
        SlotVect = new List<Vector3>();
        boxPack = new List<string>();
        PlastickPack = new List<string>();
        styrofoamPack = new List<string>();
        currlist = new List<int>();
        templist = new List<int>();
        itemsOnContainer = new List<GameObjTr>();
        PlayerX = 1;
        PlayerY = 6;
        PlayerZ = -18;       
        if (instance == null)
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    } //싱글턴  

    private void Start()
    {
        SaveNLoad.Load();  
    }
    public void SceneSaveItemTr() //씬 넘어갈때 아이템 위치 저장
    {
        for (int i = 0; i < itemsOnContainer.Count; i++)
        {
            itemsOnContainer[i].obj.SetActive(false);
        }
    }
    public void SceneLoadItemTr() //다시 메인씬으로 로드 될때 아이템 위치 원상복구
    {
        for (int i = 0; i < itemsOnContainer.Count; i++)
        {
            itemsOnContainer[i].obj.SetActive(true);
        }
    }

    public void CubeActive() //create 큐브
    {
        obj = ObjPool.instance.GetPooledObj();

        if (obj == null)
        {
            return;
        }
        else
        {
            obj.name = cubename;
            obj.transform.position = startPos;
            obj.transform.rotation = Quaternion.Euler(rot);
            if (obj.name =="Wood")
            {
                gameObjTr = new GameObjTr(obj, obj.transform.position, 0, "Wood");
                obj.GetComponent<MeshRenderer>().material = M_Wood;
                obj.GetComponent<Container_Move>().colors = "Wood";
            }else if(obj.name == "Sand")
            {
                gameObjTr = new GameObjTr(obj, obj.transform.position, 0, "Sand");
                obj.GetComponent<MeshRenderer>().material = M_Sand;
                obj.GetComponent<Container_Move>().colors = "Sand";
            }
            else if (obj.name == "IronStone")
            {
                gameObjTr = new GameObjTr(obj, obj.transform.position, 0, "IronStone");
                obj.GetComponent<MeshRenderer>().material = M_IronOre;
                obj.GetComponent<Container_Move>().colors = "IronStone";
            }
            else if (obj.name == "Lithium")
            {
                gameObjTr = new GameObjTr(obj, obj.transform.position, 0, "Lithium");
                obj.GetComponent<MeshRenderer>().material = M_Lithum;
                obj.GetComponent<Container_Move>().colors = "Lithium";
            }
            else if (obj.name == "Rubber")
            {
                gameObjTr = new GameObjTr(obj, obj.transform.position, 0, "Rubber");
                obj.GetComponent<MeshRenderer>().material = M_Rubber;
                obj.GetComponent<Container_Move>().colors = "Rubber";
            }
            itemsOnContainer.Add(gameObjTr);
            obj.SetActive(true);
        }
    }
    public void LoaditemTr(List<Vector3> transformList, List<int> numLIst, List<bool> startMoveList, List<string> colorsList, List<string> nameList)  //나중에 리스트 로드용으로 사용될 예정
    {
        Workitem = null;
        Packitem = null;
        itemsOnContainer.Clear();
        for (int i = 0; i < transformList.Count; i++)
        {
            GameObject Clone = ObjPool.instance.GetPooledObj();
            if (Clone.Equals(null))
            {
                return;
            }
            Clone.transform.position = transformList[i];
            Clone.name = nameList[i];
            GameObjTr gameObjTr = new GameObjTr(Clone, Clone.transform.position, numLIst[i], colorsList[i]);
            itemsOnContainer.Add(gameObjTr);
            switch (numLIst[i])
            {
                case 1:
                    Clone.GetComponent<Container_Move>().go_contain_1 = true;
                    break;
                case 2:
                    Clone.GetComponent<Container_Move>().go_contain_2 = true;
                    break;
                case 3:
                    Clone.GetComponent<Container_Move>().go_contain_3 = true;
                    break;
                case 4:
                    Clone.GetComponent<Container_Move>().go_contain_4 = true;
                    break;
            }
            switch (colorsList[i])
            {
                case "White":
                    Clone.GetComponent<MeshRenderer>().material.color = Color.white;
                    Clone.GetComponent<Container_Move>().colors = "White";
                    break;
                case "Red":
                    Clone.GetComponent<MeshRenderer>().material.color = Color.red;
                    Clone.GetComponent<Container_Move>().colors = "Red";
                    break;
                case "Green":
                    Clone.GetComponent<MeshRenderer>().material.color = Color.green;
                    Clone.GetComponent<Container_Move>().colors = "Green";
                    break;
                case "Brown":
                    Clone.GetComponent<MeshRenderer>().material.color = new Color(90 / 256f, 57 / 256f, 49 / 256f);
                    Clone.GetComponent<Container_Move>().colors = "Green";
                    break;
                case "Bule":
                    Clone.GetComponent<MeshRenderer>().material.color = Color.blue;
                    Clone.GetComponent<Container_Move>().colors = "Bule";
                    break;
                case "Black":
                    Clone.GetComponent<MeshRenderer>().material.color = Color.black;
                    Clone.GetComponent<Container_Move>().colors = "Black";
                    break;
                case "Gray":
                    Clone.GetComponent<MeshRenderer>().material.color = Color.gray;
                    Clone.GetComponent<Container_Move>().colors = "Gray";
                    break;
                case "Yellow":
                    Clone.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    Clone.GetComponent<Container_Move>().colors = "Yellow";
                    break;
                case "Wood":
                    Clone.GetComponent<MeshRenderer>().material.color = new Color(108 / 255f, 0, 0);
                    Clone.GetComponent<Container_Move>().colors = "Wood";
                    break;
                case "Sand":
                    Clone.GetComponent<MeshRenderer>().material.color = new Color(255/255f, 243/255f, 119/255f);
                    Clone.GetComponent<Container_Move>().colors = "Sand";
                    break;
                case "IronStone":
                    Clone.GetComponent<MeshRenderer>().material.color = new Color(186 / 255f, 186 / 255f, 186 / 255f);
                    Clone.GetComponent<Container_Move>().colors = "IronStone";
                    break;
                case "Lithium":
                    Clone.GetComponent<MeshRenderer>().material.color = new Color(154 / 255f, 77 / 255f, 77 / 255f);
                    Clone.GetComponent<Container_Move>().colors = "Lithium";
                    break;
                case "Rubber":
                    Clone.GetComponent<MeshRenderer>().material.color = new Color(216 / 255f, 223 / 255f, 50 / 255f);
                    Clone.GetComponent<Container_Move>().colors = "Rubber";
                    break;
            }
            Clone.SetActive(true);
            Clone.GetComponent<Container_Move>().startMove = startMoveList[i];

        }
    }

    public void BoxDataAdd(BoxData data)
    {
        if (boxname.Contains(data.name))
        {
            boxdata.Find(x => x.name == data.name).counts++;
        }
        else
        {
            boxdata.Add(data);
            boxname.Add(data.name);
        }
    }
    public void SaveItemsTr()
    {
        for (int i = 0; i < itemsOnContainer.Count; i++)
        {
            itemsOnContainer[i].objTr = itemsOnContainer[i].obj.transform.position;
            itemsOnContainer[i].num = itemsOnContainer[i].obj.GetComponent<Container_Move>().Go_num;
            itemsOnContainer[i].colors = itemsOnContainer[i].obj.GetComponent<Container_Move>().colors;
        }
    }

    public void CheckPack()
    {
        bool Checkedbool= false;
        if(boxPack.Count > 0)
        {
            for (int i = 0; i < boxPack.Count; i++)
            {
                if(Packitem.name == boxPack[i])
                {
                    Packitem.GetComponent<Container_Move>().colors = "Brown";
                    Packitem.GetComponent<MeshRenderer>().material.color = new Color(90/256f, 57/256f, 49/256f);
                    Packitem.GetComponent<Container_Move>().startMove = true;
                    Packitem.name +="-Box";
                    Packitem.GetComponent<Container_Move>().PackCheck = true;
                    Checkedbool = true;
                    break;
                }
                else
                {
                    Checkedbool = false;
                }
            }
        }
        if (PlastickPack.Count > 0)
        {
            for (int i = 0; i < PlastickPack.Count; i++)
            {
                if (Packitem.name == PlastickPack[i])
                {
                    Packitem.GetComponent<Container_Move>().colors = "Green";
                    Packitem.GetComponent<MeshRenderer>().material.color = Color.green;
                    Packitem.GetComponent<Container_Move>().startMove = true;
                    Packitem.name += "-PlastickPack";
                    Packitem.GetComponent<Container_Move>().PackCheck = true;
                    Checkedbool = true;
                    break;
                }
                else
                {
                    Checkedbool = false;
                }
            }
        }
        if (styrofoamPack.Count > 0)
        {
            for (int i = 0; i < styrofoamPack.Count; i++)
            {
                if (Packitem.name == styrofoamPack[i])
                {
                    Packitem.GetComponent<Container_Move>().colors = "White";
                    Packitem.GetComponent<MeshRenderer>().material.color = Color.white;
                    Packitem.GetComponent<Container_Move>().startMove = true;
                    Packitem.name += "-StyrofoamPack";
                    Packitem.GetComponent<Container_Move>().PackCheck = true;
                    Checkedbool = true;
                    break;
                }
                else
                {
                    Checkedbool = false;
                }
            }
        }
        if (Checkedbool ==false)
        {
            Packitem.GetComponent<Container_Move>().startMove = true;
            Packitem.GetComponent<Container_Move>().colors = "Black";
            Packitem.GetComponent<MeshRenderer>().material.color = Color.black;
        }
    }

    public void CheckedSauce()
    {
        if (Workitem.name == Cutname)
        {
            switch (Cutname)
            {
                case "Wood":
                    Workitem.GetComponent<MeshRenderer>().material = M_CutWood;
                    Workitem.GetComponent<Container_Move>().colors = "Red";
                    Workitem.name = "WoodPlank";
                    break;
            }
            Workitem.GetComponent<Container_Move>().startMove = true;
        }
        else if (Workitem.name == bunname)
        {
            switch (bunname)
            {
                case "Sand":
                    Workitem.GetComponent<MeshRenderer>().material = M_Glass;
                    Workitem.GetComponent<Container_Move>().colors = "Blue";
                    Workitem.name = "Glass";
                    break;
            }
            Workitem.GetComponent<Container_Move>().startMove = true;
        }
        else if (Workitem.name == combintname)
        {
            switch (combintname)
            {
                case "Silicon":
                    Workitem.GetComponent<MeshRenderer>().material = M_Sillicon;
                    Workitem.GetComponent<Container_Move>().colors = "Gray";
                    Workitem.name = "Semiconductor";
                    break;
            }
            Workitem.GetComponent<Container_Move>().startMove = true;
        }
        else if (Workitem.name == smeltname)
        {
            switch (smeltname)
            {
                case "IronStone":
                    Workitem.GetComponent<MeshRenderer>().material = M_Iron;
                    Workitem.GetComponent<Container_Move>().colors = "Gray";
                    Workitem.name = "Iron";
                    break;
            }
            Workitem.GetComponent<Container_Move>().startMove = true;
        }
        else
        {
            Debug.Log("없다. 설정해달라");
        }
    }
}