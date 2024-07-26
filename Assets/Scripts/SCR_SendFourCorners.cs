using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_SendFourCorners : MonoBehaviour{
    public SCR_WorldPositioner upperLeft;
    public SCR_WorldPositioner upperRight;
    public SCR_WorldPositioner lowerRight;
    public SCR_WorldPositioner lowerLeft;
    public SpriteRenderer colorSetter;
    public float destroyTime;
    public float distance;
    public bool relative;

    float timer;
    
    void Start(){        
        if (relative){
            distance = (Screen.width * distance)/960;
        }
        timer = destroyTime;
        upperLeft.desiredWorldPos  = new Vector2(transform.position.x - distance , transform.position.y + distance);
        upperRight.desiredWorldPos = new Vector2(transform.position.x + distance , transform.position.y + distance);
        lowerRight.desiredWorldPos = new Vector2(transform.position.x + distance , transform.position.y - distance);
        lowerLeft.desiredWorldPos  = new Vector2(transform.position.x - distance , transform.position.y - distance);
    }

    void Update(){
        timer -= Time.deltaTime;
        if (timer <= 0f){
            Destroy(gameObject);
        }
    }
}
