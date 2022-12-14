using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public static DialogueManager instance;
    public Text text;
   // public SpriteRenderer rendererDialogueWindow;

    private List<string> listSentences; //대사 저장리스트
    //private List<Sprite> listDialogueWindows;//대화창 스프라이트 저장리스트

    private int count; // 대화 진행 상황 카운트.
    //public Animator animDialogueWindow;//애니메이션 효과

    public bool talking = false; //이야기 중 확인
    private bool keyActivated = false; //키 눌렸는지 확인

    public bool prom=false;  //문제가 출력됐는지?
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        count = 0; //대화 진행 초기화
        text.text = "";
        listSentences = new List<string>(); //리스트 생성
        //listDialogueWindows = new List<Sprite>();
    }

    public void ShowDialogue(Dialogue dialogue)
    {
        talking = true; //대화중으로 변경
        for (int i = 0; i < dialogue.sentences.Length; i++)
        {
            listSentences.Add(dialogue.sentences[i]);//대사 저장 i개의 대사를 리스트에 저장시킴
           // listDialogueWindows.Add(dialogue.dialogueWindows[i]);//스프라이트 대화창(모양)을 저장
        }
        //animDialogueWindow.SetBool("Appear", true);//대화창 보이게하기 애니메이션
        StartCoroutine(StartDialogueCoroutine()); //대화창 시작
    }
    public void ExitDialogue() //대화종료
    {
        text.text = ""; //대사 초기화
        count = 0; //대사갯수 초기화
        listSentences.Clear(); //리스트 저장 초기화
        //listDialogueWindows.Clear();//대화창 초기화
       // animDialogueWindow.SetBool("Appear", false);//대화창 카메라에서 안보이게 하기
        talking = false; //대화해제   
    }


    IEnumerator StartDialogueCoroutine()//시작
    {
        if (count > 0)
        {
            //if (listDialogueWindows[count] != listDialogueWindows[count - 1])
            {              
                //animDialogueWindow.SetBool("Appear", false);//카운트-1 에서 카운트꺼의 대화창을 보이게 하기 위해 잠깐 안보이게 바꿈
                //yield return new WaitForSeconds(0.2f);
                //rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];//대화창 모양 변경
                //animDialogueWindow.SetBool("Appear", true); //새로운 대화창 보이게하기
            }
            //else
            {
                yield return new WaitForSeconds(0.1f);
            }

        }
        else
        {
            yield return new WaitForSeconds(0.05f);
            //rendererDialogueWindow.GetComponent<SpriteRenderer>().sprite = listDialogueWindows[count];//처음 대화창
        }
        keyActivated = true; //키가 눌림
        for (int i = 0; i < listSentences[count].Length; i++)
        {
            text.text += listSentences[count][i]; // 1글자씩 출력            
            yield return new WaitForSeconds(0.04f); //천천히 찍어야 1글짜씩 출력을 볼 수 있음
        }

    }
    void Update()
    {
        if (talking && keyActivated)
        {
            if (Input.GetMouseButtonDown(0))
            {
                keyActivated = false; //키가 한번 눌렸으니 다시 변경
                count++; //다음 대사로 넘어 갈려면 필요

                if (count == listSentences.Count)
                {
                    if (prom == false)
                    {
                        StopAllCoroutines();//이걸 실행시켜야 다른 코루틴을 다 무시하고 밑에있는걸 실행시킬 수 있음
                        ExitDialogue();//대화창 내용을 모두 출력하여 키가 눌렸을때 종료시킴
                        text.text = "";//대사 초기화
                    }
                    else
                    {
                        count--;
                    }
                    
                }
                else
                {
                    text.text = "";//대사 초기화
                    StopAllCoroutines();
                    StartCoroutine(StartDialogueCoroutine());//다음 대사를 출력
                }
            }
        }
    }
}