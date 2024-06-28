using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_RowIndex : MonoBehaviour{
    public GameObject[] objects;


    [HideInInspector]
    public GameObject[] slots;
    // Start is called before the first frame update
    void Start(){
        slots = new GameObject[objects.Length];
        CreateObjects();
    }

    void CreateObjects(){
        int i = 0;
        Vector2 position;
        foreach (GameObject obj in objects){
            position = new Vector2((float)i,transform.position.y);
            slots[i] = Instantiate(obj,(Vector3)position,Quaternion.identity,transform);
            i++;
        }
    }

    [ContextMenu("Print every element to Log")]
    void DebugAll(){
        foreach (GameObject obj in slots){
            Debug.Log(obj);
        }
    }
}
