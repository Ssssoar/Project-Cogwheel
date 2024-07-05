using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_RowIndex : MonoBehaviour{
    public GameObject[] objects;
    public Vector2Int[] additional;

    int addIndex = 0;

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
            if (slots[i].tag == "Collectable"){
                SCR_WhenCollected collectScript = slots[i].GetComponent<SCR_WhenCollected>();
                collectScript.replaceCoord = additional[addIndex];
                addIndex ++;
            }
            i++;
        }
        additional = null;
        addIndex = 0;
    }

    public void PositionObjects(){
        int i = 0;
        foreach (SCR_WorldPositioner positioner in slots){
            Vector2 position = new Vector2((float)i , transform.position.y);
            positioner.desiredWorldPos = position;
            i++;
        }
    }

    public void RuntimeCreate(Vector2Int pos, GameObject prefab){
        Vector3 position = new Vector3((float)pos.x , (float)pos.y , 0f);
        slots[pos.x] = Instantiate(prefab,position,Quaternion.identity,transform).GetComponent<SCR_WorldPositioner>();
        slots[pos.x].desiredWorldPos = pos;
    }

    [ContextMenu("Print every element to Log")]
    void DebugAll(){
        foreach (SCR_WorldPositioner obj in slots){
            Debug.Log(obj.gameObject);
        }
    }
}
