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
    public float zRot = 0f;
    public float actualzRot = 0f;

    void Update(){
        if (focus != null){
            transform.position = Vector3.Lerp(
                transform.position ,                                            //START POS
                new Vector3(focus.position.x,focus.position.y,-10f) ,           //END POS
                10f * Time.deltaTime                                            //LERP STRENGTH
            );

        actualzRot = Mathf.Lerp(actualzRot , zRot , 5f * Time.deltaTime);
        transform.eulerAngles = new Vector3( 0f, 0f, actualzRot);
        }
    }

    public void SetFocus(Vector2 coord){
        DestroyFocus();
        focus = new GameObject("CameraFocus").transform;
        focus.transform.position = coord;
    }

    public void SetFocus(GameObject newFocus){
        DestroyFocus();
        focus = newFocus.transform;
        zRot = 0f;
        actualzRot = 0f;
    }

    void DestroyFocus(){
        if (focus == null) return;
        if (focus.name == "CameraFocus") Destroy(focus.gameObject);
    }

    public void SetSize(Vector2Int fieldSize){
        float xSize = ((float)fieldSize.x )/ 2;
        float ySize = ((float)fieldSize.y )/ 2;
        cameraComp.orthographicSize = ((xSize > ySize) ? xSize : ySize) + padding;
    }
}
