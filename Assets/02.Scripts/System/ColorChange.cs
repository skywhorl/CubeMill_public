using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour
{
    public List<Color> Pink;
    public List<Color> Pupple;
    public SpriteRenderer Background;
    public SpriteRenderer Pass;
    public SpriteRenderer Assemble;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Background.color = Pink[0];
            Pass.color = Pink[1];
            Assemble.color = Pink[2];
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            Background.color = Pupple[0];
            Pass.color = Pupple[1];
            Assemble.color = Pupple[2];
        }
    }
}
