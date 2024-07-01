using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_CheckerPattern : MonoBehaviour{
    public float darkenFactor;

    public void Darken(SCR_RowIndex[] map){
        for(int i = 0; i < map.Length; i++){
            for(int j = 0; j < map[i].slots.Length; j++){
                if ((i%2 == 0)^(j%2 == 0))
                    DarkenTile(map[i].slots[j]);
            }
        }
    }

    void DarkenTile(SCR_WorldPositioner obj){
        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
        if (sr == null) return;
        Color newColor = sr.color * darkenFactor;
        newColor.a = 1f;
        sr.color = newColor;
    }
}
