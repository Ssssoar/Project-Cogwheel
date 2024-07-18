using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_WorldPositioner : MonoBehaviour{
    public Vector2 desiredWorldPos;
    public Transform positionOverride;
    float lerpStrength = 10f;
    public float desiredzRot = 0f;
    public float actualzRot = 0f;
    public bool preventRotations = false;

    void Update(){
        if (positionOverride != null) desiredWorldPos = positionOverride.transform.position;
        if (desiredWorldPos != (Vector2)transform.position)
            transform.position = Vector2.Lerp(transform.position , desiredWorldPos , lerpStrength * Time.deltaTime);
        
        if (!preventRotations){
            actualzRot = Mathf.Lerp(actualzRot , desiredzRot , lerpStrength * Time.deltaTime);
            transform.eulerAngles = new Vector3( 0f, 0f, actualzRot);
        }
    }

    public Vector2Int IntDesiredWorldPos(){
        return new Vector2Int((int)desiredWorldPos.x,(int)desiredWorldPos.y);
    }

    public void SetDesiredWorldPosFromInt(Vector2Int intPos){
        desiredWorldPos = new Vector2((float)intPos.x , (float)intPos.y);
    }

    public void SetZRot(float offset){
        desiredzRot += offset;
    }
}
