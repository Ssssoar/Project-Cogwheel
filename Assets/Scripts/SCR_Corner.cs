using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_Corner : MonoBehaviour{
    [Header("References")]
    public SCR_WorldPositioner posScript;

    [Header("Parameters")]
    public Command openSide1;
    public Command openSide2;

    Vector2Int openCoord1;
    Vector2Int openCoord2;

    public Vector2Int ReturnPosition(Vector2Int pos){
        UpdateCoords();
        if (pos == openCoord1) return openCoord2;
        if (pos == openCoord2) return openCoord1;
        return pos;
    }

    void UpdateCoords(){
        openCoord1 = Side2Coord(openSide1);
        openCoord2 = Side2Coord(openSide2);
    }
    
    Vector2Int Side2Coord(Command openSide){
        Vector2 coord = posScript.desiredWorldPos;
        switch(openSide){
            case(Command.up):
                coord.y += 1f;
            break;
            case(Command.down):
                coord.y -= 1f;
            break;
            case(Command.right):
                coord.x += 1f;
            break;
            case(Command.left):
                coord.x -= 1f;
            break;
        }
        return new Vector2Int((int)coord.x , (int)coord.y);
    }
}
