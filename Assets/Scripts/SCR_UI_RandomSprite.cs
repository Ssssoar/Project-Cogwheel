using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SCR_UI_RandomSprite : MonoBehaviour{
    public Image img;
    public Sprite[] spriteList;
    public float switchTime;

    float timer;
    int index;
    // Start is called before the first frame update
    void Start(){
        timer = switchTime;
    }

    // Update is called once per frame
    void Update(){
        timer -= Time.deltaTime;
        if (timer <= 0f){
            timer += switchTime;
            index = GetRandIndex(spriteList.Length,index);
            img.sprite = spriteList[index];
        }
    }

    int GetRandIndex(int length, int index){
        int toReturn = Random.Range(0,length-1);
        if (toReturn == index)
            return length-1;
        return toReturn;
    }
}
