using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_RandomColor : MonoBehaviour{
    public SpriteRenderer sr;
    public float lowerBound;

    void Start(){
        Color colorToAssign = new Color(
            Random.Range(lowerBound,1f),
            Random.Range(lowerBound,1f),
            Random.Range(lowerBound,1f),
            1f
        );
        sr.color = colorToAssign;
        Destroy(this);
    }
}
