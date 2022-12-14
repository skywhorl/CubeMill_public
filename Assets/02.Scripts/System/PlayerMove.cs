using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    float playerSpeed = 10.0f;
    float FB = 0f;
    float LR = 0f;

    void Update()
    {
        FB = Input.GetAxis("Horizontal");
        LR = Input.GetAxis("Vertical");

        FB = FB * playerSpeed * Time.deltaTime;
        LR = LR * playerSpeed * Time.deltaTime;

        transform.Translate(Vector3.right * FB);
        transform.Translate(Vector3.forward * LR);
        transform.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * 1000);
    }
}
