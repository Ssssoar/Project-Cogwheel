using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_HatToImage : MonoBehaviour{
    public SCR_UI_RandomSprite randomerComp;
    public SCR_RandomSprite spriteComp;
    public Sprite[] normalSprites;
    public Sprite[] hatSprites;
    public bool chance = false;

    bool toUpdate = false;

    void Start(){
        toUpdate = true;
        UpdateList();
    }

    void OnEnable(){
        toUpdate = true;
        UpdateList();
    }

    void Update(){
        if ((toUpdate) || (SCR_HatManager.instance.changed)){
            UpdateList();
        }
    }

    public void UpdateList(){
        if (SCR_HatManager.instance == null) return;
        Sprite[] list = normalSprites;
        Sprite first = normalSprites[0];
        if (((chance) && (Random.Range(0,2) == 1)) || (!chance)){
            switch(SCR_HatManager.instance.GetHat()){
                case(Hat.huaso):
                    list = hatSprites;
                    first = hatSprites[0];
                break;
                case(Hat.none):
                    list = normalSprites;
                    first = normalSprites[0];
                break;
            }
        }
        if (randomerComp != null){
            randomerComp.img.sprite = first;
            randomerComp.spriteList = list;
        }
        if (spriteComp != null){
            spriteComp.img.sprite = first;
            spriteComp.spriteList = list;
        }
        toUpdate = false;
    }
}
