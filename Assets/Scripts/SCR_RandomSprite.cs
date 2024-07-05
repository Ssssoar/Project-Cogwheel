using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_RandomSprite : MonoBehaviour{
    public SpriteRenderer img;
    public Sprite[] spriteList;
    public WiggleType wiggleType;
    int index;

    void OnEnable(){
        img.sprite = spriteList[0];
        index = 0;
        SCR_WiggleTimer.instance.NewWiggle(wiggleType,this);
    }

    // Update is called once per frame
    public void Wiggle(){
        int previous = index;
        index = GetRandIndex(spriteList.Length,index);
        img.sprite = spriteList[index];
    }

    int GetRandIndex(int length, int index){
        int toReturn = Random.Range(0,length-1);
        if (toReturn == index)
            return length-1;
        return toReturn;
    }

    void OnDestroy(){
        SCR_WiggleTimer.instance.RemoveWiggle(wiggleType,this);
    }

    void OnDisable(){
        SCR_WiggleTimer.instance.RemoveWiggle(wiggleType,this);
    }
}
