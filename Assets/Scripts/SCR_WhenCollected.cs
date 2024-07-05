using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_WhenCollected : MonoBehaviour{
    public SCR_WorldPositioner coordScript;
    public SCR_ColorCopy colorScript;
    public Vector2Int replaceCoord;
    public GameObject indicatorPrefab;
    public GameObject replaceWith;
    public GameObject deleteObj; //NOT A PART OF THE GRID SYSTEM; DELETE HERE;

    void Start(){
        deleteObj = Instantiate(indicatorPrefab,Vector3.zero,Quaternion.identity,transform.parent);
        Vector2 worldPos = new Vector2((float)replaceCoord.x , (float)replaceCoord.y);
        deleteObj.GetComponent<SCR_WorldPositioner>().desiredWorldPos = worldPos;
        colorScript.copyTo = deleteObj.GetComponent<SpriteRenderer>();
    }

    public void Execute(){
        SCR_LevelGenerator.instance.ReplaceObject(replaceCoord, replaceWith);
        Destroy(deleteObj);
    }
}
