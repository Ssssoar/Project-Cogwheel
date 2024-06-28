using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CameraFocuser : MonoBehaviour{
    //SINGLETON INSTANCE
    public static SCR_CameraFocuser instance;
    private void Awake(){
        if (SCR_CameraFocuser.instance != null) Destroy(gameObject);
        else instance = this;
    }

    [Header("References")]
    public Camera cameraComp;
    public Transform focus;

    [Header("Variables")]
    public float padding;

    void Update(){
        if (focus != null){
            transform.position = new Vector3(focus.position.x,focus.position.y,-10f);
        }
    }

    public void SetFocus(Vector2 coord){
        focus = new GameObject("CameraFocus").transform;
        focus.transform.position = coord;
    }

    public void SetFocus(GameObject newFocus){
        focus = newFocus.transform;
    }

    public void SetSize(Vector2Int fieldSize){
        float xSize = ((float)fieldSize.x )/ 2;
        float ySize = ((float)fieldSize.y )/ 2;
        cameraComp.orthographicSize = ((xSize > ySize) ? xSize : ySize) + padding;
    }
}
