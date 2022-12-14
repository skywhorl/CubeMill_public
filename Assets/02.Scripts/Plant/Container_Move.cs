using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container_Move : MonoBehaviour
{
    //컨테이너의 진행경로를 바꿔줄 Bool값
    public bool go_contain_1 = false;
    public bool go_contain_2 = false;
    public bool go_contain_3 = false;
    public bool go_contain_4 = false;
    public bool startMove = false;
    public int Go_num;
    public string colors;
    public bool PackCheck;

    private void OnCollisionEnter(Collision coll)
    {
        if (coll.collider.CompareTag("Floor"))
        {
            DataManager.instance.itemsOnContainer.Remove(DataManager.instance.itemsOnContainer.Find(x => x.obj == this.gameObject));
            transform.rotation = Quaternion.Euler(0, 180.661f, 0);
            this.gameObject.SetActive(false);
        }
        //1번 컨테이너에서의 이동을 위한 bool값 변경
        if (coll.collider.CompareTag("Container_1"))
        {
            transform.rotation = Quaternion.Euler(0, 180.661f, 0);
            go_contain_4 = false;
            go_contain_3 = false;
            go_contain_2 = false;
            go_contain_1 = true;
            startMove = true;
            Go_num = 1;
        }
        else if (coll.collider.CompareTag("NextContainer"))
        {
            if (DataManager.instance.Workitem == this.gameObject)
            {
                DataManager.instance.Workitem = null;
            }else if (DataManager.instance.AssembleItem==this.gameObject)
            {
                DataManager.instance.AssembleItem = null;
            }
            else if(DataManager.instance.Packitem == this.gameObject)
            {
                SpawnStart.instance.Coll.SetActive(true);
                DataManager.instance.Packitem = null;
               
            }
        }
        else if (coll.collider.CompareTag("CheckPoint"))
        {
            go_contain_1 = false;
            go_contain_2 = true;
            go_contain_3 = false;
            go_contain_4 = false;
            transform.rotation = Quaternion.Euler(0, -90f, 0);
        }
        //1번 컨테이너에서 2번 컨테이너로 진행경로 변경을 위한 bool값 변경
        else if (coll.collider.CompareTag("Container_2"))
        {
            if (go_contain_2.Equals(false))
            {
                startMove = false;
            }
            Go_num = 2;
            go_contain_1 = false;
            go_contain_2 = true;
            go_contain_3 = false;
            go_contain_4 = false;
            transform.rotation = Quaternion.Euler(0, -90f, 0);
            DataManager.instance.Workitem = this.gameObject;
            DataManager.instance.CheckedSauce();
            transform.rotation = Quaternion.Euler(0, -90f, 0);
        }

        //2번 컨테이너에서 3번 컨테이너로진행경로 변경을 위한 bool값 변경
        else if (coll.collider.CompareTag("Container_3"))
        {
            if (go_contain_3.Equals(false))
            {
                startMove = false;
                Go_num = 3;
            }
            DataManager.instance.AssembleItem = this.gameObject;
            for (int i = 0; i < Assemble_Data.instance.InHole_name.Count; i++)
            {
                if (this.gameObject.name ==Assemble_Data.instance.InHole_name[i])
                {
                    if (Assemble_Data.instance.InHole_ItemMaxium[i] != Assemble_Data.instance.InHole_ItemNow[i])
                    {
                        Assemble_Data.instance.UpNum(this.name);
                        if (Assemble_Data.instance.isTrue == false)
                        {
                            DataManager.instance.AssembleItem = null;
                            this.gameObject.SetActive(false);
                            DataManager.instance.itemsOnContainer.Remove(DataManager.instance.itemsOnContainer.Find(x => x.obj.Equals(this.gameObject)));
                        }
                        else
                        {
                            this.name = Assemble_Data.instance.GetResult();
                            GetComponent<MeshRenderer>().material.color = Color.yellow;
                            colors = "Yellow";
                        }
                        break;
                    }                    
                }
            }
            go_contain_1 = false;
            go_contain_2 = false;
            go_contain_3 = true;
            go_contain_4 = false;
            transform.rotation = Quaternion.Euler(0, 0f, 0);
            startMove = true ;
        }
        else if (coll.collider.CompareTag("Container_3-1"))
        {
            if (go_contain_3.Equals(false))
            {
                startMove = false;
                Go_num = 3;
            }
            for (int i = 0; i < Assemble_Data.instance.InHole_name.Count; i++)
            {
                if (this.gameObject.name == Assemble_Data.instance.InHole_name[i])
                {
                    if (Assemble_Data.instance.InHole_ItemMaxium[i] != Assemble_Data.instance.InHole_ItemNow[i])
                    {
                        Assemble_Data.instance.UpNum(this.name);
                        if (Assemble_Data.instance.isTrue == false)
                        {
                            DataManager.instance.AssembleItem = null;
                            this.gameObject.SetActive(false);
                            DataManager.instance.itemsOnContainer.Remove(DataManager.instance.itemsOnContainer.Find(x => x.obj.Equals(this.gameObject)));
                        }
                        else
                        {
                            this.name = Assemble_Data.instance.GetResult();
                            GetComponent<MeshRenderer>().material.color = Color.yellow;
                            colors = "Yellow";                            
                        }
                        break;
                    }
                }
            }
            this.gameObject.transform.position = new Vector3(12.118f, 1.562f, 19.236f);
            go_contain_1 = false;
            go_contain_2 = false;
            go_contain_3 = true;
            go_contain_4 = false;
            transform.rotation = Quaternion.Euler(0, 0f, 0);
            startMove = true;
        }
        else if (coll.collider.CompareTag("Container_4"))
        {
            if (go_contain_4.Equals(false))
            {
                startMove = false;
                Go_num = 4;
            }
            Go_num = 4;
            go_contain_1 = false;
            go_contain_2 = false;
            go_contain_3 = false;
            go_contain_4 = true;
            transform.rotation = Quaternion.Euler(0, 0f, 0);
            SpawnStart.instance.Coll.SetActive(false);
            DataManager.instance.Packitem = this.gameObject;
            DataManager.instance.CheckPack();
            
        }
        //모든 이동이 끝나고 마무리를 위한 bool값 변경
        else if (coll.collider.CompareTag("Container_End"))
        {
            go_contain_4 = false;
            startMove = false;

            if(PackCheck == true)
            {
                Debug.Log(1);
                DataManager.instance.BoxDataAdd(new DataManager.BoxData(this.name, 1));
                PackCheck= false;
            }           
            DataManager.instance.itemsOnContainer.Remove(DataManager.instance.itemsOnContainer.Find(x => x.obj.Equals(this.gameObject)));
            this.gameObject.SetActive(false);
        }

    }
    private void Update()
    {
        StartCoroutine(Movings());
    }

    IEnumerator Movings()
    {
        if (startMove)
        {
            // 2번 컨테이너로 직진
            if (go_contain_1)
            {
                if (DataManager.instance.Workitem == null)
                {
                    transform.Translate(new Vector3(0, 0, -1) * 5 * Time.deltaTime);
                }
            }

            // 각도를 틀어서 3번 컨테이너로 직진
            if (go_contain_2)
            {
                if(DataManager.instance.AssembleItem == null)
                {
                    transform.rotation = Quaternion.Euler(0, -90f, 0);
                    transform.Translate(new Vector3(0, 0, -1) * 5 * Time.deltaTime);
                }                 
            }

            // 각도를 틀어서 완성품 창출 기계로 직진
            if (go_contain_3)
            {
                if(DataManager.instance.Packitem == null)
                {
                    transform.rotation = Quaternion.Euler(0, 0f, 0);
                    transform.Translate(new Vector3(0, 0, -1) * 5 * Time.deltaTime);
                }  
            }
            if (go_contain_4)
            {
                transform.rotation = Quaternion.Euler(0, 0f, 0);
                transform.Translate(new Vector3(0, 0, -1) * 5 * Time.deltaTime);
            }
        }
        yield return new WaitForSeconds(0.1f);
    }
}