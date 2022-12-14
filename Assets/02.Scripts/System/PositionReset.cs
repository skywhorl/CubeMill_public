using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PositionReset : MonoBehaviour
{
    public void Reset() {
        SceneManager.LoadScene("Merge");
    }
}
