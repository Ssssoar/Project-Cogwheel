using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_ColorCopy : MonoBehaviour{
    public SpriteRenderer copyFrom;
    public SpriteRenderer copyTo;

    // Update is called once per frame
    void Update(){
        if (
            (copyFrom == null) ||
            (copyTo == null) ||
            (copyFrom.color == copyTo.color)
        ) return;
        copyTo.color = copyFrom.color;
    }
}
