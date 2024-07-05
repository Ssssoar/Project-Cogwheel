using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_ColorSystem : MonoBehaviour{
    public Color background;
    public Color element;
    public Color highlight;
    public Color[] keyColors;
    public float darkenFactor;
    public float lerpFactor;
    
    int keyIndex = 0;

    void Update(){
        SCR_CameraFocuser.instance.cameraComp.backgroundColor = Color.Lerp(SCR_CameraFocuser.instance.cameraComp.backgroundColor , background , lerpFactor * Time.deltaTime);
    }

    public void SwitchAllColors(SCR_RowIndex[] grid){
        for(int i = 0; i < grid.Length; i++){
            for(int j = 0; j < grid[i].slots.Length; j++)
                SwitchColorSingle(new Vector2Int(j,i));
        }
    }

    public void SwitchColorSingle(Vector2Int position){
        SCR_LevelGenerator.instance.GetElementAtCoord(position).GetComponent<SpriteRenderer>().color = GetColor(position);

    }

    Color GetColor(Vector2Int position){
        string objTag = SCR_LevelGenerator.instance.GetElementAtCoord(position).tag;
        Color colorToReturn;
        if ((objTag == "Wall")||(objTag == "Space"))
            colorToReturn = element;
        else if(objTag == "End")
            colorToReturn = highlight;
        else if((objTag == "Collectable")&&(keyColors.Length > 0)){
            colorToReturn = keyColors[keyIndex];
            keyIndex++;
            if (keyIndex >= keyColors.Length) keyIndex = 0;
        }else
            return Color.white;
        if((position.x%2 == 0)^(position.y%2 == 0))
            return DarkenColor(colorToReturn);
        else
            return colorToReturn;

    }

    Color DarkenColor(Color color){
        Color newColor = color * darkenFactor;
        newColor.a = 1f;
        return newColor;
    }
}
