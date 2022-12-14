using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine.SceneManagement;

public class SaveNLoad : MonoBehaviour
{
    // 모든 저장 및 로드할 데이터들
    [System.Serializable]
    public class Data
    {
        // DataManager스크립트 내용
        public float PlayerX;
        public float PlayerY;
        public float PlayerZ;
        public float startPosX;
        public float startPosY;
        public float startPosZ;
        public string SceneName;
        public bool startspawn;
        public List<float> ObjXTr;
        public List<float> ObjYTr;
        public List<float> ObjZTr;
        public List<int> Go_numList; // 몇 번째 라인인지 구분
        public List<bool> startMoveList;
        public List<string> colorList;
        public List<string> nameList;
        public float Timer;

        // UI_QuestOrCheck스크립트 내용
        public bool isComplete_pick;
        public bool isComplete_work;
        public bool isComplete_assemble;

        //coding관련
        public List<int> currlist;
        public int currline;
        public List<int> templist;
        public int temp;
        public int Righttemp;

        public string Cutname; //자르기 이름
        public string bunname; //태우기 이름
        public string combintname;  //결합하기 이름
        public string smeltname; //제련하기 이름

        //input
        public string cubename;         //input에서 선택한 재료의 이름
        public List<float> InputSlotX;
        public List<float> InputSlotY;
        public List<float> InputSlotZ;
        public List<int> InputSlortint;

        //Package
        public List<float> SlotVectX;
        public List<float> SlotVectY;
        public List<float> SlotVectZ;
        public List<int> Slotint;
        public List<string> boxPack; //박스포장의 슬롯이름들 저장
        public List<string> styrofoamPack;
        public List<string> PlastickPack;

        //Aseemble
        public List<float> InHole_ItemPosX;
        public List<float> InHole_ItemPosY;
        public List<float> InHole_ItemPosZ;
        public List<string> InHole_name;
        public List<int> InHole_ItemMaxium;
        public List<int> InHole_ItemNow;

        //BoxData
        public List<string> Boxnames;
        public List<int> BoxInt;

        //Monitering
        public bool[] MachineCheck =new bool [4] ;
    }


