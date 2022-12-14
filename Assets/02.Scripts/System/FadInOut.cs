using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadInOut : MonoBehaviour
{
    public static FadInOut instance;
    public Image _Image;
    Color blackColor = Color.black;   
    Color offColor = Color.clear;   
    Color startColor = Color.black;
    Color targetColor = Color.black;
    private bool isOnTransition = false;
    
    float fadeTime; // 색 변화하는 속도
    float delay;  // 딜레이 시킬 시간.
    string nextScene = "";  // 다음 씬으로 이동할 이름

    // t 명칭 다른 걸로 정해주세요^^
    float t;

    // 싱글턴
    private void Awake()
    {
        if (instance ==null)
        {
            instance = this;
        }
    } 
    
     
    void Start()
    { 
        BlackIn(2f, 1); // Fade In               
    }


    public void BlackIn(float a_fadeTime, float a_delay) // Fade In
    {
        fadeTime = a_fadeTime;
        delay = a_delay;
        _Image.enabled = true;
        _Image.color = blackColor;
        startColor = blackColor;
        targetColor = offColor;
        _Image.raycastTarget = false;

        if (isOnTransition)
            StopCoroutine("UpdateColorCoroutine");

        StartCoroutine(UpdateColorCoroutine(a_fadeTime, a_delay));
    }


    public void BlackOut(float a_fadeTime, float a_delay, string a_nextScene) // Fade Out
    {
        fadeTime = a_fadeTime; 
        delay = a_delay;
        nextScene = a_nextScene;
        _Image.enabled = true;
        startColor = _Image.color;
        targetColor = blackColor;
        _Image.raycastTarget = true;

        if (isOnTransition)
            StopCoroutine("UpdateColorCoroutine");

        StartCoroutine(UpdateColorCoroutine(a_fadeTime,a_delay));
    }


    // 설정한 시간에 따라 천천히 색 변화
    IEnumerator UpdateColorCoroutine(float a_fadeTime, float a_delay)    
    {
        isOnTransition = true; // 변화 하는 중

        if (delay != 0)
            yield return new WaitForSeconds(delay); // 딜레이 시간

        t = 0;

        while (t < 1)
        {
            _Image.color = Color.Lerp(startColor, targetColor, t); // 처음 색, 목표색, 시간?
            t += Time.deltaTime / fadeTime;
            yield return new WaitForEndOfFrame();
        }

        if (targetColor.Equals(Color.clear)) {
            _Image.enabled = false; // 흰색이 되면 안보이기
        }    
        else if (nextScene != "")
        {
            SceneManager.LoadScene(nextScene);  // 씬 이름이 있으면 다음 씬으로 넘어가기
            BlackIn(a_fadeTime,a_delay); // Fade In
        }

        isOnTransition = false; // 막아두기
    }
}
