using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_WhenCollected : MonoBehaviour{
    [Header("References")]
    public SCR_WorldPositioner coordScript;
    public SCR_ColorCopy colorScript;

    [Header("Variables")]
    public Vector2Int replaceCoord;
    public float timeToDisappear;

    float timer = -1f;

    [Header("Prefabs")]
    public GameObject indicatorPrefab;
    public GameObject replaceWith;

    [HideInInspector]
    public GameObject deleteObj; //NOT A PART OF THE GRID SYSTEM; DELETE HERE;
    void Start(){
        deleteObj = Instantiate(indicatorPrefab,Vector3.zero,Quaternion.identity,transform.parent);
        Vector2 worldPos = new Vector2((float)replaceCoord.x , (float)replaceCoord.y);
        deleteObj.GetComponent<SCR_WorldPositioner>().desiredWorldPos = worldPos;
        colorScript.copyTo = deleteObj.GetComponent<SpriteRenderer>();
    }

    void Update(){
        if (timer == -1f) return;
        timer -= Time.deltaTime;
        if (timer <= 0f){
            SCR_LevelGenerator.instance.ReplaceObject(replaceCoord, replaceWith, colorScript.copyFrom.color);
            Destroy(deleteObj);
            Destroy(gameObject);
        }
    }

    public void Execute(){
        coordScript.SetDesiredWorldPosFromInt(replaceCoord);
        timer = timeToDisappear;
    }
}
