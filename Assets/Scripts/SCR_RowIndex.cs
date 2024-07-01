using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_RowIndex : MonoBehaviour{
    public GameObject[] objects;


    [HideInInspector]
    public SCR_WorldPositioner[] slots;
    // Start is called before the first frame update
    void Awake(){
        slots = new SCR_WorldPositioner[objects.Length];
        CreateObjects();
    }

    void CreateObjects(){
        int i = 0;
        foreach (GameObject obj in objects){
            slots[i] = Instantiate(obj,Vector3.zero,Quaternion.identity,transform).GetComponent<SCR_WorldPositioner>();
            i++;
        }
    }

    public void PositionObjects(){
        int i = 0;
        foreach (SCR_WorldPositioner positioner in slots){
            Vector2 position = new Vector2((float)i , transform.position.y);
            positioner.desiredWorldPos = position;
            i++;
        }
    }

    [ContextMenu("Print every element to Log")]
    void DebugAll(){
        foreach (SCR_WorldPositioner obj in slots){
            Debug.Log(obj.gameObject);
        }
    }
}
