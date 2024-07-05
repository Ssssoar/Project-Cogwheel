using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_WorldPositioner : MonoBehaviour{
    public Vector2 desiredWorldPos;
    float lerpStrength = 10f;

    void Update(){
        if (desiredWorldPos == (Vector2)transform.position) return;

        transform.position = Vector2.Lerp(transform.position , desiredWorldPos , lerpStrength * Time.deltaTime);
    }

    public Vector2Int IntDesiredWorldPos(){
        return new Vector2Int((int)desiredWorldPos.x,(int)desiredWorldPos.y);
    }

    public void SetDesiredWorldPosFromInt(Vector2Int intPos){
        desiredWorldPos = new Vector2((float)intPos.x , (float)intPos.y);
    }
}