    // 저장
    public static void Save()
    {
        Data data = new Data();

        //리스트들 초기화
        data.InHole_ItemMaxium = new List<int>();
        data.InHole_ItemNow = new List<int>();
        data.InHole_ItemPosX = new List<float>();
        data.InHole_ItemPosY = new List<float>();
        data.InHole_ItemPosZ = new List<float>();
        data.InHole_name = new List<string>();

        data.BoxInt = new List<int>();
        data.Boxnames = new List<string>();
        data.nameList = new List<string>();
        data.currlist = new List<int>();
        data.templist = new List<int>();
        data.InputSlotX = new List<float>();
        data.InputSlotY = new List<float>();
        data.InputSlotZ = new List<float>();
        data.InputSlortint = new List<int>();
        data.Slotint = new List<int>();
        data.SlotVectX = new List<float>();
        data.SlotVectY = new List<float>();
        data.SlotVectZ = new List<float>();
        data.boxPack = new List<string>();
        data.styrofoamPack = new List<string>();
        data.PlastickPack = new List<string>();
        data.Go_numList = new List<int>();
        data.ObjXTr = new List<float>();
        data.ObjYTr = new List<float>();
        data.ObjZTr = new List<float>();
        data.startMoveList = new List<bool>();
        data.colorList = new List<string>();
        //여기서부터 저장값넣기
        data.PlayerX = DataManager.instance.PlayerX;
        data.PlayerY = DataManager.instance.PlayerY;
        data.PlayerZ = DataManager.instance.PlayerZ;
        data.startPosX = DataManager.instance.startPos.x;
        data.startPosY = DataManager.instance.startPos.y;
        data.startPosZ = DataManager.instance.startPos.z;
        data.isComplete_assemble = UI_QuestOrCheck.isComplete_assemble;
        data.isComplete_pick = UI_QuestOrCheck.isComplete_pick;
        data.isComplete_work = UI_QuestOrCheck.isComplete_work;
        data.SceneName = DataManager.instance.SceneName;
        DataManager.instance.SaveItemsTr();
        data.Timer = DataManager.instance.timer;
        for (int i = 0; i < DataManager.instance.itemsOnContainer.Count; i++)
        {
            data.Go_numList.Add(DataManager.instance.itemsOnContainer[i].num);
            data.ObjXTr.Add(DataManager.instance.itemsOnContainer[i].objTr.x);
            data.ObjYTr.Add(DataManager.instance.itemsOnContainer[i].objTr.y);
            data.ObjZTr.Add(DataManager.instance.itemsOnContainer[i].objTr.z);
            data.startMoveList.Add(DataManager.instance.itemsOnContainer[i].obj.GetComponent<Container_Move>().startMove);
            data.colorList.Add(DataManager.instance.itemsOnContainer[i].obj.GetComponent<Container_Move>().colors);
            data.nameList.Add(DataManager.instance.itemsOnContainer[i].obj.name);
        }

        //추가
        for (int i = 0; i < DataManager.instance.currlist.Count; i++)
        {
            data.currlist.Add(DataManager.instance.currlist[i]);
        }
        for (int i = 0; i < DataManager.instance.templist.Count; i++)
        {
            data.templist.Add(DataManager.instance.templist[i]);
        }
        data.temp = DataManager.instance.temp;
        data.currline = DataManager.instance.currline;
        data.Righttemp = DataManager.instance.Righttemp;
        data.Cutname = DataManager.instance.Cutname;
        data.bunname = DataManager.instance.bunname;
        data.combintname = DataManager.instance.combintname;
        data.smeltname = DataManager.instance.smeltname;
        data.cubename = DataManager.instance.cubename;
        for (int i = 0; i < DataManager.instance.InputSlortint.Count; i++)
        {
            data.InputSlortint.Add(DataManager.instance.InputSlortint[i]);
        }
        for (int i = 0; i < DataManager.instance.InputSlot.Count; i++)
        {
            data.InputSlotX.Add(DataManager.instance.InputSlot[i].x);
            data.InputSlotY.Add(DataManager.instance.InputSlot[i].y);
            data.InputSlotZ.Add(DataManager.instance.InputSlot[i].z);
        }
        for (int i = 0; i < DataManager.instance.SlotVect.Count; i++)
        {
            data.SlotVectX.Add(DataManager.instance.SlotVect[i].x);
            data.SlotVectY.Add(DataManager.instance.SlotVect[i].y);
            data.SlotVectZ.Add(DataManager.instance.SlotVect[i].z);
        }
        for (int i = 0; i < DataManager.instance.Slotint.Count; i++)
        {
            data.Slotint.Add(DataManager.instance.Slotint[i]);
        }
        for (int i = 0; i < DataManager.instance.boxPack.Count; i++)
        {
            data.boxPack.Add(DataManager.instance.boxPack[i]);
        }
        for (int i = 0; i < DataManager.instance.PlastickPack.Count; i++)
        {
            data.PlastickPack.Add(DataManager.instance.PlastickPack[i]);
        }
        for (int i = 0; i < DataManager.instance.styrofoamPack.Count; i++)
        {
            data.styrofoamPack.Add(DataManager.instance.styrofoamPack[i]);
        }
        data.startspawn = DataManager.instance.startSpawn;
        //Assemble
        for (int i = 0; i < Assemble_Data.instance.InHole_name.Count; i++)
        {
            data.InHole_name.Add(Assemble_Data.instance.InHole_name[i]);
            data.InHole_ItemPosX.Add(Assemble_Data.instance.InHole_ItemPos[i].x);
            data.InHole_ItemPosY.Add(Assemble_Data.instance.InHole_ItemPos[i].y);
            data.InHole_ItemPosZ.Add(Assemble_Data.instance.InHole_ItemPos[i].z);
            data.InHole_ItemMaxium.Add(Assemble_Data.instance.InHole_ItemMaxium[i]);
            data.InHole_ItemNow.Add(Assemble_Data.instance.InHole_ItemNow[i]);
        }
       
        //BoxDataSave
        for (int i = 0; i < DataManager.instance.boxdata.Count; i++)
        {
            data.Boxnames.Add(DataManager.instance.boxdata[i].name);
            data.BoxInt.Add(DataManager.instance.boxdata[i].counts);
        }
        //Moniter
        for (int i = 0; i < MachineCheckManger.instance.MachineCheck.Length; i++)
        {
            data.MachineCheck[i] = MachineCheckManger.instance.MachineCheck[i];
        }
        //파일 생성저장
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.dataPath + "/SaveFile.dat");

        bf.Serialize(file, data);
        file.Close();

