using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_EnableState : MonoBehaviour{
    public MonoBehaviour[] startEnabled;
    public MonoBehaviour[] startDisabled;
    public bool resetPosition;

    Vector3 initialPosition;
    bool positionRecorded = false;
    // Start is called before the first frame update
    void Start(){
        initialPosition = transform.position;
        positionRecorded = true;
    }
    
    void OnEnable(){
        foreach (MonoBehaviour comp in startEnabled ) { comp.enabled = true  ;}
        foreach (MonoBehaviour comp in startDisabled) { comp.enabled = false ;}
        if (resetPosition && positionRecorded)
            transform.position = initialPosition;
    }
}
