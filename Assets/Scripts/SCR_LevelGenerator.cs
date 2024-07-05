using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_LevelGenerator : MonoBehaviour{
    public static SCR_LevelGenerator instance;
    private void Awake(){
        if (SCR_LevelGenerator.instance != null) Destroy(SCR_LevelGenerator.instance.gameObject);
        instance = this;
    }

    [Header("References")]
    public GameObject nextLevel;
    public SCR_RowIndex[] rows;
    public SCR_ColorSystem colorScript;
    public GameObject emptyPrefab;

    [HideInInspector]
    public Vector2Int maxSize;

    void Start(){
        maxSize = CalculateSize();
        PositionRows();
        colorScript.SwitchAllColors(rows);
        Vector2 middlePos = (Vector2)maxSize;
        middlePos.x -= 1f;
        middlePos.y -= 1f;
        middlePos /= 2;
        SCR_CameraFocuser.instance.SetFocus(middlePos);
        SCR_CameraFocuser.instance.SetSize(maxSize);
    }

    void PositionRows(){
        int yPos = 0;
        foreach(SCR_RowIndex row in rows){
            row.transform.position = new Vector3(0f,(float)yPos,0f);
            row.PositionObjects();
            yPos++;
        }
    }

    Vector2Int CalculateSize(){
        Vector2Int size = new Vector2Int(0,0);
        size.y = rows.Length;
        int maxX = 0;
        foreach (SCR_RowIndex row in rows){
            if (row.objects.Length > maxX)
                maxX = row.objects.Length;
        }
        size.x = maxX;
        return size;
    }

    public SCR_WorldPositioner GetElementAtCoord(Vector2Int coord){
        if (coord.x < 0) coord.x = 0;
        if (coord.y < 0) coord.y = 0;
        if (coord.x > maxSize.x-1) coord.x = maxSize.x-1;
        if (coord.y > maxSize.y-1) coord.y = maxSize.y-1;
        return rows[coord.y].slots[coord.x];
    }

    public Vector2Int DirectionToVect(Command direction){
        switch(direction){
            case(Command.up):    return new Vector2Int( 0, 1);
            case(Command.down):  return new Vector2Int( 0,-1);
            case(Command.left):  return new Vector2Int(-1, 0);
            case(Command.right): return new Vector2Int( 1, 0);
        }
        return new Vector2Int(0,0);
    }

    public void SwitchObjects(Vector2Int pos1, Vector2Int pos2){
        SCR_WorldPositioner obj1 = GetElementAtCoord(pos1);
        SCR_WorldPositioner obj2 = GetElementAtCoord(pos2);
        rows[pos1.y].slots[pos1.x] = obj2;
        rows[pos2.y].slots[pos2.x] = obj1;
        obj1.desiredWorldPos = pos2;
        obj2.desiredWorldPos = pos1;
        colorScript.SwitchColorSingle(pos1);
        colorScript.SwitchColorSingle(pos2);
    }

    public void ReplaceObject(Vector2Int pos, GameObject prefab){
        if (prefab == null) prefab = emptyPrefab;
        Destroy(rows[pos.y].slots[pos.x].gameObject);
        rows[pos.y].RuntimeCreate(pos, prefab);
        colorScript.SwitchColorSingle(pos);
    }

    [ContextMenu("TriggerNextLevel")]
    public void LoadLevel(){
        Instantiate(nextLevel,Vector3.zero,Quaternion.identity,transform.parent);
    }
}