        Debug.Log(Application.dataPath + "위치에 저장 완료");
    }


    // 저장된 내용 불러오기
    public static void Load()
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.dataPath + "/SaveFile.dat", FileMode.Open);

            if (file != null && file.Length > 0)
            {
                Data data = (Data)bf.Deserialize(file);
                DataManager.instance.PlayerX = data.PlayerX;
                DataManager.instance.PlayerY = data.PlayerY;
                DataManager.instance.PlayerZ = data.PlayerZ;
                DataManager.instance.SceneName = data.SceneName;
                List<Vector3> transformList = new List<Vector3>();

                for (int i = 0; i < data.ObjXTr.Count; i++)
                    transformList.Add(new Vector3(data.ObjXTr[i], data.ObjYTr[i], data.ObjZTr[i]));

                DataManager.instance.LoaditemTr(transformList, data.Go_numList, data.startMoveList, data.colorList, data.nameList);
                UI_QuestOrCheck.isComplete_work = data.isComplete_work;
                UI_QuestOrCheck.isComplete_pick = data.isComplete_pick;
                UI_QuestOrCheck.isComplete_assemble = data.isComplete_assemble;
                DataManager.instance.startPos = new Vector3(data.startPosX, data.startPosY, data.startPosZ);


                //추가
                DataManager.instance.InputSlortint.Clear();
                DataManager.instance.InputSlot.Clear();
                DataManager.instance.SlotVect.Clear();
                DataManager.instance.Slotint.Clear();
                DataManager.instance.boxPack.Clear();
                DataManager.instance.PlastickPack.Clear();
                DataManager.instance.styrofoamPack.Clear();
                DataManager.instance.currlist.Clear();
                DataManager.instance.templist.Clear();
                for (int i = 0; i < data.InputSlortint.Count; i++)
                {
                    DataManager.instance.InputSlortint.Add(data.InputSlortint[i]);
                }
                for (int i = 0; i < data.InputSlotX.Count; i++)
                {
                    DataManager.instance.InputSlot.Add(new Vector3(data.InputSlotX[i], data.InputSlotY[i], data.InputSlotZ[i]));
                }
                for (int i = 0; i < data.SlotVectX.Count; i++)
                {
                    DataManager.instance.SlotVect.Add(new Vector3(data.SlotVectX[i], data.SlotVectY[i], data.SlotVectZ[i]));
                }
                for (int i = 0; i < data.Slotint.Count; i++)
                {
                    DataManager.instance.Slotint.Add(data.Slotint[i]);
                }
                for (int i = 0; i < data.boxPack.Count; i++)
                {
                    DataManager.instance.boxPack.Add(data.boxPack[i]);
                }
                for (int i = 0; i < data.PlastickPack.Count; i++)
                {
                    DataManager.instance.PlastickPack.Add(data.PlastickPack[i]);
                }
                for (int i = 0; i < data.styrofoamPack.Count; i++)
                {
                    DataManager.instance.styrofoamPack.Add(data.styrofoamPack[i]);
                }
                for (int i = 0; i < data.currlist.Count; i++)
                {
                    DataManager.instance.currlist.Add(data.currlist[i]);
                }
                for (int i = 0; i < data.templist.Count; i++)
                {
                    DataManager.instance.templist.Add(data.templist[i]);
                }
                DataManager.instance.temp = data.temp;
                DataManager.instance.currline = data.currline;
                DataManager.instance.Righttemp = data.Righttemp;
                DataManager.instance.Cutname = data.Cutname;
                DataManager.instance.bunname = data.bunname;
                DataManager.instance.combintname = data.combintname;
                DataManager.instance.smeltname = data.smeltname;
                DataManager.instance.cubename = data.cubename;
                DataManager.instance.startSpawn = data.startspawn;
                //Assemble 
                Assemble_Data.instance.InHole_ItemMaxium.Clear();
                Assemble_Data.instance.InHole_ItemNow.Clear();
                Assemble_Data.instance.InHole_ItemPos.Clear();
                Assemble_Data.instance.InHole_name.Clear();
                for (int i = 0; i < data.InHole_name.Count; i++)
                {
                    Assemble_Data.instance.InHole_name.Add(data.InHole_name[i]);
                    Assemble_Data.instance.InHole_ItemPos.Add(new Vector3(data.InHole_ItemPosX[i], data.InHole_ItemPosY[i], data.InHole_ItemPosZ[i]));
                    Assemble_Data.instance.InHole_ItemMaxium.Add(data.InHole_ItemMaxium[i]);
                    Assemble_Data.instance.InHole_ItemNow.Add(data.InHole_ItemNow[i]);
                }
                //BoxDataLoad
                DataManager.instance.boxname.Clear();
                DataManager.instance.boxdata.Clear();
                for (int i = 0; i < data.Boxnames.Count; i++)
                {
                    DataManager.instance.boxdata.Add(new DataManager.BoxData(data.Boxnames[i], data.BoxInt[i]));
                    DataManager.instance.boxname.Add(data.Boxnames[i]);
                }
                //Moniter
                for (int i = 0; i < data.MachineCheck.Length; i++)
                {
                    MachineCheckManger.instance.MachineCheck[i] = data.MachineCheck[i];
                }

                SceneManager.LoadScene(data.SceneName);
                Debug.Log("Load 성공!");
            }
            else
            {
                Debug.Log("저장된 세이브 파일이 없습니다.");
            }

            file.Close();
        
        }catch(Exception e)
        {
            Debug.Log(e.Message);
        }
    }
}