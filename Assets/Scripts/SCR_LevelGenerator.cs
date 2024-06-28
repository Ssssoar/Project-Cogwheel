using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_LevelGenerator : MonoBehaviour{
    [Header("References")]
    public SCR_RowIndex[] rows;
    public SCR_CheckerPattern checkerScript;

    [HideInInspector]
    public Vector2Int maxSize;

    void Start(){
        maxSize = CalculateSize();
        PositionRows();
        checkerScript.Darken(rows);
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
        Debug.Log(size);
        return size;
    }

    public GameObject GetElementAtCoord(Vector2Int coord){
        if (coord.x < 0) coord.x = 0;
        if (coord.y < 0) coord.y = 0;
        if (coord.x > maxSize.x-1) coord.x = maxSize.x-1;
        if (coord.y > maxSize.y-1) coord.y = maxSize.y-1;
        return rows[coord.y].slots[coord.x];
    }
}
